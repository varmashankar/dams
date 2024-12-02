using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;
using System.Net.Mail;
using System.Net;
using System.IO;
using Newtonsoft.Json;


public partial class registration : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            /*all the data from the inputs*/
            Guid patient_id = Guid.NewGuid();
            string firstname = txtFirstName.Text;
            string lastname = txtLastName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            DateTime dob = DateTime.Parse(txtDob.Text);
            string address = txtAddress.Text.Trim();
            string password = txtPassword.Text;
            string gender = ddlGender.SelectedValue;
            string emergencyContact = txtEmergencyContact.Text.Trim();
            // Hash the password
            string passwordHash = HashPassword(password);

            // Check if terms are accepted
            if (!cbTerms.Checked)
            {
                cvTerms.IsValid = false;
                return;
            }

            string query = @"INSERT INTO patient_master (patient_id,firstname, lastname, email, phone, dob, address, passwordHash, gender, emergency_contact, terms_accepted, registrationDate) VALUES (@patient_id, @firstname, @lastname, @email, @phone, @dob, @address, @passwordHash, @gender, @emergency_contact, @terms_accepted, getdate())";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@patient_id", patient_id);
                    cmd.Parameters.AddWithValue("@firstname", firstname);
                    cmd.Parameters.AddWithValue("@lastname", lastname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@dob", dob);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@gender",gender);
                    cmd.Parameters.AddWithValue("@emergency_contact", emergencyContact);
                    cmd.Parameters.AddWithValue("@terms_accepted", cbTerms.Checked);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SendConfirmationEmail(email, firstname);

                    // Set session variables
                    Session["Email"] = email;
                    Session["Phone"] = phone;
                    Session["PatientID"] = patient_id;

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Registration is Successfull. Click Ok to go to Dashboard.'); window.location.href='patient/dashboard.aspx';", true);
                    
                }
            }
        }
        catch (Exception ex)
        {
            string message = HttpUtility.JavaScriptStringEncode(ex.Message);
            Response.Write("<script>alert('" + message + "');</script>");
            
        }
    }


    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    protected void cvTerms_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // Validate that the checkbox is checked
        args.IsValid = cbTerms.Checked;
    }

    private void SendConfirmationEmail(string toEmail, string userName)
    {
        var emailSettings = ConfigLoader.LoadEmailSettings();

        string subject = "Welcome to DOCTIME!";
        string body = $@"
<div style='font-family: Arial, sans-serif; color: #333; padding: 20px; background-color: #f9f9f9; border: 1px solid #ddd; border-radius: 8px;'>
<h2 style='color: #4CAF50; text-align: center;'>Welcome to DOCTIME!</h2>
<p style='font-size: 16px; color: #555;'>Dear {userName},</p>
<p style='font-size: 16px; color: #555;'>Thank you for registering with DOCTIME! We're excited to have you on board.</p>
<p style='font-size: 16px; color: #555;'>You can now log in to your account, explore services, and schedule appointments with ease.</p>
<br/>
<div style='text-align: center;'>
<a href='https://www.doctime.com/login' style='background-color: #4CAF50; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px; font-size: 16px;'>Log In to Your Account</a>
</div>
<br/>
<p style='font-size: 14px; color: #777;'>Thank you for choosing DOCTIME!</p>
<p style='font-size: 12px; color: #999; text-align: center;'>This is an automated message. Please do not reply to this email.</p>
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
                smtp.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);
                smtp.EnableSsl = true;
                smtp.Timeout = 20000;

                try
                {
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    // Handle the exception (you can choose to log it or alert the user)
                    string message = HttpUtility.JavaScriptStringEncode(ex.Message);
                    Response.Write("<script>alert('Error sending email: " + message + "');</script>");
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


