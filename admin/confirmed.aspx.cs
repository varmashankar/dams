using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_confirmed : System.Web.UI.Page
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
            Context.ApplicationInstance.CompleteRequest();
        }
        else
        {
            if (!IsPostBack)
            {
                // Load confirmed appointments based on the user's role
                string userRole = Session["role"].ToString();
                if (userRole == "Admin")
                {
                    LoadAllConfirmedAppointments(); // Load all confirmed appointments for Admin
                }
                else if (userRole == "Doctor")
                {
                    string departmentID = GetDepartmentID(); // Get department ID for Doctor
                    if (!string.IsNullOrEmpty(departmentID))
                    {
                        LoadConfirmedAppointments(departmentID); // Load appointments for specific doctor
                    }
                }
            }
        }
    }

    // Method to load all confirmed appointments for Admin
    private void LoadAllConfirmedAppointments()
    {
        string query = @"
    SELECT A.AppointmentID, A.PatientID, A.AppointmentDate, A.AppointmentTime, A.Status, D.departmentName
    FROM appointments A
    LEFT JOIN department D ON A.DepartmentID = D.departmentID
    WHERE A.Status = @Status";

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Status", "Confirmed");
                conn.Open();

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    // Bind data to GridView
                    gvConfirmedAppointments.DataSource = dt;
                    gvConfirmedAppointments.DataBind();
                }
            }
        }
    }

    // Existing method to load confirmed appointments for Doctor
    private void LoadConfirmedAppointments(string departmentID)
    {
        string query = @"
    SELECT A.AppointmentID, A.PatientID, A.AppointmentDate, A.AppointmentTime, A.Status, D.departmentName
    FROM appointments A
    LEFT JOIN department D ON A.DepartmentID = D.departmentID
    WHERE A.Status = @Status AND A.DepartmentID = @DepartmentID";

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Status", "Confirmed");
                cmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                conn.Open();

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    // Bind data to GridView
                    gvConfirmedAppointments.DataSource = dt;
                    gvConfirmedAppointments.DataBind();
                }
            }
        }
    }

    // Existing method to get department ID for Doctor
    private string GetDepartmentID()
    {
        string departmentID = string.Empty; // Initialize the variable to store the department ID

        // SQL query to fetch the department ID based on the user's role or another identifier
        string query = "SELECT DepartmentID FROM doctors WHERE DoctorID = @DoctorID"; // Adjust the query according to your table structure

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            // Open the connection
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Add the doctor ID parameter from the session or context
                cmd.Parameters.AddWithValue("@DoctorID", Session["doctorID"]); // Adjust the session variable name as necessary

                // Execute the command and retrieve the department ID
                object result = cmd.ExecuteScalar();

                // Check if a result was returned
                if (result != null)
                {
                    departmentID = result.ToString(); // Convert the result to string
                }
            }
        }

        return departmentID; // Return the department ID or empty string if not found
    }



}