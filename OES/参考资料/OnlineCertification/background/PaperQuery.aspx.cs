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

public partial class background_PaperQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBox2.Text = "今天";
        }

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
       
        string d2;
        if (TextBox2.Text == "" || TextBox2.Text == "今天")
            d2 = DateTime.Now.ToString();
        else d2 = TextBox1.Text;
        SqlDataSourcePaper.SelectParameters["date1"].DefaultValue = TextBox1.Text;
        SqlDataSourcePaper.SelectParameters["date2"].DefaultValue=d2;
        SqlDataSourcePaper.SelectParameters["subjectId"].DefaultValue = DropDownList1.SelectedValue;
       
        
    }
}
