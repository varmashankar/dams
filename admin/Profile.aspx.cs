using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Profile : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUserProfile();
        }
    }

    private void LoadUserProfile()
    {
        string userId = GetCurrentUserId(); // Method to get current user ID
        string role = GetCurrentUserRole(); // Method to get the current user's role

        if (role == "Doctor")
        {
            LoadDoctorProfile(userId);
        }
        else if (role == "Admin")
        {
            LoadAdminProfile(userId);
        }
        
    }

    private void LoadDoctorProfile(string doctorId)
    {
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = "SELECT * FROM Doctors WHERE DoctorID = @DoctorID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@DoctorID", doctorId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtFullName.Text = reader["FullName"] != DBNull.Value ? reader["FullName"].ToString() : "N/A";
                txtEmail.Text = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "N/A";
                txtPhone.Text = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : "N/A";
                txtRole.Text = reader["role"] != DBNull.Value ? reader["role"].ToString() : "N/A";
                txtSpeciality.Text = reader["Specialty"] != DBNull.Value ? reader["Specialty"].ToString() : "N/A";
            }
            conn.Close();
        }
    }

    private void LoadAdminProfile(string adminId)
    {
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = "SELECT * FROM Admins WHERE AdminID = @AdminID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@AdminID", adminId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtFullName.Text = reader["FullName"] != DBNull.Value ? reader["FullName"].ToString() : "N/A";
                txtEmail.Text = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "N/A";
                txtRole.Text = reader["role"] != DBNull.Value ? reader["role"].ToString() : "N/A"; // Include role if applicable
            }
            conn.Close();
        }
    }




    private string GetCurrentUserId()
    {
        // Implement your logic to retrieve the current user's ID
        // For example, from session or authentication context
        return Session["UserID"]?.ToString(); // Replace with actual session variable
    }

    private string GetCurrentUserRole()
    {
        // Implement your logic to retrieve the current user's role
        return Session["Role"]?.ToString(); // Replace with actual session variable
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        // Clear the session
        Session.Clear();
        Session.Abandon();
        Response.Redirect("~/doctorlogin.aspx");
    }
    protected void btnEditProfile_Click(object sender, EventArgs e)
    {
        // Enable textboxes for editing
        txtFullName.ReadOnly = false;
        txtEmail.ReadOnly = false;
        txtPhone.ReadOnly = false;
        txtSpeciality.ReadOnly = false;

        // Show the Save button
        btnSaveProfile.Visible = true;

        // Optionally, hide the Edit button while editing
        btnEditProfile.Visible = false;
    }

    protected void btnSaveProfile_Click(object sender, EventArgs e)
    {
        string userId = GetCurrentUserId(); // Get the current user ID
        string role = GetCurrentUserRole(); // Get the current user's role

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = "";

            if (role == "Doctor")
            {
                query = "UPDATE Doctors SET FullName = @FullName, Email = @Email, Phone = @Phone, Specialty = @Speciality WHERE DoctorID = @DoctorID";
            }
            else if (role == "Admin")
            {
                query = "UPDATE Admins SET FullName = @FullName, Email = @Email WHERE AdminID = @AdminID";
            }

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
            cmd.Parameters.AddWithValue("@Speciality", txtSpeciality.Text);
            cmd.Parameters.AddWithValue(role == "Doctor" ? "@DoctorID" : "@AdminID", userId);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // After saving, disable editing
        txtFullName.ReadOnly = true;
        txtEmail.ReadOnly = true;
        txtPhone.ReadOnly = true;
        txtSpeciality.ReadOnly = true;

        // Hide the Save button and show the Edit button again
        btnSaveProfile.Visible = false;
        btnEditProfile.Visible = true;
    }


}

