<%@ Page Language="C#" MasterPageFile="~/User/ExamPage.master" AutoEventWireup="true" CodeFile="OnlineExam_Completion.aspx.cs" Inherits="User_OnlineCompletion" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
            <asp:ListView ID="ListView1" runat="server" 
                DataKeyNames="PaperID,CompletionQuestionID" 
                DataSourceID="SqlDataSourceCompletion">
                <ItemTemplate>
                    <li style="">
                    <asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' />
                        <br />
                       答:
                        <asp:Label ID="UserAnswerLabel" runat="server" 
                            Text='<%# Eval("UserAnswer") %>' />
                        <br />
                        
                        
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="答题" />
                    </li>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <li style="">PaperID:
                       <asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' />
                        <br />
                       答:
                        <asp:Label ID="UserAnswerLabel" runat="server" 
                            Text='<%# Eval("UserAnswer") %>' />
                        <br />
                        
                        
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="答题" />
                    </li>
                </AlternatingItemTemplate>
                <EmptyDataTemplate>
                    未返回数据。
                </EmptyDataTemplate>
     
                <LayoutTemplate>
                    <ul ID="itemPlaceholderContainer" runat="server" style="">
                        <li ID="itemPlaceholder" runat="server" />
                        </ul>
                        <div style="">
                            <asp:DataPager ID="DataPager1" runat="server" PageSize="10">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                        ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                    <asp:NumericPagerField />
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" 
                                        ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                </Fields>
                            </asp:DataPager>
                        </div>
                    </LayoutTemplate>
                    <EditItemTemplate>
                        <li style=""><asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' />
                        <br />
                       答:
                        <asp:TextBox ID="UserAnswerLabel" runat="server"  Width="300px" Height="200px"
                            Text='<%# Bind("UserAnswer") %>' />
                        <br />
                        
                        
                    
                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="确定" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="取消" />
                        </li>
                    </EditItemTemplate>
                    <ItemSeparatorTemplate>
                        <br />
                    </ItemSeparatorTemplate>
                    <SelectedItemTemplate>
                        <li style="">PaperID:
                            <asp:Label ID="PaperIDLabel" runat="server" Text='<%# Eval("PaperID") %>' />
                            <br />
                            CompletionQuestionID:
                            <asp:Label ID="CompletionQuestionIDLabel" runat="server" 
                                Text='<%# Eval("CompletionQuestionID") %>' />
                            <br />
                            UserAnswer:
                            <asp:Label ID="UserAnswerLabel" runat="server" 
                                Text='<%# Eval("UserAnswer") %>' />
                            <br />
                            Question:
                            <asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' />
                            <br />
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                        </li>
                    </SelectedItemTemplate>
                </asp:ListView>
                <asp:SqlDataSource ID="SqlDataSourceCompletion" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
                    SelectCommand="SELECT Paper_CompletionQuestion.PaperID, Paper_CompletionQuestion.CompletionQuestionID, Paper_CompletionQuestion.UserAnswer, QuestionCompletion.Question FROM Paper_CompletionQuestion INNER JOIN QuestionCompletion ON Paper_CompletionQuestion.CompletionQuestionID = QuestionCompletion.ID
where paperId=@PaperId

" 
                    
                    
                    
                    UpdateCommand="UPDATE [Paper_CompletionQuestion] SET [UserAnswer] = @UserAnswer WHERE [PaperID] = @PaperID AND [CompletionQuestionID] = @CompletionQuestionID">
                    <SelectParameters>
                        <asp:Parameter Name="PaperId" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="UserAnswer" Type="String" />
                        <asp:Parameter Name="PaperID" Type="Object" />
                        <asp:Parameter Name="CompletionQuestionID" Type="Int64" />
                    </UpdateParameters>
                </asp:SqlDataSource>
</asp:Content>

