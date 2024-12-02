<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="gallery.aspx.cs" Inherits="gallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
     // Function to display modal with the image
     function showImageModal(imageUrl) {
         var modal = document.getElementById("imageModal");
         var modalImg = document.getElementById("modalImage");
         modal.style.display = "block";
         modalImg.src = imageUrl;
     }

     // Function to close modal
     function closeImageModal() {
         var modal = document.getElementById("imageModal");
         modal.style.display = "none";
     }
    </script>
    <style>
        .category-list {
    display: flex;
    justify-content: center;
    flex-wrap: wrap;
}

.category-link {
    font-size: 1.2rem;
    margin: 0.5rem;
}

.image-gallery {
    display: flex;
    justify-content: center;
    flex-wrap: wrap;
}

.gallery-image {
    max-width: 100%;
    height: auto;
    cursor: pointer;
    transition: transform 0.2s;
}

.gallery-image:hover {
    transform: scale(1.05);
}

.enlarged-image-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.8);
    display: none;
    justify-content: center;
    align-items: center;
    z-index: 9999;
}

.enlarged-image {
    max-width: 90%;
    max-height: 90%;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.3);
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Header Start -->
<div class="container-fluid bg-breadcrumb">
    <div class="container text-center py-5" style="max-width: 900px;">
        <h4 class="text-white display-4 mb-4 wow fadeInDown" data-wow-delay="0.1s">Gallery</h4>
        <ol class="breadcrumb d-flex justify-content-center mb-0 wow fadeInDown" data-wow-delay="0.3s">
            <li class="breadcrumb-item"><a href="index.html">Home</a></li>
            <li class="breadcrumb-item active text-primary">Gallery</li>
        </ol>    
    </div>
</div>
<!-- Header End -->

<!-- Main Gallery Section Start -->
<div class="container-fluid py-5 wow fadeInUp" data-wow-delay="0.1s">
    <div class="container">
        <!-- Category List -->
        <div class="category-list mb-5 text-center">
            <asp:Repeater ID="CategoryRepeater" runat="server">
                <ItemTemplate>
                    <!-- Center the category links -->
                    <a href='<%# "gallery.aspx?CategoryID=" + Eval("CategoryID") %>' class="category-link btn btn-outline-primary mx-2 my-2">
                        <%# Eval("CategoryName") %>
                    </a>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <!-- Image Gallery -->
        <div class="row justify-content-center">
            <asp:ListView ID="ImageListView" runat="server">
                <ItemTemplate>
                    <!-- Wrap each image inside a column to make it responsive -->
                    <div class="col-md-3 col-sm-6 col-xs-12 mb-4 d-flex justify-content-center">
                        <asp:Image ID="Image1" runat="server" 
                                   ImageUrl='<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("ImageData")) %>' 
                                   CssClass="gallery-image img-fluid" 
                                   onclick="enlargeImage(this)" />
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</div>

<!-- Enlarged Image Modal -->
<div id="myModal" class="enlarged-image-overlay" onclick="closeModal()">
    <img class="enlarged-image" id="img01" onclick="event.stopPropagation()">
</div>
<!-- Main Gallery Section End -->



</asp:Content>

