<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="contact.aspx.cs" Inherits="contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Header Start -->
    <div class="container-fluid bg-breadcrumb">
        <div class="container text-center py-5" style="max-width: 900px;">
            <h4 class="text-white display-4 mb-4 wow fadeInDown" data-wow-delay="0.1s">Contact Us</h4>
            <ol class="breadcrumb d-flex justify-content-center mb-0 wow fadeInDown" data-wow-delay="0.3s">
                <li class="breadcrumb-item"><a href="index.html">Home</a></li>
                <li class="breadcrumb-item active text-primary">Contact Us</li>
            </ol>    
        </div>
    </div>
    <!-- Header End -->
   <!-- Contact Start -->
<div class="container-fluid contact bg-light py-5">
    <div class="container py-5">
        <div class="text-center mx-auto pb-5 wow fadeInUp" data-wow-delay="0.2s" style="max-width: 800px;">
            <h4 class="text-primary">Contact Us</h4>
            <h1 class="display-4 mb-4">We're here to help you with your health needs</h1>
        </div>
        <div class="row g-5">
            <div class="col-xl-6 wow fadeInLeft" data-wow-delay="0.2s">
                <div class="contact-img d-flex justify-content-center">
                    <div class="contact-img-inner">
                        <img src="assets/img/hospital-contact-img.jpg" class="img-fluid w-100" alt="Hospital Image">
                    </div>
                </div>
            </div>
            <div class="col-xl-6 wow fadeInRight" data-wow-delay="0.4s">
                <div>
                    <h4 class="text-primary">Send Your Message</h4>
                    <p class="mb-4">Our team is here to assist you. Feel free to reach out with any questions, concerns, or to schedule an appointment.</p>
                    <div class="col-12">
                        <asp:Label ID="lblConfirmation" runat="server" CssClass="text-success" Visible="false"></asp:Label>
                    </div>
                    <div>
                        <div class="row g-3">
                            <div class="col-lg-12 col-xl-6">
                                <div class="form-floating">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control border-0" placeholder="Your Name"></asp:TextBox>
                                    <label for="txtName">Your Name</label>
                                </div>
                            </div>
                            <div class="col-lg-12 col-xl-6">
                                <div class="form-floating">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control border-0" placeholder="Your Email" TextMode="Email"></asp:TextBox>
                                    <label for="txtEmail">Your Email</label>
                                </div>
                            </div>
                            <div class="col-lg-12 col-xl-6">
                                <div class="form-floating">
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control border-0" placeholder="Phone" TextMode="Phone"></asp:TextBox>
                                    <label for="txtPhone">Your Phone</label>
                                </div>
                            </div>
                            <div class="col-lg-12 col-xl-6">
                                <div class="form-floating">
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control border-0">
                                        <asp:ListItem Text="Select Department" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Dermatologist" Value="Dermatologist"></asp:ListItem>
                                        <asp:ListItem Text="Physiotherapy" Value="Physiotherapy"></asp:ListItem>
                                        <asp:ListItem Text="Dentist" Value="Dentist"></asp:ListItem>
                                    </asp:DropDownList>
                                    <label for="ddlDepartment">Preferred Department</label>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-floating">
                                    <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control border-0" placeholder="Subject"></asp:TextBox>
                                    <label for="txtSubject">Subject</label>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-floating">
                                    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" CssClass="form-control border-0" placeholder="Leave a message here" Rows="6"></asp:TextBox>
                                    <label for="txtMessage">Message</label>
                                </div>
                            </div>
                            <div class="col-12">
                                <asp:Button ID="btnSendMessage" runat="server" CssClass="btn btn-primary w-100 py-3" Text="Send Message" OnClick="btnSendMessage_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12 bg-white py-3 rounded">
                <div class="row g-4">
                    <div class="col-md-6 col-lg-3 wow fadeInUp" data-wow-delay="0.2s">
                        <div class="contact-add-item">
                            <div class="contact-icon text-primary mb-4">
                                <i class="fas fa-map-marker-alt fa-2x"></i>
                            </div>
                            <div>
                                <h4>Address</h4>
                                <p class="mb-0">123 Road, Vadodara, Gujarat.</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-3 wow fadeInUp" data-wow-delay="0.4s">
                        <div class="contact-add-item">
                            <div class="contact-icon text-primary mb-4">
                                <i class="fas fa-envelope fa-2x"></i>
                            </div>
                            <div>
                                <h4>Email Us</h4>
                                <p class="mb-0">contact@doctimehospital.com</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-3 wow fadeInUp" data-wow-delay="0.6s">
                        <div class="contact-add-item">
                            <div class="contact-icon text-primary mb-4">
                                <i class="fa fa-phone-alt fa-2x"></i>
                            </div>
                            <div>
                                <h4>Call Us</h4>
                                <p class="mb-0">(+123) 456-7890</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-3 wow fadeInUp" data-wow-delay="0.8s">
                        <div class="contact-add-item">
                            <div class="contact-icon text-primary mb-4">
                                <i class="fab fa-firefox-browser fa-2x"></i>
                            </div>
                            <div>
                                <h4>Visit Our Website</h4>
                                <p class="mb-0">www.doctimehospital.com</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12">
                <div class="rounded">
                    <iframe class="rounded w-100" 
                    style="height: 400px;" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3692.039202647443!2d73.19763407512974!3d22.27650477970377!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x395fc56216ad31a7%3A0xaf14a406cd60497d!2sSunshine%20Global%20Hospital!5e0!3m2!1sen!2sin!4v1725447763561!5m2!1sen!2sin" 
                    loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
                </div>
            </div>
        </div>
        <div class="p-5 bg-white rounded">
            <h4 class="text-primary">Appointments & Service Availability</h4>
            <p>Our specialists are available from 9:00 AM to 8:00 PM, Monday to Saturday. Appointments can be booked through our website, by phone, or in person at our reception. For emergency services, we are open 24/7.</p>
            <p>For more details on how to book an appointment or to inquire about specific services, please visit our <a class="text-primary fw-bold" href="bookAppointment.aspx">Appointments Page</a> or contact us directly.</p>
        </div>
    </div>
</div>
<!-- Contact End -->

</asp:Content>

