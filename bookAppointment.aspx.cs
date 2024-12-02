using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.IO;

public partial class bookAppointment : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateTimeSlots();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            //retriveing form data
            string firstname = txtFirstName.Text.ToUpper();
            string lastname = txtLastName.Text.ToUpper();
            string email = txtEmail.Text;
            string phone = txtPhone.Text;   
            string gender = ddlGender.SelectedValue;
            DateTime appointmentDate;
            DateTime.TryParse(txtDate.Text, out appointmentDate); // Handle date parsing
            string department = ddlDepartment.SelectedValue;
            string appointmentTime = ddlTimeSlot.SelectedValue;
            string emgerceny = txtEPhone.Text;

            string departmentID = "";

            string passwordHash = HashPassword(phone);

            // Get or create patient record
            string patientID = GetOrCreatePatient(firstname, lastname, email, phone, passwordHash, gender, emgerceny);

            // Generate a unique appointment ID
            string appointmentID = GenerateAppointmentID();


            // Insert new appointment record
            try
            {
                departmentID = GetDepartmentID(department);

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    string insertAppointmentQuery = @"
                INSERT INTO Appointments (appointmentID, PatientID, AppointmentTime, AppointmentDate, DepartmentID, Status, DateCreated)
                VALUES (@AppointmentID, @PatientID, @AppointmentTime, @AppointmentDate, @DepartmentID, @Status, @DateCreated)";

                    using (SqlCommand cmd = new SqlCommand(insertAppointmentQuery, con))
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
                        string patientName = firstname + lastname;
                        string departmentName = department;
                        DateTime appointmentDates;
                        DateTime.TryParse(txtDate.Text, out appointmentDates);
                        string time = appointmentTime;
                        SendBookingConfirmationEmail(patientEmail,patientName, departmentName, appointmentTime, appointmentDates);


                        if (rowsAffected > 0)
                        {
                            Session["AppointmentID"] = appointmentID;
                            Response.Redirect("success.aspx");
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
        else
        {
            DisplayToastNotification("Error Booking Your Appointment.");
        }
    }

    public string CheckPatientExists(string email, string phone)
    {
        try
        {
            string patientID = null;

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                conn.Open();
                string query = @"
                SELECT patient_id
                FROM patient_master 
                WHERE Email = @Email OR Phone = @Phone";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);

                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        patientID = result.ToString();
                    }
                }
            }

            return patientID;
        }
        catch (Exception ex)
        {
            string message = HttpUtility.JavaScriptStringEncode(ex.Message);
            Response.Write("<script>alert('" + message + "');</script>");

            return null;
        }
    }


    public string InsertNewPatient(string firstname, string lastname, string email, string phone, string passwordHash, string gender, string emgerceny)
    {
        string patientID = null;

        try
        {
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                conn.Open();
                // Generate a new Guid for patient_id
                Guid newPatientId = Guid.NewGuid();

                string query = @"
            INSERT INTO patient_master (patient_id, firstname, lastname, email, phone, passwordHash, gender, emergency_contact)
            VALUES (@patient_id, @firstname, @lastname, @email, @phone, @passwordHash, @gender, @emergency_contact);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@patient_id", newPatientId); // Ensure patient_id column is of type uniqueidentifier
                    cmd.Parameters.AddWithValue("@firstname", firstname);
                    cmd.Parameters.AddWithValue("@lastname", lastname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@emergency_contact", emgerceny);

                    // Use ExecuteNonQuery since this is an INSERT statement
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Check if the insert was successful
                    if (rowsAffected > 0)
                    {
                        patientID = newPatientId.ToString(); // Assign the new ID if the insert was successful
                    }
                    else
                    {
                        // Handle the case where no rows were affected
                        Console.WriteLine("No rows were inserted.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string message = HttpUtility.JavaScriptStringEncode(ex.Message);
            Response.Write("<script>alert('" + message + "');</script>");
            // Optionally log the error for further investigation
            Console.WriteLine("Error: " + ex.Message);
        }

        return patientID; // This will return null if insertion fails
    }



    public string GetOrCreatePatient(string firstname, string lastname, string email, string phone, string passwordHash, string gender, string emgerceny)
    {
        string patientID = null; // Initialize patientID

        try
        {
            // Check if patient already exists
            patientID = CheckPatientExists(email, phone);

            // Log patient check result
            if (!string.IsNullOrEmpty(patientID))
            {
                Console.WriteLine($"Patient found: {patientID}");
                return patientID; // Return existing patient ID if found
            }

            // If patient does not exist, insert a new record
            patientID = InsertNewPatient(firstname, lastname, email, phone, passwordHash, gender, emgerceny);
            if (string.IsNullOrEmpty(patientID))
            {
                Console.WriteLine("Failed to create a new patient record.");
                return null; // Return null if patient creation failed
            }

            // Log new patient creation
            Console.WriteLine($"New patient created: {patientID}");
        }
        catch (Exception ex)
        {
            string message = HttpUtility.JavaScriptStringEncode(ex.Message);
            Response.Write("<script>alert('" + message + "');</script>");
        }

        return patientID; // Return the patient ID (could be null if there was an error)
    }


    private string HashPassword(string phone)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(phone));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
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

    private void SendBookingConfirmationEmail(string toEmail, string patientName, string departmentName, string appointmentTime, DateTime appointmentDates)
    {
        string subject = "Your Appointment Booking Request.";
        string body = $@"
<div style='font-family: Arial, sans-serif; color: #333; padding: 20px; border: 1px solid #E0E0E0; border-radius: 8px; background-color: #FFFFFF;'>
    <div style='background-color: #4CAF50; color: #FFFFFF; padding: 10px; border-radius: 8px 8px 0 0;'>
        <h2 style='margin: 0;'>DOCTIME Appointment Request</h2>
    </div>
    <div style='padding: 20px;'>
        <p style='font-size: 16px; color: #424242;'>Dear <strong>{patientName}</strong>,</p>
        <p style='font-size: 16px; color: #424242;'>We are pleased to inform you about your appointment. We received your booking request and we will confirm it after checking the availability.</p>
        <p style='font-size: 16px; color: #424242;'><strong>Appointment Details:</strong></p>
        <ul style='font-size: 16px; color: #424242;'>
            <li><strong>Time:</strong> {appointmentTime}</li>
            <li><strong>Date:</strong> {appointmentDates}</li>
            <li><strong>Department:</strong> {departmentName}</li>
        </ul>
        <p style='font-size: 16px; color: #424242;'>Please arrive at least 15 minutes before your scheduled appointment time. If you need to reschedule or have any questions, do not hesitate to contact us.</p>
        <p style='font-size: 16px; color: #424242;'>Thank you for choosing DOCTIME!</p>
    </div>
    <div style='background-color: #F5F5F5; color: #777; padding: 10px; border-radius: 0 0 8px 8px; text-align: center;'>
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


    private string GetDepartmentID(string department)
    {
        string departmentID = "";
        string query = "SELECT departmentID FROM department WHERE departmentName = @Specialty";

        try
        {

            using (SqlConnection con = new SqlConnection(strcon))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Specialty", department);
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        departmentID = result.ToString();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            string message = HttpUtility.JavaScriptStringEncode(ex.Message);
            Response.Write("<script>alert('" + message + "');</script>");
        }
        return departmentID;
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