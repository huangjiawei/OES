<%@ Page Language="C#" MasterPageFile="~/background/MasterPage.master" AutoEventWireup="true" CodeFile="RealName_Censor.aspx.cs" Inherits="background_RealName_Censor" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         <div>申请实名认证的考生：</div>
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
                                 CommandName="Authorize" Text="授权"></asp:LinkButton>
                             
                             <asp:LinkButton ID="LinkButton1"  CommandName="Deny"  CommandArgument='<%# Eval("UserName") %>' runat="server">拒绝</asp:LinkButton>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:BoundField DataField="UserName"  HeaderText="用户名" ReadOnly="True" 
                         SortExpression="UserName" />
                     <asp:BoundField DataField="IdCardNo" HeaderText="身份证号" ReadOnly="True" 
                         SortExpression="IdCardNo" />
                     <asp:BoundField DataField="RealName" HeaderText="姓名" ReadOnly="True" 
                         SortExpression="RealName" />
                     <asp:BoundField DataField="Sex" HeaderText="性别" ReadOnly="True" 
                         SortExpression="Sex" />
                     <asp:BoundField DataField="BirthPlace" HeaderText="籍贯" ReadOnly="True" 
                         SortExpression="BirthPlace" />
                     <asp:BoundField DataField="BirthDay" HeaderText="生日" ReadOnly="True" 
                         SortExpression="BirthDay" />
                     <asp:BoundField DataField="Telephone" HeaderText="电话号码" ReadOnly="True" 
                         SortExpression="Telephone" />
                     <asp:BoundField DataField="Address" HeaderText="地址" ReadOnly="True" 
                         SortExpression="Address" />
                     <asp:ImageField DataImageUrlField="UserName" 
                         DataImageUrlFormatString="~/User/UserPhoto.aspx?UserName={0}" HeaderText="头像">
                         <ItemStyle Height="100px" Width="75px" />
                     </asp:ImageField>
                 </Columns>
             </asp:GridView>
</div>
         <div>   </div>
        
</asp:Content>

