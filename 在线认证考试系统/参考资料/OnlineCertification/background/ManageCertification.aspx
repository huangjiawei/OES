<%@ Page Language="C#" MasterPageFile="~/background/MasterPage.master" AutoEventWireup="true" CodeFile="ManageCertification.aspx.cs" Inherits="background_Certification" Title="无标题页" %>
<%@ OutputCache Duration="9999999" SqlDependency="CommandNotification" VaryByParam="None" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div>
    <div>
    <h3>认证分类：<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        </h3>
        <asp:GridView ID="gvCategory" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="ID" DataSourceID="SqlDataSourceCategory"  ShowFooter="True"
           BackColor="White" BorderColor="#CCCCCC" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            onselectedindexchanged="gvCategory_SelectedIndexChanged1">
            <RowStyle ForeColor="#000066" />
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                            CommandName="Update" Text="更新"></asp:LinkButton>
                        <asp:ConfirmButtonExtender ID="LinkButton1_ConfirmButtonExtender" 
                            runat="server" ConfirmText="确认更新" Enabled="True" TargetControlID="LinkButton1">
                        </asp:ConfirmButtonExtender>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="取消"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandName="Edit" Text="编辑"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Select" Text="选择"></asp:LinkButton>&nbsp;<asp:LinkButton 
                            ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Delete" 
                            Text="删除"></asp:LinkButton><asp:ConfirmButtonExtender 
                            ID="LinkButton3_ConfirmButtonExtender" runat="server" ConfirmText="确认要删除吗?" 
                            Enabled="True" TargetControlID="LinkButton3">
                        </asp:ConfirmButtonExtender>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="名称" SortExpression="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtName" runat="server" ></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="详细信息" SortExpression="info">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("info") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("info") %>'></asp:Label>
                    </ItemTemplate>
                   <InsertItemTemplate></InsertItemTemplate>
                   <FooterTemplate><asp:TextBox ID="txtInfo" runat="server" ></asp:TextBox></FooterTemplate> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编号" InsertVisible="False" SortExpression="ID">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">新建</asp:LinkButton>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
       
    </div>
    <div >
    <span id="planeCertification" visible="false" runat="server">
    <h4>选中分类下的专业认证：</h4>
        <asp:GridView ID="gvCertification" runat="server" AutoGenerateColumns="False"  Visible="False"
            DataKeyNames="ID" DataSourceID="SqlDataSourceCertification" ShowFooter="True" 
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="3" 
            onselectedindexchanged="gvCertification_SelectedIndexChanged">
            <RowStyle ForeColor="#000066" />
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                            CommandName="Update" Text="更新"></asp:LinkButton>
                             <asp:ConfirmButtonExtender ID="LinkButton1_ConfirmButtonExtender" 
                            runat="server" ConfirmText="确认更新" Enabled="True" TargetControlID="LinkButton1">
                        </asp:ConfirmButtonExtender>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="取消"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
    
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandName="Edit" Text="编辑"></asp:LinkButton>
      
       
                        &nbsp;</span><asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Select" Text="选择"></asp:LinkButton></span>&nbsp;</span><asp:LinkButton 
                            ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Delete" 
                            Text="删除"></asp:LinkButton><asp:ConfirmButtonExtender 
                            ID="LinkButton3_ConfirmButtonExtender" runat="server" ConfirmText="确认要删除吗?" 
                            Enabled="True" TargetControlID="LinkButton3">  
       
                        </asp:ConfirmButtonExtender>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="名称" SortExpression="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="详细信息" SortExpression="info">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("info") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="tbInfo" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("info") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="分类编号" SortExpression="CategoryID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CategoryID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="linkBtnInsert" runat="server" onclick="linkBtnInsert_Click">新建到选中分类</asp:LinkButton>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("CategoryID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="编号" SortExpression="ID" 
                    InsertVisible="False" ReadOnly="True" />
            </Columns>
            
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            
            <EmptyDataTemplate>
                Name：<asp:TextBox ID="newName" runat="server"></asp:TextBox>Info:<asp:TextBox 
                    ID="newInfo" runat="server" TextMode="MultiLine" ></asp:TextBox>分类：<asp:DropDownList 
                    ID="NewCategory" runat="server" DataSourceID="SqlDataSourceCategory" 
                    DataTextField="Name" DataValueField="ID">
                </asp:DropDownList>
                <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton4_Click">新建</asp:LinkButton>
            </EmptyDataTemplate>
            
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            
        </asp:GridView>
        </span>
       
    </div>
    <div>
    <span id="planeSubject" visible="false" runat="server">
    <h5>选中专业认证下的考试科目：</h5>
        <asp:GridView ID="gvSubject" runat="server" AutoGenerateColumns="False" Visible="False"
            DataSourceID="SqlDataSourceSubject" 
            onselectedindexchanged="GridViewSubject_SelectedIndexChanged" 
            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" ShowFooter="True">
            <RowStyle ForeColor="#000066" />
            <Columns>
                <asp:TemplateField HeaderText="编号" SortExpression="ID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="分类编号" 
                    SortExpression="CertificationID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CertificationID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("CertificationID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="科目名称" SortExpression="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlSubject" runat="server" 
                            DataSourceID="SqlDataSourceAllSubjects" DataTextField="Name" 
                            DataValueField="ID">
                        </asp:DropDownList>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="详情" SortExpression="info">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("info") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="lbtnAdd" runat="server" onclick="lbtnAdd_Click">添加科目</asp:LinkButton>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("info") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <EmptyDataTemplate>
                尚未添加科目，请添加科目<asp:DropDownList ID="ddlSubject" runat="server" 
                    DataSourceID="SqlDataSourceAllSubjects" DataTextField="Name" 
                    DataValueField="ID">
                </asp:DropDownList>
                <asp:LinkButton ID="lbtnNew" runat="server" onclick="lbtnNew_Click">添加</asp:LinkButton>
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        </span>
        <div>
        </div>
    </div>
    
</div>
 <asp:SqlDataSource ID="SqlDataSourceCategory" runat="server" 
            ConflictDetection="CompareAllValues" 
            ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
            DeleteCommand="DELETE FROM [CertificationCategory] WHERE [ID] = @original_ID AND [Name] = @original_Name AND (([info] = @original_info) OR ([info] IS NULL AND @original_info IS NULL))" 
            InsertCommand="INSERT INTO [CertificationCategory] ([Name], [info]) VALUES (@Name, @info)" 
            OldValuesParameterFormatString="original_{0}" 
            SelectCommand="SELECT * FROM [CertificationCategory]" 
            UpdateCommand="UPDATE [CertificationCategory] SET [Name] = @Name, [info] = @info WHERE [ID] = @original_ID AND [Name] = @original_Name AND (([info] = @original_info) OR ([info] IS NULL AND @original_info IS NULL))">
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
   <asp:SqlDataSource ID="SqlDataSourceCertification" runat="server" 
            ConflictDetection="CompareAllValues" 
            ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
            DeleteCommand="DELETE FROM [ProfessionalCertification] WHERE [ID] = @original_ID AND [Name] = @original_Name AND [info] = @original_info AND [CategoryID] = @original_CategoryID" 
            InsertCommand="INSERT INTO [ProfessionalCertification] ([Name], [info], [CategoryID]) VALUES (@Name, @info, @CategoryID)" 
            OldValuesParameterFormatString="original_{0}" 
            SelectCommand="SELECT * FROM [ProfessionalCertification] WHERE ([CategoryID] = @CategoryID)" 
            UpdateCommand="UPDATE [ProfessionalCertification] SET [Name] = @Name, [info] = @info, [CategoryID] = @CategoryID WHERE [ID] = @original_ID AND [Name] = @original_Name AND [info] = @original_info AND [CategoryID] = @original_CategoryID">
            <SelectParameters>
                <asp:ControlParameter ControlID="gvCategory" DefaultValue="" Name="CategoryID" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="original_Name" Type="String" />
                <asp:Parameter Name="original_info" Type="String" />
                <asp:Parameter Name="original_CategoryID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="info" Type="String" />
                <asp:Parameter Name="CategoryID" Type="Int32" />
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="original_Name" Type="String" />
                <asp:Parameter Name="original_info" Type="String" />
                <asp:Parameter Name="original_CategoryID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="info" Type="String" />
                <asp:Parameter Name="CategoryID" Type="Int32" />
            </InsertParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceSubject" runat="server" 
            ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
            OldValuesParameterFormatString="original_{0}" 
            
        SelectCommand="SELECT [ID], [CertificationID], [Name], [info] FROM [View_SubjectInCertification] WHERE ([CertificationID] = @CertificationID)" 
        InsertCommand="INSERT INTO Certification_Subject(CertificationID, SubjectID) VALUES (@CertificationID, @SubjectID)">
            <SelectParameters>
                <asp:ControlParameter ControlID="gvCertification" Name="CertificationID" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
            <InsertParameters>
                <asp:Parameter Name="CertificationID" />
                <asp:Parameter Name="SubjectID" />
            </InsertParameters>
        </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceAllSubjects" runat="server" 
    ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
    SelectCommand="SELECT [Name], [ID] FROM [TestSubjects]"></asp:SqlDataSource>
</asp:Content>

