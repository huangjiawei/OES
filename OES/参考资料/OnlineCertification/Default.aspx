<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="无标题页" %>
<%@ OutputCache Duration="999999" VaryByParam="*" Location="Client"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       
    <div id="sidebar">
         <div class="box"><asp:Login ID="Login1" runat="server" CreateUserText="注册" 
            CreateUserUrl="~/Register.aspx" PasswordRecoveryText="忘记密码" 
            PasswordRecoveryUrl="~/PassWordRecovery.aspx">
        </asp:Login></div>	<br class="clearfix" /></div>   
    
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

