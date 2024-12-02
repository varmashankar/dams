using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_PrescribeMedicine : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static List<Dictionary<string, string>> GetMedicineNames(string prefixText)
    {
        List<Dictionary<string, string>> medicines = new List<Dictionary<string, string>>();
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        // Validate input
        if (string.IsNullOrWhiteSpace(prefixText) || prefixText.Length < 1)
        {
            return medicines; // Return empty list if input is invalid
        }

        try
        {
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                // SQL query to get both medicine name and type
                string query = "SELECT MedicineName, MedicineType FROM Medicines WHERE MedicineName LIKE @prefixText + '%'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@prefixText", prefixText);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Create a dictionary for each record
                            var medicine = new Dictionary<string, string>
                        {
                            { "Name", reader["MedicineName"].ToString() },
                            { "Type", reader["MedicineType"].ToString() }
                        };
                            medicines.Add(medicine);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            Console.WriteLine("Error: " + ex.Message);
        }

        return medicines; // Return the list of medicine dictionaries
    }



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
                // Get the AppointmentID and PatientID from the query string
                string appointmentID = Request.QueryString["AppointmentID"];
                string patientID = Request.QueryString["PatientID"];

                // Store these values in hidden fields for use when submitting the form
                hfAppointmentID.Value = appointmentID;
                hfPatientID.Value = patientID;

                
            }
        }
    }

    protected void SubmitPrescription(object sender, EventArgs e)
    {
        string appointmentID = hfAppointmentID.Value;
        string patientID = hfPatientID.Value;
        string doctorID = Session["DoctorID"].ToString(); // Retrieve DoctorID from session
        string medicineName = txtMedicineName.Text;
        string medicineType = ddlMedicineType.SelectedValue;
        string dosage = txtDosage.Text;
        string frequency = ddlFrequency.SelectedValue;
        string duration = txtDuration.Text;
        string allergies = txtAllergies.Text;

        // Capture the selected timings
        List<string> timings = new List<string>();
        if (chkMorning.Checked) timings.Add("Morning");
        if (chkAfternoon.Checked) timings.Add("Afternoon");
        if (chkEvening.Checked) timings.Add("Evening");
        if (chkNight.Checked) timings.Add("Night");

        string timing = string.Join(", ", timings); // Combines the selected timings into a single string
        string doctorNotes = txtDoctorNotes.Text;

        // Insert the prescription details into the database and get the new PrescriptionID
        string prescriptionID = SavePrescription(appointmentID, patientID, medicineName, medicineType, dosage, frequency, duration, allergies, timing, doctorNotes, doctorID);

        // Change the status of the appointment to 'Completed'
        UpdateAppointmentStatus(appointmentID, "Completed");

        // Redirect to the ViewPrescription page with the PrescriptionID
    }
    private string SavePrescription(string appointmentID, string patientID, string medicineName, string medicineType,
                                 string dosage, string frequency, string duration, string allergies,
                                 string timing, string doctorNotes, string doctorID)
    {
        string prescriptionID = Guid.NewGuid().ToString(); // Generate a new GUID for the PrescriptionID

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = @"
        INSERT INTO Prescriptions (PrescriptionID, AppointmentID, PatientID, MedicineName, MedicineType, 
                                   Dosage, Frequency, Duration, AllergyInformation, Timing, DoctorNotes, CreatedAt, DoctorID) 
        VALUES (@PrescriptionID, @AppointmentID, @PatientID, @MedicineName, @MedicineType, 
                @Dosage, @Frequency, @Duration, @AllergyInformation, @Timing, @DoctorNotes, GETDATE(), @DoctorID)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PrescriptionID", new Guid(prescriptionID));
            cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);
            cmd.Parameters.AddWithValue("@PatientID", patientID);
            cmd.Parameters.AddWithValue("@MedicineName", medicineName);
            cmd.Parameters.AddWithValue("@MedicineType", medicineType);
            cmd.Parameters.AddWithValue("@Dosage", dosage);
            cmd.Parameters.AddWithValue("@Frequency", frequency);
            cmd.Parameters.AddWithValue("@Duration", duration);
            cmd.Parameters.AddWithValue("@AllergyInformation", allergies);
            cmd.Parameters.AddWithValue("@Timing", timing);
            cmd.Parameters.AddWithValue("@DoctorNotes", doctorNotes);
            cmd.Parameters.AddWithValue("@DoctorID", doctorID);

            conn.Open();
            cmd.ExecuteNonQuery();  // Execute the insert command
            conn.Close();
            Response.Redirect("dashboard.aspx");
        }

        return prescriptionID; // Return the PrescriptionID for redirection
    }

    private void UpdateAppointmentStatus(string appointmentID, string newStatus)
    {
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = @"
        UPDATE Appointments 
        SET Status = @NewStatus 
        WHERE AppointmentID = @AppointmentID";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NewStatus", newStatus);
            cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);

            conn.Open();
            cmd.ExecuteNonQuery();  // Execute the update command
            conn.Close();
        }
    }


}