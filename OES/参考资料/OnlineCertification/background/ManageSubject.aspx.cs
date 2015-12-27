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

public partial class background_ManageSubject : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void LinkButtonInsert_Click(object sender, EventArgs e)
    {
       // SqlDataSourceRelatedCertifications.InsertParameters["SubjectID"].DefaultValue = gvAllSubject.SelectedDataKey.Value.ToString();
      //  SqlDataSourceRelatedCertifications.InsertParameters["CertificationID"].DefaultValue = ((DropDownList)gvRelatedCertification.FooterRow.FindControl("ddlCertification")).SelectedValue.ToString();
       // SqlDataSourceRelatedCertifications.Insert();
    }
  //  protected void LinkButton1_Click(object sender, EventArgs e)
    //{
       // FormView1.ChangeMode(FormViewMode.Insert);
    //}
    protected void gvAllSubject_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void FormViewSubjectDetail_PageIndexChanging(object sender, FormViewPageEventArgs e)
    {

    }
    protected void FormViewSubjectDetail_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        //Response.Redirect("");
        Response.Write("<script>window.location.href=window.location.href;</script>");
    }
    protected void linkNew_Click(object sender, EventArgs e)
    {
        FormViewSubjectDetail.ChangeMode(FormViewMode.Insert);
    }
}
