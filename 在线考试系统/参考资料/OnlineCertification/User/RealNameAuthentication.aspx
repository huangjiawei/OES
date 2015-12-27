<%@ Page Language="C#" MasterPageFile="~/User/UserPage.master" AutoEventWireup="true" CodeFile="RealNameAuthentication.aspx.cs" Inherits="User_RealNameAuthentication" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
          
         <table>
                 <tr><td>
                     
                     身份证号</td><td colspan="2">
                     <asp:TextBox ID="tbIdCardNo" runat="server"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="tbIdCardNo_FilteredTextBoxExtender" 
                                            runat="server" TargetControlID="tbIdCardNo" FilterType="Numbers">
                                        </asp:FilteredTextBoxExtender>
                                    </td></tr>
                 <tr><td>姓名</td><td>
                     <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
                     
                                    <asp:FilteredTextBoxExtender ID="tbName_FilteredTextBoxExtender" runat="server" 
                                        TargetControlID="tbName" FilterMode="InvalidChars" FilterType="Numbers" >
                                    </asp:FilteredTextBoxExtender>
                     
                     </td><td>性别</td><td>
                     <asp:RadioButtonList ID="RadioButtonListSex" runat="server" 
                         RepeatDirection="Horizontal">
                         <asp:ListItem Value="True">男</asp:ListItem>
                         <asp:ListItem Value="False">女</asp:ListItem>
                     </asp:RadioButtonList>
                     </td></tr>
                  <tr><td>籍贯</td><td>
                      <asp:TextBox ID="tbBirthPlace" runat="server" Width="128px"></asp:TextBox>
                      <asp:FilteredTextBoxExtender ID="tbBirthPlace_FilteredTextBoxExtender" 
                          runat="server" Enabled="True" TargetControlID="tbBirthPlace" FilterMode="InvalidChars" FilterType="Numbers" >
                      </asp:FilteredTextBoxExtender>
                      </td><td>电话</td><td>
                          <asp:TextBox ID="tbPhone" runat="server"></asp:TextBox>
                                    
                                    <asp:FilteredTextBoxExtender ID="tbPhone_FilteredTextBoxExtender" 
                                        runat="server" TargetControlID="tbPhone" FilterMode="ValidChars" 
                                         FilterType="Numbers">
                                    </asp:FilteredTextBoxExtender>
                                    
                                    </td></tr>
                          
                   <tr><td>出生日期</td><td>
                       <asp:TextBox ID="tbBirthDay" runat="server"></asp:TextBox>
                       
                                    <asp:CalendarExtender ID="tbBirthDay_CalendarExtender" runat="server" 
                                        Enabled="True" TargetControlID="tbBirthDay" ClearTime="true" 
                                        Format="yyyy-MM-dd" >
                                    </asp:CalendarExtender>
                       
                   </td><td>邮箱</td><td>
                       <asp:TextBox ID="tbEmail" runat="server" Enabled="false"></asp:TextBox></td></tr>
                       <tr><td>住址</td><td colspan="3">
                              <asp:TextBox ID="tbAddress" runat="server" Width="316px"></asp:TextBox>
                              
                              </td></tr>
                   <tr><td>照片</td>
                   <td>
                   <table><tr><td>
                       <asp:Image ID="imgPhoto" runat="server" AlternateText="未上传" BackColor="#666666" 
                           BorderStyle="Double" Height="132px" Width="105px"   /></td></tr><tr><td><asp:FileUpload ID="FileUpload1" runat="server" /></td></tr><tr><td>  
                       <asp:Button ID="Button1" runat="server" Text="上传" onclick="Button1_Click" /></td>
                             </tr></table>
                   
                 
                       </td></tr>
                       <tr><td colspan="2"></td><td colspan="2">
                           
                           </td>
                       </tr>
                       <tr><td>
                           <asp:Button ID="btnSave" runat="server" Text="申请认证" onclick="btnSave_Click" /></td><td>
                               <asp:Label ID="lbStatus" runat="server" ForeColor="Red"></asp:Label></td></tr>
                       
           </table>
           
    </div>
    
</asp:Content>

