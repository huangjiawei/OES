<%@ Page Language="C#" MasterPageFile="~/User/ExamPage.master" AutoEventWireup="true" CodeFile="OnlineExam_MultipleChoice.aspx.cs" Inherits="User_OnlineExam_MultipleChoice" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DataList ID="dlMultipleChoice" runat="server" DataKeyField="PaperID"  RepeatColumns="2"
                    DataSourceID="SqlDataSourceMultipleChoiceQuestion">
                    <ItemTemplate>                        
                        <div class="questionItem">
                        <p>--------------------</p>
                        <asp:Label ID="paperId" runat="server" Text='<%# Eval("PaperID") %>' 
                            Visible="False" />
                       
                        &nbsp;<asp:Label ID="questionId" runat="server" 
                            Text='<%# Eval("MultipleChoiceID") %>' Visible="False" />
                     
                        Question:
                        <asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>' />
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="cbA" runat="server" AutoPostBack="true" 
                                    Checked='<%# Bind("AisSelected") %>' 
                                    oncheckedchanged="CheckBox1_CheckedChanged" Text='<%# Eval("optionA") %>' />
                                <br>
                              </br>
                                <asp:CheckBox ID="cbB" runat="server" AutoPostBack="true" 
                                    Checked='<%# Bind("BisSelected") %>' 
                                    oncheckedchanged="CheckBox1_CheckedChanged" Text='<%# Eval("optionB") %>' />
                                <br />
                                <asp:CheckBox ID="cbC" runat="server" AutoPostBack="true" 
                                  oncheckedchanged="CheckBox1_CheckedChanged"  Checked='<%# Bind("CisSelected") %>' Text='<%# Eval("optionC") %>' />
                                <br />
                                <asp:CheckBox ID="cbD" runat="server" AutoPostBack="true" 
                                  oncheckedchanged="CheckBox1_CheckedChanged"  Checked='<%# Bind("DisSelected") %>' Text='<%# Eval("optionD") %>' />
                                <br />
                                <asp:CheckBox ID="cbE" runat="server" AutoPostBack="true" 
                                  oncheckedchanged="CheckBox1_CheckedChanged"  Checked='<%# Bind("EisSelected") %>' Text='<%# Eval("optionE") %>' />
                            </ContentTemplate>
                          <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="cbA" EventName="CheckedChanged" />
                          <asp:AsyncPostBackTrigger ControlID="cbB" EventName="CheckedChanged" />
                          <asp:AsyncPostBackTrigger ControlID="cbC" EventName="CheckedChanged" />
                          <asp:AsyncPostBackTrigger ControlID="cbD" EventName="CheckedChanged" />
                          <asp:AsyncPostBackTrigger ControlID="cbE" EventName="CheckedChanged" />
                          
                          </Triggers>
                        </asp:UpdatePanel></div>
                    </ItemTemplate>
                </asp:DataList>
            <asp:SqlDataSource ID="SqlDataSourceMultipleChoiceQuestion" runat="server" 
                ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
                SelectCommand="SELECT Paper_MultipleChoiceQuestion.PaperID, Paper_MultipleChoiceQuestion.MultipleChoiceID, Paper_MultipleChoiceQuestion.BisSelected, Paper_MultipleChoiceQuestion.AisSelected, Paper_MultipleChoiceQuestion.CisSelected, Paper_MultipleChoiceQuestion.DisSelected, Paper_MultipleChoiceQuestion.EisSelected, QuestionMultipleChoice.Question, QuestionMultipleChoice.optionA, QuestionMultipleChoice.optionB, QuestionMultipleChoice.optionC, QuestionMultipleChoice.optionD, QuestionMultipleChoice.optionE FROM Paper_MultipleChoiceQuestion INNER JOIN QuestionMultipleChoice ON Paper_MultipleChoiceQuestion.MultipleChoiceID = QuestionMultipleChoice.ID
where paperID=@PaperId
order by QuestionMultipleChoice.optionB"       
                    UpdateCommand="UPDATE [Paper_MultipleChoiceQuestion] SET [BisSelected] = @BisSelected, [AisSelected] = @AisSelected, [CisSelected] = @CisSelected, [DisSelected] = @DisSelected, [EisSelected] = @EisSelected WHERE [PaperID] = @PaperID AND [MultipleChoiceID] = @MultipleChoiceID">
                <SelectParameters>
                    <asp:Parameter Name="PaperId" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="BisSelected" Type="Boolean" />
                    <asp:Parameter Name="AisSelected" Type="Boolean" />
                    <asp:Parameter Name="CisSelected" Type="Boolean" />
                    <asp:Parameter Name="DisSelected" Type="Boolean" />
                    <asp:Parameter Name="EisSelected" Type="Boolean" />
                    <asp:Parameter Name="PaperID" Type="Object" />
                    <asp:Parameter Name="MultipleChoiceID" Type="Int64" />
                </UpdateParameters>
            </asp:SqlDataSource>
        
</asp:Content>

