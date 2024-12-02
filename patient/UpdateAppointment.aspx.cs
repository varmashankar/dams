using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class patient_UpdateAppointment : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["appointmentID"] != null)
            {
                string appointmentID = Request.QueryString["appointmentID"];
                PopulateTimeSlots();
                LoadAppointmentDetails(appointmentID);
            }
            else
            {
                // Handle error: no appointmentID in query string
                Response.Redirect("Dashboard.aspx");
            }
        }
    }

    private void LoadAppointmentDetails(string appointmentID)
    {

        using (SqlConnection con = new SqlConnection(strcon))
        {
            string query = "SELECT * FROM appointments WHERE appointmentID = @appointmentID AND status != 'Cancelled'";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@appointmentID", appointmentID);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtAppointmentID.Text = reader["appointmentID"].ToString();
                    txtAppointmentDate.Text = Convert.ToDateTime(reader["AppointmentDate"]).ToString("yyyy-MM-dd");
                    string departmentID = reader["DepartmentID"].ToString();
                    ddlDepartment.SelectedValue = GetDepartmentName(departmentID);
                    ddlTimeSlot.SelectedValue = reader["AppointmentTime"].ToString();
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

    private void PopulateTimeSlots()
    {
        ddlTimeSlot.Items.Clear(); // Clear existing items
        ddlTimeSlot.Items.Add(new ListItem("Select Time Slot", ""));

        // Define start time as 7 AM
        DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 0, 0);

        // Define end time as 9 PM
        DateTime endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 0, 0); // 9 PM

        // Generate time slots for every 30 minutes from 7 AM to 9 PM
        while (startTime <= endTime)
        {
            string timeText = startTime.ToString("hh:mm tt");
            ddlTimeSlot.Items.Add(new ListItem(timeText, timeText));
            startTime = startTime.AddMinutes(30); // Increment by 30 minutes
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string appointmentID = txtAppointmentID.Text;
        DateTime newAppointmentDate;
        DateTime.TryParse(txtAppointmentDate.Text, out newAppointmentDate);
        string newTimeSlot = ddlTimeSlot.SelectedValue;
        string departmentID = ddlDepartment.SelectedValue;

        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();

            // Fetch the current data
            string selectQuery = "SELECT AppointmentDate, AppointmentTime, Status FROM appointments WHERE appointmentID = @appointmentID";
            SqlCommand selectCmd = new SqlCommand(selectQuery, con);
            selectCmd.Parameters.AddWithValue("@appointmentID", appointmentID);

            SqlDataReader reader = selectCmd.ExecuteReader();
            if (reader.Read())
            {
                DateTime currentAppointmentDate;
                DateTime.TryParse(reader["AppointmentDate"].ToString(), out currentAppointmentDate);
                string currentTimeslot = reader["AppointmentTime"].ToString();
                string currentStatus = reader["Status"].ToString();
                reader.Close();

                // Determine whether to update the status
                string newStatus;
                if (currentAppointmentDate != newAppointmentDate || currentTimeslot != newTimeSlot)
                {
                    newStatus = "Rescheduled";
                }
                else
                {
                    newStatus = currentStatus;
                }

                // Update the appointment details
                string updateQuery = "UPDATE appointments SET AppointmentDate = @appointmentDate, AppointmentTime = @appointmentTime, DepartmentID = @departmentID, Status = @Status WHERE appointmentID = @appointmentID";
                SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                updateCmd.Parameters.AddWithValue("@appointmentID", appointmentID);
                updateCmd.Parameters.AddWithValue("@appointmentDate", newAppointmentDate);
                updateCmd.Parameters.AddWithValue("@appointmentTime", newTimeSlot);
                updateCmd.Parameters.AddWithValue("@Status", newStatus);
                updateCmd.Parameters.AddWithValue("@departmentID", departmentID);

                updateCmd.ExecuteNonQuery();
            }

        }

        // Redirect to the updated details page o
        Response.Redirect("AppointmentDetails.aspx?appointmentID=" + appointmentID);
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string appointmentID = txtAppointmentID.Text;
        // Redirect to the updated details page or list
        Response.Redirect("AppointmentDetails.aspx?appointmentID=" + appointmentID);
    }
}