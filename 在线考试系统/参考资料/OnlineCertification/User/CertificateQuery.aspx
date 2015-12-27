<%@ Page Language="C#" MasterPageFile="~/User/UserPage.master" AutoEventWireup="true" CodeFile="CertificateQuery.aspx.cs" Inherits="User_CertificateQuery" Title="无标题页" %>

<%@ Register src="CertificateGenerate.ascx" tagname="CertificateGenerate" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div>
     <div>已通过认证</div>
     <div>
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
             DataSourceID="sqlPass" DataKeyNames="CertificateID">
             <Columns>
                 <asp:BoundField DataField="CertificateID" HeaderText="CertificateID" 
                     ReadOnly="True" SortExpression="CertificateID" />
                 <asp:BoundField DataField="CertificateName" HeaderText="CertificateName" 
                     SortExpression="CertificateName" />
                 <asp:BoundField DataField="ownerName" HeaderText="ownerName" 
                     SortExpression="ownerName" />
                 <asp:BoundField DataField="ownerID" HeaderText="ownerID" 
                     SortExpression="ownerID" />
                 <asp:BoundField DataField="generatedate" HeaderText="generatedate" 
                     SortExpression="generatedate" />
                 <asp:TemplateField>                   
                     <ItemTemplate>
                         <asp:Image ID="Image1" runat="server"  Width="100px"
                             ImageUrl='<%# Eval("CertificateID", "~/User/PhotoInCertificate.aspx?CertificateID={0}") %>' />
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
             <EmptyDataTemplate>
                 尚未通过获取任何认证考试
             </EmptyDataTemplate>
         </asp:GridView>
         <asp:SqlDataSource ID="sqlPass" runat="server"  EnableCaching="true" CacheDuration="Infinite" SqlCacheDependency="CommandNotification"
             ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
             
             
             SelectCommand="GetUserCertificates" SelectCommandType="StoredProcedure">
             <SelectParameters>
                 <asp:Parameter Name="ID" />
             </SelectParameters>
         </asp:SqlDataSource>
     </div>
     <div>
        
         <uc1:CertificateGenerate ID="CertificateGenerate1" runat="server" />
        
     </div>
</div>
</asp:Content>

