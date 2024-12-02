using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_feedback : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMessagesGrid();
        }
    }

    private void BindMessagesGrid()
    {
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = "SELECT Name, Email, Phone, Department, Subject, Message, DateSent FROM ContactMessages ORDER BY DateSent DESC";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                gvMessages.DataSource = dt;
                gvMessages.DataBind();
            }
        }
    }
}
