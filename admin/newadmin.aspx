<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="newadmin.aspx.cs" Inherits="admin_newadmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container mt-5">
        <div class="card">
            <div class="card-header text-center py-3">
                <h2 class="mb-4 text-primary display-5">Admin Form</h2>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Username" />
                        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername"
                            ErrorMessage="Username is required." CssClass="text-danger" Display="Dynamic" />
                    </div>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email" />
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Email is required." CssClass="text-danger" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Invalid email format." CssClass="text-danger" Display="Dynamic"
                            ValidationExpression="^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$" />

                    </div>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control mt-3" Placeholder="Full Name" />
                    </div>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control mt-3" TextMode="Password" Placeholder="Password" />
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                            ErrorMessage="Password is required." CssClass="text-danger" Display="Dynamic" />
                    </div>
                </div>
                <div class="mt-4 float-end">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-lg" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary btn-lg" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

