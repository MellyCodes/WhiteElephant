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
    public partial class CategoryMaintenance : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            lblMessage.Text = "";

            if (!IsPostBack)
            {
                LoadCategoriesGridView();
            }
        }

        private void LoadCategoriesGridView()
        {
            DBHelper.DataBindingWithPaging(this.grdCategories, "SelectCategories"); // Need to make Sproc



        }

        protected void grdCategories_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnNew")
            {
                //New Category
                AddNewCategory();
                LoadCategoriesGridView();
            }

        }

        protected void grdCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int categoryId = Convert.ToInt32(this.grdCategories.DataKeys[e.RowIndex].Values[0]);
            SqlParameter prmCategoryId = new SqlParameter() { ParameterName = "@CategoryId", SqlDbType = SqlDbType.Int, Value = categoryId };

            // TODO: Need to check if product exists in category before deleteing it

            DBHelper.NonQuery("DeleteCategory", new SqlParameter[] { prmCategoryId });

            LoadCategoriesGridView();
        }

        protected void grdCategories_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblMessage.Text = "";
            List<SqlParameter> prms = new List<SqlParameter>();

            TextBox txtCategoryName = (TextBox)this.grdCategories.Rows[e.RowIndex].FindControl("txtCategoryName");
            TextBox txtDesc = (TextBox)this.grdCategories.Rows[e.RowIndex].FindControl("txtDesc");

            try
            {
                int categoryId = Convert.ToInt32(this.grdCategories.DataKeys[e.RowIndex].Value);
                string categoryName = txtCategoryName.Text.Trim();
                string categoryDesc = txtDesc.Text.Trim();


                prms.Add(DBHelper.SetCategoryNameParam(categoryName));
                prms.Add(DBHelper.SetCategoryDescParam(categoryDesc));




                prms.Add(DBHelper.SetCategoryIdParam(categoryId));

                /*
                 *
                 * prms.Add(DBHelper.SetCategoryIdParam(categoryId));

                DBHelper.NonQuery("UpdateProduct", prms.ToArray());

                lblMessage.Text = "Product was updated";
                 */



                DBHelper.NonQuery("UpdateCategory", prms.ToArray());

                lblMessage.Text = "Category was updated.";
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            grdCategories.EditIndex = -1;
            LoadCategoriesGridView();
        }

        protected void grdCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdCategories.EditIndex = e.NewEditIndex;


            LoadCategoriesGridView();

            //int categoryId = Convert.ToInt32(this.grdCategories.DataKeys[e.NewEditIndex].Value[0]);
            //SqlParameter prmCategoryId = new SqlParameter() { ParameterName = "@CategoryId", SqlDbType = SqlDbType.Int, Value = categoryId };
        }

        protected void grdCategories_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdCategories.EditIndex = -1;
            LoadCategoriesGridView();
        }

        protected void grdCategories_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCategories.PageIndex = e.NewPageIndex;
            LoadCategoriesGridView();
        }

        private void AddNewCategory()
        {
            List<SqlParameter> prms = new List<SqlParameter>();

            TextBox txtCategoryNameNew = (TextBox)this.grdCategories.FooterRow.FindControl("txtCategoryNameNew");
            TextBox txtDescNew = (TextBox)this.grdCategories.FooterRow.FindControl("txtDescNew");

            try
            {
                string categoryName = txtCategoryNameNew.Text.Trim();
                string categoryDesc = txtDescNew.Text.Trim();


                prms.Add(DBHelper.SetCategoryNameParam(categoryName));
                prms.Add(DBHelper.SetCategoryDescParam(categoryDesc));


                prms.Add(new SqlParameter()
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                });

                int id = DBHelper.Insert<int>("InsertCategory", "@Id", prms.ToArray());

                lblMessage.Text = $"Category was created. Id: {id.ToString()}";
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}