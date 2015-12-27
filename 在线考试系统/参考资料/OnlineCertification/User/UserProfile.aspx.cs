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

public partial class User_UserProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProfileDataBind();
            
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       
        Profile.Telephone =StringEncryptor.EncryptUTF(tbPhone.Text.Trim());
        ProfileDataBind();
       
        
    }
    protected void ProfileDataBind()
    {

         if (User.IsInRole("Examinee") ||  User.IsInRole("Teacher") || User.IsInRole("Admin"))
        {
            tbBirthPlace.Text =StringEncryptor.DecryptUTF(Profile.BirthPlace);
            tbName.Text = StringEncryptor.DecryptUTF(Profile.RealName);
            tbPhone.Text = StringEncryptor.DecryptUTF(Profile.Telephone);
            tbIdCardNo.Text = StringEncryptor.DecryptUTF(Profile.IdCardNo);
            tbBirthDay.Text = StringEncryptor.DecryptUTF(Profile.BirthDay);
            tbAddress.Text = StringEncryptor.DecryptUTF(Profile.Address);
             lbSex.Text = StringEncryptor.DecryptUTF(Profile.Sex)=="True"?"男":"女";
            tbEmail.Text = Membership.GetUser().Email;
            if (Profile.Photo != null)
            {
                imgPhoto.ImageUrl = "~/User/UserPhoto.aspx";
            }
            else
            {
                Utilities.LogError(new Exception("用户已验证，但相片未设置"));
            }
        }
    }
    
}
