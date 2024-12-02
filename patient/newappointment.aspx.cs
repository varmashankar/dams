using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Newtonsoft.Json;


public partial class patient_newappointment : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if the session variables are set
        if (Session["Email"] == null || Session["Phone"] == null)
        {
            string script = @"
                <script type='text/javascript'>
                    alert('Session Expired. Kindly Login Again.');
                        window.location.href = '../login.aspx';
                </script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sessionExpired", script, true);
        }
        else
        {
            if (!IsPostBack)
            {
                PopulateTimeSlots();
                populateServices();
            }
        }
        
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (Page.IsValid)
        {
            DateTime appointmentDate;
            DateTime.TryParse(txtDate.Text, out appointmentDate); // Handle date parsing
            string department = ddlDepartment.SelectedValue;
            string appointmentTime = ddlTimeSlot.SelectedValue;
            string appointmentID = GenerateAppointmentID();

            //getting patientID
            string email = Session["email"].ToString();
            string phone = Session["phone"].ToString();
            var patientDetails = GetPatientDetails(email, phone);

            string patientID = patientDetails.Item1;
            string firstName = patientDetails.Item2;
            string lastName = patientDetails.Item3;


            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    string newAppointment = @"INSERT INTO Appointments (appointmentID, PatientID, AppointmentTime, AppointmentDate, DepartmentID, Status, DateCreated)
                VALUES (@AppointmentID, @PatientID, @AppointmentTime, @AppointmentDate, @DepartmentID, @Status, @DateCreated)";

                    using (SqlCommand cmd = new SqlCommand(newAppointment, con))
                    {
                        cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);
                        cmd.Parameters.AddWithValue("@PatientID", patientID);
                        cmd.Parameters.AddWithValue("@AppointmentTime", appointmentTime);
                        cmd.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
                        cmd.Parameters.AddWithValue("@DepartmentID", department);
                        cmd.Parameters.AddWithValue("@Status", "Pending"); // Default status
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        //sending the confirmation mail
                        string patientEmail = email;
                        string patientName = firstName + lastName;
                        string departmentName = department;
                        DateTime appointmentDates;
                        DateTime.TryParse(txtDate.Text, out appointmentDates);
                        string time = appointmentTime;
                        SendBookingConfirmationEmail(patientEmail, patientName, appointmentTime, appointmentDates);


                        if (rowsAffected > 0)
                        {
                            Response.Redirect("AppointmentDetails.aspx?appointmentID=" + appointmentID);
                        }
                        else
                        {
                            DisplayToastNotification("Error booking appointment. Please try again.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = HttpUtility.JavaScriptStringEncode(ex.Message);
                Response.Write("<script>alert('" + message + "');</script>");
            }
        }

    }

    private void populateServices()
    {
        string query = "SELECT departmentID, departmentName FROM department";

        ddlDepartment.Items.Clear();

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
                                ddlDepartment.Items.Add(item);
                            }
                        }
                    }
                }
            }

            // Optionally, add a default "Select" item at the top of the dropdown
            ddlDepartment.Items.Insert(0, new ListItem("-- Select a Service --", "0"));
        }
        catch (Exception ex)
        {
            string message = HttpUtility.JavaScriptStringEncode(ex.Message);
            Response.Write("<script>alert('" + message + "');</script>");
        }

    }

    public Tuple<string, string, string> GetPatientDetails(string email, string phone)
    {
        string patientID = null;
        string firstName = null;
        string lastName = null;

        using (SqlConnection con = new SqlConnection(strcon))
        {
            // Modify the query to select patient_id, first_name, and last_name
            string query = "SELECT patient_id, firstname, lastname FROM patient_master WHERE email = @Email AND phone = @Phone";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);

                con.Open();

                // Use SqlDataReader to fetch the result
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) // Ensure there's at least one record
                    {
                        patientID = reader["patient_id"].ToString();
                        firstName = reader["firstname"].ToString();
                        lastName = reader["lastname"].ToString();
                    }
                }
            }
        }

        // Return a tuple containing patientID, firstName, and lastName
        return Tuple.Create(patientID, firstName, lastName);
    }


    private string GenerateAppointmentID()
    {
        Random random = new Random();
        int randomNumber = random.Next(10000, 99999);

        string appointmentID = "DAMS-" + randomNumber.ToString();
        return appointmentID;
    }

    private void DisplayToastNotification(string message)
    {
        string script = $@"
        <script type='text/javascript'>
            toastr.error('{message}');
        </script>";

        // Register the script on the page
        ClientScript.RegisterStartupScript(this.GetType(), "toastNotification", script);
    }

    private void SendBookingConfirmationEmail(string toEmail, string patientName, string appointmentTime, DateTime appointmentDates)
    {
        string subject = "Your Appointment Booking Request.";
        string body = $@"
<div style='font-family: Arial, sans-serif; color: #333; padding: 20px; border: 1px solid #ddd; border-radius: 8px;'>
    <div style='background-color: #3fd0b5; color: #fff; padding: 10px; border-radius: 8px 8px 0 0;'>
        <h2 style='margin: 0;'>DOCTIME Appointment Request</h2>
    </div>
    <div style='padding: 20px;'>
        <p style='font-size: 16px;'>Dear <strong>{patientName}</strong>,</p>
        <p style='font-size: 16px;'>We are pleased to inform you about your appointment, We received your booking request and we will confirm it after checking the availability.</p>
        <p style='font-size: 16px;'><strong>Appointment Details:</strong></p>
        <ul style='font-size: 16px;'>
            <li><strong>Time:</strong> {appointmentTime}</li>
            <li><strong>Date:</strong> {appointmentDates}</li>
        </ul>
        <p style='font-size: 16px;'>Please arrive at least 15 minutes before your scheduled appointment time. If you need to reschedule or have any questions, do not hesitate to contact us.</p>
        <p style='font-size: 16px;'>Thank you for choosing DOCTIME!</p>
    </div>
    <div style='background-color: #f1f1f1; color: #777; padding: 10px; border-radius: 0 0 8px 8px; text-align: center;'>
        <p style='font-size: 14px; margin: 0;'>The DOCTIME Team<br/>1234 Health St, Wellness City, HC 56789<br/>Phone: (123) 456-7890 | Email: support@doctime.com</p>
    </div>
</div>";

        using (MailMessage mail = new MailMessage())
        {
            var emailSettings = ConfigLoader.LoadEmailSettings();

            mail.From = new MailAddress(emailSettings.FromAddress, emailSettings.DisplayName); // Change to your Gmail address
            mail.To.Add(toEmail);
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

    protected void cvDate_ServerValidate(object source, ServerValidateEventArgs args)
    {
        DateTime selectedDate;
        if (DateTime.TryParse(args.Value, out selectedDate))
        {
            args.IsValid = selectedDate >= DateTime.Today; // Validate that the date is today or in the future
        }
        else
        {
            args.IsValid = false; // Invalid date
        }
    }

    protected void cvTimeSlot_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string selectedTime = ddlTimeSlot.SelectedValue;
        DateTime selectedDate;

        // Try to parse the selected date
        if (DateTime.TryParse(txtDate.Text, out selectedDate))
        {
            // Convert the selected time to a DateTime
            DateTime selectedDateTime = selectedDate.Date.Add(DateTime.Parse(selectedTime).TimeOfDay);

            // Get the current date and time
            DateTime currentDateTime = DateTime.Now;

            // Check if the selected date and time is in the future
            if (selectedDateTime <= currentDateTime)
            {
                args.IsValid = false; // Not valid if it's in the past or now
                return;
            }

            // If the selected time is valid, set IsValid to true
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false; // Invalid date
        }
    }


    private void PopulateTimeSlots()
    {
        ddlTimeSlot.Items.Clear(); // Clear existing items
        ddlTimeSlot.Items.Add(new ListItem("Select Time Slot", ""));

        // Define start time as 7 AM
        DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 0, 0);

        // Define end time as 9 PM
        DateTime endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 0, 0); // 9 PM

        // Generate time slots for every 30 minutes from 7 AM to 9 PM
        while (startTime <= endTime)
        {
            string timeText = startTime.ToString("hh:mm tt");
            ddlTimeSlot.Items.Add(new ListItem(timeText, timeText));
            startTime = startTime.AddMinutes(30); // Increment by 30 minutes
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