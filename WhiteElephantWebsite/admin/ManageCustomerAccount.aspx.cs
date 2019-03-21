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

        protected void grdCustomer_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdCustomers.EditIndex = -1;
            LoadCustomersGridView();
        }

        protected void grdCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCustomers.PageIndex = e.NewPageIndex;
            LoadCustomersGridView();
        }

        protected void grdCustomer_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdCustomers.EditIndex = e.NewEditIndex;
            LoadCustomersGridView();
        }

        protected void grdCustomer_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            int custID = Convert.ToInt32(this.grdCustomers.DataKeys[e.RowIndex].Values[0]);
            CheckBox chkArchived = (CheckBox)this.grdCustomers.Rows[e.RowIndex].FindControl("chkArchived");

            try
            {
                bool archive = chkArchived.Checked;

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
                    ParameterName = "@IsArchived",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 20,
                    Value = archive
                },
                };

                DBHelper.NonQuery("UpdateArchive", prms.ToArray());

            }
            catch (Exception ex)
            {
            }

            grdCustomers.EditIndex = -1;
            LoadCustomersGridView();


        }

    }
}