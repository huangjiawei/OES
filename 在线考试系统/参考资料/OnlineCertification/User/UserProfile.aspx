<%@ Page Language="C#" MasterPageFile="~/User/UserPage.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="User_UserProfile" Title="无标题页" %>
<%@ OutputCache Duration="99999" VaryByParam="*" Location="Client" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager> <div>
          
         <table>
                 <tr><td>                     
                     身份证号:</td><td colspan="2">
                     <asp:Label ID="tbIdCardNo" runat="server"></asp:Label>
                                    </td></tr>
                 <tr><td>姓名:</td><td>
                     <asp:Label ID="tbName" runat="server"></asp:Label>                     
                                   
                     
                     </td><td>性别:</td><td>
                     <asp:Label ID="lbSex" runat="server" Text=""></asp:Label>
                     </td></tr>
                  <tr><td>籍贯:</td><td>
                      <asp:Label ID="tbBirthPlace" runat="server" Width="128px"></asp:Label>
                      
                      </td><td>电话:</td><td>
                          <asp:TextBox ID="tbPhone" runat="server"></asp:TextBox>
                                    
                                    <asp:FilteredTextBoxExtender ID="tbPhone_FilteredTextBoxExtender" 
                                        runat="server" TargetControlID="tbPhone" FilterMode="ValidChars" 
                                         FilterType="Numbers">
                                    </asp:FilteredTextBoxExtender>
                                    
                                    </td></tr>
                          
                   <tr><td>出生日期</td><td>
                       <asp:Label ID="tbBirthDay" runat="server"></asp:Label>                      
                       
                   </td><td>邮箱</td><td>
                       <asp:Label ID="tbEmail" runat="server"  Text=""></asp:Label></td></tr>
                       <tr><td>住址:</td><td colspan="3">
                              <asp:Label ID="tbAddress" runat="server" Width="316px"></asp:Label>
                              
                              </td></tr>
                   <tr><td>照片:</td>
                   <td>
                   <table><tr><td>
                       <asp:Image ID="imgPhoto" runat="server" AlternateText="未上传" BackColor="#666666" 
                         /></td>
                             </tr></table>
                   
                 
                       </td></tr>
                       <tr><td colspan="2"></td><td colspan="2">
                           
                           </td>
                       </tr>
                       <tr><td>
                           <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click" /></td></tr>
                       
           </table>
           
    </div>
    <div>
    </div>
</asp:Content>

