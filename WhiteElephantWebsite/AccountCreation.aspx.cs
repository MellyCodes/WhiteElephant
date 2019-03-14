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
    public partial class AccountCreation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            this.lblError.Text = "";

            try
            {
                if (IsValid)
                {
                    string firstName = this.txtFirstName.Text.Trim();
                    string lastName = this.txtLastName.Text.Trim();
                    string emailAddress = this.txtEmailAddress.Text.Trim();
                    string password = this.txtPassword.Text.Trim();
                    string street = this.txtStreet.Text.Trim();
                    string city = this.txtCity.Text.Trim();
                    string province = this.txtProvince.Text.Trim();
                    string postalCode = this.txtPostalCode.Text.Trim();
                    string phone = this.txtPhone.Text.Trim();

                    if (CreateAccount(
                        firstName,
                        lastName,
                        emailAddress,
                        password,
                        street,
                        city,
                        province,
                        postalCode,
                        phone) != 0)
                    {
                        Session["authenticated"] = true;
                        Session["authenticatedUser"] = emailAddress;

                        if (Request.QueryString["returnurl"] != null)
                            Response.Redirect($"~/{Request.QueryString["returnurl"]}");
                        else
                            Response.Redirect("~/MyAccount.aspx?created");
                    }
                    else
                    {
                        this.lblError.Text = "The account was not created";
                    }
                }
            }
            catch (SqlException ex)
            {
                this.lblError.Text = ex.Message;
            }
            catch (Exception ex)
            {
                this.lblError.Text = ex.Message;
            }
        }

        private int CreateAccount
        (
            string firstName,
            string lastName,
            string emailAddress,
            string password,
            string street,
            string city,
            string province,
            string postalCode,
            string phone
        )
        {
            int id = 0;

            List<SqlParameter> prms = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@FirstName",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 20,
                    Value = firstName
                },
                new SqlParameter()
                {
                    ParameterName = "@LastName",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 20,
                    Value = lastName
                },
                new SqlParameter()
                {
                    ParameterName = "@Email",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
                    Value = emailAddress
                },
                new SqlParameter()
                {
                    ParameterName = "@Pwd",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15,
                    Value = password
                },
                new SqlParameter()
                {
                    ParameterName = "@Street",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
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
                    Value= province
                },
                new SqlParameter()
                {
                    ParameterName = "@Pcode",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 6,
                    Value= postalCode
                },
                new SqlParameter()
                {
                    ParameterName = "@Phone",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 10,
                    Value= phone
                },
                new SqlParameter()
                {
                    ParameterName = "@Identity",
                    SqlDbType = SqlDbType.Int,
                    Direction=ParameterDirection.Output
                }
            };

            id = DBHelper.Insert<int>("InsertCustomer", "@Identity", prms.ToArray());

            return id;
        }
    }
}