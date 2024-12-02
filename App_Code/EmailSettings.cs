using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmailSettings
/// </summary>
public class EmailSettings
{
    public string FromAddress { get; set; }
    public string DisplayName { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool EnableSsl { get; set; }
    public int Timeout { get; set; }
}
