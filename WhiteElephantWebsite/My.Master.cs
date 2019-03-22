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
    public partial class My : System.Web.UI.MasterPage
    {
        /// <summary>
        /// set labels and hyperlink texts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    this.lblCopyRightYear.Text = DateTime.Now.Year.ToString();
                    GetCartCount();

                    if (Common.IsUserAuthenticated(Session))
                    {
                        this.hypLogin.Text = "Logout";
                        this.hypLogin.NavigateUrl = "~/Logout.aspx";
                    }
                    else
                    {
                        this.hypLogin.Text = "Login";
                        this.hypLogin.NavigateUrl = "~/Login.aspx";
                    }
                }
                catch (Exception)
                {
                    //handle error
                }

            }
        }

        /// <summary>
        /// Get the cart count and update label
        /// </summary>
        private void GetCartCount()
        {
            string cartId = Common.GetCartId(Request, Response);
            int count = Common.GetCartCount(cartId);
          
           this.lblCartItemsCount.Text = count > 0 ? $"{count}" : "";
        }
    }
}