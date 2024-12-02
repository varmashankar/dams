using Microsoft.SqlServer.Server;
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

public partial class login : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        Username.Focus();
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        try
        {
            string username = Username.Text;
            string password = Password.Text;

            /*Hashed Password*/

            string passwordHash = HashPassword(password);

            string query = @"select * from patient_master where (email = @username OR phone = @username) AND passwordHash = @password";

            using ( SqlConnection con = new SqlConnection(strcon))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", passwordHash);

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            string patientID = dr["patient_id"].ToString();
                            string email = dr["email"].ToString();
                            string phone = dr["phone"].ToString();

                            // storing the data in session

                            Session["PatientID"] = patientID;
                            Session["Email"] = email;
                            Session["Phone"] = phone;
                        }
                        // Register the toast notification and delay script
                        string script = @"
                            <script type='text/javascript'>
                                // Display a toast notification
                            toastr.success('Login successful! Redirecting to your dashboard...');
                            window.location.href = 'patient/dashboard.aspx';
                        </script>";

                        // Register the script on the page
                        ClientScript.RegisterStartupScript(this.GetType(), "RedirectWithToast", script);
                    }
                    else
                    {
                        string failScript = @"
                            <script type='text/javascript'>
                                    toastr.error('Login failed! Please check your credentials.');
                            </script>";

                        ClientScript.RegisterStartupScript(this.GetType(), "LoginFailed", failScript);
                        Username.Text = string.Empty;
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
}