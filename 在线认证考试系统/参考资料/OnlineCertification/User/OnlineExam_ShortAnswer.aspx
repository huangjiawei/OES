<%@ Page Language="C#" MasterPageFile="~/User/ExamPage.master" AutoEventWireup="true" CodeFile="OnlineExam_ShortAnswer.aspx.cs" Inherits="User_OnlineExam_ShortAnswer" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        
            <asp:ListView ID="lvShortAnswer" runat="server" 
                DataSourceID="SqlDataSourceShortAnswer" 
                DataKeyNames="PaperID,ShortAnswerID">
                <ItemTemplate>
                    <li>
                        <h4><asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' /></h4>
                        <br />
                        答:
                        <asp:Label ID="UserAnswerLabel" runat="server" 
                            Text='<%# Eval("UserAnswer") %>' />
                                                   <br />  
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="答题" />
                    </li>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <li >
                       <h4><asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' /></h4>
                        <br />
                        答:
                        <asp:Label ID="UserAnswerLabel" runat="server" 
                            Text='<%# Eval("UserAnswer") %>' />
                                                   <br />  
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="答题" />
                    </li>
                </AlternatingItemTemplate>
                <EmptyDataTemplate>
                    试卷中没有填空题
                </EmptyDataTemplate>               
                <LayoutTemplate>
                    <ul ID="itemPlaceholderContainer" runat="server" 
                        >
                        <li >
                           
                            <h4>
                                <li ID="itemPlaceholder" runat="server"></li>
                            </h4>
                        </li>
                        </ul>
                        <div >
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
                        <asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                        <br />
                        答:
                        <asp:TextBox ID="UserAnswerLabel" runat="server" Height="130px" 
                            Text='<%# Bind("UserAnswer") %>' Width="450px"></asp:TextBox>
                        <br />
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="确定" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="取消" />
                    </EditItemTemplate>
                    <ItemSeparatorTemplate>
                        <br />
                    </ItemSeparatorTemplate>                    
            </asp:ListView>
            <asp:SqlDataSource ID="SqlDataSourceShortAnswer" runat="server" 
                ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
                SelectCommand="SELECT Paper_ShortAnswerQuestion.PaperID, Paper_ShortAnswerQuestion.ShortAnswerID, Paper_ShortAnswerQuestion.UserAnswer, QuestionShortAnswer.Question FROM Paper_ShortAnswerQuestion INNER JOIN QuestionShortAnswer ON Paper_ShortAnswerQuestion.ShortAnswerID = QuestionShortAnswer.ID where paperid=@PaperId
" 
                
                
                    
                    
                    UpdateCommand="UPDATE [Paper_ShortAnswerQuestion] SET [UserAnswer] = @UserAnswer WHERE [PaperID] = @PaperId AND [ShortAnswerID] = @ShortAnswerID">
                    <SelectParameters>
                    <asp:Parameter Name="PaperId" DbType="Object" />
                    </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="UserAnswer" Type="String" />
                    <asp:Parameter Name="PaperId" Type="Object" />
                    <asp:Parameter Name="ShortAnswerID" Type="Int64" />
                </UpdateParameters>
            </asp:SqlDataSource>
        
        </div>
</asp:Content>

