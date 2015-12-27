<%@ Page Language="C#" MasterPageFile="~/User/UserPage.master" AutoEventWireup="true" CodeFile="ExamEnrolled.aspx.cs" Inherits="User_ExamEnrolled" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
          <div>
            <div>已参加的认证考试：<asp:DataList ID="dlCertificationEnrolled" runat="server" 
                    DataSourceID="SqlDataSourceCertificationEnrolled">
                <ItemTemplate>
                    &nbsp;<asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' 
                        Font-Bold="True" ForeColor="#0000CC" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 报名日期:
                    <asp:Label ID="enrollDateLabel" runat="server" 
                        Text='<%# Eval("enrollDate") %>' />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        
                    
                    <br />
                </ItemTemplate>
                </asp:DataList>
                  
                <asp:SqlDataSource ID="SqlDataSourceCertificationEnrolled" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
                    SelectCommand="SELECT ProfessionalCertification.Name, CertificationExam_UserEnroll.enrollDate, aspnet_Users.UserName, CertificationExam_UserEnroll.CertificationID FROM CertificationExam_UserEnroll INNER JOIN aspnet_Users ON CertificationExam_UserEnroll.UserID = aspnet_Users.UserId INNER JOIN ProfessionalCertification ON CertificationExam_UserEnroll.CertificationID = ProfessionalCertification.ID WHERE (aspnet_Users.UserName = @userName)">
                    <SelectParameters>
                        <asp:Parameter Name="userName" />
                    </SelectParameters>
                </asp:SqlDataSource>
                  
            </div>
            <div>已报考的科目：</div>
          <asp:DataList ID="dlSubjectEnroll" runat="server" 
                  DataSourceID="SqlDataSourceSubjectEnrolled" 
                  onitemcommand="dlSubjectEnroll_ItemCommand">
              <ItemTemplate>
                  &nbsp;<asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                  &nbsp;&nbsp;&nbsp;&nbsp; 报名日期:
                  <asp:Label ID="enrollDateLabel" runat="server" 
                      Text='<%# Eval("enrollDate") %>' />
                  &nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:LinkButton ID="lbtnTakeExam" runat="server" Text="进入考试"
                  CommandArgument='<%# Eval("ID") %>' CommandName="TakeExam"></asp:LinkButton>
                  <br />
              </ItemTemplate>
          </asp:DataList>
              <asp:SqlDataSource ID="SqlDataSourceSubjectEnrolled" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:examinationConnectionString %>" 
                  
                  SelectCommand="SELECT TestSubjects.Name, TestSubjects.SingleChoiceQuantity, TestSubjects.SingleChoiceGrade, TestSubjects.MultipleChoiceQuantity, TestSubjects.MultipleChoiceGrade, TestSubjects.CompletionQuantity, TestSubjects.CompletionGrade, TestSubjects.ShortAnswerQuantity, TestSubjects.ShortAnswerGrade, TestSubjects.DiscussionQuantity, TestSubjects.DiscussionGrade, TestSubjects.info, SubjectExam_UserEnroll.enrollDate, aspnet_Users.UserName, TestSubjects.ID FROM aspnet_Users INNER JOIN SubjectExam_UserEnroll ON aspnet_Users.UserId = SubjectExam_UserEnroll.UserID INNER JOIN TestSubjects ON SubjectExam_UserEnroll.SubjectID = TestSubjects.ID WHERE (aspnet_Users.UserName = @userName)">
                  <SelectParameters>
                      <asp:Parameter Name="userName" />
                  </SelectParameters>
              </asp:SqlDataSource>
      </div>
</asp:Content>

