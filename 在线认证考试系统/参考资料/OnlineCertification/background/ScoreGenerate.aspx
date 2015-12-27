<%@ Page Language="C#" MasterPageFile="~/background/MasterPage.master" AutoEventWireup="true" CodeFile="ScoreGenerate.aspx.cs" Inherits="background_ScoreGenerate" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
     <div>试卷成绩管理</div>
     <div>
         <asp:SqlDataSource ID="SqlDataSourceScore" runat="server" 
             ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
           
              SelectCommand="SELECT [ID], [ObjectiveScore], [SubjectiveScore], [SubjectId] FROM [PaperFinished]
where [SubjectiveScore]=-1 
 ORDER BY [generationDate], [SubjectId]
">
         </asp:SqlDataSource>
         <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
             AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" 
             DataSourceID="SqlDataSourceScore">
             <Columns>
                 <asp:BoundField DataField="ID" HeaderText="试卷编号" ReadOnly="True" 
                     SortExpression="ID" />
                 <asp:BoundField DataField="ObjectiveScore" HeaderText="客观题总分" 
                     SortExpression="ObjectiveScore" />
                 <asp:BoundField DataField="SubjectiveScore" HeaderText="主观题总分" 
                     SortExpression="SubjectiveScore" />
                 <asp:BoundField DataField="SubjectId" HeaderText="科目编号" 
                     SortExpression="SubjectId" />
                 <asp:TemplateField ShowHeader="False" HeaderText="总分统计">
                     <ItemTemplate>
                         <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                             CommandName="" onclick="LinkButton1_Click" Text="计算主观题总分"></asp:LinkButton>
                         <asp:LinkButton ID="LinkButton2" Text="计算主观题总分" runat="server" 
                             onclick="LinkButton2_Click"></asp:LinkButton>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
         </asp:GridView>
          <p>
        <asp:Label ID="lbRemind" runat="server" Text="总分为-1表示未评分或未统计总分" ForeColor="#FF3300"></asp:Label></p>
     </div>
     <div></div>

</div>
</asp:Content>

