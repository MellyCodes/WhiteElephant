﻿/**
* @project White Elephant E-Commerce Website
* @authors Courtney Diotte
* @authors Melanie Roy-Plommer
* @version 1.0
*
* @section DESCRIPTION
* <  >
*
* @section LICENSE
* Copyright 2018 - 2019
* Permission to use, copy, modify, and/or distribute this software for
* any purpose with or without fee is hereby granted, provided that the
* above copyright notice and this permission notice appear in all copies.
*
* THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
* WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
* MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
* ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
* WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
* ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
* OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
*
* @section Academic Integrity
* I certify that this work is solely my own and complies with
* NBCC Academic Integrity Policy (policy 1111)
*/

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

        /// <summary>
        /// set min and max values for calendar 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            DateTime dteMin = DateTime.Today;
            DateTime dteMax = new DateTime(2050, 1, 1);

            rngMydate.MinimumValue = dteMin.ToShortDateString();
            rngMydate.MaximumValue = dteMax.ToShortDateString();
        }
        /// <summary>
        /// 
        /// toggle visibility - get cart and total
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Common.IsUserAuthenticated(Session))
                {
                    notLoggedIn.Visible = true;
                    shippingAddress.Visible = false;
                    creditCardInfo.Visible = false;
                    shippingCheckbox.Visible = false;
                    btnSubmitOrder.Visible = false;
                    lblTotal.Visible = false;
                    lblCartTax.Visible = false;
                    ccInfo.Visible = false;
                    lblTax.Visible = false;
                    ship.Visible = false;
                    order.Visible = false;
                }
                else
                {
                    checkoutOrder.Visible = true;
                    ccInfo.Visible = true;
                    creditCardInfo.Visible = true;
                    shippingCheckbox.Visible = true;
                    notLoggedIn.Visible = false;
                    btnSubmitOrder.Visible = true;

                    lblCartTax.Visible = true;
                    lblTax.Visible = true;
                    LoadUserDetails();
                    Common.GetCart(this.grdCart, Request, Response);
                    this.lblCartTotal.Text = Common.GetCartTotal(Request, Response).ToString("c2");
                    Double temp;
                    Double.TryParse(Common.GetCartTotal(Request, Response).ToString(), out temp);

                    temp *= 0.15;

                    this.lblCartTax.Text = temp.ToString("c2");
                }
            }
        }

        /// <summary>
        /// Load the current users details and bind to detUser
        /// </summary>
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

        /// <summary>
        /// redirect to products page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnContinueShopping_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Products.aspx");
        }

        /// <summary>
        /// Update cart - redirect to cart page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateMyCart_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Cart.aspx");
        }

        /// <summary>
        /// Submit order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (IsValid)
            {
                try
                {
                    lblCartTax.Visible = false;
                    lblTax.Visible = false;
                    int orderNumber = SubmitOrder();
                    SendEmail(orderNumber);

                    lblMessage.Text = $"Your order has now been processed.<br />Order No: { orderNumber.ToString() }<br />An email has been sent as confirmation.";

                    checkoutOrder.Visible = false;
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

                    String shippingStreet = this.txtShippingStreet.Text.Trim();
                    String shippingCity = this.txtShippingCity.Text.Trim();
                    String shippingProvince = this.txtShippingProvince.Text.Trim();
                    String shippingPostalCode = this.txtShippingPostalCode.Text.Trim();

                    string authUser = Common.GetAuthenticatedUser(Session);

                    int customerId = DBHelper.GetQueryValue<int>("SelectCustomers", "id", new SqlParameter[]{ new SqlParameter() {
                    ParameterName = "@EmailAddress",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
                    Value = authUser
                }});


                    List<SqlParameter> prms = new List<SqlParameter>();

                    prms.Add(new SqlParameter() { ParameterName = "@CustomerId", SqlDbType = SqlDbType.Int, Value = customerId });

                    prms.Add(new SqlParameter() { ParameterName = "@ShippingStreet", SqlDbType = SqlDbType.NVarChar, Value = shippingStreet });
                    prms.Add(new SqlParameter() { ParameterName = "@ShippingCity", SqlDbType = SqlDbType.NVarChar, Value = shippingCity });
                    prms.Add(new SqlParameter() { ParameterName = "@ShippingProvince", SqlDbType = SqlDbType.NVarChar, Value = shippingProvince });
                    prms.Add(new SqlParameter() { ParameterName = "@ShippingPcode", SqlDbType = SqlDbType.NVarChar, Value = shippingPostalCode });

                    DBHelper.Insert("UpdateShippingAddress", prms.ToArray());
                }

                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
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
                mail.From = new MailAddress("noreply@whiteelephant.com");
                mail.To.Add(Common.GetAuthenticatedUser(Session));

                //set the content
                mail.Subject = $"White Elephant Order Confirmation No: {orderNumber.ToString()}";
                mail.Body = $"{html}Your order has now be confirmed. It will be released " +
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

        protected void chkDifferentShipping_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDifferentShipping.Checked)
            {
                this.shippingAddress.Visible = true;
            }
            else
            {
                this.shippingAddress.Visible = false;
            }
        }

        protected void cldExpiryDate_SelectionChanged(object sender, EventArgs e)
        {
            this.txtMyCal.Text = this.cldExpiryDate.SelectedDate.ToShortDateString();

            Common.GetCart(this.grdCart, Request, Response);
            this.lblCartTotal.Text = Common.GetCartTotal(Request, Response).ToString("c2");
            Double temp;
            Double.TryParse(Common.GetCartTotal(Request, Response).ToString(), out temp);

            temp *= 0.15;

            this.lblCartTax.Text = temp.ToString("c2");
        }

        protected void ddlCardType_SelectedIndexChanged(object sender, EventArgs e)
        {

            string CardType = ddlCardType.SelectedItem.Text;
            switch (CardType)
            {
                case "Visa": //Visa
                    regCreditCardNumber.ValidationExpression = "^(4[0-9]{12}(?:[0-9]{3})?)";
                    regCreditCardNumber.ErrorMessage = "Visa numbers must be either 13 or 16 digits beginning with 4";
                    regCreditCardNumber.ToolTip = "Visa numbers must be either 13 or 16 digits begining with 4";
                    break;
                case "Mastercard": //Mastercard
                    regCreditCardNumber.ValidationExpression = "^(5[1-5][0-9]{14})";
                    regCreditCardNumber.ErrorMessage = "MasterCard numbers must be 16 digits beginning with 51 through 55";
                    regCreditCardNumber.ToolTip = "MasterCard numbers must be 16 digits beginning with 51 through 55";
                    break;
                case "American Express": //Amex
                    regCreditCardNumber.ValidationExpression = "^(3[47][0-9]{13})";
                    regCreditCardNumber.ErrorMessage = "American Express numbers must be 15 digits beginning with 34 or 37";
                    regCreditCardNumber.ToolTip = "American Express numbers must be 15 digits beginning with 34 or 37";
                    break;
                default:
                    break;
                    
            }
        }
    }
    
}