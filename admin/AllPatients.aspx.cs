using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_AllPatients : System.Web.UI.Page
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

            // Stop further processing of the page
            Context.ApplicationInstance.CompleteRequest();
        }
        else
        {
            if (!IsPostBack)
            {
                LoadPatients();
            }
        }
        
    }

    protected void btnLoadAll_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        LoadPatients();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string searchQuery = txtSearch.Text.Trim();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                // Base query for single name searches
                string query = "SELECT patient_id, firstname, lastname, email, phone, dob, gender, emergency_contact " +
                               "FROM patient_master " +
                               "WHERE (firstname LIKE @Search OR lastname LIKE @Search OR email LIKE @Search OR phone LIKE @Search OR patient_id = @Search)";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Check if the search query contains multiple words (likely two names)
                if (searchQuery.Contains(" "))
                {
                    // Split the search query into an array by spaces
                    string[] names = searchQuery.Split(' ');

                    if (names.Length >= 2)
                    {
                        // Modify the query to include first name and last name together
                        query += " OR (firstname LIKE @FirstName AND lastname LIKE @LastName)";
                        cmd.CommandText = query; // Update the command with the new query
                        cmd.Parameters.AddWithValue("@FirstName", "%" + names[0] + "%");
                        cmd.Parameters.AddWithValue("@LastName", "%" + names[1] + "%");
                    }
                }

                // Add the original search term for the single name case
                cmd.Parameters.AddWithValue("@Search", "%" + searchQuery + "%");

                // Execute the query
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Bind the results to the GridView
                if (dt.Rows.Count > 0)
                {
                    gvPatients.DataSource = dt;
                    gvPatients.DataBind();
                }
                else
                {
                    gvPatients.DataSource = null;
                    gvPatients.DataBind();
                }
            }
        }
    }

    // Method to load all patients
    private void LoadPatients()
    {
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = "SELECT patient_id, firstname, lastname, email, phone, dob, gender FROM patient_master";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                gvPatients.DataSource = dt;
                gvPatients.DataBind();
            }
            else
            {
                gvPatients.DataSource = null;
                gvPatients.DataBind();
            }
        }
    }

    // Handle Add Medical Record command
    protected void gvPatients_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddMedicalRecord")
        {
            // Get the patient_id from CommandArgument
            string patientId = e.CommandArgument.ToString();

            // Redirect to AddMedicalRecord.aspx with patientId as query string
            Response.Redirect($"AddMedicalRecord.aspx?patientId={patientId}");
        }
    }
}