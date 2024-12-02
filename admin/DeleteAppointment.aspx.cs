using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_DeleteAppointment : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Get the appointment ID from the query string
            string appointmentID = Request.QueryString["AppointmentID"];
            hfAppointmentID.Value = appointmentID;
        }
    }

    protected void btnConfirmDelete_Click(object sender, EventArgs e)
    {
        string appointmentID = hfAppointmentID.Value;

        // Call a method to delete the appointment from the database
        bool isDeleted = DeleteAppointmentFromDatabase(appointmentID);

        if (isDeleted)
        {
            // Display success message in a JavaScript alert
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Appointment deleted successfully.');", true);

        }
        else
        {
            // Display error message in a JavaScript alert
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error occurred while deleting the appointment.');", true);
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("requests.aspx"); // Redirect to another page or back to details
    }

    private bool DeleteAppointmentFromDatabase(string appointmentID)
    {
        
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = "DELETE appointments WHERE appointmentID = @appointmentid";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@appointmentid", appointmentID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // Return true if the appointment was deleted
            }
        }
    }
}
