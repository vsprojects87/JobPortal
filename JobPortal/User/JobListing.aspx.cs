using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JobPortal.User
{
    public partial class JobListing : System.Web.UI.Page
    {
        static string s = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        SqlConnection con = new SqlConnection(s);
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        string query;
        public int jobCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showJobList();
                RBSelectedColorChange();
            }

        }

        private void RBSelectedColorChange()
        {
            if (RadioButtonList1.SelectedItem.Selected == true)
            {
                RadioButtonList1.SelectedItem.Attributes.Add("class", "selectedradio");
            }
        }

        private void showJobList()
        {
            if (dt == null)
            {
                string query = @"Select JobId, Title, Salary, JobType, CompanyName,CompanyImage, Country,State, CreateDate from Jobs";
                cmd = new SqlCommand(query, con);
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
            }
            DataList1.DataSource = dt;
            DataList1.DataBind();
            lbljobCount.Text = JobCount(dt.Rows.Count);
        }

        string JobCount(int count)
        {
            if (count > 1)
            {
                return "Total <b>" + count + "</b> jobs found";
            }
            else if (count == 1)
            {
                return "Total <b>" + count + "</b> jobs found";

            }
            else
            {
                return "No Jobs Found !";
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue != "0")
            {
                string query = @"Select JobId, Title, Salary, JobType, CompanyName,CompanyImage, Country,State, CreateDate from Jobs where State='" + ddlCountry.SelectedValue + "' ";
                cmd = new SqlCommand(query, con);
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            else
            {
                showJobList();
                RBSelectedColorChange();
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

        // Getting RelativeDate for given date like -- '1 month ago'

        public static string RelativeDate(DateTime theDate)

        {

            Dictionary<long, string> thresholds = new Dictionary<long, string>();

            int minute = 60;

            int hour = 60 * minute;

            int day = 24 * hour;

            thresholds.Add(60, "{0} seconds ago");

            thresholds.Add(minute * 2, "a minute ago");

            thresholds.Add(45 * minute, "{0} minutes ago");

            thresholds.Add(120 * minute, "an hour ago");

            thresholds.Add(day, "{0} hours ago");

            thresholds.Add(day * 2, "yesterday");

            thresholds.Add(day * 30, "{0} days ago");

            thresholds.Add(day * 365, "{0} months ago");

            thresholds.Add(long.MaxValue, "{0} years ago");

            long since = (DateTime.Now.Ticks - theDate.Ticks) / 10000000;

            foreach (long threshold in thresholds.Keys)

            {

                if (since < threshold)

                {

                    TimeSpan t = new TimeSpan((DateTime.Now.Ticks - theDate.Ticks));

                    return string.Format(thresholds[threshold], (t.Days > 365 ? t.Days / 365 : (t.Days > 0 ? t.Days : (t.Hours > 0 ? t.Hours : (t.Minutes > 0 ? t.Minutes : (t.Seconds > 0 ? t.Seconds : 0))))).ToString());

                }

            }

            return "";

        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string jobType = string.Empty;
            jobType = selectedCheckbox();

            if (jobType != "")
            {
                string query = @"Select JobId, Title, Salary, JobType, CompanyName,CompanyImage, Country,State, CreateDate from Jobs where jobType In (" + jobType + ")";
                cmd = new SqlCommand(query, con);
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            else
            {
                showJobList();
            }

        }

        private string selectedCheckbox()
        {
            string jobType = string.Empty;
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    jobType += "'" + CheckBoxList1.Items[i] + "',";
                }
            }
            // when we will traverse through the checkboxlist items we will get the selectd item and we can select mutltiple items
            // so as we have wrote query like "jobType IN ()" we can get result for more than one checkbox 
            // but to have more than one checkbox concat we need "," at the end of each checkbox thats why we have added "," at end in case of 
            // multiple items
            return jobType = jobType.TrimEnd(',');
            // when we will select last element there will be "," added to it so we need to trim last items comma(,) 
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue != "0")
            {
                string postedDate = string.Empty;
                postedDate = selectedRadioButton();
                string query = @"Select JobId, Title, Salary, JobType, CompanyName,CompanyImage, Country,State, CreateDate from Jobs where Convert(DATE,CreateDate) "+postedDate+" ";
                cmd = new SqlCommand(query, con);
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            else
            {
                showJobList();
                RBSelectedColorChange();
            }

        }

        string selectedRadioButton()
        {
            string postedDate = string.Empty;
            DateTime date = DateTime.Today;
            if (RadioButtonList1.SelectedValue == "1")
            {
                postedDate = "= Convert(DATE, '" + date.ToString("yyyy/MM/dd") + "')";
            }
            else if (RadioButtonList1.SelectedValue == "2")
            {
                postedDate = "between Convert(DATE, '" + DateTime.Now.AddDays(-2).ToString("yyyy/MM/dd") + "') and Convert(DATE, '" + date.ToString("yyyy/MM/dd") + "')";

            }
            else if (RadioButtonList1.SelectedValue == "3")
            {
                postedDate = "between Convert(DATE, '"+DateTime.Now.AddDays(-3).ToString("yyyy/MM/dd")+"') and Convert(DATE, '" + date.ToString("yyyy/MM/dd") + "')";

            }
            else if (RadioButtonList1.SelectedValue == "4")
            {
                postedDate = "between Convert(DATE, '"+DateTime.Now.AddDays(-5).ToString("yyyy/MM/dd")+"') and Convert(DATE, '" + date.ToString("yyyy/MM/dd") + "')";

            }
            else
            {
                postedDate = "between Convert(DATE, '"+DateTime.Now.AddDays(-10).ToString("yyyy/MM/dd")+"') and Convert(DATE, '" + date.ToString("yyyy/MM/dd") + "')";

            }
            return postedDate;
        }
        // above code will check the date between the todays date and the date which we have requested like all the record between 2 days if select 
        // first radiobox or all records between 10 days if we select last checkbox and it will check the query in if it is first value or between the dates


        protected void lbFilter_Click(object sender, EventArgs e)
        {
            try
            {
                bool isCondition = false;
                string subQuery = string.Empty;
                string jobType = string.Empty;
                string postedDate = string.Empty;
                string addAnd = string.Empty;
                List<string> querylist = new List<string>(); 
                if(ddlCountry.SelectedValue!="0")
                {
                    querylist.Add("State= '" + ddlCountry.SelectedValue + "' ");
                    isCondition = true;
                }
                jobType = selectedCheckbox();
                if (jobType != "")
                {
                    querylist.Add("JobType IN ('" + jobType + "') ");
                    isCondition = true;
                }
                if(RadioButtonList1.SelectedValue != "0")
                {
                    postedDate = selectedRadioButton();
                    querylist.Add("Convert (DATE,CreateDate) " + postedDate);
                    isCondition = true;
                }
                if(isCondition)
                {
                    foreach(string s in querylist)
                    {
                        subQuery += s + "and";
                    }
                    subQuery = subQuery.Remove(subQuery.LastIndexOf("and"), 3);
                    query = @"Select JobId, Title, Salary, JobType, CompanyName,CompanyImage, Country,State, CreateDate from Jobs where "+subQuery+" ";
                }
                else
                {
                    query = @"Select JobId, Title, Salary, JobType, CompanyName,CompanyImage, Country,State, CreateDate from Jobs";
                }
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                dt=new DataTable();
                sda.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+ "')</script>");
            }
            finally
            {
                con.Close();
            }
        }

        protected void lbReset_Click(object sender, EventArgs e)
        {
            ddlCountry.ClearSelection();
            CheckBoxList1.ClearSelection();
            RadioButtonList1.SelectedValue = "0";
            RBSelectedColorChange();
            showJobList();
        }
    }
}