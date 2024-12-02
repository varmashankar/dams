<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="registration.aspx.cs" Inherits="registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="registration bg-light py-5">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-8">
                <div class="card shadow-lg p-4">
                    <div class="card-body">
                        <!-- Header Section -->
                        <div class="text-center mb-4">
                            <h1 class="text-primary mb-4">Patient Registration</h1>
                            <asp:Image ID="profileImage" runat="server" ImageUrl="~/assets/img/testimonial-img.jpg" CssClass="img-fluid rounded-circle mb-3" Width="120" AlternateText="profile" />
                            <p class="text-muted">Create Your Health Portal Account</p>
                        </div>

                        <!-- Validation Summary -->
                        <asp:ValidationSummary ID="vsSummary" runat="server" CssClass="alert alert-danger mb-4" HeaderText="Please correct the following errors:" />

                        <!-- Form Fields -->
                        <div class="row g-3">
                            <!-- First Name -->
                            <div class="col-md-6">
                                <asp:Label ID="lblFirstName" runat="server" CssClass="form-label" AssociatedControlID="txtFirstName" Text="First Name"></asp:Label>
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" Placeholder="Enter your first name" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" CssClass="text-danger" ErrorMessage="First name is required." Display="Dynamic" />
                            </div>

                            <!-- Last Name -->
                            <div class="col-md-6">
                                <asp:Label ID="lblLastName" runat="server" CssClass="form-label" AssociatedControlID="txtLastName" Text="Last Name"></asp:Label>
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" Placeholder="Enter your last name" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" CssClass="text-danger" ErrorMessage="Last name is required." Display="Dynamic" />
                            </div>

                            <!-- Email Address -->
                            <div class="col-md-6">
                                <asp:Label ID="lblEmail" runat="server" CssClass="form-label" AssociatedControlID="txtEmail" Text="Email Address"></asp:Label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Enter your email address" TextMode="Email"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" CssClass="text-danger" ErrorMessage="Email address is required." Display="Dynamic" />
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" CssClass="text-danger" ErrorMessage="Invalid email format." Display="Dynamic" ValidationExpression="\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b" />
                            </div>

                            <!-- Phone Number -->
                            <div class="col-md-6">
                                <asp:Label ID="lblPhone" runat="server" CssClass="form-label" AssociatedControlID="txtPhone" Text="Phone Number"></asp:Label>
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Placeholder="Enter your phone number" TextMode="Phone"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" CssClass="text-danger" ErrorMessage="Invalid phone number format." Display="Dynamic" ValidationExpression="^\+?\d{10,15}$" />
                            </div>

                            <!-- Date of Birth -->
                            <div class="col-md-6">
                                <asp:Label ID="lblDob" runat="server" CssClass="form-label" AssociatedControlID="txtDob" Text="Date of Birth"></asp:Label>
                                <asp:TextBox ID="txtDob" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDob" runat="server" ControlToValidate="txtDob" CssClass="text-danger" ErrorMessage="Date of Birth is required." Display="Dynamic" />
                            </div>

                            <!-- Address -->
                            <div class="col-md-12">
                                <asp:Label ID="lblAddress" runat="server" CssClass="form-label" AssociatedControlID="txtAddress" Text="Address"></asp:Label>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" Placeholder="Enter your address" MaxLength="150"></asp:TextBox>
                            </div>

                            <!-- Password -->
                            <div class="col-md-6">
                                <asp:Label ID="lblPassword" runat="server" CssClass="form-label" AssociatedControlID="txtPassword" Text="Password"></asp:Label>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Placeholder="Enter your password" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" CssClass="text-danger" ErrorMessage="Password is required." Display="Dynamic" />
                                <div id="passwordStrength" class="form-text"></div>
                            </div>

                            <!-- Confirm Password -->
                            <div class="col-md-6">
                                <asp:Label ID="lblConfirmPassword" runat="server" CssClass="form-label" AssociatedControlID="txtConfirmPassword" Text="Confirm Password"></asp:Label>
                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" Placeholder="Re-enter your password" TextMode="Password"></asp:TextBox>
                                <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" CssClass="text-danger" ErrorMessage="Passwords do not match." Display="Dynamic" />
                            </div>
                            <!-- Gender -->
                            <div class="col-md-6">
                                <asp:Label ID="lblGender" runat="server" CssClass="form-label" AssociatedControlID="ddlGender" Text="Gender"></asp:Label>
                                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="" Text="Select Gender" Enabled="false" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="male" Text="Male"></asp:ListItem>
                                    <asp:ListItem Value="female" Text="Female"></asp:ListItem>
                                    <asp:ListItem Value="other" Text="Other"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <!-- Emergency Contact -->
                            <div class="col-md-6">
                                <asp:Label ID="lblEmergencyContact" runat="server" CssClass="form-label" AssociatedControlID="txtEmergencyContact" Text="Emergency Contact (Optional)"></asp:Label>
                                <asp:TextBox ID="txtEmergencyContact" runat="server" CssClass="form-control" Placeholder="Enter emergency contact number" TextMode="Phone"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revEmergencyContact" runat="server" ControlToValidate="txtEmergencyContact" CssClass="text-danger" ErrorMessage="Invalid phone number format." Display="Dynamic" ValidationExpression="^\+?\d{10,15}$" />
                            </div>
                        </div>

                        <!-- Terms and Privacy -->
                        <!-- Terms and Privacy -->
                        <div class="form-check my-4 custom-checkbox">
                            <asp:CheckBox ID="cbTerms" runat="server" CssClass="form-check-input shadow-none border-0" />
                            <asp:Label ID="lblTerms" runat="server" CssClass="form-check-label mt-3" AssociatedControlID="cbTerms" Text="I agree to the terms and conditions"></asp:Label>
                            <asp:CustomValidator ID="cvTerms" runat="server" CssClass="text-danger" ErrorMessage="You must agree to the terms." OnServerValidate="cvTerms_ServerValidate" Display="Dynamic"></asp:CustomValidator>
                        </div>

                        <!-- Privacy and Security Note -->
                        <div class="privacy-note mb-4">
                            <p class="text-muted">
                                By registering, you agree to our <a href="/privacy-policy" class="link-primary">Privacy Policy</a> and <a href="/terms-of-service" class="link-primary">Terms of Service</a>. We are committed to protecting your personal information and ensuring your data is secure.
   
                            </p>
                        </div>

                        <!-- Submit Button -->
                        <div class="text-center">
                            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="btnRegister_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>

</asp:Content>

