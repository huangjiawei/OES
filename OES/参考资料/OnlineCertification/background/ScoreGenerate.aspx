<%@ Page Language="C#" MasterPageFile="~/background/MasterPage.master" AutoEventWireup="true" CodeFile="ScoreGenerate.aspx.cs" Inherits="background_ScoreGenerate" Title="�ޱ���ҳ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
     <div>�Ծ�ɼ�����</div>
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
                 <asp:BoundField DataField="ID" HeaderText="�Ծ���" ReadOnly="True" 
                     SortExpression="ID" />
                 <asp:BoundField DataField="ObjectiveScore" HeaderText="�͹����ܷ�" 
                     SortExpression="ObjectiveScore" />
                 <asp:BoundField DataField="SubjectiveScore" HeaderText="�������ܷ�" 
                     SortExpression="SubjectiveScore" />
                 <asp:BoundField DataField="SubjectId" HeaderText="��Ŀ���" 
                     SortExpression="SubjectId" />
                 <asp:TemplateField ShowHeader="False" HeaderText="�ܷ�ͳ��">
                     <ItemTemplate>
                         <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                             CommandName="" onclick="LinkButton1_Click" Text="�����������ܷ�"></asp:LinkButton>
                         <asp:LinkButton ID="LinkButton2" Text="�����������ܷ�" runat="server" 
                             onclick="LinkButton2_Click"></asp:LinkButton>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
         </asp:GridView>
          <p>
        <asp:Label ID="lbRemind" runat="server" Text="�ܷ�Ϊ-1��ʾδ���ֻ�δͳ���ܷ�" ForeColor="#FF3300"></asp:Label></p>
     </div>
     <div></div>

</div>
</asp:Content>

