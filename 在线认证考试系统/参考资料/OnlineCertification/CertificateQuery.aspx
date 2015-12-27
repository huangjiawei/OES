<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CertificateQuery.aspx.cs" Inherits="CertificateQuery" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
    <div class="box">
    <h4>请输入证书编号与证书持有人姓名进行证书查询与验证</h4>
         <table>
                <tr><td>证书编号：</td>
                <td>
                    <asp:TextBox ID="tbID" runat="server" Width="200px"></asp:TextBox>
                    </tr>
                <tr><td>姓名：</td>
                    <td><asp:TextBox ID="tbName" runat="server" Width="200px"></asp:TextBox></td></tr>
                  
                        <tr><td>
                            <asp:Button ID="btnQuery" runat="server" Text="查询" onclick="btnQuery_Click" 
                                ForeColor="Black" /></td><td>
        <asp:Label ID="lbStatus" runat="server" ForeColor="Red"></asp:Label>
                            </td></tr>
         </table>
    </div>
    <div class="box">
        <asp:FormView ID="FormView1" runat="server" DataKeyNames="CertificateID"  Visible="False"
            DataSourceID="SqlDataSourceCertificate">      
        
            <EditItemTemplate>
                CertificateID:
                <asp:Label ID="CertificateIDLabel1" runat="server" 
                    Text='<%# Eval("CertificateID") %>' />
                <br />
                CertificateName:
                <asp:TextBox ID="CertificateNameTextBox" runat="server" 
                    Text='<%# Bind("CertificateName") %>' />
                <br />
                CertificateName1:
                <asp:TextBox ID="CertificateName1TextBox" runat="server" 
                    Text='<%# Bind("CertificateName1") %>' />
                <br />
                ownerID:
                <asp:TextBox ID="ownerIDTextBox" runat="server" Text='<%# Bind("ownerID") %>' />
                <br />
                generatedate:
                <asp:TextBox ID="generatedateTextBox" runat="server" 
                    Text='<%# Bind("generatedate") %>' />
                <br />
                photo:
                <asp:TextBox ID="photoTextBox" runat="server" Text='<%# Bind("photo") %>' />
                <br />
                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                    CommandName="Update" Text="更新" />
                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="取消" />
            </EditItemTemplate>
            <InsertItemTemplate>
                CertificateID:
                <asp:TextBox ID="CertificateIDTextBox" runat="server" 
                    Text='<%# Bind("CertificateID") %>' />
                <br />
                CertificateName:
                <asp:TextBox ID="CertificateNameTextBox" runat="server" 
                    Text='<%# Bind("CertificateName") %>' />
                <br />
                CertificateName1:
                <asp:TextBox ID="CertificateName1TextBox" runat="server" 
                    Text='<%# Bind("CertificateName1") %>' />
                <br />
                ownerID:
                <asp:TextBox ID="ownerIDTextBox" runat="server" Text='<%# Bind("ownerID") %>' />
                <br />
                generatedate:
                <asp:TextBox ID="generatedateTextBox" runat="server" 
                    Text='<%# Bind("generatedate") %>' />
                <br />
                photo:
                <asp:TextBox ID="photoTextBox" runat="server" Text='<%# Bind("photo") %>' />
                <br />
                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                    CommandName="Insert" Text="插入" />
                &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="取消" />
            </InsertItemTemplate>
        
            <ItemTemplate>
                证书编号:
                <asp:Label ID="CertificateIDLabel" runat="server" 
                    Text='<%# Eval("CertificateID") %>' />
                <br />
                证书名称:
                <asp:Label ID="CertificateNameLabel" runat="server" 
                    Text='<%# Bind("CertificateName") %>' />
                <br />
                姓名:
                <asp:Label ID="ownerNameLabel" runat="server" Text='<%# Bind("ownerName") %>' />
                <br />
                身份证号:
                <asp:Label ID="ownerIDLabel" runat="server" Text='<%# Bind("ownerID") %>' />
                <br />
                生效日期:
                <asp:Label ID="generatedateLabel" runat="server" 
                    Text='<%# Bind("generatedate") %>' />
                <br />
                <asp:Image ID="photo" runat="server"  Width="100px" Height="130px"
                    
                    ImageUrl='<%# "~/PhotoInCertificate.aspx?CertificateID="+Eval("CertificateID") %>' 
                     />
            </ItemTemplate>
            <EmptyDataTemplate>
                <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                    Text=" 没有该证书记录，请输入正确的证书编号与证书持有人姓名"></asp:Label>
            </EmptyDataTemplate>
        </asp:FormView>
        
        <asp:SqlDataSource ID="SqlDataSourceCertificate" runat="server" 
            ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
            
            SelectCommand="CertificateQuery" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="tbID" Name="CertificateID" PropertyName="Text" 
                    Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>

    </div>
</div>
</asp:Content>

