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

public partial class User_UserPhoto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["UserName"] != null)
        {
            //显示指定用户名的照片
            ProfileCommon userProfile=Profile.GetProfile(Request.QueryString["UserName"].ToString());
            Response.ContentType = userProfile.PhotoType;
            Response.BinaryWrite(userProfile.Photo);

        }
        else
        {
            //显示当前用户的照片
          Response.Redirect("~/User/UserPhoto.aspx?UserName="+Profile.UserName);
        }
        
    }
}
