<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="gallery.aspx.cs" Inherits="admin_gallery" %>

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

        function validateUploadForm() {
            var fileUpload = document.getElementById('<%= FileUpload1.ClientID %>');
            var categoryDropDown = document.getElementById('<%= CategoryDropDownList.ClientID %>');

            if (fileUpload.value === "") {
                alert('Please select a file to upload.');
                return false;
            }

            if (categoryDropDown.value === "") {
                alert('Please select a category.');
                return false;
            }

            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="body-wrapper p-5 bg-white shadow-19">
    <div class="card-title">
        <h2 class="page-title fw-bolder">Upload Images</h2>
        <hr />
    </div>
    <div class="card">
        <div class="card-body">
            <h5 class="card-title py-2">Image Upload</h5>
            <div id="uploadForm" runat="server" enctype="multipart/form-data">
                <div class="form-group">
                    <asp:Label ID="CategoryLabel" runat="server" Text="Select Category:" CssClass="form-label"></asp:Label>
                    <asp:DropDownList ID="CategoryDropDownList" runat="server" CssClass="form-control mb-3">
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control mb-3" />
                </div>
                <div class="form-group">
                    <asp:Button ID="UploadButton" runat="server" Text="Upload" OnClick="UploadButton_Click" OnClientClick="return validateUploadForm();" CssClass="btn btn-success mb-3" />
                    <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelButton_Click" CssClass="btn btn-danger mb-3" />
                </div>
            </div>
        </div>
    </div>
    <div class="container p-4">
        <div class="card-title">
            <h2 class="page-title fw-bolder">View Images</h2>
            <hr />
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowCommand="GridView1_RowCommand" CssClass="table text-nowrap mb-0 align-middle">
            <Columns>
                <asp:TemplateField HeaderText="Sr.No.">
                    <ItemTemplate>
                        <h6 class="fw-semibold mb-0"><%#Container.DataItemIndex+1 %></h6>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Image">
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Eval("ImageData")) %>' CssClass="gallery-image" onclick="enlargeImage(this)" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Image Category">
                    <ItemTemplate>
                        <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("categoryName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="filename" HeaderText="Image Name" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="btnHideUnhide" runat="server" Text='<%# (Convert.ToBoolean(Eval("isActive")) ? "Hide" : "Show") %>' CssClass='<%# (Convert.ToBoolean(Eval("isActive")) ? "btn btn-danger" : "btn btn-success") %>'  OnClick="toggleActiveState" data-id='<%# Eval("ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton ID="EditButton" runat="server" CssClass="btn btn-primary" Text="Edit" CommandName="EditRow" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="False"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <div class="text-center">
                    <p class="p-4">
                        <i class="fas fa-image fa-3x pb-3"></i>
                        <br />
                        No Images Found.
                    </p>
                </div>
            </EmptyDataTemplate>
        </asp:GridView>

        <div id="myModal" class="enlarged-image-overlay" onclick="closeModal()">
        <img class="enlarged-image" id="img01" onclick="event.stopPropagation()">
    </div>
</div>
    <style>
       .gallery-image{
           width:80px;
       }
    </style>
</div>
</asp:Content>

