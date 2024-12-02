using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Services;
using System.Web.Configuration;

public partial class admin_ViewMedicalRecords : System.Web.UI.Page
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
        // Check if the user is an admin or a doctor
        if (Session["role"] != null && Session["role"].ToString() == "Admin")
        {
            LoadAllMedicalRecords();
        }
        else if (Session["DoctorID"] != null)
        {
            LoadDoctorMedicalRecords(Session["DoctorID"].ToString());
        }
        else
        {
            // Handle the case where the role is not found or the DoctorID is not set
            // You may want to show a message or redirect the user
            return;
        }
    }

    private void LoadDoctorMedicalRecords(string doctorId)
    {
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            // Add WHERE clause to filter by DoctorID
            string query = "SELECT * FROM MedicalRecords WHERE DoctorID = @DoctorID";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.SelectCommand.Parameters.AddWithValue("@DoctorID", doctorId); // Pass DoctorID as a parameter

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
                // Handle exceptions here
            }
        }
    }

    private void LoadAllMedicalRecords()
    {
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = "SELECT * FROM MedicalRecords"; // Fetch all medical records
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
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
                // Handle exceptions here
            }
        }
    }

    protected void gvMedicalRecords_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewRecord")
        {
            string patientId = e.CommandArgument.ToString();
            // Redirect to ViewRecord.aspx with the patientId
            Response.Redirect($"ViewRecord.aspx?patientId={patientId}");
        }
    }

}



