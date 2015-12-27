<%@ Page Language="C#" MasterPageFile="~/User/UserPage.master" AutoEventWireup="true" CodeFile="SubjectPass.aspx.cs" Inherits="User_SubjectPass" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<div>
   <div>已通过科目</div>
   <div>
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
           DataKeyNames="ID" DataSourceID="SqlDataSourceSubjectPass">
           <Columns>
               <asp:BoundField DataField="Name" HeaderText="科目" SortExpression="Name" />
               <asp:BoundField DataField="Score" HeaderText="成绩" SortExpression="Score" />
           </Columns>
           <EmptyDataTemplate>
               未通过任何科目
           </EmptyDataTemplate>
       </asp:GridView>
       <asp:SqlDataSource ID="SqlDataSourceSubjectPass" runat="server"  EnableCaching="true" CacheDuration="3000000"  SqlCacheDependency="CommandNotification"
           ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
           SelectCommand="SELECT [Name], [Score], [ID], [UserId] FROM [dbo].[View_SubjectPass] 
where UserId=@ID">
           <SelectParameters>
               <asp:Parameter Name="ID" />
           </SelectParameters>
          
       </asp:SqlDataSource>
    </div>
   <div></div>

</div>
</asp:Content>

