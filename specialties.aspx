<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="specialties.aspx.cs" Inherits="specialties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Header Start -->
    <div class="container-fluid bg-breadcrumb">
        <div class="container text-center py-5" style="max-width: 900px;">
            <h4 class="text-white display-4 mb-4 wow fadeInDown" data-wow-delay="0.1s">Our Specialties</h4>
            <ol class="breadcrumb d-flex justify-content-center mb-0 wow fadeInDown" data-wow-delay="0.3s">
                <li class="breadcrumb-item"><a href="index.html">Home</a></li>
                <li class="breadcrumb-item active text-primary">Our Specialties</li>
            </ol>
        </div>
    </div>
    <!-- Header End -->

    <!-- Navbar Start -->
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <div class="container">
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ms-auto">
                    <li class="nav-item"><a class="nav-link" href="#Dermatologist">Dermatologist</a></li>
                    <li class="nav-item"><a class="nav-link" href="#Dentist">Dentist</a></li>
                    <li class="nav-item"><a class="nav-link" href="#physiotherapy">Physiotherapy</a></li>
                </ul>
            </div>
        </div>
    </nav>
    <!-- Navbar End -->


    

    <section id="Dermatologist" class="container mb-5 border-bottom border-dark py-5">
    <!-- Hero Section Start -->
    <div class="container-fluid text-center py-3 mb-3">
        <div class="container py-5">
            <h1 class="display-4">Dermatology Department</h1>
            <p class="lead">Specialized Skin Care and Treatment</p>
        </div>
    </div>
    <!-- Hero Section End -->
    <div class="row align-items-center">
        <div class="col-md-6">
            <img src="assets/img/service-5.jpg" alt="Dermatology Department" class="img-fluid rounded">
        </div>
        <div class="col-md-6">
            <h2 class="mb-4">Overview</h2>
            <p>At the Dermatology Department of DocTime Hospital, we offer a wide range of diagnostic and treatment services for skin, hair, and nail conditions. Our experienced dermatologists provide personalized care to address conditions such as acne, eczema, psoriasis, and skin cancer.</p>
            <p>We utilize advanced diagnostic tools and treatments to ensure effective care and promote healthy skin. From cosmetic procedures to managing chronic skin conditions, we are committed to delivering quality care for all your dermatological needs.</p>
            <p>Our mission is to improve skin health and enhance the well-being of our patients through comprehensive and compassionate care.</p>
        </div>
    </div>
</section>

    <section id="Dentist" class="container mb-5 border-bottom border-dark py-5">
    <!-- Hero Section Start -->
    <div class="container-fluid text-center py-3 mb-3">
        <div class="container py-5">
            <h1 class="display-4">Dentistry Department</h1>
            <p class="lead">Comprehensive Dental Care for a Healthy Smile</p>
        </div>
    </div>
    <!-- Hero Section End -->
    <div class="row align-items-center">
        <div class="col-md-6">
            <img src="assets/img/service-7.jpg" alt="Dentistry Department" class="img-fluid rounded">
        </div>
        <div class="col-md-6">
            <h2 class="mb-4">Overview</h2>
            <p>The Dentistry Department at DocTime Hospital provides comprehensive dental care for patients of all ages. Our team of experienced dentists offers a range of services, from routine cleanings and fillings to advanced restorative treatments such as crowns, implants, and cosmetic procedures.</p>
            <p>We focus on preventive care and patient education to ensure long-term oral health. Whether you're seeking treatment for a specific dental issue or looking for routine dental care, our goal is to provide high-quality services in a comfortable and caring environment.</p>
            <p>We are dedicated to helping you achieve and maintain a healthy, beautiful smile through personalized care and modern dental techniques.</p>
        </div>
    </div>
</section>
<section id="Physiotherapy" class="container mb-5 py-5">
    <!-- Hero Section Start -->
    <div class="container-fluid text-center py-3 mb-3">
        <div class="container py-5">
            <h1 class="display-4">Physiotherapy Department</h1>
            <p class="lead">Restoring Mobility and Improving Quality of Life</p>
        </div>
    </div>
    <!-- Hero Section End -->
    <div class="row align-items-center">
        <div class="col-md-6">
            <img src="assets/img/service-6.jpg" alt="Physiotherapy Department" class="img-fluid rounded">
        </div>
        <div class="col-md-6">
            <h2 class="mb-4">Overview</h2>
            <p>The Physiotherapy Department at DocTime Hospital specializes in the rehabilitation and improvement of physical function. Our highly trained physiotherapists provide personalized care for a wide range of conditions including musculoskeletal injuries, neurological disorders, and post-surgical recovery.</p>
            <p>We offer evidence-based therapies such as manual therapy, therapeutic exercise, and electrotherapy to help patients regain strength, mobility, and independence. Our goal is to enhance patient outcomes by promoting healing and reducing pain through effective treatment plans.</p>
            <p>Our patient-centered approach ensures that every treatment is tailored to the specific needs of each individual, helping them achieve their rehabilitation goals and improve their quality of life.</p>
        </div>
    </div>
</section>



</asp:Content>

