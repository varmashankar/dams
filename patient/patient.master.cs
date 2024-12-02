using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class patient_patient : System.Web.UI.MasterPage
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

        // Check if the session variables are set
        if (Session["Email"] == null || Session["Phone"] == null)
        {
            string script = @"
                <script type='text/javascript'>
                    alert('Session Expired. Kindly Login Again.');
                        window.location.href = '../login.aspx';
                </script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sessionExpired", script, true);
        }
        else
        {
            // Retrieve session information
            string email = Session["Email"].ToString();
            string phone = Session["Phone"].ToString();

            // Fetch and load data from the database
            LoadUserData(email, phone);
        }

    }

    private void LoadUserData(string email, string phone)
    {

        string query = @"SELECT * FROM patient_master WHERE Email = @Email OR Phone = @Phone";

        using (SqlConnection con = new SqlConnection(strcon))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        patientName.Text = reader["firstname"].ToString().ToUpper() + " " +  reader["lastname"].ToString().ToUpper();
                    }
                }
                else
                {
                    string nodata = @"<script>alert('No Data Found!')</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "noData", nodata, true);
                }
            }
        }
    }

    protected void logout_click(object sender, EventArgs e)
    {
        // Clear the session
        Session.Clear();
        Session.Abandon();
        Response.Redirect("~/login.aspx");
    }
}
