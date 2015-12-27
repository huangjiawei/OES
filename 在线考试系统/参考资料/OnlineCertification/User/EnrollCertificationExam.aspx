<%@ Page Language="C#" MasterPageFile="~/User/UserPage.master" AutoEventWireup="true" CodeFile="EnrollCertificationExam.aspx.cs" Inherits="User_EnrollCertificationExam" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div>
    <div>
           <span>请选择要报考的认证考试：</span><br />
         认证分类：<asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" 
               DataSourceID="SqlDataSourceCategory" DataTextField="Name" DataValueField="ID">
         </asp:DropDownList>
         认证考试：<asp:DropDownList ID="ddlCertification" runat="server" 
               DataSourceID="SqlDataSourceCertification" DataTextField="Name" 
               DataValueField="ID">
         </asp:DropDownList> 
         <asp:Button ID="btnEnroll" runat="server" Text="一键报考" onclick="btnEnroll_Click" />        
     
           <br />
           <asp:Label ID="lbStatus" runat="server"></asp:Label>
     
           <asp:SqlDataSource ID="SqlDataSourceCategory" runat="server"  EnableCaching="true" CacheDuration="Infinite" SqlCacheDependency="CommandNotification"
               ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
               SelectCommand="SELECT Name, ID, info FROM CertificationCategory"></asp:SqlDataSource>
           <asp:SqlDataSource ID="SqlDataSourceCertification" runat="server"  EnableCaching="true" CacheDuration="Infinite" SqlCacheDependency="CommandNotification"
               ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
               
               SelectCommand="SELECT [ID], [Name] FROM [ProfessionalCertification] WHERE ([CategoryID] = @CategoryID)">
               <SelectParameters>
                   <asp:ControlParameter ControlID="ddlCategory" Name="CategoryID" 
                       PropertyName="SelectedValue" Type="Int32" />
               </SelectParameters>
           </asp:SqlDataSource>
     
     </div>
  </div>
</asp:Content>

