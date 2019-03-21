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
    public partial class ManageCustomerAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCustomersGridView();

            }
        }

        private void LoadCustomersGridView()
        {
            DBHelper.DataBindingWithPaging(this.grdCustomers, "SelectCustomers", null);
            
        }

        protected void grdCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCustomers.PageIndex = e.NewPageIndex;
            LoadCustomersGridView();
        }

        protected void UpdateArchive(object sender, GridViewDeleteEventArgs e)
        {
            
            int custID = Convert.ToInt32(this.grdCustomers.DataKeys[e.RowIndex].Values[0]);
            CheckBox chkArchived = (CheckBox)this.grdCustomers.Rows[e.RowIndex].FindControl("chkArchivedDisplay");
            chkArchived.Checked = true;
            try
            {
                int temp = 0;
                bool archive = chkArchived.Checked;

                if(archive == true)
                {
                    temp = 1;
                }
                else
                {
                    temp = 0;
                }

                List<SqlParameter> prms = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@CustomerID",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 20,
                    Value = custID
                },
                new SqlParameter()
                {
                    ParameterName = "@Archive",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 20,
                    Value = temp
                },
                };

                DBHelper.Insert("UpdateArchive", prms.ToArray());

            }
            catch (Exception ex)
            {
            }

            grdCustomers.EditIndex = -1;
            LoadCustomersGridView();


        }
        

    }
}