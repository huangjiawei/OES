using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class User_ExamEnrolled : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        SqlDataSourceCertificationEnrolled.SelectParameters["userName"].DefaultValue = HttpContext.Current.User.Identity.Name;
        SqlDataSourceSubjectEnrolled.SelectParameters["userName"].DefaultValue = HttpContext.Current.User.Identity.Name;
    }
  
    protected void dlSubjectEnroll_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "TakeExam")
        {
            if (User.Identity.IsAuthenticated)
            {
                Session["SubjectID"] = e.CommandArgument.ToString();
                Response.Redirect("~/User/OnlineExam_Default.aspx");
                
            }

        }
    }
}
