using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_newdepartment : System.Web.UI.Page
{
    private string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

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
        else
        {
            if (!IsPostBack)
            {
                BindDepartmentGrid();
            }
        }
        
    }

    protected void btnAddDepartment_Click(object sender, EventArgs e)
    {
        string departmentName = txtDepartmentName.Text.Trim();
        DateTime createdate = DateTime.Now;
        string createdby = Session["Username"].ToString();

        try
        {
            string query = @"
                INSERT INTO department (departmentID, departmentName, CreatedBy, CreatedDate)
                VALUES (@departmentID, @departmentName, @CreatedBy, @CreatedDate)";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@departmentID", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@departmentName", departmentName);
                    cmd.Parameters.AddWithValue("@CreatedBy", createdby);
                    cmd.Parameters.AddWithValue("@CreatedDate", createdate);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Department added successfully.";
                    lblMessage.CssClass = "text-success";


                    txtDepartmentName.Text = " ";

                    // Rebind grid
                    BindDepartmentGrid();
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
            lblMessage.CssClass = "text-danger";
        }
    }

    private void BindDepartmentGrid()
    {
        string query = "SELECT * FROM department WHERE isActive = 1 ORDER BY CreatedDate ASC";

        using (SqlConnection con = new SqlConnection(strcon))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvDepartments.DataSource = dt;
                    gvDepartments.DataBind();
                }
            }
        }
    }

    protected void gvDepartments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDepartments.PageIndex = e.NewPageIndex;
        BindDepartmentGrid(); // Method to rebind data to the GridView
    }



    protected void gvDepartments_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string departmentID = gvDepartments.DataKeys[e.RowIndex].Value.ToString();

        try
        {
            string query = "DELETE FROM department WHERE departmentID = @DepartmentID";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DepartmentID", departmentID);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Department deleted successfully.";
                    lblMessage.CssClass = "text-success";

                    // Rebind grid
                    BindDepartmentGrid();
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
            lblMessage.CssClass = "text-danger";
        }
    }
}