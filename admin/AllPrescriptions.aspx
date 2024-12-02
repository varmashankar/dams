<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="AllPrescriptions.aspx.cs" Inherits="admin_AllPrescriptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container bg-white p-4 shadow-19 mt-4">
        <h2 class="text-center mt-4">All Prescriptions</h2>
        <hr />
        <asp:GridView ID="gvAllPrescriptions" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Appointment ID">
                    <ItemTemplate>
                        <asp:Label ID="lblAppointmentID" runat="server" Text='<%# Eval("AppointmentID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Patient Name">
                    <ItemTemplate>
                        <asp:Label ID="lblPatientName" runat="server"
                            Text='<%# Eval("firstname") + " " + Eval("lastname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Medicine Name">
                    <ItemTemplate>
                        <asp:Label ID="lblMedicineName" runat="server" Text='<%# Eval("MedicineName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Dosage">
                    <ItemTemplate>
                        <asp:Label ID="lblDosage" runat="server" Text='<%# Eval("Dosage") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Frequency">
                    <ItemTemplate>
                        <asp:Label ID="lblFrequency" runat="server" Text='<%# Eval("Frequency") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Issued">
                    <ItemTemplate>
                        <asp:Label ID="lblCreatedAt" runat="server" Text='<%# Eval("CreatedAt", "{0:MM/dd/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
    <div class="container p-3 text-center">
        <asp:Label ID="lblNoData" runat="server" CssClass="alert alert-danger" Text="No Prescription available."></asp:Label>
    </div>
</EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>

