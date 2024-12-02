<%@ Page Title="" Language="C#" MasterPageFile="~/patient/patient.master" AutoEventWireup="true" CodeFile="PatientPrescriptions.aspx.cs" Inherits="patient_PatientPrescriptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="container mt-5 bg-white p-4">
            <h2>Patient Prescriptions</h2> <hr />
            <asp:GridView ID="gvPrescriptions" AutoGenerateColumns="false" runat="server" CssClass="table table-striped">
                <Columns>
                    <asp:BoundField DataField="PrescriptionID" HeaderText="Prescription ID" Visible="false" />
                    <asp:BoundField DataField="AppointmentID" HeaderText="Appointment ID" />
                    <asp:BoundField DataField="MedicineName" HeaderText="Medicine Name" />
                    <asp:BoundField DataField="MedicineType" HeaderText="Medicine Type" />
                    <asp:BoundField DataField="Dosage" HeaderText="Dosage" />
                    <asp:BoundField DataField="Frequency" HeaderText="Frequency" />
                    <asp:BoundField DataField="CreatedAt" HeaderText="Created At" DataFormatString="{0:yyyy-MM-dd}" />
                </Columns>
            </asp:GridView>
        </div>
</asp:Content>

