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
    public partial class ucProducts : System.Web.UI.UserControl
    {

        public bool Featured { get; set; }

        /// <summary>
        /// page load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                string categoryId = Request.QueryString["categoryId"];

                if (this.Featured)
                {
                    this.lblHeading.Text = "Featured Products";

                    LoadFeaturedProducts();
                }
                else if (!string.IsNullOrEmpty(categoryId))
                {
                    int category = 0;

                    if (Int32.TryParse(categoryId, out category))
                    {
                        List<SqlParameter> prms = new List<SqlParameter>();
                        prms.Add(CategoryParamHelper(category));
                        this.lblHeading.Text = $"Products in Category: {DBHelper.GetQueryValue<string>("SelectCategories", "name", prms.ToArray())}";

                        LoadProductsByCategory(category);
                    }
                    else
                    {
                        this.lblHeading.Text = "No such category";
                    }
                }
                else if (Request.QueryString["word1"] != null)
                {
                    ProductSearch();
                }
                else if (!string.IsNullOrEmpty(id))
                {
                    int productId = 0;
                    if (Int32.TryParse(id, out productId))
                    {
                        LoadProductDetails(productId);
                        Control ctl = this.rptProducts.FindControl("lblName");

                        if (ctl != null)
                        {
                            Label lbl = (Label)ctl;
                            this.lblHeading.Text = lbl.Text;
                        }
                    }
                    else
                    {
                        this.lblHeading.Text = "No such product";
                    }
                }
                else
                {
                    this.lblHeading.Text = "Products";
                    LoadProducts();
                }

                ProductCountMessage();
            }
        }

        /// <summary>
        /// Search for a product
        /// </summary>
        private void ProductSearch()
        {
            List<string> searchKeys = Request.QueryString.AllKeys.Where(q => q.Contains("word")).ToList();
            bool all = Request.QueryString["all"] != null ? Request.QueryString["all"].ToString().ToLower() == "true" : false;

            List<SqlParameter> prms = new List<SqlParameter>();

            prms.Add(new SqlParameter() { ParameterName = "@MatchAllWords", SqlDbType = SqlDbType.Bit, Value = all });

            searchKeys.ForEach(w =>
            {
                prms.Add(new SqlParameter()
                {
                    ParameterName = $"@Key{w}",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50
                    ,
                    Value = Request.QueryString[w]
                });
            });

            this.lblHeading.Text = "Product search results";

            DBHelper.DataBinding(this.rptProducts, "SearchProducts", prms.ToArray());
        }

        /// <summary>
        /// set error label
        /// </summary>
        private void ProductCountMessage()
        {
            this.lblError.Text = this.rptProducts.Items.Count == 0 ? "No products found" : "";
        }

        /// <summary>
        /// helper method for category id param
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        private SqlParameter CategoryParamHelper(int categoryId)
        {
            return new SqlParameter()
            {
                ParameterName = "@CategoryId",
                SqlDbType = SqlDbType.Int,
                Value = categoryId
            };
        }
        /// <summary>
        /// Helper method for product id param
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private SqlParameter ProductParamHelper(int productId)
        {
            return new SqlParameter()
            {
                ParameterName = "@ProductId",
                SqlDbType = SqlDbType.Int,
                Value = productId
            };
        }

        /// <summary>
        /// Load products by category
        /// </summary>
        /// <param name="categoryId"></param>
        private void LoadProductsByCategory(int categoryId)
        {
            List<SqlParameter> prms = new List<SqlParameter>();
            if (categoryId != 0)
            {
                prms.Add(CategoryParamHelper(categoryId));
            }

            LoadProducts(prms);
        }

        /// <summary>
        /// Load a products details
        /// </summary>
        /// <param name="id"></param>
        private void LoadProductDetails(int id)
        {
            List<SqlParameter> prms = new List<SqlParameter>();
            if (id != 0)
            {
                prms.Add(ProductParamHelper(id));
            }

            LoadProducts(prms);
        }

        /// <summary>
        /// Load featured products
        /// </summary>
        private void LoadFeaturedProducts()
        {
            List<SqlParameter> prms = new List<SqlParameter>();
            prms.Add(new SqlParameter()
            {
                ParameterName = "@Featured",
                SqlDbType = SqlDbType.Bit,
                Value = true
            });

            LoadProducts(prms);
        }

        /// <summary>
        /// Load all products
        /// </summary>
        /// <param name="prms"></param>
        private void LoadProducts(List<SqlParameter> prms = null)
        {
            if (prms == null)
                prms = new List<SqlParameter>();

            //Always Status true on this uc as this is public view. Only status available viewd by the public
            prms.Add(new SqlParameter()
            {
                ParameterName = "@StatusCode",
                SqlDbType = SqlDbType.Bit,
                Value = true
            });

            DBHelper.DataBinding(this.rptProducts, "SelectProducts", prms.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Add")
                {
                    int productId = 0;

                    if (Int32.TryParse(e.CommandArgument.ToString(), out productId))
                    {
                        AddToCart(productId);
                    }
                    else
                    {
                        this.lblError.Text = "Invalid product to add to cart";
                    }
                }
            }
            catch (SqlException ex)
            {
                //Specific SQL error 
                lblError.Text = ex.Message;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        /// <summary>
        /// Add to Cart
        /// </summary>
        /// <param name="productId"></param>
        private void AddToCart(int productId)
        {
            //Get the Cart Guid from the Cookie or Make a new one
            string CartUId = Common.GetCartId(Request, Response);

            List<SqlParameter> prms = new List<SqlParameter>();
            prms.Add(ProductParamHelper(productId));

            prms.Add(new SqlParameter()
            {
                ParameterName = "@CartUId",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = CartUId
            });

            prms.Add(new SqlParameter()
            {
                ParameterName = "@Qty",
                SqlDbType = SqlDbType.Int,
                Value = 1
            });

            DBHelper.NonQuery("AddToCart", prms.ToArray());

            Response.Redirect("~/Cart.aspx");
        }
    }
}