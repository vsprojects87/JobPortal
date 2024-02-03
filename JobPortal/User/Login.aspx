<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="JobPortal.User.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <div class="container pt-50 pb-40">
            <div class="row">

                <div class="col-12 pb-20">
                    <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
                </div>

                <div class="col-12 text-center">
                    <h2 class="contact-title">Sign In</h2>
                </div>

                <div class="col-lg-6 mx-auto">
                    <div class="form-contact contact_form">
                        <div class="row">
                            <div class="col-12">Login Information</div>
                            <div class="col-12">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server">UserName</asp:Label>
                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Enter unique Username" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server">Password</asp:Label>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter Password" TextMode="Password" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server">Login Type</asp:Label>
                                    <asp:DropDownList ID="ddlLoginType" runat="server" CssClass="form-control w-100">
                                        <asp:ListItem Value="0">Select Login Type</asp:ListItem>
                                        <asp:ListItem>Admin</asp:ListItem>
                                        <asp:ListItem>User</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Login Type Required" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" Font-Size="Small" InitialValue="0" ControlToValidate="ddlLoginType"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                        </div>

                        <div class="form-group mt-3">
                            <asp:Button ID="btnLogin" runat="server" Text="Register" CssClass="button button-contactForm boxed-btn mr-4" OnClick="btnLogin_Click" />

                            <span class="clickLink"><a href="../User/Register.aspx">New User? Click Here ...</a></span>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </section>


</asp:Content>
