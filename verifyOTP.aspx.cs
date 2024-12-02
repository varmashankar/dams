using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class verifyOTP : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Email"] != null)
        {
            textOTP.Text = "OTP sent successfully to your email.";
        }
    }
    protected void btnVerifyOTP_Click(object sender, EventArgs e)
    {
        string email = Request.QueryString["email"];
        string enteredOTP = txtOTP.Text.Trim();

        if (IsValidOTP(email, enteredOTP))
        {
            Response.Redirect("ResetPassword.aspx?email=" + email);
        }
        else
        {
            Response.Write("<script>alert('Invalid or expired OTP.');</script>");
        }
    }

    private bool IsValidOTP(string email, string otp)
    {
        bool isValid = false;

        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM patient_master WHERE email = @Email AND OTP = @OTP AND OTPExpiry > @CurrentTime", con))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@OTP", otp);
                cmd.Parameters.AddWithValue("@CurrentTime", DateTime.Now);
                isValid = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
        return isValid;
    }


}