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
    public partial class ProductMaintenance : AdminPage
    {
        /// <summary>
        /// on page load, calls LoadProductsGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            lblMessage.Text = "";

            if (!IsPostBack)
            {
                LoadProductsGridView();

            }
        }

        /// <summary>
        /// bind products for maintenance into grid view
        /// </summary>
        private void LoadProductsGridView()
        {
            DBHelper.DataBindingWithPaging(this.grdProducts, "SelectProductMaintenance");
            //Load the dropdown list in the create row
            DropDownList ddlCategoriesInFooter = (DropDownList)(this.grdProducts.FooterRow.FindControl("ddlCategoriesNew"));
            BindCategoryDropdownList(ddlCategoriesInFooter);

        }

        /// <summary>
        /// if command name is btnNew , add new product and reload products into grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnNew")
            {
                //New product
                AddNewProduct();

                LoadProductsGridView();
            }
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblMessage.Text = "";
            lblError.Text = "";
            int productId = Convert.ToInt32(this.grdProducts.DataKeys[e.RowIndex].Values[0]);
            SqlParameter prmProductId = new SqlParameter() { ParameterName = "@ProductId", SqlDbType = SqlDbType.Int, Value = productId };

            try
            {

                DBHelper.NonQuery("DeleteProduct", new SqlParameter[] { prmProductId });
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

            LoadProductsGridView();
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblMessage.Text = "";
            lblError.Text = "";


            List<SqlParameter> prms = new List<SqlParameter>();

            TextBox txtProductName = (TextBox)this.grdProducts.Rows[e.RowIndex].FindControl("txtProductName");
            TextBox txtBriefDesc = (TextBox)this.grdProducts.Rows[e.RowIndex].FindControl("txtBriefDesc");
            TextBox txtFullDesc = (TextBox)this.grdProducts.Rows[e.RowIndex].FindControl("txtFullDesc");
            TextBox txtPrice = (TextBox)this.grdProducts.Rows[e.RowIndex].FindControl("txtPrice");
            CheckBox chkFeatured = (CheckBox)this.grdProducts.Rows[e.RowIndex].FindControl("chkFeatured");
            CheckBox chkStatus = (CheckBox)this.grdProducts.Rows[e.RowIndex].FindControl("chkStatus");
            FileUpload uplImage = (FileUpload)this.grdProducts.Rows[e.RowIndex].FindControl("uplUpdateImage");
            DropDownList ddlCategories = (DropDownList)this.grdProducts.Rows[e.RowIndex].FindControl("ddlCategories");

            try
            {
                string productName = txtProductName.Text.Trim();
                string productBriefDesc = txtBriefDesc.Text.Trim();
                string productFullDesc = txtFullDesc.Text.Trim();
                
                double price;
                Double.TryParse(txtPrice.Text.Trim(),out price);
                bool featured = chkFeatured.Checked;
                bool status = chkStatus.Checked;
                string fileName = $"{ Server.MapPath("~/images/")}{ uplImage.FileName}";
                string imageName = $"images/{ uplImage.FileName }";
                string altText = uplImage.FileName;
                int categoryId = ddlCategories.SelectedIndex > 0 ? Convert.ToInt32(ddlCategories.SelectedValue) : 0;
                int productId = Convert.ToInt32(this.grdProducts.DataKeys[e.RowIndex].Values[0]);

                

                prms.Add(DBHelper.SetProductIdParam(productId));
                prms.Add(DBHelper.SetProductNameParam(productName));
                prms.Add(DBHelper.SetProductBriefDescParam(productBriefDesc));
                prms.Add(DBHelper.SetProductFullDescParam(productFullDesc));
                prms.Add(DBHelper.SetProductPriceParam(price));
                prms.Add(DBHelper.SetProductFeaturedParam(featured));
                prms.Add(DBHelper.SetProductStatusCodeParam(status));

                if (uplImage.FileName != "") //Don't make changes to the current image
                {
                    uplImage.SaveAs(fileName);

                    prms.Add(DBHelper.SetProductImageName(imageName));
                    prms.Add(DBHelper.SetProductImageAlt(altText));
                    prms.Add(DBHelper.SetProductImageDate(DateTime.Now));
                }

                prms.Add(DBHelper.SetCategoryIdParam(categoryId));

                DBHelper.NonQuery("UpdateProduct", prms.ToArray());

                lblMessage.Text = "Product was updated";
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

            grdProducts.EditIndex = -1;
            LoadProductsGridView();
            

        }

        /// <summary>
        /// Edit product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// bind categories into dropdown
        /// </summary>
        /// <param name="ddlCategory"></param>
        private void BindCategoryDropdownList(DropDownList ddlCategory)
        {
            ddlCategory.DataValueField = "id";
            ddlCategory.DataTextField = "name";
            DBHelper.DataBinding(ddlCategory, "SelectCategories");
            ddlCategory.Items.Insert(0, new ListItem("--Select Category--"));
        }
        /// <summary>
        /// cancel editing of product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void grdProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdProducts.EditIndex = -1;
            LoadProductsGridView();
        }

        /// <summary>
        /// on page index change, reload products into grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProducts.PageIndex = e.NewPageIndex;
            LoadProductsGridView();
        }

        /// <summary>
        /// Add new product
        /// </summary>
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
                double price;
                Double.TryParse(txtPrice.Text.Trim(), out price);
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
                prms.Add(DBHelper.SetProductImageDate(DateTime.Now));
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