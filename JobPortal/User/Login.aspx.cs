using System;
using System.Configuration;
using System.Data.SqlClient;

namespace JobPortal.User
{
    public partial class Login : System.Web.UI.Page
    {
        static string s = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        SqlConnection con = new SqlConnection(s);
        SqlCommand cmd;
        SqlDataReader reader;
        string username, password = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlLoginType.SelectedValue == "Admin")
                {
                    username = ConfigurationManager.AppSettings["username"];
                    password = ConfigurationManager.AppSettings["password"];
                    if (username == txtUserName.Text.Trim() && password == txtPassword.Text.Trim())
                    {
                        Session["admin"] = username;
                        Response.Redirect("../Admin/Dashboard.aspx", false);
                    }
                    else
                    {
                        showErrorMsg("admin");
                    }
                }
                else
                {
                    string query = @"Select * from [User] where Username=@Username and Password=@Password";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", txtUserName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                    con.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Session["user"] = reader["Username"].ToString();
                        Session["userId"] = reader["UserId"].ToString();
                        Response.Redirect("Default.aspx", false);
                    }
                    else
                    {
                        showErrorMsg("User");
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                con.Close();
            }
        }

        private void showErrorMsg(string UserType)
        {
            lblMsg.Visible = true;
            lblMsg.Text = "<b>" + UserType + " </b> Credentials are incorrect !";
            lblMsg.CssClass = "alert alert-danger";
        }
    }
}