<%@ Page Language="C#" MasterPageFile="~/background/MasterPage.master" AutoEventWireup="true" CodeFile="QuestionBankMultipleChoiceManagement.aspx.cs" Inherits="background_QuestionBankMultipleChoiceManagement" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div>
     <div><h3>多选题</h3></div>
           <div><span>科目(未开考)：<asp:DropDownList ID="DropDownListSubject" runat="server" 
           DataSourceID="SqlDataSourceSubject" DataTextField="Name" DataValueField="ID" 
                AutoPostBack="True">
       </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSourceSubject" runat="server" 
        ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
        SelectCommand="SELECT [ID], [Name] FROM [TestSubjects] WHERE ([isReady] = 0)
">
    </asp:SqlDataSource>
       </span></div>
           <div>
               <asp:GridView ID="gvQuestionInSubject" runat="server" AllowPaging="True"  DataKeyNames="ID"
                   AllowSorting="True" AutoGenerateColumns="False" 
                   DataSourceID="SqlDataSourceQuestionInSubject">
                   <Columns>
                       <asp:CommandField ShowSelectButton="True" />
                       <asp:BoundField DataField="Question" HeaderText="题目" 
                           SortExpression="Question" />
                       <asp:CheckBoxField DataField="AisTrue" HeaderText="a" 
                           SortExpression="AisTrue" />
                       <asp:BoundField DataField="optionA" HeaderText="选择A" SortExpression="optionA" />
                       <asp:CheckBoxField DataField="BisTrue" HeaderText="b" 
                           SortExpression="BisTrue" />
                       <asp:BoundField DataField="optionB" HeaderText="选择B" SortExpression="optionB" />
                       <asp:CheckBoxField DataField="CisTrue" HeaderText="c" 
                           SortExpression="CisTrue" />
                       <asp:BoundField DataField="optionC" HeaderText="选择C" SortExpression="optionC" />
                       <asp:CheckBoxField DataField="DisTrue" HeaderText="d" 
                           SortExpression="DisTrue" />
                       <asp:BoundField DataField="optionD" HeaderText="选择D" SortExpression="optionD" />
                       <asp:CheckBoxField DataField="EisTrue" HeaderText="e" 
                           SortExpression="EisTrue" />
                       <asp:BoundField DataField="optionE" HeaderText="选择E" SortExpression="optionE" />
                       <asp:BoundField DataField="DateAdded" HeaderText="添加日期" 
                           SortExpression="DateAdded" />
                   </Columns>
                   <EmptyDataTemplate>
                       选择科目中尚未编入任何多选题
                   </EmptyDataTemplate>
               </asp:GridView>
               <asp:SqlDataSource ID="SqlDataSourceQuestionInSubject" runat="server" 
                   ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
                   SelectCommand="SELECT Subject_MultipleChoice.SubjectID, QuestionMultipleChoice.* FROM Subject_MultipleChoice INNER JOIN QuestionMultipleChoice ON Subject_MultipleChoice.QuestionID = QuestionMultipleChoice.ID WHERE (Subject_MultipleChoice.SubjectID = @subjectID)">
                   <SelectParameters>
                       <asp:ControlParameter ControlID="DropDownListSubject" Name="subjectID" 
                           PropertyName="SelectedValue" />
                   </SelectParameters>
               </asp:SqlDataSource>
           </div>
           <div>
               <asp:FormView ID="fvSelectedQuestion" runat="server" AllowPaging="True" 
                   DataKeyNames="ID" DataSourceID="SqlDataSourceSelectedQuestion">
                   <EditItemTemplate>
                       题目:
                       <asp:TextBox ID="QuestionTextBox" runat="server" 
                           Text='<%# Bind("Question") %>' />
                       <br />
                       optionA:
                       <asp:TextBox ID="optionATextBox" runat="server" Text='<%# Bind("optionA") %>' />
                       <asp:CheckBox ID="AisTrueCheckBox" runat="server" 
                           Checked='<%# Bind("AisTrue") %>' />
                       <br />
                       optionB:
                       <asp:TextBox ID="optionBTextBox" runat="server" Text='<%# Bind("optionB") %>' />
                       <asp:CheckBox ID="BisTrueCheckBox" runat="server" 
                           Checked='<%# Bind("BisTrue") %>' />
                       <br />
                       optionC:
                       <asp:TextBox ID="optionCTextBox" runat="server" Text='<%# Bind("optionC") %>' />
                       <asp:CheckBox ID="CisTrueCheckBox" runat="server" 
                           Checked='<%# Bind("CisTrue") %>' />
                       <br />
                       optionD:
                       <asp:TextBox ID="optionDTextBox" runat="server" Text='<%# Bind("optionD") %>' />
                       <asp:CheckBox ID="DisTrueCheckBox" runat="server" 
                           Checked='<%# Bind("DisTrue") %>' />
                       <br />
                       optionE:
                       <asp:TextBox ID="optionETextBox" runat="server" Text='<%# Bind("optionE") %>' />
                       <asp:CheckBox ID="EisTrueCheckBox" runat="server" 
                           Checked='<%# Bind("EisTrue") %>' />
                       <br />
                       <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                           CommandName="Update" Text="更新" />
                       &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                           CausesValidation="False" CommandName="Cancel" Text="取消" />
                   </EditItemTemplate>
                   <InsertItemTemplate>
                       题目:
                       <asp:TextBox ID="QuestionTextBox" runat="server" 
                           Text='<%# Bind("Question") %>' />
                       <br />
                       optionA:
                       <asp:TextBox ID="optionATextBox" runat="server" Text='<%# Bind("optionA") %>' />
                       <asp:CheckBox ID="AisTrueCheckBox" runat="server" 
                           Checked='<%# Bind("AisTrue") %>' />
                       <br />
                       optionB:
                       <asp:TextBox ID="optionBTextBox" runat="server" Text='<%# Bind("optionB") %>' />
                       <asp:CheckBox ID="BisTrueCheckBox" runat="server" 
                           Checked='<%# Bind("BisTrue") %>' />
                       <br />
                       optionC:
                       <asp:TextBox ID="optionCTextBox" runat="server" Text='<%# Bind("optionC") %>' />
                       <asp:CheckBox ID="CisTrueCheckBox" runat="server" 
                           Checked='<%# Bind("CisTrue") %>' />
                       <br />
                       optionD:
                       <asp:TextBox ID="optionDTextBox" runat="server" Text='<%# Bind("optionD") %>' />
                       <asp:CheckBox ID="DisTrueCheckBox" runat="server" 
                           Checked='<%# Bind("DisTrue") %>' />
                       <br />
                       optionE:
                       <asp:TextBox ID="optionETextBox" runat="server" Text='<%# Bind("optionE") %>' />
                       <asp:CheckBox ID="EisTrueCheckBox" runat="server" 
                           Checked='<%# Bind("EisTrue") %>' />
                       <br />
                       <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                           CommandName="Insert" Text="插入" />
                       &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                           CausesValidation="False" CommandName="Cancel" Text="取消" />
                   </InsertItemTemplate>
                   <ItemTemplate>
                       题目:
                       <asp:Label ID="QuestionLabel" runat="server" Text='<%# Bind("Question") %>' />
                       <br />
                       optionA:
                       <asp:Label ID="optionALabel" runat="server" Text='<%# Bind("optionA") %>' />
                       <asp:CheckBox ID="AisTrueCheckBox" runat="server" 
                           Checked='<%# Bind("AisTrue") %>' Enabled="false" />
                       <br />
                       optionB:
                       <asp:Label ID="optionBLabel" runat="server" Text='<%# Bind("optionB") %>' />
                       <asp:CheckBox ID="BisTrueCheckBox" runat="server" 
                           Checked='<%# Bind("BisTrue") %>' Enabled="false" />
                       <br />
                       optionC:
                       <asp:Label ID="optionCLabel" runat="server" Text='<%# Bind("optionC") %>' />
                       <asp:CheckBox ID="CisTrueCheckBox" runat="server" 
                           Checked='<%# Bind("CisTrue") %>' Enabled="false" />
                       <br />
                       optionD:
                       <asp:Label ID="optionDLabel" runat="server" Text='<%# Bind("optionD") %>' />
                       <asp:CheckBox ID="DisTrueCheckBox" runat="server" 
                           Checked='<%# Bind("DisTrue") %>' Enabled="false" />
                       <br />
                       optionE:
                       <asp:Label ID="optionELabel" runat="server" Text='<%# Bind("optionE") %>' />
                       <asp:CheckBox ID="EisTrueCheckBox" runat="server" 
                           Checked='<%# Bind("EisTrue") %>' Enabled="false" />
                       <br />
                       <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" 
                           CommandName="Edit" Text="编辑" />
&nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" 
                           Text="删除" />
                       &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" 
                           CommandName="New" Text="新建" />
                   </ItemTemplate>
                   <EmptyDataTemplate>
                       <asp:LinkButton ID="lbAdd" runat="server" onclick="lbAdd_Click">添加新题目</asp:LinkButton>
                   </EmptyDataTemplate>
               </asp:FormView>
               <asp:SqlDataSource ID="SqlDataSourceSelectedQuestion" runat="server" 
                   ConflictDetection="CompareAllValues" 
                   ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
                   DeleteCommand="DELETE FROM [QuestionMultipleChoice] WHERE [ID] = @original_ID AND [Question] = @original_Question AND [optionA] = @original_optionA AND [optionB] = @original_optionB AND [optionC] = @original_optionC AND [optionD] = @original_optionD AND [optionE] = @original_optionE AND [AisTrue] = @original_AisTrue AND [BisTrue] = @original_BisTrue AND [CisTrue] = @original_CisTrue AND [DisTrue] = @original_DisTrue AND [EisTrue] = @original_EisTrue" 
                   InsertCommand="AddMultipleChoiceQuestion" 
                   OldValuesParameterFormatString="original_{0}" 
                   SelectCommand="SELECT [ID], [Question], [optionA], [optionB], [optionC], [optionD], [optionE], [AisTrue], [BisTrue], [CisTrue], [DisTrue], [EisTrue] FROM [QuestionMultipleChoice] WHERE ([ID] = @ID)" 
                   
                   UpdateCommand="UPDATE [QuestionMultipleChoice] SET [Question] = @Question, [optionA] = @optionA, [optionB] = @optionB, [optionC] = @optionC, [optionD] = @optionD, [optionE] = @optionE, [AisTrue] = @AisTrue, [BisTrue] = @BisTrue, [CisTrue] = @CisTrue, [DisTrue] = @DisTrue, [EisTrue] = @EisTrue WHERE [ID] = @original_ID AND [Question] = @original_Question AND [optionA] = @original_optionA AND [optionB] = @original_optionB AND [optionC] = @original_optionC AND [optionD] = @original_optionD AND [optionE] = @original_optionE AND [AisTrue] = @original_AisTrue AND [BisTrue] = @original_BisTrue AND [CisTrue] = @original_CisTrue AND [DisTrue] = @original_DisTrue AND [EisTrue] = @original_EisTrue" 
                   InsertCommandType="StoredProcedure">
                   <SelectParameters>
                       <asp:ControlParameter ControlID="gvQuestionInSubject" Name="ID" 
                           PropertyName="SelectedValue" Type="Int64" />
                   </SelectParameters>
                   <DeleteParameters>
                       <asp:Parameter Name="original_ID" Type="Int64" />
                       <asp:Parameter Name="original_Question" Type="String" />
                       <asp:Parameter Name="original_optionA" Type="String" />
                       <asp:Parameter Name="original_optionB" Type="String" />
                       <asp:Parameter Name="original_optionC" Type="String" />
                       <asp:Parameter Name="original_optionD" Type="String" />
                       <asp:Parameter Name="original_optionE" Type="String" />
                       <asp:Parameter Name="original_AisTrue" Type="Boolean" />
                       <asp:Parameter Name="original_BisTrue" Type="Boolean" />
                       <asp:Parameter Name="original_CisTrue" Type="Boolean" />
                       <asp:Parameter Name="original_DisTrue" Type="Boolean" />
                       <asp:Parameter Name="original_EisTrue" Type="Boolean" />
                   </DeleteParameters>
                   <UpdateParameters>
                       <asp:Parameter Name="Question" Type="String" />
                       <asp:Parameter Name="optionA" Type="String" />
                       <asp:Parameter Name="optionB" Type="String" />
                       <asp:Parameter Name="optionC" Type="String" />
                       <asp:Parameter Name="optionD" Type="String" />
                       <asp:Parameter Name="optionE" Type="String" />
                       <asp:Parameter Name="AisTrue" Type="Boolean" />
                       <asp:Parameter Name="BisTrue" Type="Boolean" />
                       <asp:Parameter Name="CisTrue" Type="Boolean" />
                       <asp:Parameter Name="DisTrue" Type="Boolean" />
                       <asp:Parameter Name="EisTrue" Type="Boolean" />
                       <asp:Parameter Name="original_ID" Type="Int64" />
                       <asp:Parameter Name="original_Question" Type="String" />
                       <asp:Parameter Name="original_optionA" Type="String" />
                       <asp:Parameter Name="original_optionB" Type="String" />
                       <asp:Parameter Name="original_optionC" Type="String" />
                       <asp:Parameter Name="original_optionD" Type="String" />
                       <asp:Parameter Name="original_optionE" Type="String" />
                       <asp:Parameter Name="original_AisTrue" Type="Boolean" />
                       <asp:Parameter Name="original_BisTrue" Type="Boolean" />
                       <asp:Parameter Name="original_CisTrue" Type="Boolean" />
                       <asp:Parameter Name="original_DisTrue" Type="Boolean" />
                       <asp:Parameter Name="original_EisTrue" Type="Boolean" />
                   </UpdateParameters>
                   <InsertParameters>
                       <asp:Parameter Name="Question" Type="String" />
                       <asp:Parameter Name="optionA" Type="String" />
                       <asp:Parameter Name="optionB" Type="String" />
                       <asp:Parameter Name="optionC" Type="String" />
                       <asp:Parameter Name="optionD" Type="String" />
                       <asp:Parameter Name="optionE" Type="String" />
                       <asp:Parameter Name="AisTrue" Type="Boolean" />
                       <asp:Parameter Name="BisTrue" Type="Boolean" />
                       <asp:Parameter Name="CisTrue" Type="Boolean" />
                       <asp:Parameter Name="DisTrue" Type="Boolean" />
                       <asp:Parameter Name="EisTrue" Type="Boolean" />                      
                       <asp:ControlParameter ControlID="DropDownListSubject" Name="subjectID" 
                           PropertyName="SelectedValue" />
                   </InsertParameters>
               </asp:SqlDataSource>
           </div>
     </div>
</asp:Content>

