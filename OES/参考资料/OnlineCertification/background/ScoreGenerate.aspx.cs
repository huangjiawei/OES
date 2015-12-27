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

public partial class background_ScoreGenerate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        GridViewRow r = ((LinkButton)sender).Parent.Parent as GridViewRow;
        object papreId = GridView1.DataKeys[r.RowIndex].Value;
        int score;
        ExamControl.GenerateSubjectiveItemScore(papreId, out score);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        GridViewRow r = ((LinkButton)sender).Parent.Parent as GridViewRow;
        object papreId = GridView1.DataKeys[r.RowIndex].Value;
        int score = ExamControl.GenerateObjectiveItemScore(papreId);
    }
}
