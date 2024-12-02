<%@ Page Title="" Language="C#" MasterPageFile="~/patient/patient.master" AutoEventWireup="true" CodeFile="upcomingappt.aspx.cs" Inherits="patient_upcomingappt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container bg-white my-4 p-4">
        <h1 class="mb-4">Scheduled Appointments</h1>
        <!-- GridView to show appointments -->
        <asp:GridView ID="gvAppointments" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" OnRowDataBound="gvAppointments_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Appointment ID">
                    <ItemTemplate>
                        <asp:Label ID="lblAppointmentID" runat="server" Text='<%# Eval("appointmentID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblAppointmentDate" runat="server" Text='<%# Eval("AppointmentDate", "{0:MMMM dd, yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Time">
                    <ItemTemplate>
                        <asp:Label ID="lblAppointmentTime" runat="server" Text='<%# Eval("AppointmentTime") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnViewDetails" runat="server" CommandArgument='<%# Eval("appointmentID") %>' CssClass="btn btn-primary" OnClick="btnViewDetails_Click">View Details</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <div class="container p-3 text-center">
                    <asp:Label ID="lblNoData" runat="server" CssClass="alert alert-danger" Text="No appointments data available."></asp:Label>
                </div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>

