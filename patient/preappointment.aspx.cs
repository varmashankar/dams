using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class patient_preappointment : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                BindAppointments();
            }
        }
        
    }

    private void BindAppointments()
    {
        //getting patientID
        string email = Session["email"].ToString();
        string phone = Session["phone"].ToString();
        string patientID = GetPatientID(email, phone);

        using (SqlConnection con = new SqlConnection(strcon))
        {
            string query = "SELECT * FROM appointments WHERE PatientID =@patientID AND DateCreated < CONVERT(date, GETDATE()) ORDER BY AppointmentDate DESC";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("patientID", patientID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                gvAppointments.DataSource = dt;
                gvAppointments.DataBind();
            }
        }
    }

    protected void btnViewDetails_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string appointmentID = btn.CommandArgument;
        // Redirect to appointment details page with the selected appointmentID
        Response.Redirect("AppointmentDetails.aspx?appointmentID=" + appointmentID);
    }

    public string GetPatientID(string email, string phone)
    {
        string patientID = null;

        using (SqlConnection con = new SqlConnection(strcon))
        {
            string query = "SELECT patient_id FROM patient_master WHERE email = @Email AND phone = @Phone";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);

                con.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    patientID = result.ToString();
                }
            }
        }

        return patientID;
    }

    protected void gvAppointments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");

            if (lblStatus != null)
            {
                switch (lblStatus.Text.Trim())
                {
                    case "Confirmed":
                        lblStatus.CssClass = "badge bg-success";
                        break;
                    case "Cancelled":
                        lblStatus.CssClass = "badge bg-danger";
                        break;
                    case "Pending":
                        lblStatus.CssClass = "badge bg-warning";
                        break;
                    default:
                        lblStatus.CssClass = "badge bg-secondary";
                        break;
                }
            }
        }
    }
}