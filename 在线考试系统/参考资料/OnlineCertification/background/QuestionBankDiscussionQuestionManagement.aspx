<%@ Page Language="C#" MasterPageFile="~/background/MasterPage.master" AutoEventWireup="true" CodeFile="QuestionBankDiscussionQuestionManagement.aspx.cs" Inherits="background_QuestionBankDiscussionQuestionManagement" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <div><h3>议论题/应用题</h3></div>
            <div><span>科目(未开考)：<asp:DropDownList ID="DropDownListSubject" runat="server" 
           DataSourceID="SqlDataSourceSubject" DataTextField="Name" DataValueField="ID" 
                AutoPostBack="True">
       </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSourceSubject" runat="server" 
        ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
        SelectCommand="SELECT [ID], [Name] FROM [TestSubjects] WHERE ([isReady] = 0)">
    </asp:SqlDataSource>
                <asp:ListView ID="ListView1" runat="server" DataKeyNames="ID" 
                    DataSourceID="SqlDataSourceQuestionInSubject" InsertItemPosition="LastItem">
                    <ItemTemplate>
                        <tr style="background-color:#DCDCDC;color: #000000;">
                            <td>
                                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" />
                                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                            </td>
                            <td>
                                <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                            <td>
                                <asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' />
                            </td>
                            <td>
                                <asp:Label ID="answerLabel" runat="server" Text='<%# Eval("answer") %>' />
                            </td>
                            <td>
                                <asp:Label ID="infoLabel" runat="server" Text='<%# Eval("info") %>' />
                            </td>
                            <td>
                                <asp:Label ID="DateAddedLabel" runat="server" Text='<%# Eval("DateAdded") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:#FFF8DC;">
                            <td>
                                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" />
                                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                            </td>
                            <td>
                                <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                            <td>
                                <asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' />
                            </td>
                            <td>
                                <asp:Label ID="answerLabel" runat="server" Text='<%# Eval("answer") %>' />
                            </td>
                            <td>
                                <asp:Label ID="infoLabel" runat="server" Text='<%# Eval("info") %>' />
                            </td>
                            <td>
                                <asp:Label ID="DateAddedLabel" runat="server" Text='<%# Eval("DateAdded") %>' />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table runat="server" 
                            style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                            <tr>
                                <td>
                                    未返回数据。</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <InsertItemTemplate>
                        <tr style="">
                            <td>
                                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="插入" />
                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="清除" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:TextBox ID="QuestionTextBox" runat="server" 
                                    Text='<%# Bind("Question") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="answerTextBox" runat="server" Text='<%# Bind("answer") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="infoTextBox" runat="server" Text='<%# Bind("info") %>' />
                            </td>
                            
                        </tr>
                    </InsertItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                        style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                        <tr runat="server" style="background-color:#DCDCDC;color: #000000;">
                                            <th runat="server">
                                            </th>
                                            <th runat="server">
                                                ID</th>
                                            <th runat="server">
                                                Question</th>
                                            <th runat="server">
                                                answer</th>
                                            <th runat="server">
                                                info</th>
                                            <th runat="server">
                                                DateAdded</th>
                                        </tr>
                                        <tr ID="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" 
                                    style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                                    <asp:DataPager ID="DataPager1" runat="server">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                                ShowLastPageButton="True" />
                                        </Fields>
                                    </asp:DataPager>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <EditItemTemplate>
                        <tr style="background-color:#008A8C;color: #FFFFFF;">
                            <td>
                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="更新" />
                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="取消" />
                            </td>
                            <td>
                                <asp:Label ID="IDLabel1" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="QuestionTextBox" runat="server" 
                                    Text='<%# Bind("Question") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="answerTextBox" runat="server" Text='<%# Bind("answer") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="infoTextBox" runat="server" Text='<%# Bind("info") %>' />
                            </td>
                           
                        </tr>
                    </EditItemTemplate>
                    <SelectedItemTemplate>
                        <tr style="background-color:#008A8C;font-weight: bold;color: #FFFFFF;">
                            <td>
                                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" />
                                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                            </td>
                            <td>
                                <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                            <td>
                                <asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' />
                            </td>
                            <td>
                                <asp:Label ID="answerLabel" runat="server" Text='<%# Eval("answer") %>' />
                            </td>
                            <td>
                                <asp:Label ID="infoLabel" runat="server" Text='<%# Eval("info") %>' />
                            </td>
                            <td>
                                <asp:Label ID="DateAddedLabel" runat="server" Text='<%# Eval("DateAdded") %>' />
                            </td>
                        </tr>
                    </SelectedItemTemplate>
                </asp:ListView>
       </span>
        </div>
          <div>
              <asp:SqlDataSource ID="SqlDataSourceQuestionInSubject" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
                  DeleteCommand="DELETE FROM [QuestionDiscussion] WHERE [ID] = @original_ID" 
                  InsertCommand="AddDiscussionQuestion" InsertCommandType="StoredProcedure" 
                  OldValuesParameterFormatString="original_{0}" 
                  SelectCommand="SELECT QuestionDiscussion.ID, QuestionDiscussion.Question, QuestionDiscussion.answer, QuestionDiscussion.info, QuestionDiscussion.DateAdded FROM QuestionDiscussion INNER JOIN Subject_Discussion ON QuestionDiscussion.ID = Subject_Discussion.QuestionID WHERE (Subject_Discussion.SubjectID = @SubjectId)" 
                  
                  
                  UpdateCommand="UPDATE [QuestionDiscussion] SET [Question] = @Question, [answer] = @answer, [info] = @info  WHERE [ID] = @original_ID">
                  <SelectParameters>
                      <asp:ControlParameter ControlID="DropDownListSubject" Name="SubjectId" 
                          PropertyName="SelectedValue" />
                  </SelectParameters>
                  <DeleteParameters>
                      <asp:Parameter Name="original_ID" Type="Int64" />
                  </DeleteParameters>
                  <UpdateParameters>
                      <asp:Parameter Name="Question" Type="String" />
                      <asp:Parameter Name="answer" Type="String" />
                      <asp:Parameter Name="info" Type="String" />
                      <asp:Parameter Name="original_ID" Type="Int64" />
                  </UpdateParameters>
                  <InsertParameters>
                      <asp:Parameter Name="info" Type="String" />
                      <asp:Parameter Name="answer" Type="String" />
                      <asp:Parameter Name="Question" Type="String" />
                       <asp:ControlParameter ControlID="DropDownListSubject" Name="SubjectId" 
                          PropertyName="SelectedValue" />
                  </InsertParameters>
              </asp:SqlDataSource>
            </div>
          <div></div>
     </div>
</asp:Content>

