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

public partial class CertificateQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //证书查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {

        if (tbID.Text != null && tbID.Text.Trim() != "")
        {
            if (tbName.Text != null && tbName.Text.Trim() != "")
            {
                SqlDataSourceCertificate.DataBind();
                Label lbName = FormView1.FindControl("ownerNameLabel") as Label;
                if (lbName != null && lbName.Text == tbName.Text)
                {
                   
                    FormView1.Visible = true; 
                    lbStatus.Text = "查询成功";
                }
                else
                {
                    lbStatus.Text = "证书编号或姓名信息不正确";
                    FormView1.Visible = false;
                }
             
            }
            else
            {
                lbStatus.Text = "请输入证书持有人姓名";
                FormView1.Visible = false;
            }
        }
        else
        {
            lbStatus.Text = "请输入证书编号";
            FormView1.Visible = false;
        }
       
    }
}
