<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PassWordRecovery.aspx.cs" Inherits="PassWordRecovery" Title="无标题页" %>
<%@ OutputCache Duration="9999999"  VaryByParam="None" Location="Server" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="sidebar">
    <div class="box">
    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server">        
        <MailDefinition BodyFileName="~/App_Data/PasswordRecoveryEmail.txt" 
            From="466989269@qq.com" Subject="在线考试系统，密码恢复">
        </MailDefinition>
    </asp:PasswordRecovery>
</div></div>
 <div id="content">
         <div class="box">
          <p>欢迎访问<b>在线人认证考试</b>系统，在线认证考试提供：</p>
          <ul><li>认证考试智能化，提供在线的认证考试</li>
              <li>轻松报考，注册登录，填写相关信息，提交个人相片，即可报考</li>
              <li>题库强大，计算机自动生成试卷，提供公正的考试</li>
              <li>轻松获取证书，相关考试通过后即可立即获取证书</li>
              <li>随时随地查询证书</li>
              <li>为各大小公司、学校、政府机构提供在线考试、人才测评服务</li>
              </ul>
              <br class="clearfix" /></div>
   </div>      
</asp:Content>

