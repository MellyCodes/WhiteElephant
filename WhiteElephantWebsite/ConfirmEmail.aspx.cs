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

        }

        protected void lbnConfirmEmail_Click(object sender, EventArgs e)
        {
            
            ActivateCustomer();
            
        }

        private void ActivateCustomer()
        {
            string emailAddress = "";


            try
            {
                List<SqlParameter> prms = new List<SqlParameter>()
                {

                    new SqlParameter()
                    {
                        ParameterName = "@Email",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 50,
                        Value = emailAddress
                    },
                };

                DBHelper.Insert("ActivateCustomer", prms.ToArray());
            }
            catch
            {

            }



 
            //return email;
        }
    }
}