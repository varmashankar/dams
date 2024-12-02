using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_ViewAppointment : System.Web.UI.Page
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
                // Get the AppointmentID from the query string
                string appointmentId = Request.QueryString["AppointmentID"];


                if (!string.IsNullOrEmpty(appointmentId))
                {
                    // Call the method to fetch the appointment data
                    LoadAppointmentData(appointmentId);
                }
                else
                {
                    lblMessage.Text = "Invalid appointment ID.";
                }
            }
        }
        
    }

    private void LoadAppointmentData(string appointmentId)
    {
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            conn.Open();
            string query = "SELECT DepartmentID, PatientID, AppointmentDate, AppointmentTime, Status FROM Appointments WHERE AppointmentID = @AppointmentID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@AppointmentID", appointmentId);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                // Populate the fields
                txtAppointmentID.Text = appointmentId;
                txtAppointmentDate.Text = Convert.ToDateTime(reader["AppointmentDate"]).ToString("yyyy-MM-dd");
                txtAppointmentTime.Text = reader["AppointmentTime"].ToString();
                string departmentID = reader["DepartmentID"].ToString();
                txtDepartmentName.Text = departmentID;

                // Get department name using department ID
                string departmentName = GetDepartmentName(departmentID);
                if (!string.IsNullOrEmpty(departmentName))
                {
                    txtDepartmentName.Text = departmentName;
                }

                // Set status in dropdown
                string status = reader["Status"].ToString();
                ddlStatus.SelectedValue = status;

                // Change button text based on status
                if (status == "Confirmed")
                {
                    btnConfirmCancel.Text = "Cancel Appointment";
                    btnConfirmCancel.CssClass = "btn btn-danger float-end"; // Change button color to red for cancel
                }
                else if (status == "Pending" || status == "Cancelled")
                {
                    btnConfirmCancel.Text = "Confirm Appointment";
                    btnConfirmCancel.CssClass = "btn btn-success float-end"; // Green button for confirm
                }

                hfAppointmentID.Value = appointmentId;
            }
            else
            {
                lblMessage.Text = "Appointment not found.";
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

    protected void btnConfirmCancel_Click(object sender, EventArgs e)
    {
        // Retrieve DoctorID from session
        string doctorId = Session["DoctorID"]?.ToString(); // Assuming you set this value when the doctor logs in

        // Get DoctorName using the GetDoctorName method
        string doctorName = GetDoctorName(doctorId);

        string newStatus = ddlStatus.SelectedValue;

        // If the button text is 'Confirm Appointment', update status to Confirmed, otherwise to Cancelled
        if (btnConfirmCancel.Text == "Confirm Appointment")
        {
            newStatus = "Confirmed";
        }
        else if (btnConfirmCancel.Text == "Cancel Appointment")
        {
            newStatus = "Cancelled";
        }

        // Update the status in the database
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Appointments SET Status = @Status, DoctorID = @DoctorID, DoctorName = @DoctorName WHERE AppointmentID = @AppointmentID", conn);
            cmd.Parameters.AddWithValue("@Status", newStatus);
            cmd.Parameters.AddWithValue("@DoctorID", doctorId); // Use the DoctorID from session
            cmd.Parameters.AddWithValue("@DoctorName", doctorName); // Use the DoctorName retrieved
            cmd.Parameters.AddWithValue("@AppointmentID", hfAppointmentID.Value);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                lblMessage.Text = "Appointment updated successfully!";
                LoadAppointmentData(hfAppointmentID.Value); // Reload the updated appointment data
            }
            else
            {
                lblMessage.Text = "Failed to update appointment.";
            }
        }
    }




    private string GetDoctorName(string doctorId)
    {
        string doctorName = string.Empty;

        // Query to get the doctor's name based on DoctorID
        string query = "SELECT FullName FROM doctors WHERE DoctorID = @DoctorID";

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@DoctorID", doctorId);
                conn.Open();

                // Execute the command and read the result
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    doctorName = result.ToString();
                }
            }
        }

        return doctorName; // Return the doctor's name or an empty string if not found
    }


}
