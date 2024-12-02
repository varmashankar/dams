using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class gallery : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Call a method to populate the DropDownList with categories
            PopulateCategoryRepeater();
            LoadImagesByCategory();
        }
    }

    private void PopulateCategoryRepeater()
    {
        // Fetch categories from the database
        DataTable categoriesTable = GetCategoriesFromDatabase();

        // Bind the DataTable to the Repeater
        CategoryRepeater.DataSource = categoriesTable;
        CategoryRepeater.DataBind();
    }

    private DataTable GetCategoriesFromDatabase()
    {
        // Connect to your database and fetch categories
        DataTable categoriesTable = new DataTable();
        // Replace "YourConnectionString" with your actual connection string
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT CategoryID, CategoryName FROM galleryCat where isActive = 'True'"; // Adjust the query as per your database schema
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(categoriesTable);
        }

        // Add a new row for the "All" category
        DataRow allRow = categoriesTable.NewRow();
        allRow["CategoryID"] = Guid.Empty; // You can set this to any value that doesn't conflict with existing CategoryIDs
        allRow["CategoryName"] = "All";
        categoriesTable.Rows.InsertAt(allRow, 0); // Insert the "All" category at the beginning

        return categoriesTable;
    }

    private void LoadImagesByCategory()
    {
        string categoryIdString = Request.QueryString["CategoryID"];

        // Check if CategoryID is provided in the query string
        if (!string.IsNullOrEmpty(categoryIdString))
        {
            // Attempt to parse the CategoryID to Guid
            // Declare the categoryGuid variable outside of the TryParse method
            Guid categoryGuid;
            if (!Guid.TryParse(categoryIdString, out categoryGuid))
            {
                // Handle the case when categoryId cannot be parsed to Guid
                // For example, you can display a message or log an error
                Response.Write("<script>alert('Invalid category ID format.')</script>");
                return;
            }

            // Fetch images from database based on the selected category
            DataTable imagesTable;
            if (categoryGuid == Guid.Empty) // If "All" category is selected
            {
                imagesTable = GetAllImagesFromDatabase();
            }
            else
            {
                imagesTable = GetImagesByCategoryFromDatabase(categoryGuid);
            }

            // Bind images to the ListView control
            ImageListView.DataSource = imagesTable;
            ImageListView.DataBind();
        }
        else
        {
            // Load all images
            DataTable allImagesTable = GetAllImagesFromDatabase();

            // Bind all images to the ListView control
            ImageListView.DataSource = allImagesTable;
            ImageListView.DataBind();
        }
    }

    private DataTable GetAllImagesFromDatabase()
    {
        DataTable imagesTable = new DataTable();

        // Connect to database and retrieve all images
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ddcimages where isactive = 1";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(imagesTable);
            }
        }
        catch (Exception ex)
        {
            // Handle the exception
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }

        return imagesTable;
    }


    private DataTable GetImagesByCategoryFromDatabase(Guid categoryId)
    {
        // Connect to database and retrieve images based on category ID
        DataTable imagesTable = new DataTable();
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ddcimages WHERE category = @category and isactive = 1";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@category", categoryId);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(imagesTable);
            }

        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
        return imagesTable;
    }

}