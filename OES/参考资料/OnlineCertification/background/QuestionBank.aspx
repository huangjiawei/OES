<%@ Page Language="C#" MasterPageFile="~/background/MasterPage.master" AutoEventWireup="true" CodeFile="QuestionBank.aspx.cs" Inherits="background_QuestionBank" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<div>
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
        <asp:DetailsView ID="dvSubjectQuestionSummary" runat="server" AutoGenerateRows="False" 
            DataSourceID="SqlDataSourceSubjectQuestion" Height="50px" Width="125px">
            <Fields>
                <asp:BoundField DataField="SingleChoiceAmount" HeaderText="单选题" 
                    ReadOnly="True" SortExpression="SingleChoiceAmount" />
                <asp:BoundField DataField="MultipleChoiceAmount" 
                    HeaderText="多选题" ReadOnly="True" 
                    SortExpression="MultipleChoiceAmount" />
                <asp:BoundField DataField="CompletionAmount" HeaderText="填空题" 
                    ReadOnly="True" SortExpression="CompletionAmount" />
                <asp:BoundField DataField="ShortAnswerAmount" HeaderText="简答题/计算题" 
                    ReadOnly="True" SortExpression="ShortAnswerAmount" />
                <asp:BoundField DataField="DiscussionAmount" HeaderText="讨论题/应用题" 
                    ReadOnly="True" SortExpression="DiscussionAmount" />
            </Fields>
            <HeaderTemplate>
                已编入题库的数量
            </HeaderTemplate>
        </asp:DetailsView>
        <asp:SqlDataSource ID="SqlDataSourceSubjectQuestion" runat="server" 
            ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
            SelectCommand="GetSubjectQuestionSummary" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownListSubject" Name="subjectID" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
   
   </div>
</div>
    </asp:Content>

