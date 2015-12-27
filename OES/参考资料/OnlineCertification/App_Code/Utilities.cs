using System;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web;
/// <summary>
/// Class contains miscellaneous functionality 
/// </summary>
public static class Utilities
{
	static Utilities()
	{
		//
		// TODO: Add constructor logic here
		//
	}

  // Generic method for sending emails
  public static void SendMail(string from, string to, string subject, string body)
  {
    // Configure mail client (may need additional
    // code for authenticated SMTP servers)
    SmtpClient mailClient = new SmtpClient(OnlineCertificationConfiguration.MailServer);
    // Create the mail message
    MailMessage mailMessage = new MailMessage(from, to, subject, body);
    /*
       // For SMTP servers that require authentication
       message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
       message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "SmtpHostUserName");
       message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "SmtpHostPassword");
      */
    // Send mail
    mailClient.Send(mailMessage); 
  }

  // Send error log mail
  public static void LogError(Exception ex)
  {
    // get the current date and time
    string dateTime = DateTime.Now.ToLongDateString() + ", at "
                    + DateTime.Now.ToShortTimeString();
    // stores the error message
    string errorMessage = "Exception generated on " + dateTime;
    // obtain the page that generated the error
    System.Web.HttpContext context = System.Web.HttpContext.Current;
    errorMessage += "\n\n Page location: " + context.Request.RawUrl; 
    // build the error message
    errorMessage += "\n\n Message: " + ex.Message; 
    errorMessage += "\n\n Source: " + ex.Source;
    errorMessage += "\n\n Method: " + ex.TargetSite; 
    errorMessage += "\n\n Stack Trace: \n\n" + ex.StackTrace; 
    // send error email in case the option is activated in Web.Config
    if (OnlineCertificationConfiguration.EnableErrorLogEmail)
    {
      //string from = "noreply@cristiandarie.ro";
      //string to = OnlineCertificationConfiguration.ErrorLogEmail;
      //string subject = OnlineCertificationConfiguration.SiteName + " error report";
      //string body = errorMessage;
      //SendMail(from, to, subject, body); 

        using (StreamWriter _testData = new StreamWriter(HttpContext.Current.Server.MapPath(OnlineCertificationConfiguration.ErrorLogFile), true))
        {
            _testData.WriteLine("-----------------------------------------------------------");
            _testData.WriteLine(errorMessage); // Write the file.
            _testData.WriteLine("-----------------------------------------------------------");
        }    
    }
  }


}
