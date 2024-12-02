<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="addmedicine.aspx.cs" Inherits="admin_addmedicine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container bg-white p-5 shadow-lg rounded mt-4">
            <h2 class="text-center mb-4">Add Medicine</h2>
        <!-- Confirmation Message -->
<asp:Label ID="lblConfirmation" runat="server" CssClass="alert alert-success mt-4" Visible="false"></asp:Label>
            <hr class="mb-4" />
            <div id="addMedicineForm" runat="server" class="px-5">

                <!-- Medicine Name Input -->
                <div class="form-group mb-4">
                    <label for="medicineName" class="form-label"><i class="fas fa-pills me-2"></i>Medicine Name</label>
                    <asp:TextBox ID="txtMedicineName" runat="server" CssClass="form-control" placeholder="Enter Medicine Name" required />
                </div>

                <!-- Medicine Type Selection -->
                <div class="form-group mb-4">
                    <label for="medicineType" class="form-label"><i class="fas fa-clipboard-list me-2"></i>Medicine Type</label>
                    <asp:DropDownList ID="ddlMedicineType" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select Type" Value="" />
                        <asp:ListItem Text="Tablet" Value="Tablet" />
                        <asp:ListItem Text="Syrup" Value="Syrup" />
                        <asp:ListItem Text="Capsule" Value="Capsule" />
                        <asp:ListItem Text="Injection" Value="Injection" />
                        <asp:ListItem Text="Ointment" Value="Ointment" />
                    </asp:DropDownList>
                </div>


                <!-- Action Buttons -->
                <div class="d-flex justify-content-between">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary px-4" OnClientClick="history.back(); return false;" />
                    <asp:Button ID="btnSubmit" runat="server" Text="Add Medicine" CssClass="btn btn-primary px-4" OnClick="SubmitMedicine" />
                </div>

            </div>
        </div>
</asp:Content>

