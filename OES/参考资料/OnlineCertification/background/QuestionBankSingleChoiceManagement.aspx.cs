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

public partial class background_SingleChoiceQuestionBank : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void gvSingleQuestionsInSubject_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
 
    protected void fvSelectedQuestion_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        //Response.Write("<script language=javascript>window.location.href=document.URL;</script>" ); 
    }
    protected void fvSelectedQuestion_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        //Response.Write("<script language=javascript>window.location.href=document.URL;</script>"); 
    }
    protected void fvSelectedQuestion_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
       // Response.Write("<script language=javascript>window.location.href=document.URL;</script>"); 
    }
}
