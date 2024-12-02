<%@ Page Title="" Language="C#" MasterPageFile="~/patient/patient.master" AutoEventWireup="true" CodeFile="newappointment.aspx.cs" Inherits="patient_newappointment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container my-5">
        <div class="row">
            <div class="col-lg-8 offset-lg-2">
                <div class="appointment-form bg-light rounded p-5 hover-shadow shadow-19">
                    <h2 class="display-6 mb-4 text-center text-primary">Schedule New Appointment</h2>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger mb-4" />

                    <div class="row gy-3 gx-4">

                        <!-- Department -->
                        <div class="col-md-12">
                            <asp:Label ID="Label8" runat="server" CssClass="form-label" Text="Department: "></asp:Label>
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-select py-2 border-primary bg-transparent">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment" InitialValue="" ErrorMessage="Department selection is required." CssClass="text-danger" Display="Dynamic" />
                        </div>

                        <!-- Appointment Date -->
                        <div class="col-md-6">
                            <asp:Label ID="Label6" runat="server" CssClass="form-label" Text="Appointment Date: "></asp:Label>
                            <asp:TextBox ID="txtDate" runat="server" TextMode="Date" CssClass="form-control py-3 border-primary bg-transparent"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtDate" ErrorMessage="Date selection is required." CssClass="text-danger" Display="Dynamic" />
                            <asp:CustomValidator ID="cvDate" runat="server" ControlToValidate="txtDate"
                                ErrorMessage="Please select a date in the future."
                                CssClass="text-danger"
                                Display="Dynamic"
                                OnServerValidate="cvDate_ServerValidate"></asp:CustomValidator>
                        </div>

                        <!-- Appointment Time --->
                        <div class="col-md-6">
                            <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Appointment Time: "></asp:Label>
                            <asp:DropDownList ID="ddlTimeSlot" runat="server" CssClass="form-select border-primary bg-transparent">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvTimeSlot" runat="server" ControlToValidate="ddlTimeSlot" InitialValue="" ErrorMessage="Time slot selection is required." CssClass="text-danger" Display="Dynamic" />
                            <asp:CustomValidator ID="cvTimeSlot" runat="server" ControlToValidate="ddlTimeSlot"
                                ErrorMessage="Please select a valid time slot."
                                CssClass="text-danger"
                                Display="Dynamic"
                                OnServerValidate="cvTimeSlot_ServerValidate"></asp:CustomValidator>
                        </div>


                        <!-- Submit Button -->
                        <div class="col-12">
                            <asp:Button ID="btnSubmit" runat="server" Text="Schedule" CssClass="btn btn-primary text-white py-2 px-5 float-end text-uppercase" CausesValidation="true"  OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

