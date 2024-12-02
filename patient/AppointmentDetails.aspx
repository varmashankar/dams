<%@ Page Title="" Language="C#" MasterPageFile="~/patient/patient.master" AutoEventWireup="true" CodeFile="AppointmentDetails.aspx.cs" Inherits="patient_AppointmentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div class="container mt-5">        
        <!-- Details Section -->
        <div class="card appointment-details-card bg-white shadow-19 hover-shadow">
            <div class="card-header bg-secondary">
                <h2 class="mb-4 text-center text-white my-3">Appointment Details</h2>
            </div>
            <div class="card-body text-left">
                <div class="row mb-4">
                    <div class="col-md-6">
                        <h5 class="card-title mb-2">Appointment ID: <span class="fs-5 text-primary"><asp:Label ID="lblAppointmentID" runat="server" /></span></h5>
                    </div>
                    <div class="col-md-6">
                        <h5 class="card-title mb-2">Date: <span class="fs-5 border-right px-3"><asp:Label ID="lblAppointmentDate" runat="server" /></span> <span class="fs-5 px-3">Time: <asp:Label ID="lblAppointmentTime" runat="server" /></span></h5>
                    </div>
                </div>
                
                <div class="row mb-4">
                    <div class="col-md-6">
                        <h5 class="card-title mb-2">Department ID: <span class="fs-5"><asp:Label ID="lblDepartmentID" runat="server" /></span></h5>
                    </div>
                    <div class="col-md-6">
                        <h5 class="card-title mb-2">Status: <span class="fs-5"><asp:Label ID="lblStatus" runat="server" /></span></h5>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <h5 class="card-title mb-2">Date Created: <span class="fs-5"><asp:Label ID="lblDateCreated" runat="server" /></span></h5>
                    </div>
                </div>

                <!-- Action Buttons -->
                <div class="mt-4 d-flex flex-column flex-md-row justify-content-end">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update Appointment" CssClass="btn btn-warning me-3 mb-2 mb-md-0" OnClick="btnUpdate_Click" OnClientClick="showLoadingOverlay();" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel Appointment" CssClass="btn btn-danger mb-2 mb-md-0" OnClick="btnCancel_Click" OnClientClick="showLoadingOverlay();" />
                </div>
            </div>
        </div>

        <!-- Back to List -->
        <div class="mt-4 text-center">
            <a href="upcomingappt.aspx" class="btn btn-secondary">Back to Appointments</a>
        </div>
    </div>

</asp:Content>

