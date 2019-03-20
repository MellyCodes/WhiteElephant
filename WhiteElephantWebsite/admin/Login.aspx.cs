/**
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
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhiteElephantWebsite.admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            e.Authenticated = false;
            string loginUser = Login1.UserName;
            string loginPassword = Login1.Password;

            e.Authenticated = AuthenticateAdmin(loginUser, loginPassword);

            if (e.Authenticated)
            {
                if (Request.QueryString["returnurl"] != null)
                    Response.Redirect($"~/admin/{Request.QueryString["returnurl"]}");
                else
                    Response.Redirect("~/admin/AdminDashboard.aspx");
            }
        }

        private bool AuthenticateAdmin(string userName, string password)
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

                result = !string.IsNullOrEmpty(DBHelper.GetScalarValue<string>("LoginAdmin", prms.ToArray()));

                if (result)
                {
                    Session["authenticated"] = true;
                    Session["authenticatedUser"] = userName;
                    Session["admin"] = true;
                }
                else
                {
                    Session["authenticated"] = null;
                    Session["authenticatedUser"] = null;
                    Session["admin"] = null;
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

        }

    }
}