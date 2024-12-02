using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_viewalldoctors : System.Web.UI.Page
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
                BindDoctorsGrid();
            }
        }
        
    }

    private void BindDoctorsGrid()
    {
        try
        {
            string query = "SELECT * FROM doctors WHERE isActive = 1";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvDoctors.DataSource = dt;
                        gvDoctors.DataBind();
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


    protected void btnAddDoctorAvail_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;

        string doctorID = ((HiddenField)row.FindControl("hfDoctorID")).Value;
        string departmentID = ((HiddenField)row.FindControl("hfDepartmentID")).Value;

        // Redirect to the page where availability can be added, passing the DoctorID as a query string parameter
        Response.Redirect($"AddAvailability.aspx?DoctorID={doctorID}&DepartmentID={departmentID}");
    }

}