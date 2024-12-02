using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class patient_PatientPrescriptions : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadPatientPrescriptions();
        }
    }

    private void LoadPatientPrescriptions()
    {
        // Check if patient_id exists in the session
        if (Session["PatientID"] == null)
        {
            // Handle the case where patient_id is not set
            // Redirect or show an error message
            return;
        }

        string patientId = Session["PatientID"].ToString();
        DataTable dtPrescriptions = GetPrescriptionsByPatientId(patientId);

        gvPrescriptions.DataSource = dtPrescriptions;
        gvPrescriptions.DataBind();
    }

    private DataTable GetPrescriptionsByPatientId(string patientId)
    {
        DataTable dt = new DataTable();

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = @"
                    SELECT p.PrescriptionID, p.AppointmentID, 
                           p.MedicineName, p.MedicineType, p.Dosage, p.Frequency, p.CreatedAt
                    FROM Prescriptions p
                    WHERE p.PatientID = @PatientID"; // Adjust the query to match your table structure

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PatientID", patientId);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt); // Fill the DataTable with the query result
        }

        return dt; // Return the DataTable containing prescriptions for the patient
    }
}
