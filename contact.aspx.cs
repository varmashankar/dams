using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contact : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSendMessage_Click(object sender, EventArgs e)
    {
        string name = txtName.Text.Trim();
        string email = txtEmail.Text.Trim();
        string phone = txtPhone.Text.Trim();
        string department = ddlDepartment.SelectedValue;
        string subject = txtSubject.Text.Trim();
        string message = txtMessage.Text.Trim();

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = "INSERT INTO ContactMessages (Name, Email, Phone, Department, Subject, Message) VALUES (@Name, @Email, @Phone, @Department, @Subject, @Message)";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Department", department);
                cmd.Parameters.AddWithValue("@Subject", subject);
                cmd.Parameters.AddWithValue("@Message", message);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Show a confirmation message
        lblConfirmation.Text = "Your message has been sent successfully! We will get back to you soon.";
        lblConfirmation.Visible = true; // Make the label visible

        // Optionally, clear the form
        ClearForm();
    }

    private void ClearForm()
    {
        txtName.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtPhone.Text = string.Empty;
        ddlDepartment.SelectedIndex = 0; // Reset dropdown
        txtSubject.Text = string.Empty;
        txtMessage.Text = string.Empty;
    }

}