using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_category : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadCategory();
        }
    }

    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        string catName = categoryName.Text.Trim();

        // Server-side validation for the category name
        if (string.IsNullOrEmpty(catName))
        {
            Response.Write("<script>alert('Category name cannot be empty!');</script>");
            return;
        }

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = " ";

            if (btnAddCategory.Text.Trim() == "Add")
            {
                query = "INSERT INTO galleryCat (categoryID, categoryName, createdDate) VALUES (@categoryID, @CategoryName, GETDATE())";
            }
            else
            {
                query = "UPDATE galleryCat SET categoryName = @CategoryName, createdDate = GETDATE() WHERE id = @categoryID";
            }

            SqlCommand command = new SqlCommand(query, connection);
            if (btnAddCategory.Text.Trim() == "Update")
            {
                command.Parameters.AddWithValue("@categoryID", ViewState["id"].ToString());
            }
            else
            {
                command.Parameters.AddWithValue("@categoryID", Guid.NewGuid());
            }
            command.Parameters.AddWithValue("@CategoryName", catName);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();

                if (btnAddCategory.Text.Trim() == "Update")
                {
                    Response.Write("<script>alert('Category updated successfully!');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Category added successfully!');</script>");
                    categoryName.Text = "";
                }

                Response.Redirect("category.aspx");
            }
            catch (Exception ex)
            {
                // Handle the exception, display an error message, or log it
                Response.Write("<script>alert('An error occurred while processing the request: " + ex.Message + "');</script>");
            }
        }
    }


    protected void loadCategory()
    {
        string query = "Select * from gallerycat";

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridViewCategory.DataSource = dt;
                    GridViewCategory.DataBind();
                }
            }
            GridViewCategory.UseAccessibleHeader = true;
            GridViewCategory.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex + ")</script>");
        }
    }

    protected void StatusButton_Click(object sender, EventArgs e)
    {
        // Find the reference to the clicked StatusButton
        LinkButton statusButton = (LinkButton)sender;

        // Find the row that contains the clicked StatusButton
        GridViewRow row = (GridViewRow)statusButton.NamingContainer;

        // Access the data key of the row to get the unique identifier of the item
        int itemId = Convert.ToInt32(GridViewCategory.DataKeys[row.RowIndex].Value);

        // Update the isActive column in the database
        UpdateIsActive(itemId);

        // Hide the row after updating the database
        row.Visible = false;
    }

    private void UpdateIsActive(int itemId)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string updateQuery = "UPDATE gallerycat SET isActive = ~isActive WHERE Id = @ItemId";
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@ItemId", itemId);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    // No rows were updated
                    Console.WriteLine("No rows were updated for itemId: " + itemId);
                    Response.Write("<script>alert('No rows were updated.);</script>");
                }
                else
                {
                    // Rows were updated successfully
                    Console.WriteLine("isActive column updated successfully for itemId: " + itemId);
                    Response.Write("<script>alert('Category Updated Successfully.');</script>");
                    Response.Redirect("category.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions
            Console.WriteLine("Error updating isActive column: " + ex.Message);
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditRow")
            {
                // Extract the row index from the command argument
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Set the EditIndex of the GridView to the selected row
                GridViewCategory.EditIndex = rowIndex;

                // Rebind the GridView
                loadCategory();

                // Fetch data associated with this ID from your data source
                int id = Convert.ToInt32(GridViewCategory.DataKeys[rowIndex].Value);
                // Save the ID in ViewState
                GridViewRow rowID = GridViewCategory.Rows[rowIndex];
                ViewState["id"] = GridViewCategory.DataKeys[rowID.RowIndex].Value.ToString(); ;
                DataRow row = GetDataById(id);

                if (row != null)
                {
                    // Populate form fields with data from the row
                    categoryName.Text = row["categoryName"].ToString();
                    categoryName.Focus();
                    btnAddCategory.Text = "Update";
                    // Repeat this for each form field
                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception here
            Response.Write("Error: " + ex.Message);
        }
    }
    public DataRow GetDataById(int id)
    {
        string query = "SELECT * FROM gallerycat WHERE id = @Id";
        DataTable dt = new DataTable();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
            }
        }

        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0];
        }
        else
        {
            return null;
        }
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("category.aspx");
    }
}