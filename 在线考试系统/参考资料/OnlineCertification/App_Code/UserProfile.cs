using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


/// <summary>
///UserProfile 用户个人信息类，主要用来加载到审核页面 ~\background\RealName_Censor.aspx 的ObjectDataSource中
/// </summary>
public class UserProfile
{
    public string userName;
    public string realName;
    public string birthDay;
    public string idCardNo;
    public string birthPlace;
    public string telephone;
    public string address;
    public string sex;
  //  public string Other;
//    public string PhotoUrl;
    public string UserName
    {
        get { return userName; }
    }
    public string RealName
    {
        get { return realName; }
    }
    public string IdCardNo
    {
        get { return idCardNo; }
    }
    public string BirthPlace
    {
        get { return birthPlace; }
    }
    public string BirthDay
    {
        get { return birthDay; }
    }
    public string Telephone
    {
        get { return telephone; }
    }
    public string Address
    {
        get { return address; }
    }
    public string Sex
    {
        get { return sex; }
    }


	public UserProfile()
	{
        
		
	}
    public static UserProfile[] GetProfiles()
    {
        string[] users = Roles.GetUsersInRole("WaitToBeExaminee");
        UserProfile[] userProfiles = new UserProfile[users.Length];
        ProfileCommon profileCommon = new ProfileCommon();
        for (int i = 0; i < users.Length; i++)
        {  
            //获取指定用户Profile
            ProfileCommon tempProfile=profileCommon.GetProfile(users[i]);
           
            userProfiles[i] = new UserProfile();
            userProfiles[i].address = SecurityLib.StringEncryptor.DecryptUTF(tempProfile.Address);
            userProfiles[i].birthDay = SecurityLib.StringEncryptor.DecryptUTF(tempProfile.BirthDay);
            userProfiles[i].realName = SecurityLib.StringEncryptor.DecryptUTF(tempProfile.RealName);
            userProfiles[i].telephone = SecurityLib.StringEncryptor.DecryptUTF(tempProfile.Telephone);
            userProfiles[i].idCardNo = SecurityLib.StringEncryptor.DecryptUTF(tempProfile.IdCardNo);
            userProfiles[i].sex = SecurityLib.StringEncryptor.DecryptUTF(tempProfile.Sex)=="True"?"男":"女";
            userProfiles[i].birthPlace = SecurityLib.StringEncryptor.DecryptUTF(tempProfile.BirthPlace);
            userProfiles[i].userName = users[i];
       //     userProfiles[i].PhotoUrl=

        }
        return userProfiles;
    }
}
