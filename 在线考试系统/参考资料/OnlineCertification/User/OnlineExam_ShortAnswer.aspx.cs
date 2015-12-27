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

public partial class User_OnlineExam_ShortAnswer : System.Web.UI.Page
{
    object paperId;
    protected void Page_Load(object sender, EventArgs e)
    {
        paperId = Session["PaperId"];
        SqlDataSourceShortAnswer.SelectParameters["PaperId"].DefaultValue = paperId.ToString();


    }
}
