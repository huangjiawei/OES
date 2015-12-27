<%@ Page Language="C#" MasterPageFile="~/User/UserPage.master" AutoEventWireup="true" CodeFile="EnrollSubjectExam.aspx.cs" Inherits="User_EnrollSubjectExam" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div><span>认证考试：</span>
    <asp:DropDownList ID="ddlCertificationEnrolled" runat="server" 
        DataSourceID="SqlDataSourceCertificationEnrolled" DataTextField="CertificationName" 
        DataValueField="CertificationID" Width="82px" AutoPostBack="True">
        <asp:ListItem Selected="True" Value="0">全部</asp:ListItem>
    </asp:DropDownList>  
    
    
      <asp:SqlDataSource ID="SqlDataSourceCertificationEnrolled" runat="server" EnableCaching="true" CacheDuration="Infinite" SqlCacheDependency="CommandNotification"
                    ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
                    
        
            SelectCommand="SELECT ProfessionalCertification.Name as CertificationName,CertificationExam_UserEnroll.CertificationID as CertificationID  FROM CertificationExam_UserEnroll INNER JOIN aspnet_Users ON CertificationExam_UserEnroll.UserID = aspnet_Users.UserId INNER JOIN ProfessionalCertification ON CertificationExam_UserEnroll.CertificationID = ProfessionalCertification.ID WHERE (aspnet_Users.UserName = @userName)">
                    <SelectParameters>
                        <asp:Parameter Name="userName" />
                    </SelectParameters>
                </asp:SqlDataSource>

        <br />
        <asp:GridView ID="gvSubject" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" DataSourceID="SqlDataSourceSubject"  DataKeyNames="ID"
            onrowcommand="gvSubject_RowCommand">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="开考科目" SortExpression="Name" />
                <asp:ButtonField CommandName="enroll" Text="报考" />
            </Columns>
            <EmptyDataTemplate>
                尚未开考，请耐心等候，感谢合作！
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceSubject" runat="server" EnableCaching="true" CacheDuration="Infinite" SqlCacheDependency="CommandNotification"
            ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
            SelectCommand="SELECT TestSubjects.Name, TestSubjects.info, TestSubjects.ID, Certification_Subject.CertificationID FROM TestSubjects INNER JOIN Certification_Subject ON TestSubjects.ID = Certification_Subject.SubjectID
where Certification_Subject.CertificationID=@CertificationId and isReady=1;">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlCertificationEnrolled" 
                    Name="CertificationId" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
    
    
</div>
</asp:Content>

