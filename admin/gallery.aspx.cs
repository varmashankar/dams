using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_gallery : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Populate the category dropdown list on the initial page load
            PopulateCategoryDropDownList();
            BindGridView();

        }
    }

    private void PopulateCategoryDropDownList()
    {
        string query = "SELECT categoryID, categoryName FROM galleryCat";


        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Bind the reader to the dropdown list
                CategoryDropDownList.DataSource = reader;
                CategoryDropDownList.DataTextField = "CategoryName"; // This is the field to display in the dropdown
                CategoryDropDownList.DataValueField = "CategoryId"; // This is the value corresponding to each item
                CategoryDropDownList.DataBind();

                // Add a default option
                CategoryDropDownList.Items.Insert(0, new ListItem("Select a category", "0"));

                connection.Close();
            }
        }
    }


    protected void UploadButton_Click(object sender, EventArgs e)
    {
        // Check if a file has been selected
        if (!FileUpload1.HasFile)
        {
            Response.Write("<script>alert('Please select a file to upload.');</script>");
            return;
        }

        // Check if a category has been selected
        if (string.IsNullOrEmpty(CategoryDropDownList.SelectedValue))
        {
            Response.Write("<script>alert('Please select a category.');</script>");
            return;
        }

        try
        {
            // Read the file content into a byte array
            byte[] fileBytes = FileUpload1.FileBytes;

            // Get the file name and content type
            string fileName = Path.GetFileName(FileUpload1.FileName);
            string contentType = FileUpload1.PostedFile.ContentType;

            // Get the selected category from the DropDownList
            string category = CategoryDropDownList.SelectedValue;

            // Insert the file into the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = " ";

                if (UploadButton.Text.Trim() == "Update")
                {
                    query = "UPDATE ddcimages SET filename = @filename, category = @category, filetype = @filetype, ImageData = @ImageData, uploadDate = getdate() WHERE id = @id";
                }
                else
                {
                    query = "INSERT INTO ddcimages (filename, category, filetype, ImageData, uploadDate) VALUES (@filename, @category, @filetype, @ImageData, getdate())";
                }

                SqlCommand command = new SqlCommand(query, connection);
                if (UploadButton.Text.Trim() != "Upload")
                {
                    command.Parameters.AddWithValue("@ID", ViewState["id"].ToString());
                }
                command.Parameters.AddWithValue("@filename", fileName);
                command.Parameters.AddWithValue("@category", category);
                command.Parameters.AddWithValue("@filetype", contentType);
                command.Parameters.AddWithValue("@ImageData", fileBytes);
                command.ExecuteNonQuery();

                if (UploadButton.Text.Trim() == "Upload")
                {
                    Response.Write("<script>alert('Image uploaded successfully.');</script>");
                    CategoryDropDownList.SelectedValue = null;
                }
                else
                {
                    Response.Write("<script>alert('Image updated successfully.');</script>");
                    CategoryDropDownList.SelectedValue = null;
                    Response.Redirect("gallery.aspx");
                }
            }
            BindGridView();
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
        }
    }

    private void BindGridView()
    {
        DataTable dt = new DataTable();

        try
        {
            string query = @"
            SELECT d.*, gc.categoryName
            FROM ddcimages d
            INNER JOIN gallerycat gc ON d.category = gc.categoryID
            ORDER BY d.uploadDate ASC";

            // Use a using statement to ensure the connection is properly disposed
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open(); // Open the database connection
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt); // Fill the DataTable with the result set
                }
            }
        }
        catch (Exception ex)
        {

            Response.Write($"<script>alert('An error occurred while fetching the data: {ex.Message}');</script>");
            return;
        }

        // Bind the fetched data to the GridView
        GridView1.DataSource = dt;
        GridView1.DataBind();

        // Set accessibility properties for the GridView
        GridView1.UseAccessibleHeader = true;
        if (GridView1.HeaderRow != null)
        {
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
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
                GridView1.EditIndex = rowIndex;

                // Rebind the GridView
                BindGridView();

                // Fetch data associated with this ID from your data source
                int id = Convert.ToInt32(GridView1.DataKeys[rowIndex].Value);
                ViewState["id"] = id; // Save the ID in ViewState

                DataRow row = GetDataById(id);

                if (row != null)
                {
                    // Populate form fields with data from the row
                    CategoryDropDownList.SelectedValue = row["category"].ToString();
                    UploadButton.Text = "Update";
                    // Populate other form fields as needed
                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception here
            Response.Write($"<script>alert('An error occurred: {ex.Message}');</script>");
        }
    }

    protected void toggleActiveState(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int id = Convert.ToInt32(btn.Attributes["data-id"]);

        // Get the current isActive value for the product
        int isActive = 0;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string sqlQuery = "SELECT isActive FROM ddcimages WHERE id = @id";
            using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    isActive = Convert.ToInt32(result);
                }
                else
                {
                    isActive = 0; // Set default value
                }
            }
        }

        // Toggle the isActive status of the product
        isActive = (isActive == 1) ? 0 : 1;

        // Update the database with the new isActive value
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string sqlQuery = "UPDATE ddcimages SET isActive = @isActive WHERE id = @id";
            using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@isActive", isActive);
                cmd.ExecuteNonQuery();
                BindGridView();
            }
        }
    }



    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    public DataRow GetDataById(int id)
    {
        string query = "SELECT * FROM ddcimages WHERE id = @Id";
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
        // Check if UploadButton text is "Upload"
        if (UploadButton.Text == "Upload")
        {
            // Clear selected category
            CategoryDropDownList.SelectedIndex = -1;

            // Clear selected file by creating a new instance of FileUpload control
            FileUpload1 = new FileUpload();
        }
        else
        {
            Response.Redirect("gallery.aspx");
        }
    }
}