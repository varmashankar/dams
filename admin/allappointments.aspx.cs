using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_allappointments : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

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
                string role = Session["role"].ToString(); // Get user role from session

                if (role == "Admin")
                {
                    BindAllPendingAppointments(); // Load all pending appointments for admin
                }
                else if (role == "Doctor")
                {
                    string departmentID = GetDepartmentID(); // Retrieve department ID for the doctor
                    if (!string.IsNullOrEmpty(departmentID))
                    {
                        BindAppointmentsGrid(departmentID); // Pass the department ID to the binding method for doctors
                    }
                    else
                    {
                        // Handle the case where the department ID is not found
                        // You might want to show an error message or handle it as per your requirements
                    }
                }
            }
        }
    }

    // Method to bind appointments data to the GridView for doctors (Cancelled/Completed)
    private void BindAppointmentsGrid(string departmentID, string sortExpression = null)
    {
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            // Select only "Cancelled" or "Completed" appointments and order by AppointmentDate
            string query = @"SELECT a.AppointmentID, a.PatientID, a.AppointmentDate, a.AppointmentTime, a.DoctorName, 
                         d.DepartmentName, a.Status 
                         FROM Appointments a
                         INNER JOIN Department d ON a.DepartmentID = d.DepartmentID
                         WHERE a.Status IN ('Cancelled', 'Completed') AND a.DepartmentID = @DepartmentID
                         ORDER BY a.AppointmentDate"; // Sorting by AppointmentDate by default

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@DepartmentID", departmentID); // Add the parameter
                conn.Open();

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    // Sort data if a sort expression is provided
                    if (!string.IsNullOrEmpty(sortExpression))
                    {
                        DataView dv = dt.AsDataView();
                        dv.Sort = sortExpression;
                        gvAppointments.DataSource = dv;
                    }
                    else
                    {
                        gvAppointments.DataSource = dt;
                    }

                    gvAppointments.DataBind();
                }
            }
        }
    }

    // Method to bind all pending appointments for admin
    private void BindAllPendingAppointments(string sortExpression = null)
    {
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            // Select only "Pending" appointments and order by AppointmentDate
            string query = @"SELECT a.AppointmentID, a.PatientID, a.AppointmentDate, a.AppointmentTime, a.DoctorName, 
                         d.DepartmentName, a.Status 
                         FROM Appointments a
                         INNER JOIN Department d ON a.DepartmentID = d.DepartmentID
                         WHERE a.Status = 'Pending'
                         ORDER BY a.AppointmentDate"; // Sorting by AppointmentDate by default

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    // Sort data if a sort expression is provided
                    if (!string.IsNullOrEmpty(sortExpression))
                    {
                        DataView dv = dt.AsDataView();
                        dv.Sort = sortExpression;
                        gvAppointments.DataSource = dv;
                    }
                    else
                    {
                        gvAppointments.DataSource = dt;
                    }

                    gvAppointments.DataBind();
                }
            }
        }
    }

    // Handle page indexing for GridView paging
    protected void gvAppointments_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gvAppointments.PageIndex = e.NewPageIndex;

        // Retrieve role from session to determine which appointments to load
        string role = Session["role"].ToString();

        if (role == "Admin")
        {
            BindAllPendingAppointments(); // Re-bind pending appointments for admin
        }
        else if (role == "Doctor")
        {
            string departmentID = GetDepartmentID(); // Retrieve the department ID again for paging
            BindAppointmentsGrid(departmentID); // Re-bind data for doctor
        }
    }

    // Handle sorting
    protected void gvAppointments_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {
        string sortDirection = GetSortDirection(e.SortExpression);

        // Retrieve role from session to determine sorting logic
        string role = Session["role"].ToString();

        if (role == "Admin")
        {
            BindAllPendingAppointments(e.SortExpression + " " + sortDirection); // Sort pending appointments for admin
        }
        else if (role == "Doctor")
        {
            string departmentID = GetDepartmentID(); // Retrieve department ID for sorting
            BindAppointmentsGrid(departmentID, e.SortExpression + " " + sortDirection); // Sort for doctor
        }
    }

    // Method to get the sort direction (ascending or descending)
    private string GetSortDirection(string column)
    {
        // By default, sort direction is ascending
        string sortDirection = "ASC";
        string previousSortExpression = ViewState["SortExpression"] as string;

        // If the same column is being sorted again, reverse the sort direction
        if (previousSortExpression != null && previousSortExpression == column)
        {
            string previousSortDirection = ViewState["SortDirection"] as string;
            if ((previousSortDirection != null) && (previousSortDirection == "ASC"))
            {
                sortDirection = "DESC";
            }
        }

        // Save new sort expression and direction in ViewState
        ViewState["SortExpression"] = column;
        ViewState["SortDirection"] = sortDirection;

        return sortDirection;
    }

    // Method to get department ID for doctor
    private string GetDepartmentID()
    {
        string departmentID = string.Empty; // Initialize the variable to store the department ID

        // SQL query to fetch the department ID based on the user's role or another identifier
        string query = "SELECT DepartmentID FROM doctors WHERE DoctorID = @DoctorID"; // Adjust the query according to your table structure

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            // Open the connection
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Add the doctor ID parameter from the session or context
                cmd.Parameters.AddWithValue("@DoctorID", Session["doctorID"]); // Adjust the session variable name as necessary

                // Execute the command and retrieve the department ID
                object result = cmd.ExecuteScalar();

                // Check if a result was returned
                if (result != null)
                {
                    departmentID = result.ToString(); // Convert the result to string
                }
            }
        }

        return departmentID; // Return the department ID or empty string if not found
    }


}
