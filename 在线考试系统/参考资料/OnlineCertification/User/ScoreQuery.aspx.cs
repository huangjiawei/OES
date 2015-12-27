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

public partial class User_ScoreQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataSourceExamTakenRecently.SelectParameters[0].DefaultValue = Membership.GetUser().ProviderUserKey.ToString();
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        GridViewRow r = ((Button)sender).Parent.Parent as GridViewRow;
        object papreId = gvExamTaken.DataKeys[r.RowIndex].Value;
        int score;
        ExamControl.GenerateSubjectiveItemScore(papreId,out score);
        if (score >= 0)lbScore.Text = "考试成绩为：" + score.ToString();
        else lbScore.Text = "未完成试卷评分";
       
    }

    protected void gvExamTaken_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}
