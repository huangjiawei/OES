<%@ Page Language="C#" MasterPageFile="~/background/MasterPage.master" AutoEventWireup="true" CodeFile="RealName_Censor.aspx.cs" Inherits="background_RealName_Censor" Title="�ޱ���ҳ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         <div>����ʵ����֤�Ŀ�����</div>
         <div>
             <asp:ObjectDataSource ID="ObjectDataSourceUserProfile" runat="server" 
                 SelectMethod="GetProfiles" TypeName="UserProfile"></asp:ObjectDataSource>
             <asp:GridView ID="GridViewAuthorize" runat="server" AutoGenerateColumns="False" 
                 DataSourceID="ObjectDataSourceUserProfile" 
                 onrowcommand="GridViewAuthorize_RowCommand">
                 <Columns>
                     <asp:TemplateField ShowHeader="False">
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButtonAuthorize" runat="server" 
                             CommandArgument='<%# Eval("UserName") %>' CausesValidation="false" 
                                 CommandName="Authorize" Text="��Ȩ"></asp:LinkButton>
                             
                             <asp:LinkButton ID="LinkButton1"  CommandName="Deny"  CommandArgument='<%# Eval("UserName") %>' runat="server">�ܾ�</asp:LinkButton>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:BoundField DataField="UserName"  HeaderText="�û���" ReadOnly="True" 
                         SortExpression="UserName" />
                     <asp:BoundField DataField="IdCardNo" HeaderText="���֤��" ReadOnly="True" 
                         SortExpression="IdCardNo" />
                     <asp:BoundField DataField="RealName" HeaderText="����" ReadOnly="True" 
                         SortExpression="RealName" />
                     <asp:BoundField DataField="Sex" HeaderText="�Ա�" ReadOnly="True" 
                         SortExpression="Sex" />
                     <asp:BoundField DataField="BirthPlace" HeaderText="����" ReadOnly="True" 
                         SortExpression="BirthPlace" />
                     <asp:BoundField DataField="BirthDay" HeaderText="����" ReadOnly="True" 
                         SortExpression="BirthDay" />
                     <asp:BoundField DataField="Telephone" HeaderText="�绰����" ReadOnly="True" 
                         SortExpression="Telephone" />
                     <asp:BoundField DataField="Address" HeaderText="��ַ" ReadOnly="True" 
                         SortExpression="Address" />
                     <asp:ImageField DataImageUrlField="UserName" 
                         DataImageUrlFormatString="~/User/UserPhoto.aspx?UserName={0}" HeaderText="ͷ��">
                         <ItemStyle Height="100px" Width="75px" />
                     </asp:ImageField>
                 </Columns>
             </asp:GridView>
</div>
         <div>   </div>
        
</asp:Content>

