using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_SearchPatients : System.Web.UI.Page
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
                gvPatients.DataSource = null;
                gvPatients.DataBind();
            }
        }
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

}