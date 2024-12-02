<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="viewalldoctors.aspx.cs" Inherits="admin_viewalldoctors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section>
        <div class="container bg-white my-5 p-4">
            <h2 class="mb-4">List of Doctors</h2><hr />
            <asp:GridView ID="gvDoctors" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" HeaderStyle-CssClass="thead-dark" RowStyle-CssClass="text-center">
                <Columns>
                    <asp:TemplateField HeaderText="Sr. No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                    <asp:BoundField DataField="Specialty" HeaderText="Specialty" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
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

