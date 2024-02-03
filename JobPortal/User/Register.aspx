<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="JobPortal.User.Register" %>

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
                    <h2 class="contact-title">Sign Up</h2>
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
                            <div class="col-sm-6">
                                <div class="form-group">

                                    <asp:Label ID="Label2" runat="server">Password</asp:Label>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter Password" TextMode="Password" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">

                                    <asp:Label ID="Label3" runat="server">Confirm Password</asp:Label>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" placeholder="Re-Enter the Password" TextMode="Password" required></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password must be same !" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" Font-Size="Small"></asp:CompareValidator>

                                </div>
                            </div>

                            <div class="col-12">
                                <h6>Personal Information
                                </h6>
                            </div>

                            <div class="col-12">
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server">Full Name</asp:Label>
                                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter the FullName" required></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Name Must be in Characters" ControlToValidate="txtFullName" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" Font-Size="Small" ValidationExpression="^[a-zA-Z\s]+$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server">Address</asp:Label>
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Address" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server">Mobile Number</asp:Label>
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Mobile Number" required></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Mobile number must have 10 digits" ControlToValidate="txtMobile" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" Font-Size="Small" ValidationExpression="^[0-9]{10}$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server">Email</asp:Label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email" TextMode="Email" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <asp:Label ID="Label8" runat="server">State</asp:Label>
                                    <asp:DropDownList ID="ddlState" runat="server" DataSourceID="SqlDataSource1" CssClass="form-control w-100" DataTextField="CountryName" DataValueField="CountryName">
                                        <asp:ListItem Value="0">Select Country</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select State" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" Font-Size="Small" InitialValue="0" ControlToValidate="ddlState"></asp:RequiredFieldValidator>

                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cs %>" SelectCommand="select CountryName from Country"></asp:SqlDataSource>
                                </div>
                            </div>

                        </div>
                        <div class="form-group mt-3">
                            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="button button-contactForm boxed-btn mr-4" OnClick="btnRegister_Click" />


                            <span class="clickLink"><a href="../User/Login.aspx">Already Register Click Here ...</a></span>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
