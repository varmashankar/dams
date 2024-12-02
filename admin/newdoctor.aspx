<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="newdoctor.aspx.cs" Inherits="admin_newdoctor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section>
        <div class="container bg-white card my-5 p-4 shadow-19">
            <h2 class="text-primary display-5">Add New Doctor</h2>
            <p><asp:Label ID="lblMessage" runat="server" CssClass="mt-3" /></p><hr />
            <div id="form1" runat="server">
                <div class="row mt-3">
                    <div class="col-md-6 form-group">
                        <label for="txtFullName">Full Name:</label>
                        <asp:TextBox ID="txtFullName" runat="server" placeholder="Doctor's Full Name" CssClass="form-control form-control-lg" />
                        <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName"
                            ErrorMessage="Full Name is required." CssClass="text-danger" />
                    </div>
                    <div class="col-md-6 form-group">
                        <label for="ddlSpecialty">Specialty:</label>
                        <asp:DropDownList ID="ddlSpecialty" runat="server" CssClass="form-select form-select-lg border-primary bg-transparent">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSpecialty" runat="server" ControlToValidate="ddlSpecialty"
                            InitialValue="" ErrorMessage="Specialty is required." CssClass="text-danger" />
                    </div>
                    <div class="form-group col-md-6">
                        <label for="txtPhone">Phone:</label>
                        <asp:TextBox ID="txtPhone" runat="server" placeholder="Doctor's Phone Number" CssClass="form-control form-control-lg" />
                        <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone"
                            ErrorMessage="Phone number is required." CssClass="text-danger" />
                        <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone"
                            ErrorMessage="Invalid phone number format." CssClass="text-danger"
                            ValidationExpression="^\+?\d{10,15}$" />
                    </div>
                    <div class="form-group col-md-6">
                        <label for="txtEmail">Email:</label>
                        <asp:TextBox ID="txtEmail" runat="server" placeholder="Doctor's Email" CssClass="form-control form-control-lg" />
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Email is required." CssClass="text-danger" />
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Invalid email format." CssClass="text-danger"
                            ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" />
                    </div>
                    <div class="form-group col-md-6">
                        <label for="txtPassword">Password:</label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control form-control-lg" />
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                            ErrorMessage="Password is required." CssClass="text-danger" />
                        <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword"
                            ErrorMessage="Password must be at least 6 characters long." CssClass="text-danger"
                            ValidationExpression="^.{6,}$" />
                    </div>
                    <div class="form-group col-md-6">
                        <label for="txtConfirmPassword">Confirm Password:</label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm Password" CssClass="form-control form-control-lg" />
                        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                            ErrorMessage="Confirm Password is required." CssClass="text-danger" />
                        <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPassword"
                            ControlToValidate="txtConfirmPassword" ErrorMessage="Passwords do not match." CssClass="text-danger" />
                    </div>
                    <div class="col-md-12 text-right">
                        <asp:Button ID="btnAddDoctor" runat="server" Text="Add Doctor" CssClass="btn btn-lg btn-primary" OnClick="btnAddDoctor_Click" />
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

