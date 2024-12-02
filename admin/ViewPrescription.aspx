<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="ViewPrescription.aspx.cs" Inherits="admin_ViewPrescription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        /* Prescription Container */
.prescription-container {
    background-color: #f9f9f9;
    padding: 40px;
    box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
    border-radius: 12px;
    margin-top: 40px;
}

/* Prescription Header */
.prescription-title {
    font-size: 28px;
    font-weight: bold;
    color: #4CAF50;
}

.prescription-divider {
    border-top: 1px solid #ddd;
}

/* Prescription Details Section */
.prescription-details {
    font-size: 16px;
    line-height: 1.7;
    color: #333;
}

/* Prescription Info Boxes */
.prescription-details .info-box {
    margin-bottom: 20px;
    padding: 20px;
    background-color: #fff;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    border-radius: 8px;
    display: flex;
    align-items: center;
}

.info-icon {
    font-size: 24px;
    margin-right: 15px;
    color: #4CAF50;
}

.info-box h5 {
    margin-bottom: 0;
    font-weight: 600;
    font-size: 18px;
}

.info-box small {
    font-size: 14px;
    color: #888;
}

/* Buttons */
.btn {
    padding: 12px 20px;
    font-size: 16px;
    border-radius: 6px;
}

.btn-primary {
    background-color: #007bff;
    border-color: #007bff;
}

.btn-secondary {
    background-color: #6c757d;
    border-color: #6c757d;
}

/* Grid Layout for Prescription Details */
.prescription-details .row {
    display: flex;
    flex-wrap: wrap;
}

.prescription-details .col-md-6 {
    flex: 0 0 50%;
    max-width: 50%;
    padding: 10px;
}

.prescription-details .col-md-12 {
    flex: 0 0 100%;
    max-width: 100%;
    padding: 10px;
}

/* Responsive Design */
@media (max-width: 768px) {
    .prescription-details .col-md-6 {
        flex: 0 0 100%;
        max-width: 100%;
    }
}

/* Print View */
@media print {
    .prescription-container {
        box-shadow: none;
        margin: 0;
    }

    .btn {
        display: none;
    }
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container bg-white p-5 shadow-lg rounded mt-4 prescription-container">
        <!-- Prescription Header -->
        <div class="text-center mb-4">
            <h2 class="prescription-title">Prescription Details</h2>
            <small class="text-muted">Issued by Dr.<asp:Label ID="doctorName" runat="server" Text="Label"></asp:Label></small>
        </div>
        <hr class="mb-4 prescription-divider" />

        <!-- Prescription Details Section -->
        <div id="prescriptionDetails" runat="server" class="prescription-details row">
            <!-- Dynamic Prescription Data will be injected here -->
        </div>

        <hr class="mb-4 prescription-divider" />

        <!-- Action Buttons -->
        <div class="d-flex justify-content-between mt-4">
            <asp:Button ID="btnDownload" runat="server" Text="Download PDF" CssClass="btn btn-primary btn-lg" OnClick="DownloadPrescription" />
            <asp:Button ID="btnPrint" runat="server" Text="Print Prescription" CssClass="btn btn-secondary btn-lg" OnClientClick="window.print(); return false;" />
        </div>
    </div>

</asp:Content>

