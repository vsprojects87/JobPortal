using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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
            jobTitle = dt1.Rows[0]["Title"].ToString();
            // we canonly access the data in datalist since we only binded it to datalist so we we want to use that data outside datalist we need
            // to bind that specified column data to variable like jobTitle we took above
        }


        protected void DataList1_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
        {

        }

        protected void DataList1_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {

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