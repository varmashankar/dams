<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="AllPatients.aspx.cs" Inherits="admin_AllPatients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container bg-white my-4 p-4">
        <h2 class="mb-4">Patients List</h2>
        <hr />
        <div class="row mb-3">
            <div class="col-md-6">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Placeholder="Enter patient Details"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                <asp:Button ID="btnLoadAll" runat="server" Text="Load All" CssClass="btn btn-primary" OnClick="btnLoadAll_Click" />
            </div>
        </div>
        <asp:GridView ID="gvPatients" runat="server" AutoGenerateColumns="False" GridLines="None" OnRowCommand="gvPatients_RowCommand" CssClass="table table-bordered table-striped text-capitalize" DataKeyNames="patient_id">
            <Columns>
                <asp:BoundField DataField="firstname" HeaderText="First Name" />
                <asp:BoundField DataField="lastname" HeaderText="Last Name" />
                <asp:BoundField DataField="email" HeaderText="Email" />
                <asp:BoundField DataField="phone" HeaderText="Phone" />
                <asp:BoundField DataField="dob" HeaderText="Date of Birth" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                <asp:BoundField DataField="gender" HeaderText="Gender" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkAddRecord" runat="server" CommandName="AddMedicalRecord" Text="Add Medical Record" CssClass="btn btn-sm btn-success" CommandArgument='<%# Eval("patient_id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

