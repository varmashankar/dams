<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="bookAppointment.aspx.cs" Inherits="bookAppointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Header Start -->
    <div class="container-fluid bg-breadcrumb">
        <div class="container text-center py-5" style="max-width: 900px;">
            <h4 class="text-white display-4 mb-4 wow fadeInDown" data-wow-delay="0.1s">Book Appointment</h4>
            <ol class="breadcrumb d-flex justify-content-center mb-0 wow fadeInDown" data-wow-delay="0.3s">
                <li class="breadcrumb-item"><a href="index.html">Home</a></li>
                <li class="breadcrumb-item active text-primary">Book Appointment</li>
            </ol>    
        </div>
    </div>
    <!-- Header End -->

    <!-- Book Appointment Start -->
    <div class="container-fluid appointment py-5">
        <div class="container py-5">
            <div class="row g-5 align-items-center">
                <div class="col-lg-6 wow fadeInLeft" data-wow-delay="0.2s">
                    <div class="section-title text-start">
                        <h4 class="sub-title pe-3 mb-0">Comprehensive Health Services</h4>
                        <h1 class="display-4 mb-4">Expert Care Across Specialties</h1>
                        <p class="mb-4">Receive top-tier medical services from specialized departments, all under one roof.</p>
                        <div class="row g-4">
                            <div class="col-sm-6">
                                <div class="d-flex flex-column h-100">
                                    <div class="mb-4">
                                        <h5 class="mb-3"><i class="fa fa-check text-primary me-2"></i>Multi-Specialty Expertise</h5>
                                        <p class="mb-0">Dedicated specialists offering personalized care in various medical fields.</p>
                                    </div>
                                    <div class="mb-4">
                                        <h5 class="mb-3"><i class="fa fa-check text-primary me-2"></i>Advanced Medical Technology</h5>
                                        <p class="mb-0">Cutting-edge tools and treatments for effective diagnosis and therapy.</p>
                                    </div>
                                    <div class="text-start mb-4">
                                        <asp:HyperLink ID="hlMoreDetails" runat="server" NavigateUrl="about.aspx" CssClass="btn btn-primary rounded-pill text-white py-3 px-5">
                                        More Details
                                    </asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="video h-100">
                                    <img src="assets/img/appointment.jpg" class="img-fluid rounded w-100 h-100" style="object-fit: cover;" alt="Hospital Image">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6 wow fadeInRight" data-wow-delay="0.4s">
                    <div class="appointment-form rounded p-5">
                        <p class="fs-4 text-uppercase text-primary">Book Your Appointment</p>
                        <h1 class="display-5 mb-4">Schedule Your Visit</h1>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger mb-4" />

                        <div>
                            <div class="row gy-3 gx-4">
                                <div class="col-xl-6">
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control py-3 border-primary bg-transparent" Placeholder="First Name*"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First Name is required." CssClass="text-danger" Display="Dynamic" />
                                    <asp:RegularExpressionValidator ID="revFirstName" runat="server" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z\s]+$" ErrorMessage="Please enter a valid name." CssClass="text-danger" Display="Dynamic"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-xl-6">
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control py-3 border-primary bg-transparent" Placeholder="Last Name*"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last Name is required." CssClass="text-danger" Display="Dynamic" />
                                    <asp:RegularExpressionValidator ID="revLastName" runat="server" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z\s]+$" ErrorMessage="Please enter a valid name." CssClass="text-danger" Display="Dynamic"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-xl-6">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control py-3 border-primary bg-transparent" Placeholder="Email*"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." CssClass="text-danger" Display="Dynamic" />
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid email format." ValidationExpression="\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="text-danger" Display="Dynamic" />
                                </div>
                                <div class="col-xl-6">
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control py-3 border-primary bg-transparent" Placeholder="Phone*"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone number is required." CssClass="text-danger" Display="Dynamic" />
                                    <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Invalid phone number." ValidationExpression="^\d{10}$" CssClass="text-danger" Display="Dynamic" />
                                </div>
                                <div class="col-xl-12">
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-select py-3 border-primary bg-transparent">
                                        <asp:ListItem Text="Dermatologist" Value="Dermatologist" />
                                        <asp:ListItem Text="Physiotherapy" Value="Physiotherapy" />
                                        <asp:ListItem Text="Dentist" Value="Dentist" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment" InitialValue="" ErrorMessage="Department selection is required." CssClass="text-danger" Display="Dynamic" />
                                </div>
                                <div class="col-xl-6">
                                    <asp:TextBox ID="txtDate" runat="server" TextMode="Date" CssClass="form-control py-3 border-primary bg-transparent"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtDate" ErrorMessage="Date selection is required." CssClass="text-danger" Display="Dynamic" />
                                    <asp:CustomValidator ID="cvDate" runat="server" ControlToValidate="txtDate"
                                        ErrorMessage="Please select a date in the future."
                                        CssClass="text-danger"
                                        Display="Dynamic"
                                        OnServerValidate="cvDate_ServerValidate"></asp:CustomValidator>
                                </div>
                                
                                <div class="col-xl-6">
                                    <asp:DropDownList ID="ddlTimeSlot" runat="server" CssClass="form-select py-3 border-primary bg-transparent">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvTimeSlot" runat="server" ControlToValidate="ddlTimeSlot" InitialValue="" ErrorMessage="Time slot selection is required." CssClass="text-danger" Display="Dynamic" />
                                    <asp:CustomValidator ID="cvTimeSlot" runat="server" ControlToValidate="ddlTimeSlot"
                                        ErrorMessage="Please select a valid time slot."
                                        CssClass="text-danger"
                                        Display="Dynamic"
                                        OnServerValidate="cvTimeSlot_ServerValidate"></asp:CustomValidator>

                                </div>
                                <div class="col-xl-6">
                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-select py-3 border-primary bg-transparent">
                                        <asp:ListItem Text="Select Gender" Value="" />
                                        <asp:ListItem Text="Male" Value="Male" />
                                        <asp:ListItem Text="Female" Value="Female" />
                                        <asp:ListItem Text="Other" Value="Other" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvGender" runat="server" ControlToValidate="ddlGender" InitialValue="" ErrorMessage="Gender selection is required." CssClass="text-danger" Display="Dynamic" />
                                </div>
                                <div class="col-xl-6">
                                    <asp:TextBox ID="txtEPhone" runat="server" CssClass="form-control py-3 border-primary bg-transparent" Placeholder="Emergency Contact (Optional)"></asp:TextBox>
                                </div>
                                <div class="col-12">
                                    <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT NOW" CssClass="btn btn-primary text-white w-100 py-3 px-5" OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
<!-- Book Appointment End -->

</asp:Content>

