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
using System.Data.Common;
/// <summary>
///ExamineeAccess 的摘要说明
/// </summary>
public static class ExamineeAccess
{

    //报考认证考试
    public static bool EnrollCertificationExam(object userId,int CertificationID)
    {

        DbCommand com = GenericDataAccess.CreateCommand();
        com.CommandText = "EnrollCertificationExam";
        DbParameter para = com.CreateParameter();
        para.ParameterName = "userID";
        para.DbType = DbType.Guid;
        para.Value = userId;
        com.Parameters.Add(para);
        para = com.CreateParameter();
        para.ParameterName = "CertificationID";
        para.DbType = DbType.Int32;
        para.Value = CertificationID;
        com.Parameters.Add(para);

        para = com.CreateParameter();
        para.Direction = ParameterDirection.ReturnValue;
        para.ParameterName = "return";
        para.DbType = DbType.Int32;
        com.Parameters.Add(para);

        GenericDataAccess.ExecuteNonQuery(com);
        int effectRows = Convert.ToInt32(para.Value);
        return effectRows == -1 ? false : true;
    }
    //报考科目
    public static bool EnrollSubjectExam(object userId, int SubjectID)
    {

        DbCommand com = GenericDataAccess.CreateCommand();
        com.CommandText = "EnrollSubjectExam";
        DbParameter para = com.CreateParameter();
        para.ParameterName = "userID";
        para.DbType = DbType.Guid;
        para.Value = userId;
        com.Parameters.Add(para);
        para = com.CreateParameter();
        para.ParameterName = "subjectID";
        para.DbType = DbType.Int32;
        para.Value = SubjectID;
        com.Parameters.Add(para);

        para = com.CreateParameter();
        para.Direction = ParameterDirection.ReturnValue;
        para.ParameterName = "return";
        para.DbType = DbType.Int32;
        com.Parameters.Add(para);
        GenericDataAccess.ExecuteNonQuery(com);

        int effectRows =Convert.ToInt32( para.Value);
        return effectRows == -1 ? false : true;
    }
    //读取证书中的图像
    public static byte[] GetCertificatePhoto(string certificateID)
    {
        DbCommand com = GenericDataAccess.CreateCommand();

        com.CommandText = "GetCertificatePhoto";

        DbParameter para = com.CreateParameter();
        para.ParameterName = "certificateId";
        para.DbType = DbType.String;
        para.Value = certificateID;
        //para.Size = 20;
        com.Parameters.Add(para);
       // com.CommandType = CommandType.Text;
        DataTable dt=GenericDataAccess.ExecuteSelectCommand(com);
        return (byte[])dt.Rows[0][0];

    }
}
