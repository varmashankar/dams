using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class patient_dashboard : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if the session variables are set
        if (Session["Email"] == null || Session["Phone"] == null)
        {
            // Handle the scenario if the session has expired or is not set
            string script = @"
                <script type='text/javascript'>
                    alert('Session Expired. Kindly Login Again.');
                    setTimeout(function() {
                        window.location.href = '../login.aspx';
                    }, 1000);
                </script>";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "SessionExpiredScript", script, true);
        }
        else
        {
            // Retrieve session information
            string email = Session["Email"].ToString();
            string phone = Session["Phone"].ToString();
            string patientID = Session["PatientID"].ToString();

            // Fetch and load data from the database
            LoadUserData(email, phone);
            BindAppointments(patientID);



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
                        lblPatientName.Text = reader["firstname"].ToString() + " " + reader["lastname"].ToString();
                        string patientId = reader["patient_id"].ToString();
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

    private void BindAppointments(string patientID)
    {

        using (SqlConnection con = new SqlConnection(strcon))
        {
            string query = "SELECT * FROM appointments where PatientID = @PatientID AND AppointmentDate > CONVERT(date, GETDATE()) AND Status NOT IN ('Cancelled', 'Completed') ORDER BY AppointmentDate ASC;";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();

                cmd.Parameters.AddWithValue("PatientID", patientID);

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


    protected void btnViewDetails_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string appointmentID = btn.CommandArgument;
        Response.Redirect("AppointmentDetails.aspx?appointmentID=" + appointmentID);
    }

    public class Appointment
    {
        public DateTime AppointmentDate { get; set; }
        public string Time { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string PatientID { get; set; }
    }


    protected void AppointmentCalendar_DayRender(object sender, DayRenderEventArgs e)
    {
        string patientID = Session["PatientID"]?.ToString();

        if (!string.IsNullOrEmpty(patientID))
        {
            Appointment appointment = GetAppointmentForDay(e.Day.Date, patientID);

            if (appointment != null)
            {
                Color backgroundColor;
                switch (appointment.Status.ToLower())
                {
                    case "pending":
                        backgroundColor = System.Drawing.Color.Yellow;
                        break;
                    case "rescheduled":
                        backgroundColor = System.Drawing.Color.LightGray;
                        break;
                    case "confirmed":
                        backgroundColor = System.Drawing.Color.LightGreen;
                        break;
                    case "cancelled":
                        backgroundColor = System.Drawing.Color.LightCoral;
                        break;
                    default:
                        backgroundColor = System.Drawing.Color.LightGray;
                        break;
                }
                e.Cell.BackColor = backgroundColor;

                // Add appointment details with styling
                var appointmentDetails = new LiteralControl(
                    $"<div style='font-size: 0.9em; color: #333;'>{appointment.Time}  ({appointment.Title})</div>"
                );
                e.Cell.Controls.Add(appointmentDetails);

                e.Cell.Attributes["title"] = $"{appointment.Time}: {appointment.Title} (Status: {appointment.Status})";

                e.Cell.Attributes["style"] = "border: 1px solid #ddd; border-radius: 5px; padding: 5px;";

            }
            else
            {
                e.Cell.Controls.Add(new LiteralControl("<div style='padding: 5px; color: #999;'>No appointments</div>"));
            }
        }
        else
        {
            e.Cell.Controls.Add(new LiteralControl("<div style='padding: 5px; color: #f00;'>Patient ID is not set</div>"));
        }
    }



    private Appointment GetAppointmentForDay(DateTime date, string patientID)
    {
        Appointment appointment = null;

        // Replace with actual database logic
        
        using (SqlConnection con = new SqlConnection(strcon))
        {
            string query = "SELECT AppointmentDate, AppointmentTime, appointmentID, Status FROM appointments WHERE CAST(AppointmentDate AS DATE) = @AppointmentDate AND PatientID = @PatientID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@AppointmentDate", date);
                cmd.Parameters.AddWithValue("@PatientID", patientID);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        appointment = new Appointment
                        {
                            AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                            Time = reader["AppointmentTime"].ToString(),
                            Title = reader["appointmentID"].ToString(),
                            Status = reader["Status"].ToString(),
                            PatientID = patientID
                        };
                    }
                }
            }
        }

        return appointment;
    }



}