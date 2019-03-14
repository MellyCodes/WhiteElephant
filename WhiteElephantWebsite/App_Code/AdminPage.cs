using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteElephantWebsite
{
    public class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.Url.ToString();
            if (!Common.IsAdminAuthenticated(Session))
                Response.Redirect($"~/admin/Login.aspx?returnurl={url}");
        }
    }
}