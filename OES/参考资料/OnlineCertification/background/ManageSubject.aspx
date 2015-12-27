<%@ Page Language="C#" MasterPageFile="~/background/MasterPage.master" AutoEventWireup="true" CodeFile="ManageSubject.aspx.cs" Inherits="background_ManageSubject" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <div>
      <div>
      <h3>考试科目：</h3>
         <asp:GridView ID="gvAllSubject" runat="server" AllowPaging="True" 
             AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" 
             DataSourceID="SqlDataSourceAllSubjects" style="margin-top: 2px" 
              BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
              CellPadding="3" onselectedindexchanged="gvAllSubject_SelectedIndexChanged" 
              EnableTheming="True">
             <RowStyle ForeColor="#000066" />
             <Columns>
                 <asp:CommandField ShowSelectButton="True" />
                 <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                     ReadOnly="True" SortExpression="ID" />
                 <asp:BoundField DataField="Name" HeaderText="科目" SortExpression="Name" />
                 <asp:BoundField DataField="info" HeaderText="科目描述" SortExpression="info" />
                 <asp:CheckBoxField DataField="isReady" HeaderText="已公开考试(不允许修改题库)" 
                     SortExpression="isReady" />
             </Columns>
             <FooterStyle BackColor="White" ForeColor="#000066" />
             <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
             <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
             <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
         </asp:GridView> 
          
      <asp:SqlDataSource ID="SqlDataSourceAllSubjects" runat="server" 
             ConflictDetection="CompareAllValues" 
             ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
             DeleteCommand="DELETE FROM [TestSubjects] WHERE [ID] = @original_ID AND [Name] = @original_Name AND [info] = @original_info" 
             InsertCommand="INSERT INTO [TestSubjects] ([Name], [info]) VALUES (@Name, @info)" 
             OldValuesParameterFormatString="original_{0}" 
             SelectCommand="SELECT ID, Name, info, isReady FROM TestSubjects" 
             
              
              UpdateCommand="UPDATE [TestSubjects] SET [Name] = @Name, [info] = @info WHERE [ID] = @original_ID AND [Name] = @original_Name AND [info] = @original_info">
        <DeleteParameters>
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_Name" Type="String" />
            <asp:Parameter Name="original_info" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="info" Type="String" />
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_Name" Type="String" />
            <asp:Parameter Name="original_info" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="info" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
      </div>
      <div>
         <asp:FormView ID="FormViewSubjectDetail" runat="server" AllowPaging="True" 
             DataKeyNames="ID" DataSourceID="SqlDataSourceSubjectDetail" 
             BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
             CellPadding="3" GridLines="Both" 
              oniteminserted="FormViewSubjectDetail_ItemInserted" 
              onpageindexchanging="FormViewSubjectDetail_PageIndexChanging">
             <FooterStyle BackColor="White" ForeColor="#000066" />
             <RowStyle ForeColor="#000066" />
             <EditItemTemplate>
               
                 科目:
                 <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                 开考(如果勾选则不能修改题库)：<asp:CheckBox ID="CheckBox1" Checked='<%# Bind("isReady") %>' runat="server" />
                 <br />
                 
                 详情:
                 <asp:TextBox ID="infoTextBox" runat="server" Text='<%# Bind("info") %>' />
                      <table>
                 <tr><th class="style1">题型</th><th>数量</th><th>分数</th></tr>
                 <tr>
                        <td class="style1">单选题</td>
                        <td><asp:TextBox ID="SingleChoiceQuantityTextBox" runat="server" 
                              Text='<%# Bind("SingleChoiceQuantity") %>' />
                        </td> 
                         <td><asp:TextBox ID="SingleChoiceGradeTextBox" runat="server" 
                         Text='<%# Bind("SingleChoiceGrade") %>' /></td>
                     </tr>
                 <tr>
                       <td class="style1">多选题</td>
                       <td><asp:TextBox ID="MultipleChoiceQuantityTextBox" runat="server" 
                           Text='<%# Bind("MultipleChoiceQuantity") %>' /></td>
                     <td><asp:TextBox ID="MultipleChoiceGradeTextBox" runat="server" 
                         Text='<%# Bind("MultipleChoiceGrade") %>' />
                       </td></tr>
                 <tr>
                       <td class="style1">填空题</td>
                       <td><asp:TextBox ID="CompletionQuantityTextBox" runat="server" 
                     Text='<%# Bind("CompletionQuantity") %>' /></td>
                       <td><asp:TextBox ID="CompletionGradeTextBox" runat="server" 
                     Text='<%# Bind("CompletionGrade") %>' /></td>
                 </tr>
                 <tr>   
                       <td class="style1">简答题/计算题</td>
                       <td><asp:TextBox ID="ShortAnswerQuantityTextBox" runat="server" 
                     Text='<%# Bind("ShortAnswerQuantity") %>' /></td>
                       <td> <asp:TextBox ID="ShortAnswerGradeTextBox" runat="server" 
                     Text='<%# Bind("ShortAnswerGrade") %>' /></td>
                     </tr>
                 <tr> 
                       <td class="style1">议论题/综合题</td>
                       <td> <asp:TextBox ID="DiscussionQuantityTextBox" runat="server" 
                     Text='<%# Bind("DiscussionQuantity") %>' /></td>
                       <td> 
                 <asp:TextBox ID="DiscussionGradeTextBox" runat="server" 
                     Text='<%# Bind("DiscussionGrade") %>' /></td>
                 </tr>
              
                 
                 </table>             
                 <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                     CommandName="Update" Text="更新" />
                 &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                     CausesValidation="False" CommandName="Cancel" Text="取消" />
             </EditItemTemplate>
             <InsertItemTemplate>
                 科目:
                 <asp:TextBox ID="NameTextBox0" runat="server" Text='<%# Bind("Name") %>' />
                 开考：<asp:CheckBox ID="CheckBox1" Checked='<%# Bind("isReady") %>' runat="server" />
                 <br />
                 详情:
                 <asp:TextBox ID="infoTextBox0" runat="server" Text='<%# Bind("info") %>' />
                  <table>
                 <tr><th class="style1">题型</th><th>数量</th><th>分数</th></tr>
                 <tr>
                        <td class="style1">单选题</td>
                        <td><asp:TextBox ID="TextBox1" runat="server" 
                              Text='<%# Bind("SingleChoiceQuantity") %>' />
                        </td> 
                         <td><asp:TextBox ID="TextBox2" runat="server" 
                         Text='<%# Bind("SingleChoiceGrade") %>' /></td>
                     </tr>
                 <tr>
                       <td class="style1">多选题</td>
                       <td><asp:TextBox ID="TextBox3" runat="server" 
                           Text='<%# Bind("MultipleChoiceQuantity") %>' /></td>
                     <td><asp:TextBox ID="TextBox4" runat="server" 
                         Text='<%# Bind("MultipleChoiceGrade") %>' />
                       </td></tr>
                 <tr>
                       <td class="style1">填空题</td>
                       <td><asp:TextBox ID="TextBox5" runat="server" 
                     Text='<%# Bind("CompletionQuantity") %>' /></td>
                       <td><asp:TextBox ID="TextBox6" runat="server" 
                     Text='<%# Bind("CompletionGrade") %>' /></td>
                 </tr>
                 <tr>   
                       <td class="style1">简答题/计算题</td>
                       <td><asp:TextBox ID="TextBox7" runat="server" 
                     Text='<%# Bind("ShortAnswerQuantity") %>' /></td>
                       <td> <asp:TextBox ID="TextBox8" runat="server" 
                     Text='<%# Bind("ShortAnswerGrade") %>' /></td>
                     </tr>
                 <tr> 
                       <td class="style1">议论题/综合题</td>
                       <td> <asp:TextBox ID="TextBox9" runat="server" 
                     Text='<%# Bind("DiscussionQuantity") %>' /></td>
                       <td> 
                 <asp:TextBox ID="TextBox10" runat="server" 
                     Text='<%# Bind("DiscussionGrade") %>' /></td>
                 </tr>
              
                 
                 </table>  
                 <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                     CommandName="Insert" Text="插入" />
                 &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                     CausesValidation="False" CommandName="Cancel" Text="取消" />
             </InsertItemTemplate>
             <ItemTemplate>
               
                 科目:
                 <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>' />
                 <br />
                 详情:
                 <asp:Label ID="infoLabel" runat="server" Text='<%# Bind("info") %>' />
                 <br />
                 <table>
                 <tr><th class="style1">题型</th><th>数量</th><th>分数</th></tr>
                 <tr>
                        <td class="style1">单选题</td>
                        <td><asp:Label ID="SingleChoiceQuantityLabel" runat="server" 
                              Text='<%# Bind("SingleChoiceQuantity") %>' />
                        </td> 
                         <td><asp:Label ID="SingleChoiceGradeLabel" runat="server" 
                         Text='<%# Bind("SingleChoiceGrade") %>' /></td>
                     </tr>
                 <tr>
                       <td class="style1">多选题</td>
                       <td><asp:Label ID="MultipleChoiceQuantityLabel" runat="server" 
                           Text='<%# Bind("MultipleChoiceQuantity") %>' /></td>
                     <td><asp:Label ID="MultipleChoiceGradeLabel" runat="server" 
                         Text='<%# Bind("MultipleChoiceGrade") %>' />
                       </td></tr>
                 <tr>
                       <td class="style1">填空题</td>
                       <td><asp:Label ID="CompletionQuantityLabel" runat="server" 
                     Text='<%# Bind("CompletionQuantity") %>' /></td>
                       <td><asp:Label ID="CompletionGradeLabel" runat="server" 
                     Text='<%# Bind("CompletionGrade") %>' /></td>
                 </tr>
                 <tr>   
                       <td class="style1">简答题/计算题</td>
                       <td><asp:Label ID="ShortAnswerQuantityLabel" runat="server" 
                     Text='<%# Bind("ShortAnswerQuantity") %>' /></td>
                       <td> <asp:Label ID="ShortAnswerGradeLabel" runat="server" 
                     Text='<%# Bind("ShortAnswerGrade") %>' /></td>
                     </tr>
                 <tr> 
                       <td class="style1">议论题/综合题</td>
                       <td> <asp:Label ID="DiscussionQuantityLabel" runat="server" 
                     Text='<%# Bind("DiscussionQuantity") %>' /></td>
                       <td> 
                 <asp:Label ID="DiscussionGradeLabel" runat="server" 
                     Text='<%# Bind("DiscussionGrade") %>' /></td>
                 </tr>
                 <tr><td>总分：</td><td>
                     <asp:Label ID="Label1" runat="server" 
                         Text='<%# Eval("amount")  %>'></asp:Label></td></tr>
                 
                 </table>
                 
                 <br />
                
                 <br />
                 <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" 
                     CommandName="Edit" Text="编辑" />
                 &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" 
                     CommandName="Delete" Text="删除" />
                 &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" 
                     CommandName="New" Text="新建" />
             </ItemTemplate>
             <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
             <EmptyDataTemplate>
                 <asp:LinkButton ID="linkNew" runat="server" onclick="linkNew_Click">新建科目</asp:LinkButton>
             </EmptyDataTemplate>
             <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
             <HeaderTemplate>
                 科目详细信息：
             </HeaderTemplate>
             <EditRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
         </asp:FormView>
         <asp:SqlDataSource ID="SqlDataSourceSubjectDetail" runat="server" 
             ConflictDetection="CompareAllValues" 
             ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
             DeleteCommand="DELETE FROM [TestSubjects] WHERE [ID] = @original_ID AND [Name] = @original_Name AND [info] = @original_info AND [SingleChoiceQuantity] = @original_SingleChoiceQuantity AND [SingleChoiceGrade] = @original_SingleChoiceGrade AND [MultipleChoiceQuantity] = @original_MultipleChoiceQuantity AND [MultipleChoiceGrade] = @original_MultipleChoiceGrade AND [CompletionQuantity] = @original_CompletionQuantity AND [CompletionGrade] = @original_CompletionGrade AND [ShortAnswerQuantity] = @original_ShortAnswerQuantity AND [ShortAnswerGrade] = @original_ShortAnswerGrade AND [DiscussionQuantity] = @original_DiscussionQuantity AND [DiscussionGrade] = @original_DiscussionGrade" 
             InsertCommand="INSERT INTO [TestSubjects] ([Name], [info], [SingleChoiceQuantity], [SingleChoiceGrade], [MultipleChoiceQuantity], [MultipleChoiceGrade], [CompletionQuantity], [CompletionGrade], [ShortAnswerQuantity], [ShortAnswerGrade], [DiscussionQuantity], [DiscussionGrade],[isReady]) VALUES (@Name, @info, @SingleChoiceQuantity, @SingleChoiceGrade, @MultipleChoiceQuantity, @MultipleChoiceGrade, @CompletionQuantity, @CompletionGrade, @ShortAnswerQuantity, @ShortAnswerGrade, @DiscussionQuantity, @DiscussionGrade,@isReady)" 
             OldValuesParameterFormatString="original_{0}" 
             SelectCommand="SELECT * ,(((TestSubjects.SingleChoiceQuantity * TestSubjects.SingleChoiceGrade + TestSubjects.MultipleChoiceQuantity * TestSubjects.MultipleChoiceGrade) + TestSubjects.CompletionQuantity * TestSubjects.CompletionGrade) + TestSubjects.ShortAnswerQuantity * TestSubjects.ShortAnswerGrade) + TestSubjects.DiscussionQuantity * TestSubjects.DiscussionGrade AS amount FROM [TestSubjects] WHERE ([ID] = @ID)" 
             
             
             
              
              
              
              UpdateCommand="UPDATE [TestSubjects] SET [Name] = @Name, [info] = @info, [SingleChoiceQuantity] = @SingleChoiceQuantity, [SingleChoiceGrade] = @SingleChoiceGrade, [MultipleChoiceQuantity] = @MultipleChoiceQuantity, [MultipleChoiceGrade] = @MultipleChoiceGrade, [CompletionQuantity] = @CompletionQuantity, [CompletionGrade] = @CompletionGrade, [ShortAnswerQuantity] = @ShortAnswerQuantity, [ShortAnswerGrade] = @ShortAnswerGrade, [DiscussionQuantity] = @DiscussionQuantity, [DiscussionGrade] = @DiscussionGrade ,isReady=@isReady  WHERE [ID] = @original_ID AND [Name] = @original_Name AND [info] = @original_info AND [SingleChoiceQuantity] = @original_SingleChoiceQuantity AND [SingleChoiceGrade] = @original_SingleChoiceGrade AND [MultipleChoiceQuantity] = @original_MultipleChoiceQuantity AND [MultipleChoiceGrade] = @original_MultipleChoiceGrade AND [CompletionQuantity] = @original_CompletionQuantity AND [CompletionGrade] = @original_CompletionGrade AND [ShortAnswerQuantity] = @original_ShortAnswerQuantity AND [ShortAnswerGrade] = @original_ShortAnswerGrade AND [DiscussionQuantity] = @original_DiscussionQuantity AND [DiscussionGrade] = @original_DiscussionGrade">
             <SelectParameters>
                 <asp:ControlParameter ControlID="gvAllSubject" 
                     Name="ID" PropertyName="SelectedValue" Type="Int32" />
             </SelectParameters>
             <DeleteParameters>
                 <asp:Parameter Name="original_ID" Type="Int32" />
                 <asp:Parameter Name="original_Name" Type="String" />
                 <asp:Parameter Name="original_info" Type="String" />
                 <asp:Parameter Name="original_SingleChoiceQuantity" Type="Int32" />
                 <asp:Parameter Name="original_SingleChoiceGrade" Type="Int32" />
                 <asp:Parameter Name="original_MultipleChoiceQuantity" Type="Int32" />
                 <asp:Parameter Name="original_MultipleChoiceGrade" Type="Int32" />
                 <asp:Parameter Name="original_CompletionQuantity" Type="Int32" />
                 <asp:Parameter Name="original_CompletionGrade" Type="Int32" />
                 <asp:Parameter Name="original_ShortAnswerQuantity" Type="Int32" />
                 <asp:Parameter Name="original_ShortAnswerGrade" Type="Int32" />
                 <asp:Parameter Name="original_DiscussionQuantity" Type="Int32" />
                 <asp:Parameter Name="original_DiscussionGrade" Type="Int32" />
             </DeleteParameters>
             <UpdateParameters>
                 <asp:Parameter Name="Name" Type="String" />
                 <asp:Parameter Name="info" Type="String" />
                 <asp:Parameter Name="SingleChoiceQuantity" Type="Int32" />
                 <asp:Parameter Name="SingleChoiceGrade" Type="Int32" />
                 <asp:Parameter Name="MultipleChoiceQuantity" Type="Int32" />
                 <asp:Parameter Name="MultipleChoiceGrade" Type="Int32" />
                 <asp:Parameter Name="CompletionQuantity" Type="Int32" />
                 <asp:Parameter Name="CompletionGrade" Type="Int32" />
                 <asp:Parameter Name="ShortAnswerQuantity" Type="Int32" />
                 <asp:Parameter Name="ShortAnswerGrade" Type="Int32" />
                 <asp:Parameter Name="DiscussionQuantity" Type="Int32" />
                 <asp:Parameter Name="DiscussionGrade" Type="Int32" />
                 <asp:Parameter Name="isReady" />
                 <asp:Parameter Name="original_ID" Type="Int32" />
                 <asp:Parameter Name="original_Name" Type="String" />
                 <asp:Parameter Name="original_info" Type="String" />
                 <asp:Parameter Name="original_SingleChoiceQuantity" Type="Int32" />
                 <asp:Parameter Name="original_SingleChoiceGrade" Type="Int32" />
                 <asp:Parameter Name="original_MultipleChoiceQuantity" Type="Int32" />
                 <asp:Parameter Name="original_MultipleChoiceGrade" Type="Int32" />
                 <asp:Parameter Name="original_CompletionQuantity" Type="Int32" />
                 <asp:Parameter Name="original_CompletionGrade" Type="Int32" />
                 <asp:Parameter Name="original_ShortAnswerQuantity" Type="Int32" />
                 <asp:Parameter Name="original_ShortAnswerGrade" Type="Int32" />
                 <asp:Parameter Name="original_DiscussionQuantity" Type="Int32" />
                 <asp:Parameter Name="original_DiscussionGrade" Type="Int32" />
             </UpdateParameters>
             <InsertParameters>
                 <asp:Parameter Name="Name" Type="String" />
                 <asp:Parameter Name="info" Type="String" />
                 <asp:Parameter Name="SingleChoiceQuantity" Type="Int32" />
                 <asp:Parameter Name="SingleChoiceGrade" Type="Int32" />
                 <asp:Parameter Name="MultipleChoiceQuantity" Type="Int32" />
                 <asp:Parameter Name="MultipleChoiceGrade" Type="Int32" />
                 <asp:Parameter Name="CompletionQuantity" Type="Int32" />
                 <asp:Parameter Name="CompletionGrade" Type="Int32" />
                 <asp:Parameter Name="ShortAnswerQuantity" Type="Int32" />
                 <asp:Parameter Name="ShortAnswerGrade" Type="Int32" />
                 <asp:Parameter Name="DiscussionQuantity" Type="Int32" />
                 <asp:Parameter Name="DiscussionGrade" Type="Int32" />
                 <asp:Parameter Name="isReady" />
             </InsertParameters>
         </asp:SqlDataSource>
         </div>
      <div></div>
   </div>
</asp:Content>

