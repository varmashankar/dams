<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="EditAppointment.aspx.cs" Inherits="admin_EditAppointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container bg-white my-4 p-4">
            <h2>Edit Appointment</h2>
        <hr />
            <div class="form-group">
                <asp:Label ID="lblMessage" runat="server" CssClass="text-bold fs-3"></asp:Label>
            </div>
            <div class="form-group">
                <label for="txtAppointmentDate">Appointment Date:</label>
                <asp:TextBox ID="txtAppointmentDate" runat="server" CssClass="form-control" TextMode="Date" />
            </div>
            <div class="form-group">
            <label for="txtAppointmentTime">Appointment Time:</label>
            <!-- Appointment Time --->
            <div>
                <asp:DropDownList ID="ddlTimeSlot" runat="server" CssClass="form-select border-primary bg-transparent">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvTimeSlot" runat="server" ControlToValidate="ddlTimeSlot" InitialValue="" ErrorMessage="Time slot selection is required." CssClass="text-danger" Display="Dynamic" />
                <asp:CustomValidator ID="cvTimeSlot" runat="server" ControlToValidate="ddlTimeSlot"
                    ErrorMessage="Please select a valid time slot."
                    CssClass="text-danger"
                    Display="Dynamic"
                    OnServerValidate="cvTimeSlot_ServerValidate"></asp:CustomValidator>
            </div>
        </div>
            <div class="form-group">
            <label for="txtDepartmentName">Department:</label>
            <asp:DropDownList ID="ddlSpecialty" runat="server" CssClass="form-select form-select-lg border-primary bg-transparent">
            </asp:DropDownList>
        </div>
            <asp:HiddenField ID="hfAppointmentID" runat="server" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClientClick="history.back(); return false;" />
        </div>
</asp:Content>

