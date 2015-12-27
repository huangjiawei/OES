<%@ Page Language="C#" MasterPageFile="~/background/MasterPage.master" AutoEventWireup="true" CodeFile="PaperQuery.aspx.cs" Inherits="background_PaperQuery" Title="无标题页" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>科目:<asp:DropDownList ID="DropDownList1" runat="server" 
        DataSourceID="SqlDataSourceSubject" DataTextField="Name" DataValueField="ID">
    </asp:DropDownList><asp:SqlDataSource ID="SqlDataSourceSubject" runat="server"  CacheDuration="Infinite" EnableCaching="true" SqlCacheDependency="CommandNotification" 
        ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
        SelectCommand="SELECT [ID], [Name] FROM [TestSubjects]">
    </asp:SqlDataSource>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <br />
    时间范围:
   <asp:TextBox ID="TextBox1"
        runat="server"></asp:TextBox><asp:CalendarExtender 
        ID="TextBox1_CalendarExtender" runat="server" Enabled="True" 
        TargetControlID="TextBox1" Format="yyyy-MM-dd">
    </asp:CalendarExtender> 到
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><asp:CalendarExtender 
        ID="TextBox2_CalendarExtender" runat="server" Enabled="True" 
        TargetControlID="TextBox2"   Format="yyyy-MM-dd">
    </asp:CalendarExtender>
    <asp:Button ID="btnQuery" runat="server" 
        Text="查看" onclick="btnQuery_Click" />
</div>
<div>
    <asp:SqlDataSource ID="SqlDataSourcePaper" runat="server" 
        ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" SelectCommand="SELECT [generationDate], [SubjectId], [ObjectiveScore], [SubjectiveScore], [ID] 
FROM [PaperFinished]
where [generationDate] between @date1 and @date2 and [SubjectId]=@subjectId
 ORDER BY [generationDate]">
        <SelectParameters>
            <asp:Parameter Name="date1" />
            <asp:Parameter Name="date2" />
            <asp:Parameter Name="subjectId" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ID" DataSourceID="SqlDataSourcePaper">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="编号" ReadOnly="True" 
                SortExpression="ID" />
            <asp:BoundField DataField="ObjectiveScore" HeaderText="客观题总分" 
                SortExpression="ObjectiveScore" />
            <asp:BoundField DataField="SubjectiveScore" HeaderText="主观题总分" 
                SortExpression="SubjectiveScore" />
            <asp:BoundField DataField="generationDate" HeaderText="试卷生成时间" 
                SortExpression="generationDate" />
        </Columns>
    </asp:GridView> 
    <p>
        <asp:Label ID="lbRemind" runat="server" Text="总分为-1表示未评分或未统计总分" ForeColor="#FF3300"></asp:Label></p>
    </div>
   
</asp:Content>

