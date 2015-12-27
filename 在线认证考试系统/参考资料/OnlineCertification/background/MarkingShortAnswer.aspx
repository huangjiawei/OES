<%@ Page Language="C#" MasterPageFile="~/background/MasterPage.master" AutoEventWireup="true" CodeFile="MarkingShortAnswer.aspx.cs" Inherits="background_Marking" Title="无标题页" %>

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
            <asp:SqlDataSource ID="SqlDataSourceShortAnswer" runat="server" 
                ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
                SelectCommand="SELECT Paper_ShortAnswerQuestion.PaperID, Paper_ShortAnswerQuestion.ShortAnswerID, Paper_ShortAnswerQuestion.UserAnswer, QuestionShortAnswer.Question, QuestionShortAnswer.Answer, QuestionShortAnswer.Info, Paper_ShortAnswerQuestion.score FROM Paper_ShortAnswerQuestion INNER JOIN QuestionShortAnswer ON Paper_ShortAnswerQuestion.ShortAnswerID = QuestionShortAnswer.ID INNER JOIN PaperFinished ON Paper_ShortAnswerQuestion.PaperID = PaperFinished.ID WHERE (PaperFinished.SubjectId = @SubjectId)" 
                
                
                    
             UpdateCommand="UPDATE Paper_ShortAnswerQuestion SET score = @score WHERE (PaperID = @PaperId) AND (ShortAnswerID = @ShortAnswerID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownListSubject" Name="SubjectId" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="score" />
                    <asp:Parameter Name="PaperId" Type="Object" />
                    <asp:Parameter Name="ShortAnswerID" Type="Int64" />
                </UpdateParameters>
            </asp:SqlDataSource>
        
         <asp:GridView ID="gvMarking" runat="server" AllowPaging="True" 
             AllowSorting="True" AutoGenerateColumns="False" 
             DataKeyNames="PaperID,ShortAnswerID" DataSourceID="SqlDataSourceShortAnswer">
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
                         <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Question") %>' Enabled="false"></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("Question") %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="考生回答" SortExpression="UserAnswer">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("UserAnswer") %>' Enabled="false"></asp:TextBox>
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
                         <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Answer") %>' Enabled="false"></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label4" runat="server" Text='<%# Bind("Answer") %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="评分标准" SortExpression="Info">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Info") %>' Enabled="false"></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label5" runat="server" Text='<%# Bind("Info") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
         </asp:GridView>
     </div>
     <div></div>
</div>
</asp:Content>

