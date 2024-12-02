using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

public partial class doctorlogin : System.Web.UI.Page
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
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        try
        {
            string userType = ddlUserType.SelectedValue;
            string username = Username.Text.Trim();
            string password = Password.Text.Trim();
            string passHash = HashPassword(password);

            string query = "";
            SqlParameter[] parameters = null;

            // Define the query and parameters based on user type
            switch (userType)
            {
                case "Admin":
                    query = @"SELECT * FROM Admins WHERE Username = @username AND PasswordHash = @password";
                    parameters = new[]
                    {
                    new SqlParameter("@username", username),
                    new SqlParameter("@password", passHash)
                };
                    break;
                case "Doctor":
                case "Staff":
                    query = @"SELECT * FROM doctors WHERE (email = @username OR phone = @username) AND passwordHash = @password";
                    parameters = new[]
                    {
                    new SqlParameter("@username", username),
                    new SqlParameter("@password", passHash)
                };
                    break;
                default:
                    throw new Exception("Invalid user type selected.");
            }

            using (SqlConnection con = new SqlConnection(strcon))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddRange(parameters);
                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            StoreUserSession(userType, dr);

                            string script = @"
                        <script type='text/javascript'>
                            toastr.success('Login successful! Redirecting to your dashboard...');
                            window.location.href = 'admin/dashboard.aspx';
                        </script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "RedirectWithToast", script);
                        }
                        else
                        {
                            ShowErrorMessage("Invalid username or password.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }

    private void StoreUserSession(string userType, SqlDataReader dr)
    {
        switch (userType)
        {
            case "Admin":
                Session["UserID"] = dr["AdminID"].ToString();
                Session["Username"] = dr["Username"].ToString();
                Session["Email"] = dr["Email"].ToString();
                Session["FullName"] = dr["FullName"].ToString();
                Session["role"] = dr["role"].ToString();
                break;
            case "Doctor":
                Session["UserID"] = dr["DoctorID"].ToString();
                Session["DoctorID"] = dr["DoctorID"].ToString();
                Session["Email"] = dr["Email"].ToString();
                Session["Phone"] = dr["Phone"].ToString();
                Session["role"] = dr["role"].ToString();
                break;
            
        }
    }

    private void ShowErrorMessage(string message)
    {
        string script = $@"
    <script type='text/javascript'>
        toastr.error('{HttpUtility.JavaScriptStringEncode(message)}');
    </script>";
        ClientScript.RegisterStartupScript(this.GetType(), "LoginFailed", script);
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