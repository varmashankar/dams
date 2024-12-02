using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_newadmin : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        txtUsername.Focus();

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // Retrieve form values
        string username = txtUsername.Text.Trim();
        string email = txtEmail.Text.Trim();
        string fullName = txtFullName.Text.Trim();
        string password = txtPassword.Text;
        string role = "Admin";

        //hash password
        string passwordHash = HashPassword(password);

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = "INSERT INTO Admins (Username, PasswordHash, Email, FullName, role) VALUES (@Username, @PasswordHash, @Email, @FullName, @role)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash); // Make sure to hash passwords in a real application
                cmd.Parameters.AddWithValue("@role", role);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Write("<script type='text/javascript'>alert('Admin Created Successfully.');</script>");
                clearall();
            }
        }
        

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("newadmin.aspx");
    }

    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    void clearall()
    {
        txtUsername.Text = null;
        txtEmail.Text = null;
        txtFullName.Text = null;
    }
}