<%@ Page Language="C#" MasterPageFile="~/background/MasterPage.master" AutoEventWireup="true" CodeFile="QuestionBankShortAnswerQuestionManagement.aspx.cs" Inherits="background_QuestionBankShortAnswerQuestionManagement" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div>
     <div><h3>简答题</h3></div>
            <div><span>科目(未开考)：<asp:DropDownList ID="DropDownListSubject" runat="server" 
           DataSourceID="SqlDataSourceSubject" DataTextField="Name" DataValueField="ID" 
                AutoPostBack="True">
       </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSourceSubject" runat="server" 
        ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
        SelectCommand="SELECT [ID], [Name] FROM [TestSubjects] WHERE ([isReady] = 0)
">
    </asp:SqlDataSource>
       </span>
        </div>
          <div>
              <asp:ListView ID="ListView1" runat="server" DataKeyNames="ID" 
                  DataSourceID="SqlDataSourceSubjectInQuestion" InsertItemPosition="LastItem">
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
                              <asp:Label ID="AnswerLabel" runat="server" Text='<%# Eval("Answer") %>' />
                          </td>
                          <td>
                              <asp:Label ID="InfoLabel" runat="server" Text='<%# Eval("Info") %>' />
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
                              <asp:Label ID="AnswerLabel" runat="server" Text='<%# Eval("Answer") %>' />
                          </td>
                          <td>
                              <asp:Label ID="InfoLabel" runat="server" Text='<%# Eval("Info") %>' />
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
                              <asp:TextBox ID="AnswerTextBox" runat="server" Text='<%# Bind("Answer") %>' />
                          </td>
                          <td>
                              <asp:TextBox ID="InfoTextBox" runat="server" Text='<%# Bind("Info") %>' />
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
                                              Answer</th>
                                          <th runat="server">
                                              Info</th>
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
                                              ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                          <asp:NumericPagerField />
                                          <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" 
                                              ShowNextPageButton="False" ShowPreviousPageButton="False" />
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
                              <asp:TextBox ID="AnswerTextBox" runat="server" Text='<%# Bind("Answer") %>' />
                          </td>
                          <td>
                              <asp:TextBox ID="InfoTextBox" runat="server" Text='<%# Bind("Info") %>' />
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
                              <asp:Label ID="AnswerLabel" runat="server" Text='<%# Eval("Answer") %>' />
                          </td>
                          <td>
                              <asp:Label ID="InfoLabel" runat="server" Text='<%# Eval("Info") %>' />
                          </td>
                          <td>
                              <asp:Label ID="DateAddedLabel" runat="server" Text='<%# Eval("DateAdded") %>' />
                          </td>
                      </tr>
                  </SelectedItemTemplate>
              </asp:ListView>
              <asp:SqlDataSource ID="SqlDataSourceSubjectInQuestion" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
                  DeleteCommand="DELETE FROM [QuestionShortAnswer] WHERE [ID] = @ID" 
                  InsertCommand="AddShortAnswerQuestion" InsertCommandType="StoredProcedure" 
                  SelectCommand="SELECT QuestionShortAnswer.ID, QuestionShortAnswer.Question, QuestionShortAnswer.Answer, QuestionShortAnswer.Info , QuestionShortAnswer.DateAdded FROM QuestionShortAnswer INNER JOIN Subject_ShortAnswer ON QuestionShortAnswer.ID = Subject_ShortAnswer.QuestionID WHERE (Subject_ShortAnswer.SubjectID = @subjectID)" 
                  UpdateCommand="UPDATE [QuestionShortAnswer] SET [Question] = @Question, [Answer] = @Answer, [Info] = @Info WHERE [ID] = @ID">
                  <SelectParameters>
                      <asp:ControlParameter ControlID="DropDownListSubject" Name="subjectID" 
                          PropertyName="SelectedValue" />
                  </SelectParameters>
                  <DeleteParameters>
                      <asp:Parameter Name="ID" Type="Int64" />
                  </DeleteParameters>
                  <UpdateParameters>
                      <asp:Parameter Name="Question" Type="String" />
                      <asp:Parameter Name="Answer" Type="String" />
                      <asp:Parameter Name="Info" Type="String" />
                      <asp:Parameter Name="ID" Type="Int64" />
                  </UpdateParameters>
                  <InsertParameters>
                      <asp:Parameter Name="Question" Type="String" />
                      <asp:Parameter Name="Answer" Type="String" />
                      <asp:Parameter Name="Info" Type="String" />
                    <asp:ControlParameter ControlID="DropDownListSubject" Name="subjectID" 
                          PropertyName="SelectedValue" />
                  </InsertParameters>
              </asp:SqlDataSource>
            </div>
          <div></div>
     </div>
</asp:Content>

