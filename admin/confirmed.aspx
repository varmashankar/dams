<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="confirmed.aspx.cs" Inherits="admin_confirmed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
    .custom-dropdown {
        position: relative;
        display: inline-block;
    }

    .action-button {
        background-color: #6200EE; /* Primary Color */
        color: white;
        padding: 10px 15px;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        font-size: 16px;
        transition: background-color 0.3s, transform 0.2s;
    }

    .action-button:hover {
        background-color: #3700B3; /* Darker shade for hover effect */
        transform: scale(1.05); /* Slightly enlarge button on hover */
    }

    .dropdown-content {
        display: none;
        position: absolute;
        background-color: #f9f9f9;
        min-width: 160px;
        box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
        z-index: 1;
    }

    .custom-dropdown:hover .dropdown-content {
        display: block;
    }

    .dropdown-content a {
        color: black;
        padding: 12px 16px;
        text-decoration: none;
        display: block;
        transition: background-color 0.3s;
    }

    .dropdown-content a:hover {
        background-color: #f1f1f1; /* Light grey background on hover */
    }
</style>
     <script>
     function toggleDropdown(event) {
         event.stopPropagation(); // Prevent the click event from bubbling up
         const dropdown = event.target.nextElementSibling;
         dropdown.style.display = dropdown.style.display === "block" ? "none" : "block";
     }

     // Close the dropdown if the user clicks outside of it
     window.onclick = function (event) {
         const dropdowns = document.getElementsByClassName("dropdown-content");
         for (let i = 0; i < dropdowns.length; i++) {
             dropdowns[i].style.display = "none";
         }
     };
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container bg-white p-4 shadow-19 mt-4">
        <h2>Confirmed Appointments</h2>
        <asp:GridView ID="gvConfirmedAppointments" runat="server" CssClass="table table-striped table-bordered"
            AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Appointment ID">
                    <ItemTemplate>
                        <%# Eval("AppointmentID") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <%# Eval("AppointmentDate", "{0:yyyy-MM-dd}") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Time">
                    <ItemTemplate>
                        <%# Eval("AppointmentTime") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department">
                    <ItemTemplate>
                        <%# Eval("departmentName") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <span class="badge 
                        <%# Eval("Status").ToString() == "Pending" ? "badge-warning" : 
                            Eval("Status").ToString() == "Confirmed" ? "badge-success" : 
                            Eval("Status").ToString() == "Cancelled" ? "badge-danger" : 
                            "badge-secondary" %>">
                            <%# Eval("Status") %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <div class="custom-dropdown">
                            <button class="action-button" onclick="toggleDropdown(event)">Actions</button>
                            <div class="dropdown-content">
                                <a href='<%# "ViewAppointment.aspx?AppointmentID=" + Eval("AppointmentID") %>'>View</a>
                                <a href='<%# "EditAppointment.aspx?AppointmentID=" + Eval("AppointmentID") %>'>Edit</a>
                                <a href='<%# "DeleteAppointment.aspx?AppointmentID=" + Eval("AppointmentID") %>'>Delete</a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <EmptyDataTemplate>
                <div class="container p-3 text-center">
                    <asp:Label ID="lblNoData" runat="server" CssClass="alert alert-danger"
                        Text="No appointments data available."></asp:Label>
                </div>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:Label ID="lblMessage" runat="server" CssClass="alert" Visible="false"></asp:Label>
    </div>
</asp:Content>

