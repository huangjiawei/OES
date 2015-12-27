<%@ Page Language="C#" MasterPageFile="~/background/MasterPage.master" AutoEventWireup="true" CodeFile="QuestionBankSingleChoiceManagement.aspx.cs" Inherits="background_SingleChoiceQuestionBank" Title="无标题页" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <div><h3>单选题</h3></div>
        <div><span>科目(未开考)：<asp:DropDownList ID="DropDownListSubject" runat="server" 
           DataSourceID="SqlDataSourceSubject" DataTextField="Name" DataValueField="ID" 
                AutoPostBack="True">
       </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSourceSubject" runat="server" 
        ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
        SelectCommand="SELECT [ID], [Name] FROM [TestSubjects] WHERE ([isReady] = 0)">
    </asp:SqlDataSource>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
       </span>
        </div>
        <div>                   
                      <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                          AutoGenerateColumns="False" DataKeyNames="ID" 
                          DataSourceID="SqlDataSourceQuestions">
                          <Columns>
                              <asp:TemplateField ShowHeader="False">
                                  <EditItemTemplate>
                                      <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                                          CommandName="Update" Text="更新"></asp:LinkButton>
                                      &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                          CommandName="Cancel" Text="取消"></asp:LinkButton>
                                  </EditItemTemplate>
                                  <ItemTemplate>
                                      <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                          CommandName="Edit" Text="编辑"></asp:LinkButton>
                                      &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                          CommandName="Delete" Text="删除"></asp:LinkButton><asp:ConfirmButtonExtender 
                                          ID="LinkButton2_ConfirmButtonExtender" runat="server" ConfirmText="确定要删除吗?" 
                                          Enabled="True" TargetControlID="LinkButton2">
                                      </asp:ConfirmButtonExtender>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="ID" HeaderText="编号" InsertVisible="False" 
                                  ReadOnly="True" SortExpression="ID" />
                              <asp:BoundField DataField="Question" HeaderText="题目" 
                                  SortExpression="Question" />
                              <asp:CheckBoxField DataField="AisTrue" HeaderText="A" 
                                  SortExpression="AisTrue" />
                              <asp:BoundField DataField="optionA" HeaderText="答案A" SortExpression="optionA" />
                              <asp:CheckBoxField DataField="BisTrue" HeaderText="B" 
                                  SortExpression="BisTrue" />
                              <asp:BoundField DataField="optionB" HeaderText="答案B" SortExpression="optionB" />
                              <asp:CheckBoxField DataField="CisTrue" HeaderText="C" 
                                  SortExpression="CisTrue" />
                              <asp:BoundField DataField="optionC" HeaderText="答案C" SortExpression="optionC" />
                              <asp:CheckBoxField DataField="DisTrue" HeaderText="D" 
                                  SortExpression="DisTrue" />
                              <asp:BoundField DataField="optionD" HeaderText="答案D" SortExpression="optionD" />
                              <asp:BoundField DataField="DateAdded" HeaderText="添加日期" 
                                  SortExpression="DateAdded" />
                          </Columns>
                      </asp:GridView>
                      <asp:SqlDataSource ID="SqlDataSourceQuestions" runat="server" 
                ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
                DeleteCommand="DELETE FROM [QuestionSingleChoice] WHERE [ID] = @ID" 
                InsertCommand="AddSingleChoiceQuestion" InsertCommandType="StoredProcedure" 
                SelectCommand="SELECT QuestionSingleChoice.ID, QuestionSingleChoice.Question, QuestionSingleChoice.optionA, QuestionSingleChoice.optionB, QuestionSingleChoice.optionC, QuestionSingleChoice.optionD,QuestionSingleChoice.AisTrue,QuestionSingleChoice.BisTrue,QuestionSingleChoice.CisTrue,QuestionSingleChoice.DisTrue,QuestionSingleChoice.DateAdded FROM QuestionSingleChoice INNER JOIN Subject_SingleChoice ON QuestionSingleChoice.ID = Subject_SingleChoice.QuestionID WHERE (Subject_SingleChoice.SubjectID = @subjectID)" 
                
                
                UpdateCommand="UPDATE QuestionSingleChoice SET Question = @Question, optionA = @optionA, optionB = @optionB, optionC = @optionC, optionD = @optionD, AisTrue = @AisTrue, BisTrue = @BisTrue, CisTrue = @CisTrue, DisTrue = @DisTrue WHERE (ID = @ID)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="DropDownListSubject" Name="subjectID" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int64" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Question" Type="String" />
                    <asp:Parameter Name="CorrectAnswer" Type="String" />
                    <asp:Parameter Name="optionB" />
                    <asp:Parameter Name="optionC" />
                    <asp:Parameter Name="optionD" />
                    <asp:Parameter Name="AisTrue" />
                    <asp:Parameter Name="BisTrue" />
                    <asp:Parameter Name="CisTrue" />
                    <asp:Parameter Name="DisTrue" />
                    <asp:Parameter Name="ID" Type="Int64" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="Question" Type="String" />
                    <asp:Parameter Name="optionA" Type="String" />
                    <asp:Parameter Name="optionB" Type="String" />
                    <asp:Parameter Name="optionC" Type="String" />
                    <asp:Parameter Name="optionD" Type="String" />
                    <asp:Parameter Name="AisTrue" Type="Boolean" />
                    <asp:Parameter Name="BisTrue" Type="Boolean" />
                    <asp:Parameter Name="CisTrue" Type="Boolean" />
                    <asp:Parameter Name="DisTrue" Type="Boolean" />
                   <asp:ControlParameter ControlID="DropDownListSubject" Name="subjectID" 
                        PropertyName="SelectedValue" />
                </InsertParameters>
            </asp:SqlDataSource>
      
        </div>
        <div>
        </div>
        <div>
  
        </div>
        <div></div>
   </div>
</asp:Content>

