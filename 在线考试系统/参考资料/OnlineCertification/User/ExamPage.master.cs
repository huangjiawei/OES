using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class User_ExamPage : System.Web.UI.MasterPage
{
 
    private object paperId;
    public object PaperID
    {
        get { return paperId; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //获取用户Id
                object userid = Membership.GetUser().ProviderUserKey;
                if (Session["SubjectID"] == null)
                {
                    Response.Redirect("~/User/ExamEnrolled.aspx");
                }
                int subjectId = int.Parse(Session["SubjectID"].ToString());
                SubjectQuestionQuantity sqq = ExamControl.GetSubjectQuestionQuantity(subjectId);
                //检查是否开考
                if (sqq.isReady)
                {
                    lbSubject.Text = sqq.Name;
                    /* string detail="试题中包含:";
                     if(sqq.SingleChoice>0)
                     detail += sqq.SingleChoice+"题单选题;";
                     if (sqq.MultipleChoice > 0)
                         detail += "";
                     lbSubjectDetail.Text = detail;*/
                    lbSubjectDetail.Text = "单选题x" + sqq.SingleChoice.ToString() + "；多选题x" + sqq.MultipleChoice.ToString()
                        + "；填空题x" + sqq.Filling.ToString() + "；简答题x" + sqq.ShortAnswer.ToString() + "；议论题x" + sqq.Discussion.ToString();
                    // object paperId;

                    //检查是否已经存在试卷
                    if (Session["UserName"] != null
                        && Session["UserName"].ToString() == HttpContext.Current.User.Identity.Name
                        && Session["PaperId"] != null)
                    {
                        paperId = (object)Session["PaperId"];


                    }
                    else
                    {

                        //生成试卷；
                        paperId = ExamControl.GenerationExamPaper(subjectId, userid);
                        Session["PaperId"] = paperId.ToString();
                        //试卷Id考试完后就过期，考试时间为120分钟
                        // Response.Cookies["PaperId"].Expires = DateTime.Now.AddMinutes(121);
                        Session["UserName"] = HttpContext.Current.User.Identity.Name;
                        // Response.Cookies["UserName"].Expires = DateTime.Now.AddMinutes(121);
                        //设置考试时间
                        Session["TimeOut"] = DateTime.Now.AddHours(2);
                    }
                    //show time
                    lbTime.Text = "剩余时间: " + ((Int32)DateTime.Parse(Session["TimeOut"].ToString()).Subtract(DateTime.Now).TotalMinutes).ToString() + "分钟";

                    //显示试卷
                    //SqlDataSourceQuestionSingleChoice.SelectParameters["PaperId"].DefaultValue = paperId.ToString();
               //     SqlDataSourceSingleChoiceQuestion.SelectParameters["PaperId"].DefaultValue = paperId.ToString();
                 //   SqlDataSourceMultipleChoiceQuestion.SelectParameters["PaperId"].DefaultValue = paperId.ToString();
                 //   SqlDataSourceCompletion.SelectParameters["PaperId"].DefaultValue = paperId.ToString();
                 //   SqlDataSourceShortAnswer.SelectParameters["PaperId"].DefaultValue = paperId.ToString();
                 //   SqlDataSourceDiscussion.SelectParameters["PaperId"].DefaultValue = paperId.ToString();
                }
                else
                {
                    //为开考
                }


            }
            else
            {
                //未验证用户
            }

        }

    }


    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    //定时更新时间与到点交卷
    protected void TimerExam_Tick(object sender, EventArgs e)
    {
        //DateTime timeOut=(DateTime)Session["TimeOut"];
        if (Session["TimeOut"] != null && 0 > DateTime.Compare(DateTime.Now,
        DateTime.Parse(Session["TimeOut"].ToString())))
        {
            lbTime.Text = "剩余时间: " +
           ((Int32)DateTime.Parse(Session["TimeOut"].ToString()).Subtract(DateTime.Now).TotalMinutes).ToString() + "分钟";
        }
        else
        {

            object paperId = (object)(Session["PaperId"]);
            //生成成绩
            int objectiveScore = ExamControl.GenerateObjectiveItemScore(paperId);
            Session.Clear();
            //转到考试完成页面
            Response.Redirect("~/User/FinishExam.aspx?Score=" + objectiveScore);

        }
    }
    protected void btnSumit_Click(object sender, EventArgs e)
    {
        object paperId = (object)(Session["PaperId"]);
        //生成成绩
        int objectiveScore = ExamControl.GenerateObjectiveItemScore(paperId);
        Session.Clear();
        //转到考试完成页面
        Response.Redirect("~/User/FinishExam.aspx?Score=" + objectiveScore);

    }
 
}
