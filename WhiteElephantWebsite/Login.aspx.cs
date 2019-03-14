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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            e.Authenticated = false;
            string loginUser = Login1.UserName;
            string loginPassword = Login1.Password;

            e.Authenticated = AuthenticateUser(loginUser, loginPassword);

            if (e.Authenticated)
            {
                if (Request.QueryString["returnurl"] != null)
                    Response.Redirect($"~/{Request.QueryString["returnurl"]}");
                else
                    Response.Redirect("~/Default.aspx");
            }
        }

        private bool AuthenticateUser(string userName, string password)
        {
            bool result = false;
            try
            {
                List<SqlParameter> prms = new List<SqlParameter>();

                prms.Add(new SqlParameter()
                {
                    ParameterName = "@Email",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
                    Value = userName
                });

                prms.Add(new SqlParameter()
                {
                    ParameterName = "@Password",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
                    Value = password
                });

                result = !string.IsNullOrEmpty(DBHelper.GetScalarValue<string>("Login", prms.ToArray()));

                if (result)
                {
                    Session["authenticated"] = true;
                    Session["authenticatedUser"] = userName;
                }
                else
                {
                    Session["authenticated"] = null;
                    Session["authenticatedUser"] = null;
                }
            }
            catch (SqlException ex)
            {
                Login1.FailureText = ex.Message;
            }

            return result;
        }

        protected void Login1_LoginError(object sender, EventArgs e)
        {
            //I can do something here on error if I wish
        }


    }
}