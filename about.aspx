<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="about.aspx.cs" Inherits="about" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Header Start -->
        <div class="container-fluid bg-breadcrumb">
            <div class="container text-center py-5" style="max-width: 900px;">
                <h4 class="text-white display-4 mb-4 wow fadeInDown" data-wow-delay="0.1s">About Us</h4>
                <ol class="breadcrumb d-flex justify-content-center mb-0 wow fadeInDown" data-wow-delay="0.3s">
                    <li class="breadcrumb-item"><a href="index.html">Home</a></li>
                    <li class="breadcrumb-item active text-primary">About</li>
                </ol>    
            </div>
        </div>
        <!-- Header End -->

     <!-- About Start -->
        <div class="container-fluid bg-light about py-5">
            <div class="container py-5">
                <div class="row g-5">
                    <div class="col-xl-6 wow fadeInLeft" data-wow-delay="0.2s">
                        <div class="about-item-content bg-white rounded p-5 h-100">
                            <h4 class="text-primary">About Our Hospital</h4>
                            <h1 class="display-4 mb-4">
                                "Horizon from a healthy today to a healthier tomorrow"
                            </h1>
                            <p>
                                DocTime Hospital, the best hospital in Vadodara, incepted in 2012, is a long-cherished dream of the DocTime Arogya Seva Mandal Trust situated at Waghodia, Vadodara.
                            </p>
                            <p>Established with a vision to achieve the goal of “Health for all,” our efforts are always directed towards providing healthcare excellence to our patients, focusing on quality and standards of healthcare.
                            </p>
                            <p>
                                NABH Accredited DocTime Hospital is a facility spread over 4.2 lakh square feet with a total of 750+ beds, designed spaciously with well-ventilated, modern architecture that gives a positive energy and treats thousands of patients each year.
                            </p>
                            <p>
                                We offer the most advanced procedures and treatments for everyday ailments, bringing relief to patients from all walks of life. We use state-of-the-art technology and cutting-edge surgical and medical techniques to deliver outstanding outcomes.
                            </p>
                        </div>
                    </div>
                    <div class="col-xl-6 wow fadeInRight" data-wow-delay="0.2s">
                        <div class="bg-white rounded p-5 h-100">
                            <div class="row g-4 justify-content-center">
                                <div class="col-12">
                                    <div class="rounded bg-light">
                                        <img src="assets/img/service-1.jpg" class="img-fluid rounded w-100" alt="">
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="counter-item bg-light rounded p-3 h-100">
                                        <div class="counter-counting">
                                            <span class="text-primary fs-2 fw-bold" data-toggle="counter-up">129</span>
                                            <span class="h1 fw-bold text-primary">+</span>
                                        </div>
                                        <h4 class="mb-0 text-dark">Specialized Wards</h4>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="counter-item bg-light rounded p-3 h-100">
                                        <div class="counter-counting">
                                            <span class="text-primary fs-2 fw-bold" data-toggle="counter-up">99</span>
                                            <span class="h1 fw-bold text-primary">+</span>
                                        </div>
                                        <h4 class="mb-0 text-dark">Experienced Doctors</h4>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="counter-item bg-light rounded p-3 h-100">
                                        <div class="counter-counting">
                                            <span class="text-primary fs-2 fw-bold" data-toggle="counter-up">556</span>
                                            <span class="h1 fw-bold text-primary">+</span>
                                        </div>
                                        <h4 class="mb-0 text-dark">Skilled Staffs</h4>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="counter-item bg-light rounded p-3 h-100">
                                        <div class="counter-counting">
                                            <span class="text-primary fs-2 fw-bold" data-toggle="counter-up">967</span>
                                            <span class="h1 fw-bold text-primary">+</span>
                                        </div>
                                        <h4 class="mb-0 text-dark">Team Members</h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- About End -->
        <!-- Hospital Feature Start -->
<div class="container-fluid hospital-feature bg-light pb-5">
    <div class="container pb-5">
        <div class="text-center mx-auto pb-5 wow fadeInUp" data-wow-delay="0.2s" style="max-width: 800px;">
            <h4 class="text-primary">Our Services</h4>
            <h1 class="display-4 mb-4">Comprehensive Care for a Healthier Tomorrow</h1>
            <p class="mb-0">At DocTime Hospital, we are committed to delivering top-notch medical services across various specialties. Our modern facilities, advanced technology, and expert teams ensure that you receive the best possible care for your health needs.</p>
        </div>
        <div class="row g-4">
            <div class="col-md-6 col-lg-6 col-xl-3 wow fadeInUp" data-wow-delay="0.2s">
                <div class="hospital-feature-item p-4 pt-0">
                    <div class="hospital-feature-icon p-4 mb-4">
                        <i class="far fa-handshake fa-3x"></i>
                    </div>
                    <h4 class="mb-4">Trusted Healthcare</h4>
                    <p class="mb-4">Our hospital is recognized for its compassionate care, with a reputation built on trust and excellence in the medical field.</p>
                    <a class="btn btn-primary rounded-pill py-2 px-4" href="#">Learn More</a>
                </div>
            </div>
            <div class="col-md-6 col-lg-6 col-xl-3 wow fadeInUp" data-wow-delay="0.4s">
                <div class="hospital-feature-item p-4 pt-0">
                    <div class="hospital-feature-icon p-4 mb-4">
                        <i class="fa fa-user-md fa-3x"></i>
                    </div>
                    <h4 class="mb-4">Expert Specialists</h4>
                    <p class="mb-4">Our team of experienced specialists covers a wide range of medical fields, comprehensive care for all your health needs.</p>
                    <a class="btn btn-primary rounded-pill py-2 px-4" href="#">Learn More</a>
                </div>
            </div>
            <div class="col-md-6 col-lg-6 col-xl-3 wow fadeInUp" data-wow-delay="0.6s">
                <div class="hospital-feature-item p-4 pt-0">
                    <div class="hospital-feature-icon p-4 mb-4">
                        <i class="fa fa-heartbeat fa-3x"></i>
                    </div>
                    <h4 class="mb-4">Advanced Treatments</h4>
                    <p class="mb-4">We offer the latest in medical technology and treatments to provide effective solutions for a wide range of health conditions.</p>
                    <a class="btn btn-primary rounded-pill py-2 px-4" href="#">Learn More</a>
                </div>
            </div>
            <div class="col-md-6 col-lg-6 col-xl-3 wow fadeInUp" data-wow-delay="0.8s">
                <div class="hospital-feature-item p-4 pt-0">
                    <div class="hospital-feature-icon p-4 mb-4">
                        <i class="fa fa-clock fa-3x"></i>
                    </div>
                    <h4 class="mb-4">24/7 Emergency Care</h4>
                    <p class="mb-4">Our emergency department is always ready to provide immediate and critical care around the clock to the patient.</p>
                    <a class="btn btn-primary rounded-pill py-2 px-4" href="#">Learn More</a>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- Hospital Feature End -->
    <!-- Team Start -->
<div class="container-fluid team py-5">
    <div class="container py-5">
        <div class="section-title mb-5 wow fadeInUp" data-wow-delay="0.1s">
            <div class="sub-style">
                <h4 class="sub-title px-3 mb-0">Meet Our Team</h4>
            </div>
            <h1 class="display-3 mb-4">Experienced Professionals Dedicated to Your Recovery</h1>
            <p class="mb-0">Our team of skilled therapists and medical professionals are committed to providing top-notch care tailored to your needs. Learn more about our experts below.</p>
        </div>
        <div class="row g-4 justify-content-center">
            <div class="col-md-6 col-lg-6 col-xl-3 wow fadeInUp" data-wow-delay="0.1s">
                <div class="team-item rounded">
                    <div class="team-img rounded-top h-100">
                        <img src="assets/img/team-1.jpg" class="img-fluid rounded-top w-100" alt="Dr. Sarah Johnson">
                        <div class="team-icon d-flex justify-content-center">
                            <a class="btn btn-square btn-primary text-white rounded-circle mx-1" href="https://www.facebook.com/drsarahjohnson" target="_blank"><i class="fab fa-facebook-f"></i></a>
                            <a class="btn btn-square btn-primary text-white rounded-circle mx-1" href="https://twitter.com/drsarahjohnson" target="_blank"><i class="fab fa-twitter"></i></a>
                            <a class="btn btn-square btn-primary text-white rounded-circle mx-1" href="https://www.instagram.com/drsarahjohnson" target="_blank"><i class="fab fa-instagram"></i></a>
                            <a class="btn btn-square btn-primary text-white rounded-circle mx-1" href="https://www.linkedin.com/in/drsarahjohnson" target="_blank"><i class="fab fa-linkedin-in"></i></a>
                        </div>
                    </div>
                    <div class="team-content text-center border border-primary border-top-0 rounded-bottom p-4">
                        <h5>Dr. Sarah Johnson</h5>
                        <p class="mb-0">Senior Physiotherapist</p>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-lg-6 col-xl-3 wow fadeInUp" data-wow-delay="0.3s">
                <div class="team-item rounded">
                    <div class="team-img rounded-top h-100">
                        <img src="assets/img/team-2.jpg" class="img-fluid rounded-top w-100" alt="John Doe">
                        <div class="team-icon d-flex justify-content-center">
                            <a class="btn btn-square btn-primary text-white rounded-circle mx-1" href="https://www.facebook.com/johndoe" target="_blank"><i class="fab fa-facebook-f"></i></a>
                            <a class="btn btn-square btn-primary text-white rounded-circle mx-1" href="https://twitter.com/johndoe" target="_blank"><i class="fab fa-twitter"></i></a>
                            <a class="btn btn-square btn-primary text-white rounded-circle mx-1" href="https://www.instagram.com/johndoe" target="_blank"><i class="fab fa-instagram"></i></a>
                            <a class="btn btn-square btn-primary text-white rounded-circle mx-1" href="https://www.linkedin.com/in/johndoe" target="_blank"><i class="fab fa-linkedin-in"></i></a>
                        </div>
                    </div>
                    <div class="team-content text-center border border-primary border-top-0 rounded-bottom p-4">
                        <h5>John Doe</h5>
                        <p class="mb-0">Rehabilitation Specialist</p>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-lg-6 col-xl-3 wow fadeInUp" data-wow-delay="0.5s">
                <div class="team-item rounded">
                    <div class="team-img rounded-top h-100">
                        <img src="assets/img/team-3.jpg" class="img-fluid rounded-top w-100" alt="Emily Davis">
                        <div class="team-icon d-flex justify-content-center">
                            <a class="btn btn-square btn-primary text-white rounded-circle mx-1" href="https://www.facebook.com/emilydavis" target="_blank"><i class="fab fa-facebook-f"></i></a>
                            <a class="btn btn-square btn-primary text-white rounded-circle mx-1" href="https://twitter.com/emilydavis" target="_blank"><i class="fab fa-twitter"></i></a>
                            <a class="btn btn-square btn-primary text-white rounded-circle mx-1" href="https://www.instagram.com/emilydavis" target="_blank"><i class="fab fa-instagram"></i></a>
                            <a class="btn btn-square btn-primary text-white rounded-circle mx-1" href="https://www.linkedin.com/in/emilydavis" target="_blank"><i class="fab fa-linkedin-in"></i></a>
                        </div>
                    </div>
                    <div class="team-content text-center border border-primary border-top-0 rounded-bottom p-4">
                        <h5>Emily Davis</h5>
                        <p class="mb-0">Doctor of Physical Therapy</p>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-lg-6 col-xl-3 wow fadeInUp" data-wow-delay="0.7s">
                <div class="team-item rounded">
                    <div class="team-img rounded-top h-100">
                        <img src="assets/img/team-4.jpg" class="img-fluid rounded-top w-100" alt="Michael Brown">
                        <div class="team-icon d-flex justify-content-center">
                            <a class="btn btn-square btn-primary text-white rounded-circle mx-1" href="https://www.facebook.com/michaelbrown" target="_blank"><i class="fab fa-facebook-f"></i></a>
                            <a class="btn btn-square btn-primary text-white rounded-circle mx-1" href="https://twitter.com/michaelbrown" target="_blank"><i class="fab fa-twitter"></i></a>
                            <a class="btn btn-square btn-primary text-white rounded-circle mx-1" href="https://www.instagram.com/michaelbrown" target="_blank"><i class="fab fa-instagram"></i></a>
                            <a class="btn btn-square btn-primary text-white rounded-circle mx-1" href="https://www.linkedin.com/in/michaelbrown" target="_blank"><i class="fab fa-linkedin-in"></i></a>
                        </div>
                    </div>
                    <div class="team-content text-center border border-primary border-top-0 rounded-bottom p-4">
                        <h5>Michael Brown</h5>
                        <p class="mb-0">Doctor of Physical Therapy</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- Team End -->
</asp:Content>

