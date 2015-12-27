<%@ Page Language="C#" MasterPageFile="~/background/MasterPage.master" AutoEventWireup="true" CodeFile="RoleManagement.aspx.cs" Inherits="background_AuthorityManagement" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
    <div>
       
       
        <table>
              <tr>
                 <td>管理员：</td>
                  <td><asp:DropDownList ID="ddlAdmin" runat="server"></asp:DropDownList> </td>
                  <td> <asp:Button ID="btnAdminToTeacher" runat="server" Text="转到教师" 
                          onclick="btnAdminToTeacher_Click" /></td>
                  <td>
        <asp:Button ID="btnAdminToUser" runat="server" Text="转到一般用户" onclick="btnAdminToUser_Click" /></td></tr>
              <tr><td>教师：</td><td>
                  <asp:DropDownList ID="ddlTeacher" runat="server">
                  </asp:DropDownList>
              </td><td>
                  <asp:Button ID="btnTeacherToAdmin" runat="server" Text="转到管理员" 
                          onclick="btnTeacherToAdmin_Click" /></td><td>
                      <asp:Button ID="btnTeacherToUser" runat="server" Text="转到一般用户" 
                          onclick="btnTeacherToUser_Click" /></td></tr>
                      <tr><td>用户名：</td><td>
                          <asp:TextBox ID="tbName" runat="server"></asp:TextBox></td><td>
                              <asp:Button ID="btnUserToTeacher" runat="server" Text="转到教师" 
                                  onclick="btnUserToTeacher_Click" /></td><td>
                              <asp:Button ID="btnUserToAdmin" runat="server" Text="转到管理员" 
                                  onclick="btnUserToAdmin_Click" style="height: 26px" />
                          </td></tr>
        </table>
    </div>
    <div></div>
    <div></div>
</div>
</asp:Content>

