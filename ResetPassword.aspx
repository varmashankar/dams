<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <section class="bg-light py-5">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card shadow-lg hover-shadow">
                    <div class="card-header bg-dark s">
                        <h3 class="card-title mb-0 text-white">Reset Password</h3>
                    </div>
                    <div class="card-body">
                        <div class="form-group mb-4">
                            <label for="txtNewPassword" class="form-label">New Password:</label>
                            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="New Password"></asp:TextBox>
                        </div>
                        <div class="form-group mb-4">
                            <label for="txtConfirmPassword" class="form-label">Confirm Password:</label>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirm Password"></asp:TextBox>
                        </div>
                        <div class="d-grid">
                            <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" CssClass="btn btn-primary btn-block" OnClick="btnResetPassword_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
</asp:Content>

