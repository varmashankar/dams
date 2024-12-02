<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="ViewAppointment.aspx.cs" Inherits="admin_ViewAppointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        body{
            background:#eeeeee;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="m-auto">
        <div class="container p-4 bg-white shadow-19">
            <h2>Appointment Details</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

            <div class="form-group">
                <label for="txtPatientID">Appointment ID:</label>
                <asp:TextBox ID="txtAppointmentID" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
            <div class="form-group">
                <label for="txtAppointmentDate">Appointment Date:</label>
                <asp:TextBox ID="txtAppointmentDate" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
            <div class="form-group">
                <label for="txtAppointmentTime">Appointment Time:</label>
                <asp:TextBox ID="txtAppointmentTime" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
            <div class="form-group">
                <label for="txtDoctorName">Department:</label>
                <asp:TextBox ID="txtDepartmentName" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
            <div class="form-group">
                <label for="ddlStatus">Status:</label>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" Enabled="false">
                    <asp:ListItem Value="Pending">Pending</asp:ListItem>
                    <asp:ListItem Value="Confirmed">Confirmed</asp:ListItem>
                    <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                </asp:DropDownList>
            </div>

            <asp:HiddenField ID="hfAppointmentID" runat="server" />
            <asp:Button ID="btnConfirmCancel" runat="server" Text="Confirm Appointment" CssClass="btn btn-success float-end" OnClick="btnConfirmCancel_Click" />
        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-secondary" OnClientClick="history.back(); return false;"/>
        </div>
    </section>
</asp:Content>

