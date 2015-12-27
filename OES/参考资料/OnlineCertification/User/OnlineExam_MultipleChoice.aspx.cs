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

public partial class User_OnlineExam_MultipleChoice : System.Web.UI.Page
{
    object paperId;
    protected void Page_Load(object sender, EventArgs e)
    {
        paperId = Session["PaperId"];
        SqlDataSourceMultipleChoiceQuestion.SelectParameters["PaperId"].DefaultValue = paperId.ToString();
  

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e1)
    {
        CheckBox cb = (CheckBox)sender;
        DataListItem item = (DataListItem)cb.Parent.Parent.Parent;
        if (paperId != null)
        {
            long questionId = long.Parse(((Label)item.FindControl("questionId")).Text.ToString());
            bool a = ((CheckBox)(item.FindControl("cbA"))).Checked;
            bool b = ((CheckBox)(item.FindControl("cbB"))).Checked;
            bool c = ((CheckBox)(item.FindControl("cbC"))).Checked;
            bool d = ((CheckBox)(item.FindControl("cbD"))).Checked;
            bool e = ((CheckBox)(item.FindControl("cbE"))).Checked;
            ExamControl.AnswerMultipleChoiceQuestion(paperId, questionId, a, b, c, d, e);
        }

    }
}
