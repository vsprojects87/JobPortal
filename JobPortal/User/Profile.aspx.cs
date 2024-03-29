﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace JobPortal.User
{
    public partial class Profile : System.Web.UI.Page
    {
        static string s = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        SqlConnection con = new SqlConnection(s);
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }
            if (!IsPostBack)
            {
                showUserProfile();
            }

        }

        private void showUserProfile()
        {
            string query = "Select UserId, Username, Name, Address, Mobile , Email, State, Resume from [User] where Username=@Username";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", Session["user"]);
            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            if (dt != null)
            {
                dlProfile.DataSource = dt;
                dlProfile.DataBind();
            }
            else
            {
                Response.Write("<script>alert('Please Login again with new username');</script>");
            }
        }

        protected void dlProfile_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dlProfile_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "EditUserProfile")
            {
                Response.Redirect("ResumeBuild.aspx?id=" + e.CommandArgument.ToString());
            }
        }
    }
}