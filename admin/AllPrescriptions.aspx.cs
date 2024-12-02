using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class admin_AllPrescriptions : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string doctorID = GetCurrentDoctorID(); // Method to get the current doctor's ID
            LoadAllPrescriptions(doctorID);
        }
    }

    private void LoadAllPrescriptions(string doctorID)
    {
        // Call GetAllPrescriptions without passing the DoctorID if the user is an admin
        DataTable dtPrescriptions = GetAllPrescriptions(doctorID);
        gvAllPrescriptions.DataSource = dtPrescriptions;
        gvAllPrescriptions.DataBind();
    }

    private DataTable GetAllPrescriptions(string doctorID)
    {
        DataTable dt = new DataTable();

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query;

            // Check if the current user is an admin
            if (IsUserAdmin())
            {
                // Admin can see all prescriptions
                query = @"
                SELECT p.PrescriptionID, p.AppointmentID, 
                       pa.firstname, pa.lastname, 
                       p.MedicineName, p.Dosage, p.Frequency, p.CreatedAt
                FROM Prescriptions p
                INNER JOIN patient_master pa ON p.PatientID = pa.patient_id";
            }
            else
            {
                // Doctor can only see their prescriptions
                query = @"
                SELECT p.PrescriptionID, p.AppointmentID, 
                       pa.firstname, pa.lastname, 
                       p.MedicineName, p.Dosage, p.Frequency, p.CreatedAt
                FROM Prescriptions p
                INNER JOIN patient_master pa ON p.PatientID = pa.patient_id
                WHERE p.DoctorID = @DoctorID";
            }

            SqlCommand cmd = new SqlCommand(query, conn);

            // Add parameter only if the user is a doctor
            if (!IsUserAdmin())
            {
                cmd.Parameters.AddWithValue("@DoctorID", doctorID);
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt); // Fill the DataTable with the query result
        }

        return dt; // Return the DataTable containing all prescriptions
    }

    private string GetCurrentDoctorID()
    {
        // Implement your logic to retrieve the current doctor's ID
        // For example, from session or authentication context
        return Session["DoctorID"] != null ? Session["DoctorID"].ToString() : string.Empty; // Return empty if no ID
    }

    private bool IsUserAdmin()
    {
        // Check if the user's role is admin
        return Session["Role"] != null && Session["Role"].ToString() == "Admin";
    }

}
