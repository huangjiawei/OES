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
using System.Collections.Generic;

 public struct SubjectQuestionQuantity
{
     public string Name;
    public int SingleChoice;
    public int MultipleChoice;
    public int Filling;
    public int ShortAnswer;
    public int Discussion;
    public bool isReady;

}
 public class Paper
 {
     Guid paperId;
     string subjectName;
     public Paper(Guid pId,string subject)
     {
         paperId = pId;
         subjectName = subject;
     }
     Guid PaperId
     {
         get { return paperId; }

     }
     string Subject
     {
         get { return subjectName; }
     }


 }
/// <summary>
///ExamControl 的摘要说明
/// </summary>
public static class ExamControl
{
    //获取科目各种题型题数
    public static SubjectQuestionQuantity GetSubjectQuestionQuantity(int SubjectId)
    {
        SubjectQuestionQuantity sqq;
        DbCommand com = GenericDataAccess.CreateCommand();
        com.CommandText = "GetSubjectQuestionQuantity";
        DbParameter para = com.CreateParameter();
        para.DbType = DbType.Int32;
        para.ParameterName = "@subjectId";
        para.Value = SubjectId;
        com.Parameters.Add(para);
        DataTable  dt=GenericDataAccess.ExecuteSelectCommand(com);
        if (dt.Rows.Count == 1)
        {
            sqq.SingleChoice = Convert.ToInt32(dt.Rows[0][0]);
            sqq.MultipleChoice = Convert.ToInt32(dt.Rows[0][1]);
            sqq.Filling = Convert.ToInt32(dt.Rows[0][2]);
            sqq.ShortAnswer = Convert.ToInt32(dt.Rows[0][3]);
            sqq.Discussion = Convert.ToInt32(dt.Rows[0][4]);
            sqq.isReady =  Convert.ToBoolean(dt.Rows[0][5]);
            sqq.Name = dt.Rows[0][6].ToString();

        }
        else
        {
            sqq.Discussion = sqq.ShortAnswer = sqq.SingleChoice = sqq.MultipleChoice = sqq.Filling = -1;
            sqq.isReady = false;
            sqq.Name = "";
        }
        return sqq;
    }
  

    //生成试卷
    public static object GenerationExamPaper(int SubjectId,object userId)
    {
        DbCommand com = GenericDataAccess.CreateCommand();
        com.CommandText = "GenerationExamPaper";
        DbParameter para = com.CreateParameter();
        para.Value = SubjectId;
        para.DbType = DbType.Int32;
        para.ParameterName = "SubjectId";
        com.Parameters.Add(para);
        para = com.CreateParameter();
        para.ParameterName = "UserId";
        para.DbType = DbType.Guid;
        para.Value = userId;
        com.Parameters.Add(para);
        return GenericDataAccess.ExecuteScalar(com);
        
    }
    //生成试卷客观成绩
    public static int GenerateObjectiveItemScore(object paperId)
    {
        DbCommand com = GenericDataAccess.CreateCommand();
        com.CommandText = "GenerateObjectiveScore";
        DbParameter para = com.CreateParameter();
        para.DbType = DbType.Guid;
        para.ParameterName = "PaperId";
        para.Value = new Guid(paperId.ToString());
        com.Parameters.Add(para);
        return int.Parse(GenericDataAccess.ExecuteScalar(com));
    }
    //生成试卷主观题成绩并返回总分
    public static int GenerateSubjectiveItemScore(object paperId,out int score)
    {
        score = 0;
        DbCommand com = GenericDataAccess.CreateCommand();
        com.CommandText = "GenerateSubjectiveScoreAndGetSum";
        DbParameter para = com.CreateParameter();
        para.DbType = DbType.Guid;
        para.ParameterName = "PaperId";
        para.Value = new Guid(paperId.ToString());
        com.Parameters.Add(para);
        para = com.CreateParameter();
        para.DbType = DbType.Int32;
        para.ParameterName = "@totalScore"; 
        para.Direction = ParameterDirection.Output;
        para.Value = score;
       
        com.Parameters.Add(para);
         GenericDataAccess.ExecuteNonQuery(com);
         score = (int)para.Value;
        return (score);
    }
    public static bool AnswerSingleChoice(object paperId, Int64 questionId, bool a, bool b, bool c, bool d)
    {
        DbCommand com = GenericDataAccess.CreateCommand();
        com.CommandText = "AnswerSingleChoiceQuestion";

        DbParameter para = com.CreateParameter();
        para.DbType = DbType.Guid;
        para.ParameterName = "PaperId";
        para.Value = new Guid(paperId.ToString());
        com.Parameters.Add(para);

        para = com.CreateParameter();
        para.DbType = DbType.Int64;
        para.ParameterName = "@QuestionID";
        para.Value = questionId;
        com.Parameters.Add(para);

        para = com.CreateParameter();
        para.DbType = DbType.Boolean;
        para.ParameterName = "A";
        para.Value = a;
        com.Parameters.Add(para);

        para = com.CreateParameter();
        para.DbType = DbType.Boolean;
        para.ParameterName = "B";
        para.Value = b;
        com.Parameters.Add(para);

        para = com.CreateParameter();
        para.DbType = DbType.Boolean;
        para.ParameterName = "C";
        para.Value = c;
        com.Parameters.Add(para);

        para = com.CreateParameter();
        para.DbType = DbType.Boolean;
        para.ParameterName = "D";
        para.Value = d;
        com.Parameters.Add(para);

        para = com.CreateParameter();
        para.Direction = ParameterDirection.ReturnValue;
        para.ParameterName = "return";
        para.DbType = DbType.Int32;
        com.Parameters.Add(para);

        GenericDataAccess.ExecuteNonQuery(com);

        return Convert.ToInt32(para.Value) == 1 ? true : false;



    }

    public static bool AnswerMultipleChoiceQuestion(object paperId, Int64 questionId, bool a, bool b, bool c, bool d, bool e)
    {
        DbCommand com = GenericDataAccess.CreateCommand();
        com.CommandText = "AnswerMultipleChoiceQuestion";

        DbParameter para = com.CreateParameter();
        para.DbType = DbType.Guid;
        para.ParameterName = "PaperId";
        para.Value = new Guid(paperId.ToString());
        com.Parameters.Add(para);

        para = com.CreateParameter();
        para.DbType = DbType.Int64;
        para.ParameterName = "@QuestionID";
        para.Value = questionId;
        com.Parameters.Add(para);

        para = com.CreateParameter();
        para.DbType = DbType.Boolean;
        para.ParameterName = "A";
        para.Value = a;
        com.Parameters.Add(para);

        para = com.CreateParameter();
        para.DbType = DbType.Boolean;
        para.ParameterName = "B";
        para.Value = b;
        com.Parameters.Add(para);

        para = com.CreateParameter();
        para.DbType = DbType.Boolean;
        para.ParameterName = "C";
        para.Value = c;
        com.Parameters.Add(para);

        para = com.CreateParameter();
        para.DbType = DbType.Boolean;
        para.ParameterName = "D";
        para.Value = d;
        com.Parameters.Add(para);

        para = com.CreateParameter();
        para.DbType = DbType.Boolean;
        para.ParameterName = "E";
        para.Value = e;
        com.Parameters.Add(para);


        para = com.CreateParameter();
        para.Direction = ParameterDirection.ReturnValue;
        para.ParameterName = "return";
        para.DbType = DbType.Int32;
        com.Parameters.Add(para);

        GenericDataAccess.ExecuteNonQuery(com);

        return Convert.ToInt32(para.Value) == 1 ? true : false;



    }
    //生成认证证书
    public static bool GenerateCertificate(object userId,int cartificationId,string cardId,string name,byte[] photo)
    {
        DbCommand com = GenericDataAccess.CreateCommand();
        com.CommandText = "GenerateCertificate";
        
        DbParameter para = com.CreateParameter();
        para.DbType = DbType.Guid;
        para.ParameterName = "UserId";
        para.Value = new Guid(userId.ToString());
        
        com.Parameters.Add(para);
        para = com.CreateParameter();
        para.DbType = DbType.Int32;
        para.ParameterName = "CertificationId";
        para.Value = cartificationId;
        com.Parameters.Add(para);
        
        para = com.CreateParameter();
        para.DbType = DbType.String;
        para.ParameterName = "cardId";
        para.Value = cardId;
        com.Parameters.Add(para);

        para = com.CreateParameter();
        para.DbType = DbType.String;
        para.ParameterName = "Name";
        para.Value = name;
        com.Parameters.Add(para);

        para = com.CreateParameter();
        para.DbType = DbType.Binary;
        para.ParameterName = "photo";
        para.Size = photo.Length;
        para.Value = photo;
        com.Parameters.Add(para);


        para = com.CreateParameter();
        para.Direction = ParameterDirection.ReturnValue;
        para.ParameterName = "return";
        para.DbType = DbType.Int32;
        com.Parameters.Add(para);
        GenericDataAccess.ExecuteNonQuery(com);

        int effectRows =Convert.ToInt32( para.Value);

      
        return effectRows ==-1 ? false : true;

    }


    public static string GetJson(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = 
                       new  System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows =new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName.Trim(), dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }
}
