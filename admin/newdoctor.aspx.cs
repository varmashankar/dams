using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Management;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.IO;

public partial class admin_newdoctor : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["role"] == null)
        {

            string script = @"
        <script type='text/javascript'>
            alert('Session Expired. Kindly Login Again.');
            window.location.href = '../doctorlogin.aspx';
        </script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sessionExpired", script, true);

            // Stop further processing of the page
            Context.ApplicationInstance.CompleteRequest();
        }
        else
        {
            if (!IsPostBack)
            {
                txtFullName.Focus();
                populateServices();  // Populate the dropdown only on the first load
            }
            
        }
    }

    protected void btnAddDoctor_Click(object sender, EventArgs e)
    {
        string doctorName = txtFullName.Text.Trim();
        string specialty = ddlSpecialty.SelectedItem.Text;  // departmentName (displayed text)
        string doctorEmail = txtEmail.Text.Trim();
        string phone = txtPhone.Text.Trim();
        string role = "Doctor";
        string createdBy = Session["Username"].ToString();
        string passwordHash = HashPassword(phone);
        string departmentID = ddlSpecialty.SelectedValue;  // Retrieves departmentID from dropdown

        try
        {
            
            // Check if a valid departmentID is selected
            if (departmentID == "0")
            {
                lblMessage.Text = "Invalid Specialty selected.";
                lblMessage.CssClass = "text-danger";
                return;
            }

            // Insert query
            string query = "INSERT INTO doctors (DoctorID, FullName, Specialty, phone, Email, DepartmentID, passwordHash, role, CreatedBy) " +
                           "VALUES (@DoctorID, @FullName, @Specialty, @phone, @Email, @DepartmentID, @passwordHash, @role, @CreatedBy)";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters
                    cmd.Parameters.AddWithValue("@DoctorID", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@FullName", doctorName);
                    cmd.Parameters.AddWithValue("@Specialty", specialty);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Email", doctorEmail);
                    cmd.Parameters.AddWithValue("@DepartmentID", departmentID); // Set departmentID
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@Role", role);
                    cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    // Send email notification to the doctor
                    SendAccountCreationEmail(doctorEmail, doctorName);

                    // Clear form fields after successful addition
                    clearall();
                    lblMessage.Text = "Doctor added successfully.";
                    lblMessage.CssClass = "text-success fw-bolder";
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
            lblMessage.CssClass = "text-danger";
        }
    }





    void clearall()
    {
        txtFullName.Text = "";
        ddlSpecialty.SelectedValue = null;
        txtEmail.Text = "";
        txtPhone.Text = null;
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

    private void populateServices()
    {
        string query = "SELECT departmentID, departmentName FROM department";

        ddlSpecialty.Items.Clear();

        try
        {
            // Establish a connection to the database
            using (SqlConnection con = new SqlConnection(strcon))
            {
                // Open the connection
                con.Open();

                // Create a command to execute the query
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Execute the command and use SqlDataReader to read the data
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if there are rows
                        if (reader.HasRows)
                        {
                            // Loop through the results
                            while (reader.Read())
                            {
                                // Create a ListItem for each row (ServiceID as value, ServiceName as text)
                                ListItem item = new ListItem(reader["departmentName"].ToString(), reader["departmentID"].ToString());

                                // Add the item to the DropDownList
                                ddlSpecialty.Items.Add(item);
                            }
                        }
                    }
                }
            }

            // Optionally, add a default "Select" item at the top of the dropdown
            ddlSpecialty.Items.Insert(0, new ListItem("-- Select a Service --", "0"));
        }
        catch (Exception ex)
        {
            // Handle any exceptions (e.g., display an error message)
            lblMessage.Text = "Error: " + ex.Message;
        }

    }

    private void SendAccountCreationEmail(string doctorEmail, string doctorName)
    {
        string subject = "Your DOCTIME Account Has Been Created";
        string body = $@"
<div style='font-family: Arial, sans-serif; color: #333; padding: 20px; border: 1px solid #E0E0E0; border-radius: 8px; background-color: #FFFFFF;'>
    <div style='background-color: #4CAF50; color: #FFFFFF; padding: 10px; border-radius: 8px 8px 0 0;'>
        <h2 style='margin: 0;'>Welcome to DOCTIME, Dr.{doctorName}!</h2>
    </div>
    <div style='padding: 20px;'>
        <p style='font-size: 16px; color: #424242;'>Dear <strong>Dr.{doctorName}</strong>,</p>
        <p style='font-size: 16px; color: #424242;'>Your DOCTIME account has been successfully created. You can now log in to the system using your registered email-address or phone number and the password is your phone number.</p>
        <p style='font-size: 16px; color: #424242;'>If you have any questions, please don't hesitate to contact our support team.</p>
        <p style='font-size: 16px; color: #424242;'>Thank you for joining DOCTIME, and we look forward to working with you!</p>
    </div>
    <div style='background-color: #F5F5F5; color: #777; padding: 10px; border-radius: 0 0 8px 8px; text-align: center;'>
        <p style='font-size: 14px; margin: 0;'>The DOCTIME Team<br/>1234 Health St, Wellness City, HC 56789<br/>Phone: (123) 456-7890 | Email: support@doctime.com</p>
    </div>
</div>";


        using (MailMessage mail = new MailMessage())
        {
            var emailSettings = ConfigLoader.LoadEmailSettings();

            mail.From = new MailAddress(emailSettings.FromAddress, emailSettings.DisplayName); // Change to your Gmail address
            mail.To.Add(doctorEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;  // Set to true to send HTML email

            using (SmtpClient smtp = new SmtpClient(emailSettings.SmtpServer, emailSettings.Port))
            {
                smtp.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);
                smtp.EnableSsl = true;
                smtp.Timeout = 20000; // Set timeout as needed

                try
                {
                    smtp.Send(mail);
                }
                catch (SmtpFailedRecipientException ex)
                {
                    string message = HttpUtility.JavaScriptStringEncode(ex.Message);
                    Response.Write("<script>alert('Failed to send booking confirmation: " + message + "');</script>");
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