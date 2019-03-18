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
    public partial class ProductMaintenance : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)

        {
            base.Page_Load(sender, e);
            lblMessage.Text = "";

            if (!IsPostBack)
            {
                LoadProductsGridView();

            }
        }

        private void LoadProductsGridView()
        {
            DBHelper.DataBindingWithPaging(this.grdProducts, "SelectProductMaintenance");
            //Load the dropdown list in the create row
            DropDownList ddlCategoriesInFooter = (DropDownList)(this.grdProducts.FooterRow.FindControl("ddlCategoriesNew"));
            BindCategoryDropdownList(ddlCategoriesInFooter);

        }

        protected void grdProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnNew")
            {
                //New product
                AddNewProduct();

                LoadProductsGridView();
            }
        }

        protected void grdProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int productId = Convert.ToInt32(this.grdProducts.DataKeys[e.RowIndex].Values[0]);
            SqlParameter prmProductId = new SqlParameter() { ParameterName = "@ProductId", SqlDbType = SqlDbType.Int, Value = productId };

            DBHelper.NonQuery("DeleteProduct", new SqlParameter[] { prmProductId });

            LoadProductsGridView();
        }

        protected void grdProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            List<SqlParameter> prms = new List<SqlParameter>();

            TextBox txtProductName = (TextBox)this.grdProducts.Rows[e.RowIndex].FindControl("txtProductName");
            TextBox txtBriefDesc = (TextBox)this.grdProducts.Rows[e.RowIndex].FindControl("txtBriefDesc");
            TextBox txtFullDesc = (TextBox)this.grdProducts.Rows[e.RowIndex].FindControl("txtFullDesc");
            TextBox txtPrice = (TextBox)this.grdProducts.FooterRow.FindControl("txtPrice");
            CheckBox chkFeatured = (CheckBox)this.grdProducts.Rows[e.RowIndex].FindControl("chkFeatured");
            CheckBox chkStatus = (CheckBox)this.grdProducts.Rows[e.RowIndex].FindControl("chkStatus");
            FileUpload uplImage = (FileUpload)this.grdProducts.Rows[e.RowIndex].FindControl("uplUpdateImage");
            DropDownList ddlCategories = (DropDownList)this.grdProducts.Rows[e.RowIndex].FindControl("ddlCategories");

            try
            {
                string productName = txtProductName.Text.Trim();
                string productBriefDesc = txtBriefDesc.Text.Trim();
                string productFullDesc = txtFullDesc.Text.Trim();
                decimal price = Convert.ToDecimal(txtPrice.Text.Trim());
                bool featured = chkFeatured.Checked;
                bool status = chkStatus.Checked;
                string fileName = $"{ Server.MapPath("~/images/")}{ uplImage.FileName}";
                string imageName = $"images/{ uplImage.FileName }";
                string altText = uplImage.FileName;
                int categoryId = ddlCategories.SelectedIndex > 0 ? Convert.ToInt32(ddlCategories.SelectedValue) : 0;
                int productId = Convert.ToInt32(this.grdProducts.DataKeys[e.RowIndex].Values[0]);

                uplImage.SaveAs(fileName);

                prms.Add(DBHelper.SetProductIdParam(productId));
                prms.Add(DBHelper.SetProductNameParam(productName));
                prms.Add(DBHelper.SetProductBriefDescParam(productBriefDesc));
                prms.Add(DBHelper.SetProductFullDescParam(productFullDesc));
                prms.Add(DBHelper.SetProductPriceParam(price));
                prms.Add(DBHelper.SetProductFeaturedParam(featured));
                prms.Add(DBHelper.SetProductStatusCodeParam(status));

                if (fileName != "") //Don't make changes to the current image
                {
                    prms.Add(DBHelper.SetProductImageName(imageName));
                    prms.Add(DBHelper.SetProductImageAlt(altText));
                    prms.Add(DBHelper.SetProductImageName(DateTime.Now));
                }

                prms.Add(DBHelper.SetCategoryIdParam(categoryId));

                DBHelper.NonQuery("UpdateProduct", prms.ToArray());

                lblMessage.Text = "Product was updated";
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

            LoadProductsGridView();
        }

        protected void grdProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdProducts.EditIndex = e.NewEditIndex;
            LoadProductsGridView();
            Control ddlToFind = this.grdProducts.Rows[e.NewEditIndex].FindControl("ddlCategories");
            DropDownList ddlCategory = ddlToFind != null ? (DropDownList)(ddlToFind) : null;

            if (ddlCategory != null)
            {
                int productId = Convert.ToInt32(this.grdProducts.DataKeys[e.NewEditIndex].Values[0]);
                SqlParameter prmProductId = new SqlParameter() { ParameterName = "@ProductId", SqlDbType = SqlDbType.Int, Value = productId };
                int categoryId = DBHelper.GetQueryValue<int>("SELECT categoryId FROM Products WHERE id=@ProductId", "categoryId", new SqlParameter[] { prmProductId }, CommandType.Text);

                BindCategoryDropdownList(ddlCategory);

                ddlCategory.SelectedValue = categoryId.ToString();
            }
        }

        private void BindCategoryDropdownList(DropDownList ddlCategory)
        {
            ddlCategory.DataValueField = "id";
            ddlCategory.DataTextField = "name";
            DBHelper.DataBinding(ddlCategory, "SelectCategories");
            ddlCategory.Items.Insert(0, new ListItem("--Select Category--"));
        }

        protected void grdProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdProducts.EditIndex = -1;
            LoadProductsGridView();
        }

        protected void grdProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProducts.PageIndex = e.NewPageIndex;
            LoadProductsGridView();
        }

        private void AddNewProduct()
        {
            List<SqlParameter> prms = new List<SqlParameter>();

            TextBox txtProductName = (TextBox)this.grdProducts.FooterRow.FindControl("txtProductNameNew");
            TextBox txtBriefDesc = (TextBox)this.grdProducts.FooterRow.FindControl("txtBriefDescNew");
            TextBox txtFullDesc = (TextBox)this.grdProducts.FooterRow.FindControl("txtFullDescNew");
            TextBox txtPrice = (TextBox)this.grdProducts.FooterRow.FindControl("txtPriceNew");
            CheckBox chkFeatured = (CheckBox)this.grdProducts.FooterRow.FindControl("chkFeaturedNew");
            CheckBox chkStatus = (CheckBox)this.grdProducts.FooterRow.FindControl("chkStatusNew");
            FileUpload uplImage = (FileUpload)this.grdProducts.FooterRow.FindControl("uplNewImage");
            DropDownList ddlCategories = (DropDownList)this.grdProducts.FooterRow.FindControl("ddlCategoriesNew");

            try
            {
                string productName = txtProductName.Text.Trim();
                string productBriefDesc = txtBriefDesc.Text.Trim();
                string productFullDesc = txtFullDesc.Text.Trim();
                decimal price = Convert.ToDecimal(txtPrice.Text.Trim());
                bool featured = chkFeatured.Checked;
                bool status = chkStatus.Checked;
                string fileName = $"{ Server.MapPath("~/images/")}{ uplImage.FileName}";
                string imageName = $"images/{ uplImage.FileName }";
                string altText = uplImage.FileName;
                int categoryId = ddlCategories.SelectedIndex > 0 ? Convert.ToInt32(ddlCategories.SelectedValue) : 0;

                uplImage.SaveAs(fileName);

                prms.Add(DBHelper.SetProductNameParam(productName));
                prms.Add(DBHelper.SetProductBriefDescParam(productBriefDesc));
                prms.Add(DBHelper.SetProductFullDescParam(productFullDesc));
                prms.Add(DBHelper.SetProductPriceParam(price));
                prms.Add(DBHelper.SetProductFeaturedParam(featured));
                prms.Add(DBHelper.SetProductStatusCodeParam(status));
                prms.Add(DBHelper.SetProductImageName(imageName));
                prms.Add(DBHelper.SetProductImageAlt(altText));
                prms.Add(DBHelper.SetProductImageName(DateTime.Now));
                prms.Add(DBHelper.SetCategoryIdParam(categoryId));

                prms.Add(new SqlParameter()
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                });

                int id = DBHelper.Insert<int>("InsertProduct", "@Id", prms.ToArray());

                lblMessage.Text = $"Product was created. Id: {id.ToString()}";
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}