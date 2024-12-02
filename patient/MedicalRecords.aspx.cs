using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class patient_MedicalRecords : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadMedicalRecords();
        }
    }

    private void LoadMedicalRecords()
    {
        // Check if the patient_id exists in the session
        if (Session["PatientID"] == null)
        {
            return; // Exit the method if the patient ID is not available
        }

        string patientId = Session["PatientID"].ToString();

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            // Ensure RecordID is selected
            string query = "SELECT * FROM MedicalRecords WHERE PatientID = @patientId";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@patientId", patientId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
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
                System.Diagnostics.Debug.WriteLine($"Error loading medical records: {ex.Message}");
            }
        }
    }




    protected void gvMedicalRecords_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewRecord")
        {
            // Get the RecordID from CommandArgument
            string recordId = e.CommandArgument.ToString();
            string patientId = Session["PatientID"].ToString(); // Assuming PatientID is also in the session

            // Redirect to ViewRecord.aspx with both patientId and recordId
            Response.Redirect($"ViewRecord.aspx?patientId={patientId}&recordId={recordId}");
        }
    }


}