<%@ Page Language="C#" MasterPageFile="~/User/ExamPage.master" AutoEventWireup="true" CodeFile="OnlineExam_Discussion.aspx.cs" Inherits="User_OnlineExam_Discussion" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div>
            <asp:ListView ID="lvDiscussion" runat="server" 
                DataKeyNames="PaperID,DiscussionQuestionID" 
                DataSourceID="SqlDataSourceDiscussion">
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
                    <li style="">   
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
                            <asp:DataPager ID="DataPager1" runat="server">
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
                        <li style="">   
                        <asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' />
                        <br />
                        答:
                        <asp:TextBox ID="UserAnswerLabel" runat="server"   Width="450px" Height="200px"
                            Text='<%# Bind("UserAnswer") %>' />
                        <br />                      
                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="确定" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="取消" />
                        </li>
                    </EditItemTemplate>
                    <ItemSeparatorTemplate>
                        <br />
                    </ItemSeparatorTemplate>               
            </asp:ListView>
            <asp:SqlDataSource ID="SqlDataSourceDiscussion" runat="server"  
                ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
                SelectCommand="SELECT Paper_DiscussionQuestion.PaperID, Paper_DiscussionQuestion.DiscussionQuestionID, QuestionDiscussion.Question, Paper_DiscussionQuestion.UserAnswer FROM Paper_DiscussionQuestion INNER JOIN QuestionDiscussion ON Paper_DiscussionQuestion.DiscussionQuestionID = QuestionDiscussion.ID WHERE (Paper_DiscussionQuestion.PaperID = @PaperId)


" 
                
                    
                    UpdateCommand="UPDATE [Paper_DiscussionQuestion] SET [UserAnswer] = @UserAnswer WHERE [PaperID] = @PaperID AND [DiscussionQuestionID] = @DiscussionQuestionID">
                <SelectParameters>
                    <asp:Parameter Name="PaperId" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="UserAnswer" Type="String" />
                    <asp:Parameter Name="PaperID" Type="Object" />
                    <asp:Parameter Name="DiscussionQuestionID" Type="Int64" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
</asp:Content>

