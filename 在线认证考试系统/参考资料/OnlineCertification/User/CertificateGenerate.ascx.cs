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
using SecurityLib;

public partial class User_CertificateGenerate : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlUnPass.SelectParameters["ID"].DefaultValue = Membership.GetUser().ProviderUserKey.ToString();

    }
    //申请认证
    protected void btnApply_Click(object sender, EventArgs e)
    {
        object userId = Membership.GetUser().ProviderUserKey.ToString();
        //  string a = ddlUnPass.SelectedItem.Value;
        if (ddlUnPass.SelectedValue != null && ddlUnPass.SelectedValue != "")
        {

            bool success = ExamControl.GenerateCertificate(userId,
                int.Parse(ddlUnPass.SelectedValue),
                StringEncryptor.DecryptUTF(Profile.IdCardNo),
                StringEncryptor.DecryptUTF(Profile.RealName),
                Profile.Photo);


            if (success) lbStatus.Text = "申请成功";
            else lbStatus.Text = "申请失败";
        }
        else
        {
            lbStatus.Text = "请选择要申请的认证";
        }
    }
}
