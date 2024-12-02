using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_admin : System.Web.UI.MasterPage
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
        else if(Session["role"].Equals("Admin"))
        {
            string username = Session["Username"].ToString();
            string email = Session["Email"].ToString();
            string usrRole = Session["role"].ToString();

            // Fetch and load data from the database
            LoadUserData(username, email, usrRole);
        }
        else if (Session["role"].Equals("Doctor"))
        {
            string username = Session["DoctorID"].ToString();
            string email = Session["Email"].ToString();
            string phone = Session["Phone"].ToString();
            string usrRole = Session["role"].ToString();

            // Fetch and load data from the database
            LoadUserData(username, email, usrRole);


        }
        else if (Session["role"].Equals("Staff"))
        {
            string username = Session["StaffID"].ToString();
            string email = Session["Email"].ToString();
            string phone = Session["Phone"].ToString();
            string usrRole = Session["role"].ToString();

            // Fetch and load data from the database
            LoadUserData(username, email, usrRole);

        }
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
                            patientName.Text = reader["FullName"].ToString();
                            lblRole.Text = reader["role"].ToString();
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


    protected void logout_click(object sender, EventArgs e)
    {
        // Clear the session
        Session.Clear();
        Session.Abandon();
        Response.Redirect("~/doctorlogin.aspx");
    }
}
