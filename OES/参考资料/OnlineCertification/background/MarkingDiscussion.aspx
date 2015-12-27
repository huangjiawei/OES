<%@ Page Language="C#" MasterPageFile="~/background/MasterPage.master" AutoEventWireup="true" CodeFile="MarkingDiscussion.aspx.cs" Inherits="background_MarkingDiscussion" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
     <div>阅卷</div>
     <div>
       <span>科目：<asp:DropDownList ID="DropDownListSubject" runat="server" 
           DataSourceID="SqlDataSourceSubject" DataTextField="Name" 
           DataValueField="ID" AutoPostBack="True">
       </asp:DropDownList>
       <asp:SqlDataSource ID="SqlDataSourceSubject" runat="server" 
        ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
        SelectCommand="SELECT [ID], [Name] FROM [TestSubjects]">
    </asp:SqlDataSource>
       </span>   
   </div>
      <div>
            <asp:GridView ID="gvMarking" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" 
                DataKeyNames="PaperID,DiscussionQuestionID" 
                DataSourceID="SqlDataSourceDisscussionQuestion">
                <Columns>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                                CommandName="Update" Text="确定"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                CommandName="Cancel" Text="取消"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                CommandName="Edit" Text="打分"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="题目" SortExpression="Question">
                        <EditItemTemplate>
                            <asp:Label ID="TextBox1" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Question") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="考生回答" SortExpression="UserAnswer">
                        <EditItemTemplate>
                            <asp:Label ID="TextBox2" runat="server" Text='<%# Eval("UserAnswer") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("UserAnswer") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="分数" SortExpression="score">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("score") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("score") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="正确答案" SortExpression="Answer">
                        <EditItemTemplate>
                            <asp:Label ID="TextBox4" runat="server" Text='<%# Bind("Answer") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Answer") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="评分标准" SortExpression="Info">
                        <EditItemTemplate>
                            <asp:Label ID="TextBox5" runat="server" Text='<%# Bind("Info") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Info") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSourceDisscussionQuestion" runat="server" 
                ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
                SelectCommand="SELECT Paper_DiscussionQuestion.PaperID, Paper_DiscussionQuestion.DiscussionQuestionID, Paper_DiscussionQuestion.UserAnswer, QuestionShortAnswer.Question, Paper_DiscussionQuestion.score, QuestionShortAnswer.Info, QuestionShortAnswer.Answer FROM Paper_DiscussionQuestion INNER JOIN QuestionShortAnswer ON Paper_DiscussionQuestion.DiscussionQuestionID = QuestionShortAnswer.ID INNER JOIN PaperFinished ON Paper_DiscussionQuestion.PaperID = PaperFinished.ID WHERE (PaperFinished.SubjectId = @SubjectId)" 
                
                UpdateCommand="UPDATE Paper_DiscussionQuestion SET score = @score WHERE (PaperID = @PaperId) AND (DiscussionQuestionID = @DiscussionQuestionID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownListSubject" Name="SubjectId" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="score" />
                    <asp:Parameter Name="PaperId" Type="Object" />
                    <asp:Parameter Name="DiscussionQuestionID" Type="Int64" />
                </UpdateParameters>
            </asp:SqlDataSource>
        
        </div>
      <div></div>
      
</div>
</asp:Content>

