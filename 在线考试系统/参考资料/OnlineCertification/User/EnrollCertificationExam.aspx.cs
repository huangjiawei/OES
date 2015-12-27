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

public partial class User_EnrollCertificationExam : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnEnroll_Click(object sender, EventArgs e)
    {
            object id=Membership.GetUser().ProviderUserKey;
            bool success=ExamineeAccess.EnrollCertificationExam(id,int.Parse(ddlCertification.SelectedValue));
            if (success) lbStatus.Text = "恭喜！已成功报考";
            else
                lbStatus.Text = "报考失败！你可能已经报考了该认证考试。";
    }
}
