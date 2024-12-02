using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.IO;



public partial class forgotpassword : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim();

        if (IsEmailRegistered(email))
        {
            string otp = GenerateOTP();
            SaveOTP(email, otp);
            SendOTPEmail(email, otp);
            string script = $@"
    <script type='text/javascript'>
        // Display a toast notification
        alert('OTP sent successfully to your email.');
        setTimeout(function() {{
            window.location.href = 'VerifyOTP.aspx?email={HttpUtility.JavaScriptStringEncode(email)}';
        }}, 300);  // 3 seconds delay
    </script>";

            // Register the script on the page
            ClientScript.RegisterStartupScript(this.GetType(), "RedirectWithToast", script);
        }
        else
        {
            string script = @"
                            <script type='text/javascript'>
                                // Display a toast notification
                            toastr.error('Email Not Found. Please Try Again.');
                        </script>";

            // Register the script on the page
            ClientScript.RegisterStartupScript(this.GetType(), "RedirectWithToast", script);
        }
    }

    private bool IsEmailRegistered(string email)
    {
        bool isRegistered = false;

        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM patient_master WHERE email = @Email", con))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                isRegistered = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
        return isRegistered;
    }

    private string GenerateOTP()
    {
        Random rand = new Random();
        return rand.Next(100000, 999999).ToString();
    }

    private void SaveOTP(string email, string otp)
    {

        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE patient_master SET OTP = @OTP, OTPExpiry = @Expiry WHERE email = @Email", con))
                {
                    cmd.Parameters.AddWithValue("@OTP", otp);
                    cmd.Parameters.AddWithValue("@Expiry", DateTime.Now.AddMinutes(10));
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex) 
        {
            string message = HttpUtility.JavaScriptStringEncode(ex.Message);
            Response.Write("<script>alert('" + message + "');</script>");
        }

    }

    private void SendOTPEmail(string toEmail, string otp)
    {

        var emailSettings = ConfigLoader.LoadEmailSettings();


        string subject = "OTP for Password Reset";
        string body = $@"
    <div style='font-family: Arial, sans-serif; color: #333; padding: 20px;'>
        <h2 style='color: #4CAF50;'>DOCTIME OTP Verification</h2>
        <p style='font-size: 16px;'>Dear User,</p>
        <p style='font-size: 16px;'>Your OTP code is <strong style='font-size: 20px; color: #FF5733;'>{otp}</strong>.</p>
        <p style='font-size: 16px;'>Please enter this code within the next 10 minutes to verify your identity.</p>
        <p style='font-size: 16px;'>If you did not request this code, please ignore this email.</p>
        <br/>
        <p style='font-size: 14px; color: #777;'>Thank you,<br/>The DOCTIME Team</p>
    </div>";

        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress(emailSettings.FromAddress, emailSettings.DisplayName); // Change to your Gmail address
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;  // Set to true to send HTML email

            using (SmtpClient smtp = new SmtpClient(emailSettings.SmtpServer, emailSettings.Port))
            {
                smtp.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password); // Use App Password here
                smtp.EnableSsl = true;
                smtp.Timeout = 20000; // Set timeout as needed

                try
                {
                    smtp.Send(mail);
                }
                catch (SmtpFailedRecipientException ex)
                {
                    string message = HttpUtility.JavaScriptStringEncode(ex.Message);
                    Response.Write("<script>alert('Failed to send OTP: " + message + "');</script>");
                }
                catch (SmtpException ex)
                {
                    string message = HttpUtility.JavaScriptStringEncode(ex.Message);
                    Response.Write("<script>alert('SMTP error: " + message + "');</script>");
                }
                catch (Exception ex)
                {
                    string message = HttpUtility.JavaScriptStringEncode(ex.Message);
                    Response.Write("<script>alert('An error occurred: " + message + "');</script>");
                }
            }
        }
    }

    public static class ConfigLoader
    {
        public static EmailSettings LoadEmailSettings()
        {
            string jsonFilePath = HttpContext.Current.Server.MapPath("~/appsettings.json"); // Adjust the path if needed
            string json = File.ReadAllText(jsonFilePath);
            var config = JsonConvert.DeserializeObject<Configuration>(json);
            return config.EmailSettings;
        }

        private class Configuration
        {
            public EmailSettings EmailSettings { get; set; }
        }
    }

}