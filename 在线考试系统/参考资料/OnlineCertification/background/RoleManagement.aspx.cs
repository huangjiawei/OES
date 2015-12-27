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

public partial class background_AuthorityManagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        UserDataBind();
    }
    //把用户名列表绑定到控件中
    protected void UserDataBind()
    {
        ddlAdmin.DataSource=Roles.GetUsersInRole("Admin");
        ddlAdmin.DataBind();
        ddlTeacher.DataSource = Roles.GetUsersInRole("Teacher");
        ddlTeacher.DataBind();
        


    }
    protected void btnAdminToTeacher_Click(object sender, EventArgs e)
    {
        string user = ddlAdmin.SelectedValue;
        Roles.RemoveUserFromRole(user, "Admin");
        Roles.AddUserToRole(user,"Teacher");
        UserDataBind();
        
    }
    protected void btnAdminToUser_Click(object sender, EventArgs e)
    {
        string user = ddlAdmin.SelectedValue;
        Roles.RemoveUserFromRole(user, "Admin");
        Roles.AddUserToRole(user, "User");
        UserDataBind();
    }
    protected void btnTeacherToAdmin_Click(object sender, EventArgs e)
    {
        string user = ddlTeacher.SelectedValue;
        Roles.RemoveUserFromRole(user, "Teacher");
        Roles.AddUserToRole(user, "Admin");
        UserDataBind();
    }
    protected void btnTeacherToUser_Click(object sender, EventArgs e)
    {
        string user = ddlTeacher.SelectedValue;
        Roles.RemoveUserFromRole(user, "Teacher");
        Roles.AddUserToRole(user, "User");
        UserDataBind();
    }
    protected void btnUserToTeacher_Click(object sender, EventArgs e)
    {
        string user = tbName.Text;
        if(Roles.IsUserInRole(user,"Examinee")&&!Roles.IsUserInRole(user,"Teacher")){        
        //Roles.RemoveUserFromRole(user, "User");
            Roles.AddUserToRole(user, "Teacher");
            UserDataBind();
        }
        else
        {
            tbName.Text="未能找到该用户";
            
        }
        
    }
    protected void btnUserToAdmin_Click(object sender, EventArgs e)
    {
        string user = tbName.Text;
        if(Roles.IsUserInRole(user,"Examinee"))
        {        
        //Roles.RemoveUserFromRole(user, "User");
            if(!Roles.IsUserInRole(user,"Admin"))Roles.AddUserToRole(user, "Admin");
            UserDataBind();
        }
        else
        {
            tbName.Text="未能找到该用户";
            
        }
    }
}
