using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class background_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect("~/Default.aspx");
        }

        if (HttpContext.Current.User.IsInRole("Teacher"))
            TreeViewTeacher.Visible = true;
        else
            TreeViewTeacher.Visible = false;
        if (HttpContext.Current.User.IsInRole("Admin"))
            TreeViewAdmin.Visible = true;
        else
            TreeViewAdmin.Visible = false;

        
        
    }
    
}
