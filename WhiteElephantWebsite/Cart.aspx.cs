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
    public partial class Cart : System.Web.UI.Page
    {
        /// <summary>
        /// On page load, get cart and total
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetCart();
                GetCartTotal();
            }
        }

        /// <summary>
        /// get user's cart - if empty, show no items in cart and set labels and buttons visibility to false
        /// </summary>
        private void GetCart()
        {
            Common.GetCart(this.grdCart, Request, Response);

            if (this.grdCart.Rows.Count == 0)
            {
                this.btnCheckout.Visible = false;
                this.btnContinueShopping.Visible = false;
                this.btnUpdateCart.Visible = false;
                this.lblTax.Visible = false;
                this.lblCartTax.Visible = false;
                this.lblTotal.Text = "No items in cart";
                this.lblCartTotal.Visible = false;
            }
        }

        /// <summary>
        /// Cart total with tax
        /// </summary>
        private void GetCartTotal()
        {
            this.lblCartTotal.Text = Common.GetCartTotal(Request, Response).ToString("c2");
            Double temp;
            Double.TryParse(Common.GetCartTotal(Request, Response).ToString(), out temp);

            temp *= 0.15;

            this.lblCartTax.Text = temp.ToString("c2");
        }

        /// <summary>
        /// Update cart and rebind
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateCart_Click(object sender, EventArgs e)
        {

            string CartUId = Common.GetCartId(Request, Response);
            
            //One form of saving changes. One item at a time when there is a change
            foreach (GridViewRow row in this.grdCart.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txt = (TextBox)row.FindControl("txtQty");
                    HiddenField hdn = (HiddenField)row.FindControl("hdnCurrentQty");
                    CheckBox chkRemove = (CheckBox)row.FindControl("chkRemove");

                    int qty = chkRemove.Checked ? 0 : Convert.ToInt32(txt.Text.Trim());
                    int currentQty = Convert.ToInt32(hdn.Value);
                    int productId = Convert.ToInt32(this.grdCart.DataKeys[row.RowIndex].Value);

                    //Save us a round trip
                    if (currentQty != qty)
                        UpdateCartItem(CartUId, productId, qty);
                }
            }
            
            grdCart.DataBind();

            GetCart();
            GetCartTotal();

            //Update the MasterPage label
            int count = Common.GetCartCount(CartUId);
            Label lblCartItemsCount = (Label)this.Page.Master.FindControl("lblCartItemsCount");

            if (lblCartItemsCount != null)
                lblCartItemsCount.Text = count == 1 ? $"{count} item in cart" : count > 1 ? $"{count} items in cart" : "";
        }

        /// <summary>
        /// Go back to products page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnContinueShopping_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Products.aspx");
        }

        /// <summary>
        /// Update a cart item
        /// </summary>
        /// <param name="CartUId"></param>
        /// <param name="productId"></param>
        /// <param name="qty"></param>
        private void UpdateCartItem(string CartUId, int productId, int qty)
        {
            List<SqlParameter> prms = new List<SqlParameter>();
            prms.Add(new SqlParameter()
            {
                ParameterName = "@CartUId",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = CartUId
            });
            prms.Add(new SqlParameter()
            {
                ParameterName = "@ProdId",
                SqlDbType = SqlDbType.Int,
                Value = productId
            });
            prms.Add(new SqlParameter()
            {
                ParameterName = "@Qty",
                SqlDbType = SqlDbType.Int,
                Value = qty
            });

            DBHelper.NonQuery("UpdateCart", prms.ToArray());


        }

        /// <summary>
        /// Redirect to checkout page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Checkout.aspx");
        }
    }
}