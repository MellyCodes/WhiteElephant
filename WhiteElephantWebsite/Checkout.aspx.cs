using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhiteElephantWebsite
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Common.IsUserAuthenticated(Session))
                {
                    notLoggedIn.Visible = true;
                    btnSubmitOrder.Visible = false;
                    lblTotal.Visible = false;
                    ship.Visible = false;
                    order.Visible = false;
                }
                else
                {
                    notLoggedIn.Visible = false;
                    btnSubmitOrder.Visible = true;
                    LoadUserDetails();
                    Common.GetCart(this.grdCart, Request, Response);
                    this.lblCartTotal.Text = Common.GetCartTotal(Request, Response).ToString("c2");
                }
            }
        }

        private void LoadUserDetails()
        {
            string user = Common.GetAuthenticatedUser(Session);
            SqlParameter userPrm = new SqlParameter()
            {
                ParameterName = "@EmailAddress",
                SqlDbType = SqlDbType.NVarChar,
                Size = 50,
                Value = user
            };

            DBHelper.DataBinding(this.detUser, "SelectCustomers", new SqlParameter[] { userPrm });
        }

        protected void btnContinueShopping_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Products.aspx");
        }

        protected void btnUpdateMyCart_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Cart.aspx");
        }

        protected void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            try
            {
                int orderNumber = SubmitOrder();
                SendEmail(orderNumber);

                lblMessage.Text = $"Your CandyStore order has now been processed.<br />Order No: { orderNumber.ToString() }<br />An email has been sent as confirmation.";

                //Remove the cart cookies from the user's system
                Response.Cookies["CartUId"].Expires = DateTime.Now.AddDays(-1);

                //Clear MasterPage Label
                Label lblCartItemsCount = (Label)this.Page.Master.FindControl("lblCartItemsCount");

                if (lblCartItemsCount != null)
                    lblCartItemsCount.Text = "";

                this.lblTotal.Text = "";

                this.grdCart.DataSource = null;
                this.grdCart.DataBind();

                this.btnSubmitOrder.Visible = false;
                this.btnUpdateMyCart.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private int SubmitOrder()
        {
            List<SqlParameter> prms = new List<SqlParameter>();
            DateTime orderDate = DateTime.Now;
            string authUser = Common.GetAuthenticatedUser(Session);

            int customerId = DBHelper.GetQueryValue<int>("SelectCustomers", "id", new SqlParameter[]{ new SqlParameter() {
                    ParameterName = "@EmailAddress",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
                    Value = authUser
                }});

            string cartUId = Common.GetCartId(Request, Response);

            prms.Add(new SqlParameter() { ParameterName = "@CustomerId", SqlDbType = SqlDbType.Int, Value = customerId });
            prms.Add(new SqlParameter() { ParameterName = "@OrderDate", SqlDbType = SqlDbType.Date, Value = orderDate });
            prms.Add(new SqlParameter() { ParameterName = "@CartUId", SqlDbType = SqlDbType.VarChar, Size = 50, Value = cartUId });
            prms.Add(new SqlParameter() { ParameterName = "@OrderNo", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output });

            int orderNo = DBHelper.Insert<int>("InsertOrder", "@OrderNo", prms.ToArray());

            return orderNo;
        }

        private void SendEmail(int orderNumber)
        {
            try
            {
                string html = "<html><head><style>body{font-family:Arial; color:#000;} table, th, tr, td{ color:blue;border:1px solid #000;font-family:Arial; }</style></head><body>";

                //create the mail message
                MailMessage mail = new MailMessage();

                //set the addresses
                mail.From = new MailAddress("noreply@candycompany.com");
                mail.To.Add(Common.GetAuthenticatedUser(Session));

                //set the content
                mail.Subject = $"CandyStore Order Confirmation No: {orderNumber.ToString()}";
                mail.Body = $"{html}Your order from the CandyStore has now be confirmed. It will be released " +
                    $"for delivery within 3 business days.<br /><br />Order No:{orderNumber.ToString()}<br /><br />Order " +
                    $"Details<br />{BuildEmailOrderTable(orderNumber)}</body></html>";

                mail.IsBodyHtml = true;

                DirectoryInfo dirInfo = new DirectoryInfo(@"C:\MyASPNETTestEmails");

                //check if dir exists, if not - create that directory
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(@"C:\MyASPNETTestEmails");

                SmtpClient smtp = new SmtpClient();
                smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtp.PickupDirectoryLocation = @"C:\MyASPNETTestEmails";
                smtp.Send(mail);
            }
            catch
            {
                //Log or take some action
            }
        }

        private string BuildEmailOrderTable(int orderNumber)
        {
            StringBuilder sb = new StringBuilder();
            DataTable orderDetails = DBHelper.GetQuery("SelectOrderDetails", new SqlParameter[] {
                new SqlParameter()
                {
                    ParameterName="@OrderId",
                    SqlDbType = SqlDbType.Int,
                    Value = orderNumber
                }
            });

            sb.Append("<table>");
            sb.Append("<tr><td>ProductId</td><td>Qty</td><td>Price</td><td>Subtotal</td></tr>");
            double orderTotal = 0.0;

            foreach (DataRow r in orderDetails.Rows)
            {
                orderTotal += double.Parse(r["linetotal"].ToString());
                sb.Append("<tr>");
                sb.Append($"<td>{r["ProductId"].ToString()}</td>");
                sb.Append($"<td>{r["quantity"].ToString()}</td>");
                sb.Append($"<td>{double.Parse(r["price"].ToString()).ToString("c2")}</td>");
                sb.Append($"<td>{double.Parse(r["linetotal"].ToString()).ToString("c2")}</td>");
                sb.Append("</tr>");
            }

            sb.Append("</table>");
            sb.Append($"Order Total: {orderTotal.ToString("c2") }");
            return sb.ToString();
        }
    }
}