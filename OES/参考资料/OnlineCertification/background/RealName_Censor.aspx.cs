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

public partial class background_RealName_Censor : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
  
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

    protected void GridViewAuthorize_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Authorize")
        {
            string userName=e.CommandArgument.ToString();
            try
            {
                //审核成功，将正在等候实名审核的用户变为考生角色
                if (!Roles.IsUserInRole(userName,"Examinee"))
                {
                    Roles.AddUserToRole(userName, "Examinee");
                }
                if (Roles.IsUserInRole(userName, "WaitToBeExaminee"))
                {
                    Roles.RemoveUserFromRole(userName, "WaitToBeExaminee");
                }
                Profile.GetProfile(userName).MsgBox = "恭喜！已成功通过实名验证";
                
            }
            catch(Exception ex)
            {
                Utilities.LogError(ex);
                Profile.GetProfile(userName).MsgBox = "实名验证未通过，请重新申请。抱歉！";
                throw ex;
                
            }


        }
        if (e.CommandName == "Deny")
        {
            
            string userName=e.CommandArgument.ToString();
            if (Roles.IsUserInRole(userName, "WaitToBeExaminee"))
            {
                Roles.RemoveUserFromRole(userName, "WaitToBeExaminee");
               
            }
            if(Roles.IsUserInRole(userName,"Examinee"))
                Utilities.LogError(new Exception(("拒绝审核操作异常：用户已经是考生，但仍在实名认证审核用户列表中")));

             Profile.GetProfile(userName).MsgBox = "实名验证未通过，请重新申请。抱歉！";
        }
    }
}
