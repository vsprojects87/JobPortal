using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace JobPortal.Admin
{
    public partial class JobList : System.Web.UI.Page
    {
        static string s = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        SqlConnection con = new SqlConnection(s);
        SqlCommand cmd;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }
            if (!IsPostBack)
            {
                ShowJob();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowJob();
        }

        private void ShowJob()
        {
            string query = string.Empty;
            query = @"Select Row_Number() over(Order by (Select 1)) as [Sr.No], JobId, Title, NoOfPost, Qualification, Experience, LastDateToApply, CompanyName, State, Country, CreateDate from Jobs";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            if (Request.QueryString["id"] != null)
            {
                linkback.Visible = true;
            }
            // basically when the id will be pass to this page that means we have been redirected from resume page we want linkback button to be visible
            // to go back to resume page
        }

        protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            ShowJob();
        }

        protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int jobId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                cmd = new SqlCommand("Delete from Jobs where JobId=@id", con);
                cmd.Parameters.AddWithValue("@id", jobId);
                con.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    lblMsg.Text = "Job Deleted Successfully!";
                    lblMsg.CssClass = "alert alert-success";
                }
                else
                {
                    lblMsg.Text = "Cannot Delete this Record";
                    lblMsg.CssClass = "alert alert-danger";
                }
                con.Close();
                GridView1.EditIndex = -1;
                ShowJob();
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditJob")
            {
                Response.Redirect("NewJob.aspx?id=" + e.CommandArgument.ToString());
                //- we will fetch the id from eval of templateitem and we will pass that id to newjob.aspx file 
                //- when we click on edit then the rowcommand ecent will execute
                //- and we will be making changes of updation in newjob.aspx
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.ID = e.Row.RowIndex.ToString();
                if (Request.QueryString["id"] != null)
                {
                    int jobId = Convert.ToInt32(GridView1.DataKeys[e.Row.RowIndex].Values[0]);
                    if (jobId == Convert.ToInt32(Request.QueryString["id"])) 
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    }
                }
            }
        }
        // when we will click on applied job in admin pannel then we will be redirected to joblist where the job we selected will be highlighted using above code 
    }
}