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

public partial class CertificationCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void RepeaterCertificationCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Click")
        {
            //int categoryId = Convert.ToInt32(e.CommandArgument);
            SqlDataSourceCertification.SelectParameters[0].DefaultValue = e.CommandArgument.ToString();
           // SqlDataSourceCertification.DataBind();


        }
    }

 
    protected void DataList2_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "Click")
        {
            //int categoryId = Convert.ToInt32(e.CommandArgument);
            SqlDataSourceCertification.SelectParameters[0].DefaultValue = e.CommandArgument.ToString();
            // SqlDataSourceCertification.DataBind();


        }
    }
    protected void linbtn_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)(sender);
        SqlDataSourceCertification.SelectParameters[0].DefaultValue = lb.CommandArgument.ToString();
        SqlDataSourceCertification.DataBind();
    }
  
}
