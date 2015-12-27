<%@ Page Language="C#" MasterPageFile="~/User/UserPage.master" AutoEventWireup="true" CodeFile="ScoreQuery.aspx.cs" Inherits="User_ScoreQuery" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<div>
    <div>成绩查询：</div>
    <div>
        <asp:GridView ID="gvExamTaken" runat="server" AutoGenerateColumns="False"  EnableCaching="true" CacheDuration="Infinite" SqlCacheDependency="CommandNotification"
            DataKeyNames="ID" DataSourceID="SqlDataSourceExamTakenRecently" 
            onrowcommand="gvExamTaken_RowCommand">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="科目" SortExpression="Name" />
                <asp:BoundField DataField="generationDate" HeaderText="考试日期" 
                    SortExpression="generationDate" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="查看成绩"  CommandName="query"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                最近未参加考试！
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceExamTakenRecently" runat="server"  EnableCaching="true" CacheDuration="Infinite" SqlCacheDependency="CommandNotification"
            ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
            
            SelectCommand="SELECT PaperFinished.ID, PaperFinished.generationDate, TestSubjects.Name, PaperFinished.UserId FROM dbo.PaperFinished INNER JOIN dbo.TestSubjects ON dbo.PaperFinished.SubjectId = dbo.TestSubjects.ID WHERE (dbo.PaperFinished.UserId = @UserId) ORDER BY dbo.PaperFinished.generationDate">
            <SelectParameters>
                <asp:Parameter Name="UserId" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    <div>
        <asp:Label ID="lbScore" runat="server" Text=""></asp:Label>
    </div>
</div>
</asp:Content>

