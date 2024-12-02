using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class patient_profile : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadPatientProfile();
        }
    }

    private void LoadPatientProfile()
    {
        // Assuming you have a PatientID in session
        if (Session["PatientID"] != null)
        {
            string patientId = Session["PatientID"].ToString();

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = "SELECT firstname, lastname, email, phone, dob, address, gender FROM patient_master WHERE patient_id = @PatientID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PatientID", patientId);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Check for null values and assign to text boxes
                        txtFirstName.Text = reader["firstname"] != DBNull.Value ? reader["firstname"].ToString() : "N/A";
                        txtLastName.Text = reader["lastname"] != DBNull.Value ? reader["lastname"].ToString() : "N/A";
                        txtEmail.Text = reader["email"] != DBNull.Value ? reader["email"].ToString() : "N/A";
                        txtPhone.Text = reader["phone"] != DBNull.Value ? reader["phone"].ToString() : "N/A";
                        txtDOB.Text = reader["dob"] != DBNull.Value ? Convert.ToDateTime(reader["dob"]).ToString("yyyy-MM-dd") : "N/A"; // format the date
                        txtAddress.Text = reader["address"] != DBNull.Value ? reader["address"].ToString() : "N/A";
                        txtGender.Text = reader["gender"] != DBNull.Value ? reader["gender"].ToString() : "N/A";
                    }
                    else
                    {
                        // Optionally handle the case where no records are found
                        // You might want to show a message or log it
                    }
                }
                catch (Exception ex)
                {
                    // Log the error
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        else
        {
            // Optionally handle the case where PatientID is not in session
            // Redirect to a login page or show a message
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        // Enable text boxes for editing
        txtFirstName.ReadOnly = false;
        txtLastName.ReadOnly = false;
        txtEmail.ReadOnly = false;
        txtPhone.ReadOnly = false;
        txtDOB.ReadOnly = false;
        txtAddress.ReadOnly = false;
        txtGender.ReadOnly = false;

        // Toggle button visibility
        btnEdit.Visible = false; // Hide edit button
        btnSave.Visible = true; // Show save button
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // Save updated patient profile
        if (Session["PatientID"] != null)
        {
            string patientId = Session["PatientID"].ToString();

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = "UPDATE patient_master SET firstname = @FirstName, lastname = @LastName, email = @Email, phone = @Phone, dob = @DOB, address = @Address, gender = @Gender WHERE patient_id = @PatientID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@DOB", DateTime.Parse(txtDOB.Text));
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Gender", txtGender.Text);
                cmd.Parameters.AddWithValue("@PatientID", patientId);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Optionally, display a success message
                    Console.WriteLine("Profile updated successfully!");
                }
                catch (Exception ex)
                {
                    // Log the error
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    // Set text boxes to read-only again
                    txtFirstName.ReadOnly = true;
                    txtLastName.ReadOnly = true;
                    txtEmail.ReadOnly = true;
                    txtPhone.ReadOnly = true;
                    txtDOB.ReadOnly = true;
                    txtAddress.ReadOnly = true;
                    txtGender.ReadOnly = true;

                    // Toggle button visibility
                    btnEdit.Visible = true; // Show edit button
                    btnSave.Visible = false; // Hide save button
                }
            }
        }
    }


    
}