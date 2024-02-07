using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace JobPortal.User
{
    public partial class JobDetails : System.Web.UI.Page
    {
        static string s = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        SqlConnection con = new SqlConnection(s);
        SqlCommand cmd;
        SqlDataReader sdr;
        DataTable dt1, dt2;
        SqlDataAdapter sda;
        public string jobTitle = string.Empty;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                showJobDetails();
                DataBind();
                // we are binding variable to html element using <%# jobTitle %>
                // this is a function to bind it to frontend
                // search 
            }
            else
            {
                Response.Redirect("JobListing.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void showJobDetails()
        {

            string query = @"Select * from Jobs where JobId=@id";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            sda = new SqlDataAdapter(cmd);
            dt1 = new DataTable();
            sda.Fill(dt1);
            DataList1.DataSource = dt1;
            DataList1.DataBind();
            //jobTitle = dt1.Rows[0]["Title"].ToString();
            // we canonly access the data in datalist since we only binded it to datalist so we we want to use that data outside datalist we need
            // to bind that specified column data to variable like jobTitle we took above
        }


        protected void DataList1_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
        {
            if (e.CommandName == "ApplyJob")
            {
                if (Session["user"] != null)
                {
                    try
                    {
                        string query = @"Insert into AppliedJobs values(@JobId, @UserId)";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@JobId", Request.QueryString["id"]);
                        cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
                        con.Open();
                        int r = cmd.ExecuteNonQuery();
                        if (r > 0)
                        {
                            Response.Write("<script>alert('Job Applied Successfully');</script>");
                            showJobDetails();
                            // when we click on apply job button we want it button text change to applied right after clicking it thats why we have called
                            // showJobDetails() to show data
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed to apply a job try again later');</script>");

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
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
        // Above code will perform when click on the apply button to save the job id and user id so we will know which user applied for which job

        bool isApplied()
        {
            string query = @"Select * from AppliedJobs where (UserId=@UserId) and (JobId=@JobId)";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
            cmd.Parameters.AddWithValue("@JobId", Request.QueryString["id"]);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            dt1 = new DataTable();
            adp.Fill(dt1);
            if (dt1.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // to check if we have applied or not

        protected void DataList1_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {
            if (Session["user"] != null)
            {
                LinkButton btnApplyJob = e.Item.FindControl("lbApplyJob") as LinkButton;
                if (isApplied())
                {
                    btnApplyJob.Enabled = false;
                    btnApplyJob.Text = "Applied";
                }
                else
                {
                    btnApplyJob.Enabled = true;
                    btnApplyJob.Text = "Apply Now";
                }
            }
        }

        protected string GetImageUrl(Object url)
        {
            string url1 = "";
            if (string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                url1 = "~/Images/No_Image.png";
            }
            else
            {
                url = string.Format("~/{0}", url);
            }
            return ResolveUrl(url1);
        }
    }
}