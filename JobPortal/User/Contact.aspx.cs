using System;
using System.Configuration;
using System.Data.SqlClient;

namespace JobPortal.User
{
    public partial class Contact : System.Web.UI.Page
    {
        static string s = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        SqlConnection con = new SqlConnection(s);
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"insert into Contact values(@Name,@Email,@Subject,@Message)";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name.Value.Trim());
                cmd.Parameters.AddWithValue("@Email", email.Value.Trim());
                cmd.Parameters.AddWithValue("@Subject", subject.Value.Trim());
                cmd.Parameters.AddWithValue("@Message", message.Value.Trim());
                con.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Thanks for reaching out, will look into your query !";
                    lblMsg.CssClass = "alert alert-success";
                    clear();
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Cannot save record right now, please try after some time";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            finally
            {
                con.Close();
            }
        }

        private void clear()
        {
            name.Value = string.Empty;
            email.Value = string.Empty;
            message.Value = string.Empty;
            subject.Value = string.Empty;

        }
    }
}