using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_AddMedicalRecord : System.Web.UI.Page
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
            Context.ApplicationInstance.CompleteRequest();
        }
        else
        {
            if (!IsPostBack)
            {
                // Check for query parameters
                if (Request.QueryString["AppointmentID"] != null)
                {
                    txtAppointmentID.Text = Request.QueryString["AppointmentID"];
                }

                LoadDoctors();
            }
        }
    }

    private void LoadDoctors()
    {
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = "SELECT DoctorID, FullName FROM doctors";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddlDoctor.DataSource = reader;
                ddlDoctor.DataTextField = "FullName";
                ddlDoctor.DataValueField = "DoctorID";
                ddlDoctor.DataBind();
            }
        }

        // Auto-select the doctor if DoctorID is stored in the session
        if (Session["DoctorID"] != null)
        {
            string doctorID = Session["DoctorID"].ToString();
            ListItem item = ddlDoctor.Items.FindByValue(doctorID);
            if (item != null)
            {
                item.Selected = true; // Auto-select the doctor
            }
        }
    }

    protected void btnAddRecord_Click(object sender, EventArgs e)
    {
        string doctorID = ddlDoctor.SelectedValue;
        string appointmentID = txtAppointmentID.Text.Trim();
        string details = txtDetails.Text.Trim();
        DateTime dateOfRecord;

        // Validate and parse the date
        if (DateTime.TryParse(txtDateOfRecord.Text.Trim(), out dateOfRecord))
        {
            string uploadedFilePath = null; // Initialize file path

            // Handle file upload
            if (fileUpload.HasFile)
            {
                try
                {
                    string fileName = Path.GetFileName(fileUpload.FileName); // Get the file name
                    string folderPath = Server.MapPath("~/UploadedFiles/"); // Get the server path for the root UploadedFiles directory

                    // Check if the directory exists; if not, create it
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath); // Create the directory
                    }

                    uploadedFilePath = "UploadedFiles/" + fileName; // Save relative path
                    string serverPath = Path.Combine(folderPath, fileName); // Combine path and filename
                    fileUpload.SaveAs(serverPath); // Save the uploaded file to the server
                    lblSuccess.Text = "File uploaded successfully: " + fileName; // Display success message
                }
                catch (Exception ex)
                {
                    lblError.Text = "File upload failed: " + ex.Message; // Display error message
                    return; // Exit the method
                }
            }

            // Save the medical record with the relative file path
            string patientID = Request.QueryString["PatientID"];
            try
            {
                string recordID = SaveMedicalRecord(patientID, doctorID, dateOfRecord, details, uploadedFilePath);
                lblSuccess.Text = "Medical record added successfully! Record ID: " + recordID;
                lblError.Text = ""; // Clear error label
                Response.Redirect("today.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = "Error saving medical record: " + ex.Message; // Display error message
                lblSuccess.Text = ""; // Clear success label
            }
        }
        else
        {
            lblError.Text = "Invalid date format. Please enter a valid date.";
            lblSuccess.Text = ""; // Clear success label
        }
    }



    private string SaveMedicalRecord(string patientID, string doctorID, DateTime dateOfRecord, string details, string filePath = null)
    {
        string recordID = Guid.NewGuid().ToString();

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = @"
            INSERT INTO MedicalRecords (RecordID, AppointmentID, PatientID, DoctorID, DateOfRecord, Details, FilePath) 
            VALUES (@RecordID, @AppointmentID, @PatientID, @DoctorID, @DateOfRecord, @Details, @FilePath)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@RecordID", recordID);
                cmd.Parameters.AddWithValue("@AppointmentID", txtAppointmentID.Text.Trim());
                cmd.Parameters.AddWithValue("@PatientID", patientID);
                cmd.Parameters.AddWithValue("@DoctorID", doctorID);
                cmd.Parameters.AddWithValue("@DateOfRecord", dateOfRecord);
                cmd.Parameters.AddWithValue("@Details", details);
                cmd.Parameters.AddWithValue("@FilePath", filePath ?? (object)DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        return recordID; // Return the RecordID for confirmation
    }
}
