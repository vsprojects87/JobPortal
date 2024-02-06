<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMaster.Master" AutoEventWireup="true" CodeBehind="ResumeBuild.aspx.cs" Inherits="JobPortal.User.ResumeBuild" %>

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
                    <h2 class="contact-title">Build Resume</h2>
                </div>

                <div class="col-lg-12">
                    <div class="form-contact contact_form">
                        <div class="row">
                            <div class="col-12">Personal Information</div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server">Full Name</asp:Label>
                                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter the FullName" required></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Name Must be in Characters" ControlToValidate="txtFullName" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" Font-Size="Small" ValidationExpression="^[a-zA-Z\s]+$"></asp:RegularExpressionValidator>
                                </div>
                            </div>


                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server">UserName</asp:Label>
                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Enter unique Username" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server">Address</asp:Label>
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Address" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server">Mobile Number</asp:Label>
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Mobile Number" required></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Mobile number must have 10 digits" ControlToValidate="txtMobile" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" Font-Size="Small" ValidationExpression="^[0-9]{10}$"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server">Email</asp:Label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email" TextMode="Email" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label ID="Label8" runat="server">State</asp:Label>
                                    <asp:DropDownList ID="ddlState" runat="server" DataSourceID="SqlDataSource1" CssClass="form-control w-100" DataTextField="CountryName" DataValueField="CountryName">
                                        <asp:ListItem Value="0">Select Country</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select State" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" Font-Size="Small" InitialValue="0" ControlToValidate="ddlState"></asp:RequiredFieldValidator>

                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cs %>" SelectCommand="select CountryName from Country"></asp:SqlDataSource>
                                </div>
                            </div>


                            <div class="col-12 pt-4">
                                <h6>Education/Resume Information
                                </h6>
                            </div>


                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label runat="server">10th Percentage</asp:Label>
                                    <asp:TextBox ID="txtTenth" runat="server" CssClass="form-control" placeholder="Ex. 90%" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label runat="server">12th Percentage</asp:Label>
                                    <asp:TextBox ID="txtTwelth" runat="server" CssClass="form-control" placeholder="Ex. 90%" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label runat="server">Graduation Percentage/Grade</asp:Label>
                                    <asp:TextBox ID="txtGraduation" runat="server" CssClass="form-control" placeholder="Ex. 8.5CGPA" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label runat="server">Post Graduation Percentage/Grade</asp:Label>
                                    <asp:TextBox ID="txtPostGraduation" runat="server" CssClass="form-control" placeholder="Ex. 8.5CGPA" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label runat="server">PHD Percentage/Grade</asp:Label>
                                    <asp:TextBox ID="txtPhd" runat="server" CssClass="form-control" placeholder="Ex. 8.5CGPA" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label runat="server">Job/Work Profile</asp:Label>
                                    <asp:TextBox ID="txtWork" runat="server" CssClass="form-control" placeholder="Ex. SDE, Designer" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label runat="server">Work Experience</asp:Label>
                                    <asp:TextBox ID="txtExperience" runat="server" CssClass="form-control" placeholder="Ex. 2 Years, 9 Years" required></asp:TextBox>
                                </div>
                            </div>


                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label runat="server">Resume</asp:Label>
                                    <asp:FileUpload ID="fuResume" runat="server" CssClass="form-control pt-2" ToolTip=".doc, .docx, .pdf only" />
                                </div>
                            </div>


                        </div>

                        <div class="form-group mt-3">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button button-contactForm boxed-btn mr-4" OnClick="btnUpdate_Click" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
