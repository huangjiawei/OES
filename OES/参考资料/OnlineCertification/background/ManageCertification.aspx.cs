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

public partial class background_Certification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void gvCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
  
         /***
          * protected void gvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Control c = e.Row.Cells[1].Controls[0];
            if (c is LinkButton)
            {
                ((LinkButton)c).OnClientClick =
                    "javascript:return confirm('确定要删除吗');";
            
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                Control c = e.Row.Cells[0].Controls[2];
                if (c is LinkButton)
                {

                    ((LinkButton)e.Row.Cells[0].Controls[2]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除：" + e.Row.Cells[1].Text.Trim() + "吗?')");
                }
            }
        }}
          * **/
    
    protected void GridViewSubject_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        SqlDataSourceCategory.InsertParameters["Name"].DefaultValue = 
            ((TextBox)gvCategory.FooterRow.FindControl("txtName")).Text;
        SqlDataSourceCategory.InsertParameters["info"].DefaultValue=
            ((TextBox)gvCategory.FooterRow.FindControl("txtInfo")).Text;
        SqlDataSourceCategory.Insert();
    }
    protected void linkBtnInsert_Click(object sender, EventArgs e)
    {
        SqlDataSourceCertification.InsertParameters["Name"].DefaultValue = ((TextBox)gvCertification.FooterRow.FindControl("tbName")).Text;
        SqlDataSourceCertification.InsertParameters["info"].DefaultValue = ((TextBox)gvCertification.FooterRow.FindControl("tbInfo")).Text;
        SqlDataSourceCertification.InsertParameters["CategoryID"].DefaultValue= gvCategory.SelectedDataKey.Value.ToString();
        SqlDataSourceCertification.Insert();
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        SqlDataSourceCertification.InsertParameters["info"].DefaultValue =
            ((TextBox)gvCertification.Controls[0].Controls[0].FindControl("newInfo")).Text;  //Controls[0].Controls[0]导航到EmptyDataTemplate中
        SqlDataSourceCertification.InsertParameters["Name"].DefaultValue = 
            ((TextBox)gvCertification.Controls[0].Controls[0].FindControl("newName")).Text;
        SqlDataSourceCertification.InsertParameters["CategoryID"].DefaultValue =
            ((DropDownList)gvCertification.Controls[0].Controls[0].FindControl("newCategory")).Text;
        SqlDataSourceCertification.Insert();
    }
    //添加科目到认证考试中
    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        SqlDataSourceSubject.InsertParameters["CertificationID"].DefaultValue = gvCertification.SelectedDataKey.Value.ToString();
        SqlDataSourceSubject.InsertParameters["SubjectID"].DefaultValue = ((DropDownList)gvSubject.FooterRow.FindControl("ddlSubject")).SelectedValue.ToString();
        SqlDataSourceSubject.Insert();
        
    }
    //添加科目到认证考试中
    protected void lbtnNew_Click(object sender, EventArgs e)
    {
        SqlDataSourceSubject.InsertParameters["CertificationID"].DefaultValue = gvCertification.SelectedDataKey.Value.ToString();
        SqlDataSourceSubject.InsertParameters["SubjectID"].DefaultValue = ((DropDownList)gvSubject.Controls[0].Controls[0].FindControl("ddlSubject")).SelectedValue.ToString();
        SqlDataSourceSubject.Insert();
    }
    protected void gvCategory_SelectedIndexChanged1(object sender, EventArgs e)
    {
        gvCertification.Visible = true;
        planeCertification.Visible = true;
        
    }
    protected void gvCertification_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvSubject.Visible = true;
        planeSubject.Visible = true;
    }
}
