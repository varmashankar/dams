<%@ Page Title="" Language="C#" MasterPageFile="~/patient/patient.master" AutoEventWireup="true" CodeFile="MedicalRecords.aspx.cs" Inherits="patient_MedicalRecords" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="container bg-white shadow-19 my-4 p-4">
    <h2>Medical Records</h2>
    <hr />
    <asp:GridView ID="gvMedicalRecords" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-striped table-bordered" OnRowCommand="gvMedicalRecords_RowCommand">
        <Columns>
            <asp:BoundField DataField="PatientID" HeaderText="Patient ID" Visible="false" />
            <asp:BoundField DataField="AppointmentID" HeaderText="Appointment ID" />
            <asp:BoundField DataField="Details" HeaderText="Details" />
            <asp:BoundField DataField="DateOfRecord" HeaderText="Date Of Record" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                <asp:LinkButton ID="lnkViewRecord" runat="server" CommandName="ViewRecord" CommandArgument='<%# Eval("RecordID") %>' Text="View Record" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
    <div class="container p-3 text-center">
        <asp:Label ID="lblNoRecords" runat="server" CssClass="alert alert-danger" Text="No Medical Records available."></asp:Label>
    </div>
</EmptyDataTemplate>
    </asp:GridView>
</div>
</asp:Content>

