using System;
using System.Configuration;

/// <summary>
/// Repository for BalloonShop configuration settings
/// </summary>
public static class OnlineCertificationConfiguration
{
 
  // Caches the connection string
  private readonly static string dbConnectionString;
  // Caches the data provider name 
  private readonly static string dbProviderName;


  // Store the name of your shop
  private readonly static string siteName;

  // Initialize various properties in the constructor
  static OnlineCertificationConfiguration()
  {

      dbConnectionString = ConfigurationManager.ConnectionStrings["examinationConnectionString"].ConnectionString;
      dbProviderName = ConfigurationManager.ConnectionStrings["examinationConnectionString"].ProviderName;
  
    siteName = ConfigurationManager.AppSettings["SiteName"];
  }


  // Returns the connection string for the BalloonShop database
  public static string DbConnectionString
  {
    get
    {
      return dbConnectionString;
    }
  }

  // Returns the data provider name
  public static string DbProviderName
  {
    get
    {
      return dbProviderName;
    }
  }




  // Returns the address of the mail server
  public static string MailServer
  {
    get
    {
      return ConfigurationManager.AppSettings["MailServer"];
    }
  }

  // Send error log emails?
  public static bool EnableErrorLogEmail
  {
    get
    {
      return bool.Parse(ConfigurationManager.AppSettings["EnableErrorLogEmail"]);
    }
  }

  // Returns the email address where to send error reports
  public static string ErrorLogEmail
  {
    get
    {
      return ConfigurationManager.AppSettings["ErrorLogEmail"];
    }
  }

  // Returns the length of product descriptions in products lists
  public static string SiteName
  {
    get
    {
      return siteName;
    }
  }

  public static string ErrorLogFile
  {
      get
      {
          return ConfigurationManager.AppSettings["ErrorLogFile"];
      }
  }
  
}
