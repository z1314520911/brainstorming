using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.Mvc;

namespace MVCRepeater
{
    public partial class _Default : Page
    {
        public void Page_Load(object sender, System.EventArgs e)
        {
            Response.Redirect("/index.aspx");
        }
    }
}
