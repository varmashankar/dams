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

public partial class ResetPassword : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnResetPassword_Click(object sender, EventArgs e)
    {
        string email = Request.QueryString["email"];
        string newPassword = txtNewPassword.Text.Trim();

        //Hashed Password
        string newHashPass = HashPassword(newPassword);

        if (ResetUserPassword(email, newHashPass))
        {
            string script = @"
                            <script type='text/javascript'>
                                alert('Password Reset Successfully.');
                                window.location.href = 'login.aspx';
                            </script>";

            // Register the script on the page
            ClientScript.RegisterStartupScript(this.GetType(), "resetSucess", script);
        }
        else
        {
            string script = @"
                            <script type='text/javascript'>
                                // Display a toast notification
                            alert('Failed to reset password.');
                            window.location.href = 'forgotpassword.aspx';    
                        </script>";

            // Register the script on the page
            ClientScript.RegisterStartupScript(this.GetType(), "resetSucess", script);
        }
    }

    private bool ResetUserPassword(string email, string newHashPass)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE patient_master SET passwordHash = @Password WHERE email = @Email", con))
                {
                    cmd.Parameters.AddWithValue("@Password", newHashPass);
                    cmd.Parameters.AddWithValue("@Email", email);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (SqlException sqlEx)
        {
            string message = HttpUtility.JavaScriptStringEncode(sqlEx.Message);
            Response.Write("<script>alert('" + message + "');</script>");
            return false;
        }
        catch (Exception ex)
        {
            string message = HttpUtility.JavaScriptStringEncode(ex.Message);
            Response.Write("<script>alert('" + message + "');</script>");
            return false;
        }
    }


    private string HashPassword(string newPassword)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}