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
using System.Web.SessionState;
using System.Web.UI;

namespace WhiteElephantWebsite
{
    public static class Common
    {
        public static string GetCartId(HttpRequest request, HttpResponse response)
        {
            //Get the Cart Guid from the Cookie or Make a new one
            string CartUId = "";

            if (request.Cookies["CartUId"] != null)
            {
                CartUId = request.Cookies["CartUId"].Value;
            }
            else
            {
                Guid cartGuid = Guid.NewGuid();

                response.Cookies["CartUId"].Value = cartGuid.ToString();
                response.Cookies["CartUId"].Expires.AddDays(30);
                CartUId = cartGuid.ToString();
            }

            return CartUId;
        }

        public static int GetCartCount(string cartId)
        {
            string CartUId = cartId;

            List<SqlParameter> prms = new List<SqlParameter>();

            prms.Add(new SqlParameter()
            {
                ParameterName = "@CartUId",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = CartUId
            });

            int count = DBHelper.GetScalarValue<int>("CartCount", prms.ToArray());

            return count;
        }

        public static Boolean IsUserAuthenticated(HttpSessionState session)
        {
            return session["authenticated"] != null && session["authenticatedUser"] != null && session["admin"] == null;
        }

        public static Boolean IsAdminAuthenticated(HttpSessionState session)
        {
            return session["authenticated"] != null && session["authenticatedUser"] != null && session["admin"] != null;
        }

        public static string GetAuthenticatedUser(HttpSessionState session)
        {
            if (IsUserAuthenticated(session))
                return session["authenticatedUser"].ToString();
            else
                return "";
        }

        public static void GetCart(Control ctrl, HttpRequest request, HttpResponse response)
        {
            string CartUId = Common.GetCartId(request, response);
            List<SqlParameter> prms = new List<SqlParameter>();

            prms.Add(new SqlParameter()
            {
                ParameterName = "@CartUId",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = CartUId
            });

            DBHelper.DataBinding(ctrl, "SelectCart", prms.ToArray());
        }

        public static decimal GetCartTotal(HttpRequest request, HttpResponse response)
        {
            string CartUId = Common.GetCartId(request, response);
            List<SqlParameter> prms = new List<SqlParameter>();

            prms.Add(new SqlParameter()
            {
                ParameterName = "@CartUId",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = CartUId
            });

            decimal cartTotal = DBHelper.GetQueryValue<decimal>("CartTotal", "Total", prms.ToArray());

            double temp;
            Double.TryParse(cartTotal.ToString(), out temp);
            temp = temp * 1.15;
            cartTotal = (Decimal)temp;
            return cartTotal;
        }
    }
}