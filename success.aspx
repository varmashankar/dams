<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="success.aspx.cs" Inherits="success" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container mt-5">
        <div class="alert alert-success">
            <h1 class="display-4">Appointment Scheduled Successfully!</h1>
            <p class="fs-5">Your appointment has been successfully booked.</p>
            <p class="fs-5"><strong>Appointment ID:</strong> <asp:Label ID="appointmentID" runat="server" Text="Label"></asp:Label></p>
            <p class="fs-5">You can check the status of your appointment by logging in using the following details:</p>
            <ul>
                <li><p>If its your first time with us, please follow below details for login:</p></li>
                <li><strong>Username:</strong> Your Email OR Your Phone</li>
                <li><strong>Password:</strong> Your Phone Number</li>
            </ul>
            <p class="fs-5">If you have any questions, please contact our support team.</p>
            <a href="login.aspx" class="btn btn-primary">Go to Login</a>
        </div>
    </div>
</asp:Content>

