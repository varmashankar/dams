<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="AddMedicalRecord.aspx.cs" Inherits="admin_AddMedicalRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div class="container my-5 p-4 bg-white shadow">
    <h2 class="text-center mb-4">Add Medical Record</h2>

    <div class="mb-3">
        <label for="ddlDoctor" class="form-label">Select Doctor <span class="text-danger">*</span></label>
        <asp:DropDownList ID="ddlDoctor" runat="server" CssClass="form-control" DataTextField="FullName" DataValueField="DoctorID" required>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvDoctor" runat="server" ControlToValidate="ddlDoctor" ErrorMessage="Doctor selection is required." CssClass="text-danger" />
    </div>

    <div class="mb-3">
        <label for="txtAppointmentID" class="form-label">Appointment ID <span class="text-danger">*</span></label>
        <asp:TextBox ID="txtAppointmentID" runat="server" CssClass="form-control" placeholder="Enter Appointment ID" required />
        <asp:RequiredFieldValidator ID="rfvAppointmentID" runat="server" ControlToValidate="txtAppointmentID" ErrorMessage="Appointment ID is required." CssClass="text-danger" />
    </div>

    <div class="mb-3">
        <label for="txtDetails" class="form-label">Details <span class="text-danger">*</span></label>
        <asp:TextBox ID="txtDetails" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" placeholder="Enter details about the medical record" required />
        <asp:RequiredFieldValidator ID="rfvDetails" runat="server" ControlToValidate="txtDetails" ErrorMessage="Details are required." CssClass="text-danger" />
    </div>

    <div class="mb-3">
        <label for="txtDateOfRecord" class="form-label">Date of Record <span class="text-danger">*</span></label>
        <asp:TextBox ID="txtDateOfRecord" runat="server" CssClass="form-control" TextMode="Date" required />
        <asp:RequiredFieldValidator ID="rfvDateOfRecord" runat="server" ControlToValidate="txtDateOfRecord" ErrorMessage="Date of record is required." CssClass="text-danger" />
    </div>

    <div class="mb-3">
        <label for="fileUpload" class="form-label">Upload File</label>
        <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
    </div>

    <div class="text-center">
        <asp:Button ID="btnAddRecord" runat="server" Text="Add Record" CssClass="btn btn-primary" OnClick="btnAddRecord_Click" />
    </div>
    
    <div class="text-center mt-3">
        <asp:Label ID="lblSuccess" runat="server" CssClass="text-success"></asp:Label>
        <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
    </div>
</div>

</asp:Content>

