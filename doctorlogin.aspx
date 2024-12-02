<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="doctorlogin.aspx.cs" Inherits="doctorlogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="login_form bg-light p-5">
    <!-- Header End -->
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-5">
                <div class="card shadow-lg p-4">
                    <div class="card-body">
                        <div class="text-center mb-4">
                            <h1 class="text-center mb-3">Doctor Login</h1>
                            <p class="text-center text-muted mb-4">Access Health Portal</p>
                        </div>

                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger mb-4" />

                        <div class="mb-3">
                            <asp:Label ID="lblUserType" runat="server" CssClass="form-label" Text="User Type:"></asp:Label>
                            <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-select ">
                                <asp:ListItem Text="Select User" Value="" />
                                <asp:ListItem Text="Admin" Value="Admin" />
                                <asp:ListItem Text="Doctor" Value="Doctor" />
                            </asp:DropDownList>
                        </div>

                        <div class="mb-3">
                            <asp:TextBox ID="Username" runat="server" CssClass="form-control" Placeholder="Email or Phone" TextMode="SingleLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UsernameRequired" runat="server" ControlToValidate="Username" ErrorMessage="Username is required" CssClass="text-danger" Display="Dynamic" />
                        </div>
                        <div class="mb-3">
                            <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required" CssClass="text-danger" Display="Dynamic" />
                        </div>

                        <div class="d-grid">
                            <asp:Button ID="LoginButton" runat="server" Text="Login" CssClass="btn btn-primary btn-block py-2" OnClick="LoginButton_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</section>
</asp:Content>

