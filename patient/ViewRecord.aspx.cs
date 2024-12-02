using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class patient_ViewRecord : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string patientId = Request.QueryString["patientId"];
            string recordId = Request.QueryString["recordId"];

            LoadPatientDetails(patientId);

            if (!string.IsNullOrEmpty(patientId) && !string.IsNullOrEmpty(recordId))
            {
                LoadMedicalRecords(patientId, recordId);
            }
        }
    }

    private void LoadPatientDetails(string patientId)
    {
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = "SELECT * FROM patient_master WHERE patient_id = @PatientID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PatientID", patientId);
            SqlDataReader reader;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblFullNameValue.Text = $"{reader["FirstName"]} {reader["LastName"]}";
                    lblEmailValue.Text = reader["Email"].ToString();
                    lblPhoneValue.Text = reader["Phone"].ToString();

                    // Check if dob is not null before converting
                    if (reader["dob"] != DBNull.Value)
                    {
                        lblDobValue.Text = Convert.ToDateTime(reader["dob"]).ToShortDateString();
                    }
                    else
                    {
                        lblDobValue.Text = "N/A"; // Or any default message
                    }

                    // Check if gender is not null
                    lblGenderValue.Text = reader["gender"] != DBNull.Value ? reader["gender"].ToString() : "N/A"; // Default message if null
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions (log or display error message)
                // Example: lblErrorMessage.Text = "An error occurred: " + ex.Message;
            }
        }
    }


    private void LoadMedicalRecords(string patientId, string recordId)
    {
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = "SELECT AppointmentID, Details, DateOfRecord, FilePath FROM MedicalRecords WHERE PatientID = @PatientID and RecordID = @RecordID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PatientID", patientId);
            cmd.Parameters.AddWithValue("@RecordID", recordId);

            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    gvMedicalRecords.DataSource = dt;
                    gvMedicalRecords.DataBind();
                }
                else
                {
                    gvMedicalRecords.DataSource = null;
                    gvMedicalRecords.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Log the error (for example, using NLog, log4net, or another logging framework)
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }





    protected void btnBack_Click(object sender, EventArgs e)
    {
        // Redirect back to the previous page or the main records page
        Response.Redirect("MedicalRecords.aspx"); // Adjust the redirect URL as needed
    }
}