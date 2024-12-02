using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_requests : System.Web.UI.Page
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
                string doctorID = Session["DoctorID"] as string; // Fetch DoctorID from the session
                if (!string.IsNullOrEmpty(doctorID))
                {
                    string departmentId = GetDepartmentIDByDoctorID(doctorID); // Get department ID using the DoctorID
                    LoadPendingAppointments(departmentId); // Pass the department ID to load appointments
                }
            }
        }
    }

    private void LoadPendingAppointments(string departmentId)
    {
        if (string.IsNullOrEmpty(departmentId))
        {
            return;
        }

        string query = @"
    SELECT A.AppointmentID, A.PatientID, A.AppointmentDate, A.AppointmentTime, A.Status, D.departmentName
    FROM appointments A
    LEFT JOIN department D ON A.DepartmentID = D.departmentID
    WHERE A.Status = @Status AND A.DepartmentID = @DepartmentID";

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Add parameters for Status and DepartmentID
                cmd.Parameters.AddWithValue("@Status", "Pending");
                cmd.Parameters.AddWithValue("@DepartmentID", departmentId);

                conn.Open();

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    // Bind data to GridView
                    gvPendingAppointments.DataSource = dt;
                    gvPendingAppointments.DataBind();
                }
            }
        }
    }

    private string GetDepartmentIDByDoctorID(string doctorID)
    {
        string departmentID = string.Empty; // Initialize the variable to store the DepartmentID

        // SQL query to fetch the DepartmentID based on DoctorID
        string query = "SELECT DepartmentID FROM Doctors WHERE DoctorID = @DoctorID";

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            // Open the connection
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Add the DoctorID parameter
                cmd.Parameters.AddWithValue("@DoctorID", doctorID);

                // Execute the command and retrieve the DepartmentID
                object result = cmd.ExecuteScalar();

                // Check if a result was returned
                if (result != null)
                {
                    departmentID = result.ToString(); // Convert the result to string
                }
            }
        }

        return departmentID; // Return the DepartmentID or an empty string if not found
    }

    private string GetDepartmentName(string departmentID)
    {
        string departmentName = string.Empty; // Initialize the variable to store the department name

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

}