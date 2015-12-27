<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CertificateGenerate.ascx.cs" Inherits="User_CertificateGenerate" %>
认证申请：
         <asp:DropDownList ID="ddlUnPass" runat="server" DataSourceID="sqlUnPass" DataTextField="CertificationID" DataValueField="CertificationID" AutoPostBack="true"></asp:DropDownList>
         <asp:Button ID="btnApply" runat="server" Text="申请" onclick="btnApply_Click" />
         <asp:Label ID="lbStatus" runat="server"></asp:Label>
         <asp:SqlDataSource ID="sqlUnPass" ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
          runat="server"          
             SelectCommand="SELECT CertificationExam_UserEnroll.CertificationID, CertificationExam_UserEnroll.enrollDate, ProfessionalCertification.Name AS Name FROM CertificationExam_UserEnroll INNER JOIN ProfessionalCertification ON CertificationExam_UserEnroll.CertificationID = ProfessionalCertification.ID WHERE (CertificationExam_UserEnroll.UserID = @ID) AND (ProfessionalCertification.Name NOT IN (SELECT CertificateName FROM Certificate WHERE (UserId = @ID)))">
             <SelectParameters>
                 <asp:Parameter Name="ID" />
             </SelectParameters>
         </asp:SqlDataSource>