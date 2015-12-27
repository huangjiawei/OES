<%@ Page Language="C#" MasterPageFile="~/User/ExamPage.master" AutoEventWireup="true" CodeFile="OnlineExam_SingleChoice.aspx.cs" Inherits="User_OnlineExam_SingleChoice" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DataList ID="dlSingleChoiceQuestion" runat="server"
        DataKeyField="SingleQuestionID"
               DataSourceID="SqlDataSourceSingleChoiceQuestion">
                 <ItemStyle />
                 <ItemTemplate>
                 <div class=".questionItem">
                 <p>--------------------</p>
                            <asp:Label ID="questionId" runat="server" Visible="false" Text='<%# Eval("SingleQuestionID") %>'></asp:Label>
                            <asp:Label ID="paperId" runat="server" Visible="false" Text='<%# Eval("PaperID") %>'></asp:Label>
                            <asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' />
                     <br />
                    
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                      <ContentTemplate>
                     <asp:RadioButton ID="rbA" runat="server"  GroupName="option" AutoPostBack="true"
                         Checked='<%# Bind("AisSelected") %>' Text='<%# Eval("optionA") %>' oncheckedchanged="rbA_CheckedChanged" 
                       />
                     <br />
                     <asp:RadioButton ID="rbB" runat="server"   GroupName="option"  AutoPostBack="true"
                         Checked='<%# Bind("BisSelected") %>' Text='<%# Eval("optionB") %>' oncheckedchanged="rbA_CheckedChanged"  
                      />
                     <br />
                     <asp:RadioButton ID="rbC" runat="server"   GroupName="option"  AutoPostBack="true"
                         Checked='<%# Bind("CisSelected") %>' Text='<%# Eval("optionC") %>' oncheckedchanged="rbA_CheckedChanged" 
                     />
                     <br />
                     <asp:RadioButton ID="rbD" runat="server"   GroupName="option"  AutoPostBack="true"
                         Checked='<%# Bind("DisSelected") %>' Text='<%# Eval("optionD") %>' oncheckedchanged="rbA_CheckedChanged" 
                          /></ContentTemplate>
                          <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="rbA" EventName="CheckedChanged" />
                           <asp:AsyncPostBackTrigger ControlID="rbB" EventName="CheckedChanged" /> 
                           <asp:AsyncPostBackTrigger ControlID="rbC" EventName="CheckedChanged" /> 
                           <asp:AsyncPostBackTrigger ControlID="rbD" EventName="CheckedChanged" />
                          </Triggers>
                     </asp:UpdatePanel>
     </div>
                 </ItemTemplate>
             </asp:DataList>
         <asp:SqlDataSource ID="SqlDataSourceSingleChoiceQuestion" runat="server" 
             ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
             DeleteCommand="DELETE FROM [Paper_SingleChoiceQuestion] WHERE [SingleQuestionID] = @SingleQuestionID AND [PaperID] = @PaperID" 
             SelectCommand="SELECT Paper_SingleChoiceQuestion.AisSelected, Paper_SingleChoiceQuestion.BisSelected, Paper_SingleChoiceQuestion.CisSelected, Paper_SingleChoiceQuestion.DisSelected, Paper_SingleChoiceQuestion.SingleQuestionID, Paper_SingleChoiceQuestion.PaperID, QuestionSingleChoice.optionA, QuestionSingleChoice.Question, QuestionSingleChoice.optionB, QuestionSingleChoice.optionC, QuestionSingleChoice.optionD
 FROM Paper_SingleChoiceQuestion INNER JOIN Paper ON Paper_SingleChoiceQuestion.PaperID = Paper.ID INNER JOIN QuestionSingleChoice ON Paper_SingleChoiceQuestion.SingleQuestionID = QuestionSingleChoice.ID 
WHERE (Paper_SingleChoiceQuestion.PaperID = @PaperID)
order by QuestionSingleChoice.optionA" 
             
             
                 
                 UpdateCommand="UPDATE [Paper_SingleChoiceQuestion] SET [AisSelected] = @AisSelected, [BisSelected] = @BisSelected, [CisSelected] = @CisSelected, [DisSelected] = @DisSelected WHERE [SingleQuestionID] = @SingleQuestionID AND [PaperID] = @PaperID">
             <SelectParameters>
                 <asp:Parameter Name="PaperID" />
             </SelectParameters>
             <DeleteParameters>
                 <asp:Parameter Name="SingleQuestionID" Type="Int64" />
                 <asp:Parameter Name="PaperID" Type="Object" />
             </DeleteParameters>
             <UpdateParameters>
                 <asp:Parameter Name="AisSelected" Type="Boolean" />
                 <asp:Parameter Name="BisSelected" Type="Boolean" />
                 <asp:Parameter Name="CisSelected" Type="Boolean" />
                 <asp:Parameter Name="DisSelected" Type="Boolean" />
                 <asp:Parameter Name="SingleQuestionID" Type="Int64" />
                 <asp:Parameter Name="PaperID" Type="Object" />
             </UpdateParameters>
         </asp:SqlDataSource>
</asp:Content>

