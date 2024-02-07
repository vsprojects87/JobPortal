using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobPortal.Admin
{
    public partial class ViewResume : System.Web.UI.Page
    {
        static string s = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        SqlConnection con = new SqlConnection(s);
        SqlCommand cmd;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }
            if (!IsPostBack)
            {
                ShowAppliedJob();
            }
        }

        private void ShowAppliedJob()
        {
            string query = string.Empty;
            query = @"Select Row_Number() over(Order by (Select 1)) as [Sr.No] , aj.AppliedJobId , j.CompanyName, aj.JobId, j.Title, u.Mobile, u.Name, u.Email, 
                        u.Resume FROM [dbo].[AppliedJobs] aj 
                        inner Join [User]  u on aj.UserId = u.UserId
                        inner Join [Jobs]  j on aj.JobId = j.JobId ";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            ShowAppliedJob();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int AppliedjobId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                cmd = new SqlCommand("Delete from AppliedJobs where AppliedJobId=@id", con);
                cmd.Parameters.AddWithValue("@id", AppliedjobId);
                con.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Resume Deleted Successfully!";
                    lblMsg.CssClass = "alert alert-success";
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Cannot Delete this Record";
                    lblMsg.CssClass = "alert alert-danger";
                }
                con.Close();
                GridView1.EditIndex = -1;
                ShowAppliedJob();
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "select$"+e.Row.RowIndex);
            e.Row.ToolTip = "Click to View Job Details";
        }
        // when we will hover on applied job details in admin panel this will show tooltip with message

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(GridViewRow row in GridView1.Rows)
            {
                if(row.RowIndex == GridView1.SelectedIndex)
                {
                    HiddenField jobId = (HiddenField)row.FindControl("hdnJobId");
                    Response.Redirect("JobList.aspx?id=" +jobId.Value);
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFFF");
                    row.ToolTip = "Click to select this row";
                }
            }
        }
        // this will load data from hiddenfield that we have taken which contains jobid
        // when we will click on gridview row then the above event will redirect the page to joblist where we can see that job 
    }
}