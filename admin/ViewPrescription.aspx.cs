using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class admin_ViewPrescription : System.Web.UI.Page
{
    string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string doctorID = Session["DoctorID"]?.ToString();
            // Get the PrescriptionID from the query string
            string prescriptionID = Request.QueryString["PrescriptionID"];
            if (!string.IsNullOrEmpty(prescriptionID))
            {
                LoadPrescriptionDetails(prescriptionID);
            }
            if (!string.IsNullOrEmpty(doctorID))
            {
                string name = GetDoctorName(doctorID); // Fetch doctor's name
                doctorName.Text = name; // Set the label text
            }
        }
    }

    private string GetDoctorName(string doctorID)
    {
        string doctorName = string.Empty;

        using (SqlConnection conn = new SqlConnection(strcon))
        {
            string query = "SELECT FullName FROM Doctors WHERE DoctorID = @DoctorID"; // Adjust the column name as per your DB schema

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@DoctorID", doctorID);

            conn.Open();
            object result = cmd.ExecuteScalar(); // Execute the query and get a single value
            if (result != null)
            {
                doctorName = result.ToString(); // Convert result to string
            }
            conn.Close();
        }

        return doctorName; // Return the doctor's name
    }


    private void LoadPrescriptionDetails(string prescriptionID)
    {
        using (SqlConnection conn = new SqlConnection(strcon))
        {
            // Fetch prescription and patient details in a single query
            string query = @"
            SELECT 
                p.firstname, p.lastname, p.email, p.phone, p.dob, p.address,
                pr.MedicineName, pr.MedicineType, pr.Dosage, 
                pr.Frequency, pr.Duration, pr.AllergyInformation, pr.DoctorNotes
            FROM Prescriptions pr
            JOIN patient_master p ON pr.PatientID = p.patient_id
            WHERE pr.PrescriptionID = @PrescriptionID";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PrescriptionID", new Guid(prescriptionID));

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                // Display patient and prescription details in a table format
                prescriptionDetails.InnerHtml = @"
                <div class='table-responsive'>
                    <table class='table table-bordered'>
                        <thead class='thead-light'>
                            <tr>
                                <th colspan='2' class='text-center'>Patient Details</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Full Name</td>
                                <td>" + reader["firstname"] + " " + reader["lastname"] + @"</td>
                            </tr>
                            <tr>
                                <td>Email</td>
                                <td>" + reader["email"] + @"</td>
                            </tr>
                            <tr>
                                <td>Phone</td>
                                <td>" + reader["phone"] + @"</td>
                            </tr>
                            <tr>
                                <td>Date of Birth</td>
                                <td>" + Convert.ToDateTime(reader["dob"]).ToString("dd/MM/yyyy") + @"</td>
                            </tr>
                            <tr>
                                <td>Address</td>
                                <td>" + reader["address"] + @"</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                
                <div class='table-responsive'>
                    <table class='table table-bordered'>
                        <thead class='thead-light'>
                            <tr>
                                <th colspan='2' class='text-center'>Prescription Details</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Medicine Name</td>
                                <td>" + reader["MedicineName"] + @"</td>
                            </tr>
                            <tr>
                                <td>Medicine Type</td>
                                <td>" + reader["MedicineType"] + @"</td>
                            </tr>
                            <tr>
                                <td>Dosage</td>
                                <td>" + reader["Dosage"] + @"</td>
                            </tr>
                            <tr>
                                <td>Frequency</td>
                                <td>" + reader["Frequency"] + @"</td>
                            </tr>
                            <tr>
                                <td>Duration</td>
                                <td>" + reader["Duration"] + @"</td>
                            </tr>
                            <tr>
                                <td>Allergy Information</td>
                                <td>" + reader["AllergyInformation"] + @"</td>
                            </tr>
                            <tr>
                                <td>Doctor's Notes</td>
                                <td>" + reader["DoctorNotes"] + @"</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            ";
            }

            conn.Close();
        }
    }


    protected void DownloadPrescription(object sender, EventArgs e)
    {
        // Get the PrescriptionID from the query string
        string prescriptionID = Request.QueryString["PrescriptionID"];
        if (!string.IsNullOrEmpty(prescriptionID))
        {
            // Fetch prescription and patient details in a single query
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = @"
            SELECT 
                p.firstname, p.lastname, p.email, p.phone, p.dob, p.address,
                pr.MedicineName, pr.MedicineType, pr.Dosage, 
                pr.Frequency, pr.Duration, pr.AllergyInformation, pr.DoctorNotes
            FROM Prescriptions pr
            JOIN patient_master p ON pr.PatientID = p.patient_id
            WHERE pr.PrescriptionID = @PrescriptionID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PrescriptionID", new Guid(prescriptionID));

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Create a PDF document
                    Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 60f, 60f);
                    MemoryStream stream = new MemoryStream();
                    PdfWriter.GetInstance(pdfDoc, stream).CloseStream = false;

                    pdfDoc.Open();

                    // Create a table for hospital and patient details
                    PdfPTable detailsTable = new PdfPTable(2);
                    detailsTable.WidthPercentage = 100;
                    detailsTable.SetWidths(new float[] { 1f, 1f }); // Set equal column widths

                    // Hospital Details
                    PdfPCell hospitalCell = new PdfPCell();
                    hospitalCell.AddElement(new Paragraph("Hospital Name: ABC Hospital", FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)));
                    hospitalCell.AddElement(new Paragraph("Address: 123 Main St, City, State, Zip", FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)));
                    hospitalCell.AddElement(new Paragraph("Phone: (123) 456-7890", FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)));
                    hospitalCell.AddElement(new Paragraph("Email: contact@abchospital.com", FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)));
                    hospitalCell.VerticalAlignment = Element.ALIGN_TOP;

                    // Patient Details
                    PdfPCell patientCell = new PdfPCell();
                    patientCell.AddElement(new Paragraph("Patient Name: " + reader["firstname"] + " " + reader["lastname"], FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)));
                    patientCell.AddElement(new Paragraph("Email: " + reader["email"], FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)));
                    patientCell.AddElement(new Paragraph("Phone: " + reader["phone"], FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)));
                    patientCell.AddElement(new Paragraph("DOB: " + Convert.ToDateTime(reader["dob"]).ToString("d"), FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)));
                    patientCell.AddElement(new Paragraph("Address: " + reader["address"], FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)));
                    patientCell.VerticalAlignment = Element.ALIGN_TOP;

                    // Add cells to the details table
                    detailsTable.AddCell(hospitalCell);
                    detailsTable.AddCell(patientCell);

                    // Add the details table to the PDF document
                    pdfDoc.Add(detailsTable);

                    // Add a space after the details table
                    pdfDoc.Add(new Paragraph("\n"));

                    // Add Title
                    var titleFont = FontFactory.GetFont("Arial", 20, Font.BOLD, BaseColor.BLACK);
                    Paragraph title = new Paragraph("Prescription Details", titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    pdfDoc.Add(title);

                    // Create a table for prescription details
                    PdfPTable prescriptionTable = new PdfPTable(2);
                    prescriptionTable.WidthPercentage = 100;
                    prescriptionTable.SetWidths(new float[] { 1f, 3f }); // Set column widths

                    // Adding Table Header
                    AddTableHeader(prescriptionTable);

                    // Adding Prescription Data to the Table
                    AddTableRow(prescriptionTable, "Medicine Name", reader["MedicineName"].ToString());
                    AddTableRow(prescriptionTable, "Medicine Type", reader["MedicineType"].ToString());
                    AddTableRow(prescriptionTable, "Dosage", reader["Dosage"].ToString());
                    AddTableRow(prescriptionTable, "Frequency", reader["Frequency"].ToString());
                    AddTableRow(prescriptionTable, "Duration", reader["Duration"].ToString());
                    AddTableRow(prescriptionTable, "Allergy Information", reader["AllergyInformation"].ToString());
                    AddTableRow(prescriptionTable, "Doctor's Notes", reader["DoctorNotes"].ToString());
                    // Note: Removed "Timing" if it's not in the selected query

                    // Add table to PDF document
                    pdfDoc.Add(prescriptionTable);

                    // Doctor's signature placeholder
                    Paragraph signature = new Paragraph("Signature: ___________________", FontFactory.GetFont("Arial", 12, Font.ITALIC, BaseColor.BLACK))
                    {
                        Alignment = Element.ALIGN_LEFT,
                        SpacingBefore = 20
                    };
                    pdfDoc.Add(signature);

                    pdfDoc.Close();

                    // Return the PDF document to the user
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Prescription_" + prescriptionID + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(stream.ToArray());
                    Response.End();
                }
                conn.Close();
            }
        }
    }


    // Helper method to add rows to the PDF table
    private void AddTableRow(PdfPTable table, string label, string value)
    {
        var headerFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
        var bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);

        PdfPCell labelCell = new PdfPCell(new Phrase(label, headerFont))
        {
            BackgroundColor = BaseColor.LIGHT_GRAY,
            HorizontalAlignment = Element.ALIGN_LEFT,
            Padding = 5
        };
        table.AddCell(labelCell);

        PdfPCell valueCell = new PdfPCell(new Phrase(value, bodyFont))
        {
            HorizontalAlignment = Element.ALIGN_LEFT,
            Padding = 5
        };
        table.AddCell(valueCell);
    }

    // Helper method to add header cells to the PDF table
    private void AddTableHeader(PdfPTable table)
    {
        PdfPCell headerCell = new PdfPCell(new Phrase("Prescription Detail", FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.WHITE)))
        {
            BackgroundColor = BaseColor.DARK_GRAY,
            HorizontalAlignment = Element.ALIGN_CENTER
        };
        table.AddCell(headerCell);

        headerCell = new PdfPCell(new Phrase("Detail", FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.WHITE)))
        {
            BackgroundColor = BaseColor.DARK_GRAY,
            HorizontalAlignment = Element.ALIGN_CENTER
        };
        table.AddCell(headerCell);
    }


}
