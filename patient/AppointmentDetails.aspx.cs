using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class patient_AppointmentDetails : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                if (Request.QueryString["appointmentID"] != null)
                {
                    string appointmentID = Request.QueryString["appointmentID"];
                    LoadAppointmentDetails(appointmentID);
                }
                else
                {
                    DisplayToastNotification("Appointment Not Found!");
                }
            }
        }
        
    }


    private void LoadAppointmentDetails(string appointmentID)
    {

        using (SqlConnection con = new SqlConnection(strcon))
        {
            string query = "SELECT * FROM appointments WHERE appointmentID = @appointmentID AND Status != 'Cancelled'";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@appointmentID", appointmentID);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblAppointmentID.Text = reader["appointmentID"].ToString();
                    lblAppointmentDate.Text = Convert.ToDateTime(reader["AppointmentDate"]).ToString("MMMM dd, yyyy");
                    lblAppointmentTime.Text = reader["AppointmentTime"].ToString();
                    string departmentID = reader["DepartmentID"].ToString();
                    lblDepartmentID.Text = GetDepartmentName(departmentID);
                    lblStatus.Text = reader["Status"].ToString();
                    lblDateCreated.Text = Convert.ToDateTime(reader["DateCreated"]).ToString("MMMM dd, yyyy hh:mm tt");
                }

                reader.Close();
            }
        }
    }

    private string GetDepartmentName(string departmentID)
    {
        string departmentName = string.Empty; // Initialize the variable to store the department name

        // Define the connection string
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        // SQL query to fetch the department name based on department ID
        string query = "SELECT departmentName FROM department WHERE departmentID = @DepartmentID";

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            // Open the connection
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Add the department ID parameter
                cmd.Parameters.AddWithValue("@DepartmentID", departmentID);

                // Execute the command and retrieve the department name
                object result = cmd.ExecuteScalar();

                // Check if a result was returned
                if (result != null)
                {
                    departmentName = result.ToString(); // Convert the result to string
                }
            }
        }

        return departmentName; // Return the department name or empty string if not found
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        // Redirect to the appointment update page
        string appointmentID = lblAppointmentID.Text;
        Response.Redirect("UpdateAppointment.aspx?appointmentID=" + appointmentID);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            string appointmentID = lblAppointmentID.Text;
            string status = "Cancelled";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                string statusUpdate = "UPDATE appointments SET Status = @status WHERE appointmentID = @appointmentid ";

                using (SqlCommand cmd = new SqlCommand(statusUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@appointmentid", appointmentID);
                    cmd.Parameters.AddWithValue("@status", status);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    DisplayToastNotification("Appointment Cancelled Successfully.");

                    Response.Redirect("upcomingappt.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            string message = HttpUtility.JavaScriptStringEncode(ex.Message);
            Response.Write("<script>alert('" + message + "');</script>");
        }
    }

    private void DisplayToastNotification(string message)
    {
        string script = $@"
        <script type='text/javascript'>
            toastr.error('{message}');
        </script>";

        // Register the script on the page
        ClientScript.RegisterStartupScript(this.GetType(), "toastNotification", script);
    }
}