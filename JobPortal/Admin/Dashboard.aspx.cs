using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JobPortal.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        static string s = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        SqlConnection con = new SqlConnection(s);
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }
            if (!IsPostBack)
            {
                Users();
                Jobs();
                AppliedJobs();
                ContactCount();
            }
        }

        private void AppliedJobs()
        {
            sda = new SqlDataAdapter("select Count(*) from [AppliedJobs]", con);
            dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session["AppliedJobs"] = dt.Rows[0][0];
            }
            else
            {
                Session["AppliedJobs"] = 0;
            }
        }
 

        private void Jobs()
        {
            sda = new SqlDataAdapter("select Count(*) from [Jobs]", con);
            dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session["Jobs"] = dt.Rows[0][0];
            }
            else
            {
                Session["Jobs"] = 0;
            }

        }

        private void Users()
        {
            sda = new SqlDataAdapter("select Count(*) from [User]", con);
            dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session["Users"] = dt.Rows[0][0];
            }
            else
            {
                Session["Users"] = 0;
            }
        }

        private void ContactCount()
        {
            sda = new SqlDataAdapter("select Count(*) from [Contact]", con);
            dt =new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                Session["Contact"] = dt.Rows[0][0];
            }
            else
            {
                Session["Contact"] = 0;
            }
        }
    }
}   