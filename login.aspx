<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Header Start 
    <div class="container-fluid bg-breadcrumb">
        <div class="container text-center py-5" style="max-width: 900px;">
            <h4 class="text-white display-4 mb-4 wow fadeInDown" data-wow-delay="0.1s">Patient LogIn</h4>
            <ol class="breadcrumb d-flex justify-content-center mb-0 wow fadeInDown" data-wow-delay="0.3s">
                <li class="breadcrumb-item"><a href="index.html">Home</a></li>
                <li class="breadcrumb-item active text-primary">Login</li>
            </ol>
        </div>
    </div>-->
    <section class="login_form bg-light p-5">
        <!-- Header End -->
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-5">
                    <div class="card shadow-lg p-4">
                        <div class="card-body">
                            <div class="text-center mb-4">
                                <h1 class="text-center mb-3">Patient Login</h1>
                                <img src="assets/img/testimonial-img.jpg" class="img-fluid rounded-circle" width="120" alt="profile">
                                <p class="text-center text-muted my-4">Access Your Health Portal</p>
                            </div>

                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger mb-4" />

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

                            <div class="text-center mt-3">
                                <p class="text-muted mb-0">Not registered yet? <a href="registration.aspx" class="text-primary fw-bold">Create an Account</a></p>
                                <p class="text-muted mb-0"><a href="forgotpassword.aspx" class="text-primary fw-bold">Forgot Password?</a></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>

