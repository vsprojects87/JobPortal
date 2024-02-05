using System;

namespace JobPortal.User
{
    public partial class UserMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                lbRegisterOrProfile.Text = "Profile";
                lbLoginOrLogout.Text = "Logout";
            }
            else
            {
                lbRegisterOrProfile.Text = "Register";
                lbLoginOrLogout.Text = "Login";
            }
        }

        protected void lbRegisterOrProfile_Click(object sender, EventArgs e)
        {
            if (lbRegisterOrProfile.Text == "Profile")
            {
                Response.Redirect("Profile.aspx");
            }
            else
            {
                Response.Redirect("Register.aspx");
            }
        }

        protected void lbLoginOrLogout_Click(object sender, EventArgs e)
        {
            if (lbLoginOrLogout.Text == "Login")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Session.Abandon();
                Response.Redirect("Register.aspx");
            }
        }
    }
}