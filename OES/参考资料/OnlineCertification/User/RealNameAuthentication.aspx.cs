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
using System.IO;
using SecurityLib;
public partial class User_RealNameAuthentication : System.Web.UI.Page
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
        if (tbBirthDay.Text.Trim() != "" && tbBirthPlace.Text.Trim() != "" && tbIdCardNo.Text.Trim() != "" && tbName.Text.Trim() != ""
            && tbPhone.Text.Trim() != "" && tbAddress.Text.Trim() != "" && (RadioButtonListSex.SelectedValue=="True"||RadioButtonListSex.SelectedValue=="False"))
        {
            Profile.BirthDay = StringEncryptor.EncryptUTF(tbBirthDay.Text.ToString());
            Profile.BirthPlace = StringEncryptor.EncryptUTF(tbBirthPlace.Text);
            Profile.IdCardNo = StringEncryptor.EncryptUTF(tbIdCardNo.Text.Trim());
            // Profile.Photo =imgPhoto.ImageUrl;
            Profile.RealName = StringEncryptor.EncryptUTF(tbName.Text.Trim());
            Profile.Sex = StringEncryptor.EncryptUTF(RadioButtonListSex.SelectedValue);
            Profile.Address = StringEncryptor.EncryptUTF(tbAddress.Text.Trim());
            Profile.Telephone = StringEncryptor.EncryptUTF(tbPhone.Text.Trim());
            ProfileDataBind();
            if (Profile.Photo != null)
            {
                try
                {
                    if (!User.IsInRole("WaitToBeExaminee"))
                    {
                        Roles.AddUserToRole(Profile.UserName, "WaitToBeExaminee");
                        lbStatus.Text = "认证申请已登记，请等待系统管理员进行认证";
                    }
                    else
                    {
                        Utilities.LogError(new Exception("用户已经在实名审核队列中,但仍然在申请实名认证"));
                    }
                }
                catch (Exception ex)
                {
                    Utilities.LogError(ex);
                    throw new Exception("实名认证失败", ex);

                }
            }
            else
            {
                lbStatus.Text = "请上传照片";
            }
        }
        else
        {
            lbStatus.Text = "请完善个人信息";
        }

    }
    protected void ProfileDataBind()
    {
        if (User.IsInRole("Examinee") || User.IsInRole("WaitToBeExaminee") || User.IsInRole("Teacher") || User.IsInRole("Admin"))
        {
            tbBirthPlace.Text = StringEncryptor.DecryptUTF(Profile.BirthPlace);
            tbName.Text = StringEncryptor.DecryptUTF(Profile.RealName);
            tbPhone.Text = StringEncryptor.DecryptUTF(Profile.Telephone);
            tbIdCardNo.Text = StringEncryptor.DecryptUTF(Profile.IdCardNo);
            tbBirthDay.Text = ((StringEncryptor.DecryptUTF(Profile.BirthDay)));
            tbAddress.Text = StringEncryptor.DecryptUTF(Profile.Address);
            RadioButtonListSex.SelectedValue =StringEncryptor.DecryptUTF( Profile.Sex);
            tbEmail.Text = Membership.GetUser().Email;
            if (Profile.Photo != null)
            {
                imgPhoto.ImageUrl = "~/User/UserPhoto.aspx";
                // lbLength.Text =(Profile.Photo.Length / 8 ).ToString()+"B";
            }
            else
            {
                Utilities.LogError(new Exception("用户已验证，但相片未设置"));
            }
        }
        if (User.IsInRole("Examinee") || User.IsInRole("WaitToBeExaminee"))
        {
            FileUpload1.Visible = false;
            btnSave.Enabled = false;
            Button1.Enabled = false;
        }
    }
    //上传图片
    protected void Button1_Click(object sender, EventArgs e)
    {
        //如果用户已经通过验证，则不执行任何操作
        if (User.IsInRole("Examinee")
            || User.IsInRole("WaitToBeExaminee")
            ) return;      
        HttpPostedFile file = FileUpload1.PostedFile;
        byte[] data = new byte[file.ContentLength];
        file.InputStream.Read(data, 0, file.ContentLength);
        Profile.Photo = data;
        Profile.PhotoType = file.ContentType;


    }
}
