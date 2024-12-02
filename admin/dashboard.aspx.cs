using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class admin_dashboard : System.Web.UI.Page
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
        else if (Session["role"].Equals("Admin"))
        {
            string username = Session["Username"].ToString();
            string email = Session["Email"].ToString();
            string usrRole = Session["role"].ToString();

            // Fetch and load data from the database
            LoadUserData(username, email, usrRole);

            ShowAdminControls();

        }
        else if (Session["role"].Equals("Doctor"))
        {
            string username = Session["DoctorID"].ToString();
            string email = Session["Email"].ToString();
            string phone = Session["Phone"].ToString();
            string usrRole = Session["role"].ToString();

            // Fetch and load data from the database
            LoadUserData(username, email, usrRole);

            ShowDoctorControls();

            BindAppointments(username);

        }
        else if (Session["role"].Equals("Staff"))
        {
            string username = Session["DoctorID"].ToString();
            string email = Session["Email"].ToString();
            string phone = Session["Phone"].ToString();
            string usrRole = Session["role"].ToString();

            // Fetch and load data from the database
            LoadUserData(username, email, usrRole);

            ShowStaffControls();

        }

    }

    private void ShowAdminControls()
    {
        btnAddNewDoctor.Visible = true;
        btnAddNewDepartment.Visible = true;
        btnAddNewAdmin.Visible = true;
        btnViewAllDoctors.Visible = true;
        btnAddCateory.Visible = true;
        btnAddImages.Visible = true;
        btnUpcomingAppointments.Visible = false;
        btnPreviousAppointments.Visible = false;
        btnSearchPatient.Visible = true;
        btnAddMedicine.Visible = true;
    }

    private void ShowDoctorControls()
    {
        btnAddNewDoctor.Visible = false;
        btnAddNewDepartment.Visible = false;
        btnAddNewAdmin.Visible = false;
        btnViewAllDoctors.Visible = false;
        btnAddMedicine.Visible = false;
        btnAddCateory.Visible = false;
        btnAddImages.Visible = false;
        btnUpcomingAppointments.Visible = false;
        btnPreviousAppointments.Visible = false;
        btnSearchPatient.Visible = true;
    }

    private void ShowStaffControls()
    {
        btnAddNewDoctor.Visible = false;
        btnAddNewDepartment.Visible = false;
        btnAddNewAdmin.Visible = false;
        btnViewAllDoctors.Visible = true;
        btnAddCateory.Visible = true;
        btnUpcomingAppointments.Visible = true;
        btnPreviousAppointments.Visible = true;
        btnSearchPatient.Visible = true;
        btnAddMedicine.Visible = false;
    }

    private void LoadUserData(string username, string email, string usrRole)
    {
        try
        {
            string query = "";

            // Adjust query based on the role
            switch (usrRole.ToLower())
            {
                case "admin":
                    query = @"SELECT * FROM Admins WHERE Email = @Email AND Username = @Username";
                    break;
                case "doctor":
                    query = @"SELECT * FROM doctors WHERE Email = @Email AND DoctorID = @Username";
                    break;
                case "staff":
                    query = @"SELECT * FROM staff WHERE Email = @Email AND StaffID = @Username";
                    break;
                default:
                    throw new ArgumentException("Invalid role specified.");
            }

            using (SqlConnection con = new SqlConnection(strcon))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Username", username);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Assuming 'FullName' is a common column across the tables
                            lbluserName.Text = reader["FullName"].ToString();

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
        catch (Exception ex)
        {
            string message = HttpUtility.JavaScriptStringEncode(ex.Message);
            Response.Write("<script>alert('" + message + "');</script>");
        }
    }

    private void BindAppointments(string username)
    {

        using (SqlConnection con = new SqlConnection(strcon))
        {
            string query = "SELECT * FROM appointments where DoctorID = @DoctorID AND AppointmentDate > CONVERT(date, GETDATE()) AND Status NOT IN ('Cancelled', 'Completed') ORDER BY AppointmentDate ASC;";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();

                cmd.Parameters.AddWithValue("@DoctorID", username);

                SqlDataReader reader = cmd.ExecuteReader();

                dlAppointments.DataSource = reader;
                dlAppointments.DataBind();

                reader.Close();
            }
        }
    }

    protected void dlAppointments_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var lblstatus = (HtmlGenericControl)e.Item.FindControl("status");

            if (lblstatus != null)
            {
                string lblStatus = lblstatus.InnerText.Trim();

                switch (lblStatus)
                {
                    case "Confirmed":
                        lblstatus.Attributes["class"] += " badge bg-success";
                        break;
                    case "Cancelled":
                        lblstatus.Attributes["class"] += " badge bg-danger";
                        break;
                    case "Pending":
                        lblstatus.Attributes["class"] += " badge bg-warning";
                        break;
                    default:
                        lblstatus.Attributes["class"] += " badge bg-secondary";
                        break;
                }
            }
        }
    }

    public class Appointment
    {
        public DateTime AppointmentDate { get; set; }
        public string Time { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string DoctorID { get; set; }
    }

    protected void AppointmentCalendar_DayRender(object sender, DayRenderEventArgs e)
    {
        string doctorID = Session["DoctorID"]?.ToString();
        string currentUserRole = Session["role"]?.ToString();
        bool isAdmin = (currentUserRole == "Admin");

        if (!string.IsNullOrEmpty(doctorID) || isAdmin)
        {
            var appointments = GetAppointmentForDay(e.Day.Date, doctorID, isAdmin);

            // Initialize counters for appointment statuses
            int pendingCount = 0;
            int rescheduledCount = 0;
            int confirmedCount = 0;
            int cancelledCount = 0;

            // Create a hyperlink for the entire cell
            string redirectUrl = $"upappointment.aspx?date={e.Day.Date:yyyy-MM-dd}";
            HyperLink link = new HyperLink
            {
                NavigateUrl = redirectUrl,
                Text = string.Empty,
                CssClass = "full-cell-link",
                ToolTip = $"Click to view appointments for {e.Day.Date:MMMM dd, yyyy}"
            };

            // Initialize cell styles
            e.Cell.Attributes["style"] = "border: 1px solid #ddd; border-radius: 5px; padding: 5px;";

            foreach (var appointment in appointments)
            {
                // Count appointments for admin and apply background color for non-admin
                if (isAdmin)
                {
                    switch (appointment.Status.ToLower())
                    {
                        case "pending":
                            pendingCount++;
                            break;
                        case "rescheduled":
                            rescheduledCount++;
                            break;
                        case "confirmed":
                            confirmedCount++;
                            break;
                        case "cancelled":
                            cancelledCount++;
                            break;
                    }
                }
                else
                {
                    Color backgroundColor;
                    switch (appointment.Status.ToLower())
                    {
                        case "pending":
                            backgroundColor = System.Drawing.Color.Yellow;
                            pendingCount++;
                            break;
                        case "rescheduled":
                            backgroundColor = System.Drawing.Color.LightGray;
                            rescheduledCount++;
                            break;
                        case "confirmed":
                            backgroundColor = System.Drawing.Color.LightGreen;
                            confirmedCount++;
                            break;
                        case "cancelled":
                            backgroundColor = System.Drawing.Color.LightCoral;
                            cancelledCount++;
                            break;
                        default:
                            backgroundColor = System.Drawing.Color.LightGray;
                            break;
                    }
                    e.Cell.BackColor = backgroundColor;

                    // Add appointment details with styling for non-admin
                    var appointmentDetails = new LiteralControl(
                        $"<div style='font-size: 0.9em; color: #333;'>{appointment.Time} ({appointment.Title})</div>"
                    );
                    link.Controls.Add(appointmentDetails);
                }
            }

            // Add counts for admin
            if (isAdmin)
            {
                link.Controls.Add(new LiteralControl(
                    $"<div style='font-size: 0.8em; color: #333;'>Pending: {pendingCount}, Rescheduled: {rescheduledCount}, Confirmed: {confirmedCount}, Cancelled: {cancelledCount}</div>"
                ));
            }
            else if (appointments.Count == 0)
            {
                link.Controls.Add(new LiteralControl("<div style='padding: 5px; color: #999;'>No appointments</div>"));
            }

            // Add the hyperlink to the cell
            e.Cell.Controls.Add(link);
        }
        else
        {
            e.Cell.Controls.Add(new LiteralControl("<div style='padding: 5px; color: #f00;'>Doctor ID or role is not set</div>"));
        }
    }



    private List<Appointment> GetAppointmentForDay(DateTime date, string userID, bool isAdmin)
    {
        List<Appointment> appointments = new List<Appointment>();

        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query;
                if (isAdmin)
                {
                    // Admin can view all appointments for the specified date
                    query = "SELECT AppointmentDate, AppointmentTime, appointmentID, Status, DoctorID FROM appointments WHERE CAST(AppointmentDate AS DATE) = @AppointmentDate";
                }
                else
                {
                    // Doctor-specific appointments
                    query = "SELECT AppointmentDate, AppointmentTime, appointmentID, Status FROM appointments WHERE CAST(AppointmentDate AS DATE) = @AppointmentDate AND DoctorID = @DoctorID";
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AppointmentDate", date);

                    if (!isAdmin) // Only add DoctorID parameter if the user is not an admin
                    {
                        cmd.Parameters.AddWithValue("@DoctorID", userID);
                    }

                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appointments.Add(new Appointment
                            {
                                AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                                Time = reader["AppointmentTime"].ToString(),
                                Title = reader["appointmentID"].ToString(),
                                Status = reader["Status"].ToString(),
                                DoctorID = isAdmin ? reader["DoctorID"].ToString() : userID
                            });
                        }
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            // Handle SQL exceptions (e.g., connection issues, query errors)
            Console.WriteLine("SQL Error: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Handle general exceptions
            Console.WriteLine("Error: " + ex.Message);
        }

        return appointments;
    }



}