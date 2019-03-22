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

namespace WhiteElephantWebsite
{
    public partial class MyAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Common.IsUserAuthenticated(Session))
                {
                    if (!Common.IsUserAuthenticated(Session))
                    {
                        notLoggedIn.Visible = true;
                    }
                }
                else
                {
                    notLoggedIn.Visible = false;
                    LoadUserDetails();
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

            lblConfirmationEmailSent.Text = "An email has been sent to activate your account";
        }
        

        protected void detUser_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            if (e.NewMode == DetailsViewMode.Edit)
            {
                detUser.ChangeMode(DetailsViewMode.Edit);
                LoadUserDetails();
            }
            if (e.NewMode == DetailsViewMode.Insert)
            {
                detUser.ChangeMode(DetailsViewMode.Insert);
            }
            if (e.NewMode == DetailsViewMode.ReadOnly)
            {
                detUser.ChangeMode(DetailsViewMode.ReadOnly);
                LoadUserDetails();
            }
        }

        protected void detUser_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {

            try
            {
                string street = ((TextBox)detUser.Rows[0].Cells[1].Controls[0]).Text;
                string city = ((TextBox)detUser.Rows[1].Cells[1].Controls[0]).Text;
                string prov = ((TextBox)detUser.Rows[2].Cells[1].Controls[0]).Text;
                string pCode = ((TextBox)detUser.Rows[3].Cells[1].Controls[0]).Text;
                string pNum = ((TextBox)detUser.Rows[4].Cells[1].Controls[0]).Text;
               
                string user = Common.GetAuthenticatedUser(Session);
             

                int customerId = DBHelper.GetQueryValue<int>("SelectCustomers", "id", new SqlParameter[]{ new SqlParameter() {
                    ParameterName = "@EmailAddress",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
                    Value = user
                }});

                List<SqlParameter> prms = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@CustomerID",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 20,
                    Value = customerId
                },

                 new SqlParameter()
                {
                    ParameterName = "@Street",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 20,
                    Value = street
                },

                new SqlParameter()
                {
                    ParameterName = "@City",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 20,
                    Value= city
                },
                new SqlParameter()
                {
                    ParameterName = "@Province",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 2,
                    Value= prov
                },
                new SqlParameter()
                {
                    ParameterName = "@Pcode",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 6,
                    Value= pCode
                },
                new SqlParameter()
                {
                    ParameterName = "@Phone",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10,
                    Value= pNum
                },

            };

                DBHelper.Insert("UpdateCustomer", prms.ToArray());
            }
            catch(Exception ex)
            {
                //do stuff
            }
            detUser.ChangeMode(DetailsViewMode.ReadOnly);
            LoadUserDetails();
        }
       

    }
}