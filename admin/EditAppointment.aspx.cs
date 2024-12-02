using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_EditAppointment : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string appointmentID = Request.QueryString["AppointmentID"];
            lblMessage.Text = "Appointment ID: " + appointmentID; // Temporary debug output
            if (!string.IsNullOrEmpty(appointmentID))
            {
                LoadAppointment(appointmentID);
            }
            else
            {
                lblMessage.Text = "Invalid appointment ID.";
            }
        }
    }

    private void LoadAppointment(string appointmentID)
    {
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM appointments WHERE appointmentID = @AppointmentID", conn);
            cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    hfAppointmentID.Value = reader["appointmentID"].ToString();
                    txtAppointmentDate.Text = Convert.ToDateTime(reader["AppointmentDate"]).ToString("yyyy-MM-dd");
                    populateServices();
                    ddlSpecialty.SelectedValue = reader["departmentID"].ToString();
                    PopulateTimeSlots();
                    ddlTimeSlot.SelectedValue = reader["AppointmentTime"].ToString();
                }
                else
                {
                    lblMessage.Text = "Appointment not found.";
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message; // Display error message
            }
        }
    }

    private void populateServices()
    {
        string query = "SELECT departmentID, departmentName FROM department";

        ddlSpecialty.Items.Clear();

        try
        {
            // Establish a connection to the database
            using (SqlConnection con = new SqlConnection(strcon))
            {
                // Open the connection
                con.Open();

                // Create a command to execute the query
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Execute the command and use SqlDataReader to read the data
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if there are rows
                        if (reader.HasRows)
                        {
                            // Loop through the results
                            while (reader.Read())
                            {
                                // Create a ListItem for each row (ServiceID as value, ServiceName as text)
                                ListItem item = new ListItem(reader["departmentName"].ToString(), reader["departmentID"].ToString());

                                // Add the item to the DropDownList
                                ddlSpecialty.Items.Add(item);
                            }
                        }
                    }
                }
            }

            // Optionally, add a default "Select" item at the top of the dropdown
            ddlSpecialty.Items.Insert(0, new ListItem("-- Select a Service --", "0"));
        }
        catch (Exception ex)
        {
            // Handle any exceptions (e.g., display an error message)
            lblMessage.Text = "Error: " + ex.Message;
        }

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
        string originalDate, originalTime;

        // Retrieve original values from the database based on the AppointmentID
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            conn.Open();
            string query = "SELECT AppointmentDate, AppointmentTime FROM Appointments WHERE AppointmentID = @AppointmentID";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@AppointmentID", hfAppointmentID.Value);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        originalDate = Convert.ToDateTime(reader["AppointmentDate"]).ToString("yyyy-MM-dd");
                        originalTime = reader["AppointmentTime"].ToString();
                    }
                    else
                    {
                        lblMessage.Text = "Appointment not found.";
                        return;
                    }
                }
            }
        }

        // Get the new date and time entered by the user
        string newDate = txtAppointmentDate.Text;
        string newTime = ddlTimeSlot.SelectedValue;

        // Only change status if date or time has changed
        string status = (originalDate != newDate || originalTime != newTime) ? "Rescheduled" : "Pending";

        // Update the database with the new values and status
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Appointments SET AppointmentDate = @AppointmentDate, AppointmentTime = @AppointmentTime, departmentID = @departmentID, Status = @Status WHERE AppointmentID = @AppointmentID", conn);
            cmd.Parameters.AddWithValue("@AppointmentID", hfAppointmentID.Value);
            cmd.Parameters.AddWithValue("@AppointmentDate", DateTime.Parse(newDate));
            cmd.Parameters.AddWithValue("@AppointmentTime", newTime);
            cmd.Parameters.AddWithValue("@departmentID", ddlSpecialty.SelectedValue);
            cmd.Parameters.AddWithValue("@Status", status);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Response.Redirect("requests.aspx"); // Redirect back to the appointment list
            }
            else
            {
                lblMessage.Text = "Update failed. Please try again.";
            }
        }
    }


    protected void cvTimeSlot_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string selectedTime = ddlTimeSlot.SelectedValue;
        DateTime selectedDate;

        // Try to parse the selected date
        if (DateTime.TryParse(txtAppointmentDate.Text, out selectedDate))
        {
            // Convert the selected time to a DateTime
            DateTime selectedDateTime = selectedDate.Date.Add(DateTime.Parse(selectedTime).TimeOfDay);

            // Get the current date and time
            DateTime currentDateTime = DateTime.Now;

            // Check if the selected date and time is in the future
            if (selectedDateTime <= currentDateTime)
            {
                args.IsValid = false; // Not valid if it's in the past or now
                return;
            }

            // If the selected time is valid, set IsValid to true
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false; // Invalid date
        }
    }

}
