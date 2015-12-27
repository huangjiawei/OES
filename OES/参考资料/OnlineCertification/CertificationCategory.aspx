<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CertificationCategory.aspx.cs" Inherits="CertificationCategory" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css" runat="server">
    a
    {
        text-decoration: none;
        
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="sidebar">
        <div class="box">
        <p>分类：</p>
            <asp:DataList runat="server" ID="DataListCategory" DataSourceID="SqlDataSourceCategory" DataKeyField="ID">
                <ItemTemplate>
                     <asp:LinkButton ID="link" runat="server" Text='<%# Eval("Name") %>' 
                         OnClick="linbtn_Click"  CommandArgument='<%# Eval("ID")%>' ForeColor="Blue"></asp:LinkButton>
                </ItemTemplate>
            </asp:DataList>
            <asp:SqlDataSource ID="SqlDataSourceCategory" runat="server"  EnableCaching="true" SqlCacheDependency="CommandNotification" CacheDuration="Infinite" 
                ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
                SelectCommand="SELECT [Name], [info], [ID] FROM [CertificationCategory]">
            </asp:SqlDataSource>
            
            </div>
            <div class="box">
               
            </div>
    
        <br class="clearfix" />
     </div>
    <div id="content">
             <div  class="box">
           <asp:SqlDataSource ID="SqlDataSourceCertification" runat="server"   EnableCaching="true" SqlCacheDependency="CommandNotification" CacheDuration="Infinite"
                ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
                SelectCommand="SELECT [info], [Name], [ID] FROM [ProfessionalCertification] WHERE ([CategoryID] = @CategoryID)">
                <SelectParameters>
                    <asp:Parameter Name="CategoryID" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:DataList ID="DataList1" runat="server" DataKeyField="ID" 
                DataSourceID="SqlDataSourceCertification" 
                >
                <ItemTemplate>
                    <h2><asp:LinkButton ID="linBtnCert" runat="server" Text='<%# Eval("Name") %>'
                     CommandArgument='<%# Eval("ID") %>' ForeColor="#0033CC" /></h2>
                   <h5>
                    <asp:Label ID="infoLabel" runat="server" Text='<%# Eval("info") %>' 
                        BorderColor="#6600FF" /></h5>
                    <br />
                    <br />
                </ItemTemplate>
            </asp:DataList>
      
            
        </div>
          <br  class="clearfix"/>          
    </div>

</asp:Content>

