<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="DeleteAppointment.aspx.cs" Inherits="admin_DeleteAppointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        body {
            background-color: #f4f7fa;
        }
        .container {
            margin-top: 30px;
            background-color: white;
            border-radius: 8px;
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
            padding: 30px;
        }
        h2 {
            font-weight: 600;
            margin-bottom: 20px;
        }
        .btn-danger {
            background-color: #dc3545; /* Bootstrap danger color */
            color: white;
            border: none;
            transition: background-color 0.3s;
        }
        .btn-danger:hover {
            background-color: #c82333; /* Darker shade on hover */
        }
        .btn-secondary {
            margin-left: 10px; /* Spacing between buttons */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
            <h2>Delete Appointment</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
            <asp:HiddenField ID="hfAppointmentID" runat="server" />

            <div>
                <p>Are you sure you want to delete this appointment?</p>
                <asp:Button ID="btnConfirmDelete" runat="server" Text="Yes, Delete" CssClass="btn btn-danger" OnClick="btnConfirmDelete_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
            </div>
        </div>
</asp:Content>

