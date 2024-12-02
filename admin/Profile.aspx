<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="admin_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container my-5">
    <h2 class="text-center">User Profile</h2>
    <hr />
    <div class="card p-4">
        <h4 class="card-title text-center">Profile Details</h4>
        <div class="table-responsive">
            <table class="table table-bordered">
                <tbody>
                    <tr>
                        <td class="font-weight-bold">Full Name:</td>
                        <td>
                            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="font-weight-bold">Email:</td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="font-weight-bold">Phone:</td>
                        <td>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" ReadOnly="true" Text="N/A"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="font-weight-bold">Role:</td>
                        <td>
                            <asp:TextBox ID="txtRole" runat="server" CssClass="form-control" ReadOnly="true" Text="N/A"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="font-weight-bold">Speciality:</td>
                        <td>
                            <asp:TextBox ID="txtSpeciality" runat="server" CssClass="form-control" ReadOnly="true" Text="N/A"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div class="text-center">
            <asp:Button ID="btnEditProfile" runat="server" Text="Edit Profile" CssClass="btn btn-primary" OnClick="btnEditProfile_Click" />
            <asp:Button ID="btnSaveProfile" runat="server" Text="Save Profile" CssClass="btn btn-success" OnClick="btnSaveProfile_Click" Visible="false" />
            <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-secondary" OnClick="btnLogout_Click" />
        </div>
    </div>
</div>

</asp:Content>

