using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhiteElephantWebsite
{
    public partial class My : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    this.lblCopyRightYear.Text = DateTime.Now.Year.ToString();
                    GetCartCount();

                    if (Common.IsUserAuthenticated(Session))
                    {
                        this.hypLogin.Text = "Logout";
                        this.hypLogin.NavigateUrl = "~/Logout.aspx";
                    }
                    else
                    {
                        this.hypLogin.Text = "Login";
                        this.hypLogin.NavigateUrl = "~/Login.aspx";
                    }
                }
                catch (Exception)
                {
                    //handle error
                }

            }
        }

        private void GetCartCount()
        {
            string cartId = Common.GetCartId(Request, Response);
            int count = Common.GetCartCount(cartId);
          
           this.lblCartItemsCount.Text = count > 0 ? $"{count}" : "";
        }
    }
}