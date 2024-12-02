<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="allappointments.aspx.cs" Inherits="admin_allappointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="container bg-white shadow-19 p-4">
            <h2>All Appointments</h2>

            <!-- Message label for status updates -->
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

            <!-- GridView to display the appointments -->
            <asp:GridView ID="gvAppointments" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" AllowSorting="True" OnPageIndexChanging="gvAppointments_PageIndexChanging" OnSorting="gvAppointments_Sorting">
                <Columns>
                    <asp:TemplateField HeaderText="Appointment ID" SortExpression="AppointmentID">
                        <ItemTemplate>
                            <%# Eval("AppointmentID") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Appointment Date" SortExpression="AppointmentDate">
                        <ItemTemplate>
                            <%# Eval("AppointmentDate", "{0:yyyy-MM-dd}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Appointment Time" SortExpression="AppointmentTime">
                        <ItemTemplate>
                            <%# Eval("AppointmentTime") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Doctor Name" SortExpression="DoctorName">
                        <ItemTemplate>
                            <%# Eval("DoctorName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Department" SortExpression="DepartmentID">
                        <ItemTemplate>
                            <%# Eval("DepartmentName") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status" SortExpression="Status">
                        <ItemTemplate>
                            <%# Eval("Status") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
    <div class="container p-3 text-center">
        <asp:Label ID="lblNoData" runat="server" CssClass="alert alert-danger"
            Text="No appointments data available."></asp:Label>
    </div>
</EmptyDataTemplate>
                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" />
            </asp:GridView>
        </section>
</asp:Content>

