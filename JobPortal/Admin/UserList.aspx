<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="JobPortal.Admin.UserList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 700px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed">

        <div class="container-fluid pt-4 pb-4">
            <div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </div>

            <h3 class="text-center">User List/Details</h3>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">

                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No Records to Display..!" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" DataKeyNames="UserId" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting">
                        <columns>
                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                                <itemstyle horizontalalign="Center"></itemstyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Name" HeaderText="User Name">
                                <itemstyle horizontalalign="Center"></itemstyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Email" HeaderText="Email">
                                <itemstyle horizontalalign="Center"></itemstyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Mobile" HeaderText="Mobile">
                                <itemstyle horizontalalign="Center"></itemstyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="State" HeaderText="State">
                                <itemstyle horizontalalign="Center"></itemstyle>
                            </asp:BoundField>

                            <asp:CommandField CausesValidation="false" HeaderText="Delete" ShowDeleteButton="true" DeleteImageUrl="../assets/img/icon/job-list4.png">
                                <controlstyle height="25px" width="25px"></controlstyle>
                                <itemstyle horizontalalign="Center"></itemstyle>
                            </asp:CommandField>

                        </columns>
                        <headerstyle backcolor="#7200cf" forecolor="White" />

                    </asp:GridView>


                </div>
            </div>


        </div>

    </div>


</asp:Content>
