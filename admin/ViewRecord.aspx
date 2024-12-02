<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="ViewRecord.aspx.cs" Inherits="admin_ViewRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div class="container bg-white p-4 my-5">
    <h2 class="text-center text-primary mb-4">View Medical Record</h2>
    <hr class="mb-4" />
    
    <!-- Patient Details Section -->
    <div class="card mb-4 p-4">
        <h4 class="card-title text-center">Patient Details</h4>
        <div class="card-body">
            <div class="table-responsive">
                <table class="table table-bordered">
                    <tbody>
                        <tr>
                            <td class="font-weight-bold">Full Name:</td>
                            <td><asp:Label ID="lblFullNameValue" runat="server" CssClass="ml-2"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="font-weight-bold">Email:</td>
                            <td><asp:Label ID="lblEmailValue" runat="server" CssClass="ml-2"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="font-weight-bold">Phone:</td>
                            <td><asp:Label ID="lblPhoneValue" runat="server" CssClass="ml-2"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="font-weight-bold">Date of Birth:</td>
                            <td><asp:Label ID="lblDobValue" runat="server" CssClass="ml-2"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="font-weight-bold">Gender:</td>
                            <td><asp:Label ID="lblGenderValue" runat="server" CssClass="ml-2"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <!-- Medical Records Section -->
    <div class="card shadow-sm p-4">
        <h4 class="card-title text-center">Medical Records</h4>
        <div class="card-body">
            <asp:GridView ID="gvMedicalRecords" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered table-striped">
                <Columns>
                    <asp:BoundField DataField="AppointmentID" HeaderText="Appointment ID" />
                    <asp:BoundField DataField="Details" HeaderText="Details" />
                    <asp:BoundField DataField="DateOfRecord" HeaderText="Date Of Record" />
                    <asp:TemplateField HeaderText="File">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlFileLink" runat="server"
                                NavigateUrl='<%# ResolveUrl("~/"+Eval("FilePath").ToString()) %>'
                                Text="View File"
                                Target="_blank" 
                                CssClass="btn btn-link text-primary" />
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
    </div>

    <!-- Alerts for Success/Error Messages -->
    <div class="alert alert-success mt-4" id="alertSuccess" runat="server" visible="false">
        <strong>Success!</strong> Your medical record has been saved successfully.
    </div>
    <div class="alert alert-danger mt-4" id="alertError" runat="server" visible="false">
        <strong>Error!</strong> There was a problem saving the medical record.
    </div>
</div>

</asp:Content>

