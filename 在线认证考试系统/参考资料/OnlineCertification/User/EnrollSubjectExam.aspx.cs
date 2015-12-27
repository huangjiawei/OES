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

public partial class User_EnrollSubjectExam : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataSourceCertificationEnrolled.SelectParameters["userName"].DefaultValue = HttpContext.Current.User.Identity.Name;
    }
    //报考科目
    protected void gvSubject_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "enroll")
        {
            
            int index=Convert.ToInt32(e.CommandArgument);
             int subjectID=Convert.ToInt32(gvSubject.DataKeys[index]["ID"]);
            object id = Membership.GetUser().ProviderUserKey;
            ExamineeAccess.EnrollSubjectExam(id, subjectID);

        }
    }
}
