<%@ Page Title="" Language="C#" MasterPageFile="~/patient/patient.master" AutoEventWireup="true" CodeFile="UpdateAppointment.aspx.cs" Inherits="patient_UpdateAppointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="container mt-5">        
        <div class="update-appointment-form card">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger mb-4" />

            <div class="card-header mb-3">
                <h1 class="text-center text-primary fw-bolder">Update Appointment</h1>
            </div>

            <asp:Panel ID="pnlUpdateForm" runat="server" CssClass="text-dark fw-bold p-4">
                <div class="mb-3">
                    <asp:Label ID="lblAppointmentID" runat="server" CssClass="form-label" Text="Appointment ID:" />
                    <asp:TextBox ID="txtAppointmentID" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblAppointmentDate" runat="server" CssClass="form-label" Text="Appointment Date:" />
                    <asp:TextBox ID="txtAppointmentDate" runat="server" CssClass="form-control" TextMode="Date" />
                    <asp:RequiredFieldValidator ID="rfvAppointmentDate" runat="server" ControlToValidate="txtAppointmentDate" ErrorMessage="Appointment Date is required." CssClass="text-danger" Display="Dynamic" />
                </div>

                <div class="mb-3">
                    <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Appointment Time:" />
                    <asp:DropDownList ID="ddlTimeSlot" runat="server" CssClass="form-select border-primary bg-transparent">
                    </asp:DropDownList>
<asp:RequiredFieldValidator ID="rfvTimeSlot" runat="server" ControlToValidate="ddlTimeSlot" InitialValue="" ErrorMessage="Time slot selection is required." CssClass="text-danger" Display="Dynamic" />
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblDepartmentID" runat="server" CssClass="form-label" Text="Department:" />
                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Select Department" Value="" />
                        <asp:ListItem Text="Medicine" Value="Medicine" />
                        <asp:ListItem Text="Critical Care" Value="Critical Care" />
                        <asp:ListItem Text="Surgery" Value="Surgery" />
                        <asp:ListItem Text="Orthopaedics" Value="Orthopaedics" />
                        <asp:ListItem Text="Skin Specialist" Value="Skin Specialist" />
                        <asp:ListItem Text="Radiology" Value="Radiology" />
                        <asp:ListItem Text="Pathology & Microbiology" Value="Pathology & Microbiology" />
                        <asp:ListItem Text="Anaesthesiology" Value="Anaesthesiology" />
                        <asp:ListItem Text="Physiotherapy" Value="Physiotherapy" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment" InitialValue="" ErrorMessage="Department selection is required." CssClass="text-danger" Display="Dynamic" />
                </div>

                <div class="text-center">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update Appointment" CssClass="btn btn-primary" CausesValidation="true" OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary ms-2" CausesValidation="false" OnClick="btnCancel_Click" />
                </div>
            </asp:Panel>
        </div>

        <!-- Back to List -->
        <div class="mt-4 text-center">
            <a href="upcomingappt.aspx" class="btn btn-secondary">Back to Appointments</a>
        </div>
    </div>

</asp:Content>

