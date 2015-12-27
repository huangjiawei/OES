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

public partial class User_OnlineExam_SingleChoice : System.Web.UI.Page
{
    object paperId;
    protected void Page_Load(object sender, EventArgs e)
    {
        paperId = Session["PaperId"];
        SqlDataSourceSingleChoiceQuestion.SelectParameters["PaperId"].DefaultValue = paperId.ToString();
         

    }
    protected void rbA_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton cb = (RadioButton)sender;
        if (!cb.Checked) return;
        DataListItem item = (DataListItem)cb.Parent.Parent.Parent;
        //上传答案
        if (paperId != null)
        {
            long questionId = long.Parse(((Label)item.FindControl("questionId")).Text.ToString());
            bool a = ((RadioButton)(item.FindControl("rbA"))).Checked;
            bool b = ((RadioButton)(item.FindControl("rbB"))).Checked;
            bool c = ((RadioButton)(item.FindControl("rbC"))).Checked;
            bool d = ((RadioButton)(item.FindControl("rbD"))).Checked;
            ExamControl.AnswerSingleChoice(paperId, questionId, a, b, c, d);
        }

    }
    
}
