using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_addmedicine : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SubmitMedicine(object sender, EventArgs e)
    {
        string medicineName = txtMedicineName.Text.Trim();
        string medicineType = ddlMedicineType.SelectedValue;
        

        // Save medicine details to the database
        try
        {
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                conn.Open();
                string query = "INSERT INTO Medicines (MedicineID, MedicineName, MedicineType, CreatedAt) VALUES (@MedicineID, @Name, @Type, getdate())";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MedicineID", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@Name", medicineName);
                    cmd.Parameters.AddWithValue("@Type", medicineType);
                    cmd.ExecuteNonQuery();
                }
            }

            // Show confirmation message
            lblConfirmation.Text = "Medicine added successfully!";
            lblConfirmation.Visible = true;

            // Optionally, clear the form fields
            ClearFormFields();
        }
        catch (Exception ex)
        {
            // Handle error
            lblConfirmation.Text = "Error adding medicine: " + ex.Message;
            lblConfirmation.Visible = true;
        }
    }

    private void ClearFormFields()
    {
        txtMedicineName.Text = string.Empty;
        ddlMedicineType.SelectedIndex = 0;
    }
}