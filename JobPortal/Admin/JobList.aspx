<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="JobList.aspx.cs" Inherits="JobPortal.Admin.JobList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 700px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed">

        <div class="container-fluid pt-4 pb-4">
            <%--<div>
                <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
            </div>--%>

            <div class="btn-toolbar justify-content-between mb-3">
                <div class="btn-group">
                    <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="input-group h-25">
                    <asp:HyperLink ID="linkback" runat="server" NavigateUrl="~/Admin/ViewResume.aspx" CssClass="btn btn-secondary" Visible="false">< Back</asp:HyperLink>
                </div>
            </div>
            <%--when we will be redirected to joblist page when we will click on appliedjob gridview row this will enable the back button to go back to resume
            page--%>

            <h3 class="text-center">Job List</h3>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">

                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No Records to Display..!" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" DataKeyNames="JobId" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Title" HeaderText="Job Title">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="NoOfPost" HeaderText="No. of Post">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Qualification" HeaderText="Qualification">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Experience" HeaderText="Experience">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="LastDateToApply" HeaderText="Valid Till" DataFormatString="{0:dd MMMM yyyy}">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="CompanyName" HeaderText="Company">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="State" HeaderText="State">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="Country" HeaderText="Country">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="CreateDate" HeaderText="Posted Date" DataFormatString="{0:dd MMMM yyyy}">
                                <%--mm to show minutes MM for Month in number and MMMM for Month in name--%>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="EditJob" CommandArgument='<%#Eval("JobId") %>'>
                                        <asp:Image ID="Img" runat="server" ImageUrl="~/assets/img/icon/job-list3.png" Height="25px" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>

                            <asp:CommandField CausesValidation="false" HeaderText="Delete" ShowDeleteButton="true" DeleteImageUrl="../assets/img/icon/job-list4.png">
                                <ControlStyle Height="25px" Width="25px"></ControlStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:CommandField>

                        </Columns>
                        <HeaderStyle BackColor="#7200cf" ForeColor="White" />

                    </asp:GridView>


                </div>
            </div>


        </div>

    </div>


</asp:Content>
