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
    public partial class ConfirmEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string email = Request.QueryString["email"];
            ActivateCustomer(email);

        }


        private void ActivateCustomer(string email)
        {
           


            try
            {
                List<SqlParameter> prms = new List<SqlParameter>()
                {

                    new SqlParameter()
                    {
                        ParameterName = "@Email",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 50,
                        Value = email
                    },
                };

                DBHelper.Insert("ActivateCustomer", prms.ToArray());

                lblConfirmEmail.Text = "Account activated";
            }
            catch (Exception ex)
            {
                lblConfirmEmail.Text = ex.Message;
            }



 
            //return email;
        }
    }
}