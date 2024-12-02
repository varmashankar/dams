<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="admin_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .full-cell-link {
            display: block;
            height: 100%;
            width: 100%;
            text-decoration: none;
            font-weight:400;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid mt-4">
        <!-- Welcome Message -->
        <div class="section bg-white p-3 rounded shadow-19">
            <h2 class="p-2 fw-bolder">Welcome,
            Dr.
                <asp:Label ID="lbluserName" runat="server" CssClass="text-uppercase" />
                !</h2>
            <hr />
            <!-- Quick Actions -->
            <div class="container p-3">
                <div class="row">

                    <div class="col-md-6 col-lg-3 mb-3">
                        <asp:LinkButton ID="btnAddNewDoctor" runat="server" CssClass="btn btn-primary btn-block" PostBackUrl="newdoctor.aspx">Add New Doctor</asp:LinkButton>
                    </div>
                    <div class="col-md-6 col-lg-3 mb-3">
                        <asp:LinkButton ID="btnAddNewDepartment" runat="server" CssClass="btn btn-primary btn-block" PostBackUrl="newdepartment.aspx">Add New Department</asp:LinkButton>
                    </div>
                    <div class="col-md-6 col-lg-3 mb-3">
                        <asp:LinkButton ID="btnAddNewAdmin" runat="server" CssClass="btn btn-primary btn-block" PostBackUrl="newadmin.aspx">Add New Admin</asp:LinkButton>
                    </div>
                    <div class="col-md-6 col-lg-3 mb-3">
                        <asp:LinkButton ID="btnViewAllDoctors" runat="server" CssClass="btn btn-warning btn-block" PostBackUrl="viewalldoctors.aspx">View All Doctors</asp:LinkButton>
                    </div>
                    <div class="col-md-6 col-lg-3 mb-3">
                        <asp:LinkButton ID="btnAddMedicine" runat="server" CssClass="btn btn-warning btn-block" PostBackUrl="addmedicine.aspx">Add Medicines</asp:LinkButton>
                    </div>
                    <div class="col-md-6 col-lg-3 mb-3">
                        <asp:LinkButton ID="btnAddCateory" runat="server" CssClass="btn btn-warning btn-block" PostBackUrl="category.aspx">Add Cateory</asp:LinkButton>
                    </div>
                    
                    <div class="col-md-6 col-lg-3 mb-3">
                        <asp:LinkButton ID="btnAddImages" runat="server" CssClass="btn btn-warning btn-block" PostBackUrl="gallery.aspx">Add Images</asp:LinkButton>
                    </div>

                </div>
                <div class="row">
                    <!-- Doctors Only -->
                    <div class="col-md-6 col-lg-3 mb-3">
                        <asp:LinkButton ID="btnAppointmentsRequest" runat="server" CssClass="btn btn-warning btn-block" PostBackUrl="requests.aspx">Appointment Requests</asp:LinkButton>
                    </div>
                    <div class="col-md-6 col-lg-3 mb-3">
                        <asp:LinkButton ID="btnTodayAppt" runat="server" CssClass="btn btn-info btn-block" PostBackUrl="today.aspx">Today Appointments</asp:LinkButton>
                    </div>
                    <div class="col-md-6 col-lg-3 mb-3">
                        <asp:LinkButton ID="btnConfirmedAppt" runat="server" CssClass="btn btn-success btn-block" PostBackUrl="confirmed.aspx">Confirmed Appointments</asp:LinkButton>
                    </div>
                    <div class="col-md-6 col-lg-3 mb-3">
    <asp:LinkButton ID="btnSearchPatient" runat="server" CssClass="btn btn-danger btn-block" PostBackUrl="SearchPatients.aspx">Search Patient</asp:LinkButton>
</div>
                    <div class="col-md-6 col-lg-3 mb-3">
                        <asp:LinkButton ID="btnUpcomingAppointments" runat="server" CssClass="btn btn-secondary btn-block" PostBackUrl="upcomingappt.aspx">Upcoming Appointments</asp:LinkButton>
                    </div>
                    <div class="col-md-6 col-lg-3 mb-3">
                        <asp:LinkButton ID="btnPreviousAppointments" runat="server" CssClass="btn btn-success btn-block" PostBackUrl="previousappointments.aspx">Previous Appointments</asp:LinkButton>
                    </div>
                    
                </div>
            </div>
        </div>

        <!-- Appointment Calendar View-->
        <div class="section bg-white shadow-19 p-4 my-4">
            <div class="rounded-lg overflow-hidden" style="width: 100%">
                <asp:Calendar ID="AppointmentCalendar" runat="server" OnDayRender="AppointmentCalendar_DayRender" CssClass="custom-calendar" BackColor="White" BorderColor="White" BorderWidth="0" Font-Names="Arial, sans-serif" Font-Size="10pt" ForeColor="#333" Height="400px" Width="100%">
                    <DayHeaderStyle Font-Bold="True" Font-Size="9pt" ForeColor="#666" />
                    <NextPrevStyle Font-Bold="True" Font-Size="9pt" ForeColor="#007BFF" VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#999" />
                    <SelectedDayStyle BackColor="#007BFF" ForeColor="White" BorderColor="#0056b3" BorderWidth="1px" />
                    <TitleStyle BackColor="#F8F9FA" BorderColor="#E0E0E0" BorderWidth="1px" Font-Bold="True" Font-Size="14pt" ForeColor="#007BFF" />
                    <TodayDayStyle BackColor="#E9ECEF" ForeColor="#495057" />
                </asp:Calendar>
            </div>
        </div>

        <!-- Upcoming Appointments -->
        <div class="section bg-light p-4 rounded my-4">
            <h2 class="mb-4">Scheduled Appointments</h2>
            <asp:DataList ID="dlAppointments" runat="server" OnItemDataBound="dlAppointments_ItemDataBound" RepeatDirection="Horizontal" RepeatColumns="3">
                <ItemTemplate>
                    <div class="col-md-12 col-lg-12 mb-3">
                        <div class="card shadow-sm border-light hover-shadow position-relative">
                            <div class="card-body">
                                <!-- Heading and Status Container -->
                                <div class="d-flex justify-content-between align-items-start mb-2">
                                    <h6 class="card-title text-primary mb-0 mt-2">Appointment ID: <%# Eval("appointmentID") %></h6>
                                    <p id="status" runat="server" class="card-text mb-0 position-absolute top-0 end-0 mt-0"><%# Eval("Status") %></p>
                                </div>
                                <p class="card-text"><strong>Date:</strong> <%# Eval("AppointmentDate", "{0:MMMM dd, yyyy}") %></p>
                                <p class="card-text"><strong>Time:</strong> <%# Eval("AppointmentTime") %></p>
                                <p class="card-text"><strong>Department ID:</strong> <%# Eval("DepartmentID") %></p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <a href="upcomingappt.aspx" class="btn btn-info float-end my-3">View All Appointments</a>
                    <asp:Label ID="lblNoData" runat="server" CssClass="alert alert-warning" Text="No upcoming appointments available." Visible="False"></asp:Label>
                </FooterTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Content>

