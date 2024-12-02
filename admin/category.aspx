<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="category.aspx.cs" Inherits="admin_category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
    function validateForm() {
        var categoryName = document.getElementById('<%= categoryName.ClientID %>').value.trim();
        if (categoryName === "") {
            alert('Category name cannot be empty!');
            return false;
        }
        return true;
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="body-wrapper p-5 bg-white">
        <div class="card-title">
            <h2 class="page-title fw-bolder">Gallery Categories</h2>
            <hr />
        </div>
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header">
                            <h4>Add New Category</h4>
                        </div>
                        <div class="card-body">
                            <asp:Panel ID="categoryForm" runat="server">
                                <div class="form-group">
                                    <label for="categoryName">Category Name</label>
                                    <asp:TextBox ID="categoryName" CssClass="form-control mt-2" placeholder="Enter Your Category Name" runat="server" required></asp:TextBox>
                                </div>
                                <asp:Button ID="btnAddCategory" CssClass="btn btn-primary mt-3" runat="server" Text="Add" OnClientClick="return validateForm();" OnClick="btnAddCategory_Click" />
                                <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelButton_Click" CssClass="btn btn-danger mt-3" />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container p-4">
            <div class="card-title">
                <h2 class="page-title fw-bolder">View Categories</h2>
                <hr />
            </div>
            <asp:GridView ID="GridViewCategory" runat="server" EmptyDataText="No Appointments Today." DataKeyNames="id" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" CssClass="table text-center text-nowrap mb-0 align-middle">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <h6 class="fw-semibold mb-0"><i class="fas fa-id-badge px-1"></i>Id</h6>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <h6 class="fw-semibold mb-0"><%#Container.DataItemIndex+1 %></h6>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <h6 class="fw-semibold mb-1"><i class="fas fa-calendar-check px-1"></i>Category Name</h6>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <p class="mb-0 fw-normal"><%# Eval("categoryName") %></p>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <h6 class="fw-semibold mb-1"><i class="fas fa-calendar-check px-1"></i>Created Date</h6>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <p class="mb-0 fw-normal"><%# Eval("createdDate", "{0:dd-MM-yyyy}") %></p>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="StatusButton" runat="server" CssClass='<%# Convert.ToBoolean(Eval("isActive")) ? "btn btn-success" : "btn btn-warning" %>'
                                CommandName='<%# Convert.ToBoolean(Eval("isActive")) ? "Inactive" : "Active" %>'
                                Text='<%# Convert.ToBoolean(Eval("isActive")) ? "Hide" : "Show" %>'
                                OnClick="StatusButton_Click" CausesValidation="False"></asp:LinkButton>
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
                            <i class="fas fa-calendar-times fa-3x pb-3"></i>
                            <br />
                            No Categories.
                        </p>
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>

</asp:Content>

