<%@ Page Language="C#" MasterPageFile="~/User/UserPage.master" AutoEventWireup="true" CodeFile="UserCenter.aspx.cs" Inherits="UserCenter" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div><h1>欢迎使用认证考试系统，相关功能请点击左边导航栏。</h1></div>
    <div>需要注销吗？点击这里<asp:LinkButton ID="lbtnLogout" runat="server" 
            onclick="lbtnLogout_Click">注销</asp:LinkButton></div>
</asp:Content>

