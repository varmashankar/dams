<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="newdepartment.aspx.cs" Inherits="admin_newdepartment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="mb-3">
        <div class="container bg-light card mt-5 p-4 shadow-19">
            <h2 class="display-5 text-primary">Create New Department</h2>
            <hr />

            <div class="form-group">
                <label for="txtDepartmentName">Department Name:</label>
                <asp:TextBox ID="txtDepartmentName" placeholder="New Department" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvDepartmentName" runat="server" ControlToValidate="txtDepartmentName"
                    ErrorMessage="Department Name is required." CssClass="text-danger" />
            </div>
            <asp:Button ID="btnAddDepartment" runat="server" Text="Add Department" CssClass="btn btn-primary" OnClick="btnAddDepartment_Click" />
            <asp:Label ID="lblMessage" runat="server" CssClass="mt-3" />

            <h3 class="mt-5">Department List</h3>
            <asp:GridView ID="gvDepartments" runat="server" DataKeyNames="departmentID" AllowPaging="True" PageSize="5" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" HeaderStyle-CssClass="thead-dark" RowStyle-CssClass="text-center" OnPageIndexChanging ="gvDepartments_PageIndexChanging" OnRowDeleting="gvDepartments_RowDeleting">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                <asp:TemplateField HeaderText="Sr. No.">
                    <ItemTemplate>
                         <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />
                <asp:CommandField HeaderText="Action" ControlStyle-CssClass="btn btn-primary" ShowDeleteButton="True" />
            </Columns>
                <EmptyDataTemplate>
                <div class="container p-3 text-center">
                    <asp:Label ID="lblNoData" runat="server" CssClass="alert alert-danger"><i class="fa fa-trash"></i> No Data Available</asp:Label>
                </div>
            </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </section>
</asp:Content>

