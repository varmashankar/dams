<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="verifyOTP.aspx.cs" Inherits="verifyOTP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="bg-light py-5">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card shadow-lg hover-shadow">
                    <div class="card-header bg-dark">
                        <h3 class="card-title mb-0 text-white">Verify OTP</h3>
                    </div>
                    <div class="card-body">
                        <div class="form-group mb-4">
                            <p class="card-text py-2 text-success fw-bolder"><asp:Label ID="textOTP" runat="server" Text="Label"></asp:Label></p>
                            <label for="txtOTP" class="form-label">Enter the OTP sent to your email:</label>
                            <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control" placeholder="OTP"></asp:TextBox>
                        </div>
                        <div class="d-grid">
                            <asp:Button ID="btnVerifyOTP" runat="server" Text="Verify" CssClass="btn btn-primary btn-block" OnClick="btnVerifyOTP_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>

</asp:Content>

