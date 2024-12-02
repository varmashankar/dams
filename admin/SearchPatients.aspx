<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="SearchPatients.aspx.cs" Inherits="admin_SearchPatients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div class="container bg-white mt-4 p-4 shadow-19">
            <h2 class="mb-4">Search Patients</h2>
            <div class="row mb-3">
                <div class="col-md-6">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Placeholder="Enter patient Details"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                </div>
            </div>

            <asp:GridView ID="gvPatients" runat="server" AutoGenerateColumns="False" GridLines="None" CssClass="table table-bordered table-striped">
                <Columns>
                    <asp:BoundField DataField="firstname" HeaderText="First Name" />
                    <asp:BoundField DataField="lastname" HeaderText="Last Name" />
                    <asp:BoundField DataField="email" HeaderText="Email" />
                    <asp:BoundField DataField="phone" HeaderText="Phone" />
                    <asp:BoundField DataField="dob" HeaderText="Date of Birth" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                    <asp:BoundField DataField="gender" HeaderText="Gender" />
                </Columns>
            </asp:GridView>
        </div>
</asp:Content>

