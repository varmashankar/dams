using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class admin_TodayPrescriptions : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadTodayPrescriptions();
        }
    }

    private void LoadTodayPrescriptions()
    {
        // Load today's prescriptions based on the user's role
        DataTable dt = GetTodayPrescriptions();
        gvPrescriptions.DataSource = dt; // Set the GridView data source
        gvPrescriptions.DataBind(); // Bind data to the GridView
    }

    private DataTable GetTodayPrescriptions()
    {
        DataTable dt = new DataTable();
        string doctorID = Session["DoctorID"]?.ToString(); // Get DoctorID from session

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query;

            // Check if the user is an admin or doctor
            if (IsUserAdmin())
            {
                // Admin can see all today's prescriptions
                query = @"
            SELECT p.PrescriptionID, p.AppointmentID, 
                   pa.patient_id AS PatientID, 
                   pa.firstname, pa.lastname, 
                   p.MedicineName, p.Dosage, p.Frequency, p.CreatedAt
            FROM Prescriptions p
            INNER JOIN patient_master pa ON p.PatientID = pa.patient_id
            WHERE CAST(p.CreatedAt AS DATE) = CAST(GETDATE() AS DATE)";
            }
            else if (!string.IsNullOrEmpty(doctorID))
            {
                // Doctor can only see their today's prescriptions
                query = @"
            SELECT p.PrescriptionID, p.AppointmentID, 
                   pa.patient_id AS PatientID, 
                   pa.firstname, pa.lastname, 
                   p.MedicineName, p.Dosage, p.Frequency, p.CreatedAt
            FROM Prescriptions p
            INNER JOIN patient_master pa ON p.PatientID = pa.patient_id
            WHERE p.DoctorID = @DoctorID AND CAST(p.CreatedAt AS DATE) = CAST(GETDATE() AS DATE)";
            }
            else
            {
                // Handle the case where neither admin nor doctor is logged in
                return dt; // Return an empty DataTable
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

        return dt; // Return the DataTable containing today's prescriptions
    }

    private bool IsUserAdmin()
    {
        // Check if the user's role is admin
        return Session["Role"] != null && Session["Role"].ToString() == "Admin";
    }



}