using System;
using System.Configuration;
using System.Data.SqlClient;

namespace JobPortal.Admin
{
    public partial class NewJob : System.Web.UI.Page
    {
        static string s = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        SqlConnection con = new SqlConnection(s);
        SqlCommand cmd;
        string query;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }
            Session["title"] = "Add Job";
            if (!IsPostBack)
            {
                FillData();
            }
        }

        // when we will get the id from joblist for updating the job record it will be fetch by querystring and we will use that id to 
        // select and fill the data from database directly into textbox of the newjob page
        private void FillData()
        {
            if (Request.QueryString["id"] != null)
            {
                query = "select * from Jobs where JobId='" + Request.QueryString["id"] + "' ";
                cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        txtJobTitle.Text = sdr["Title"].ToString();
                        txtNoOfPost.Text = sdr["NoOfPost"].ToString();
                        //txtDescription.Text = (sdr.GetString("Description")).;
                        txtDescription.Text = sdr[3].ToString();
                        txtQualification.Text = sdr["Qualification"].ToString();
                        txtExperience.Text = sdr["Experience"].ToString();
                        txtSpecialization.Text = sdr["Specialization"].ToString();
                        txtLastDate.Text = Convert.ToDateTime(sdr["LastDateTOApply"]).ToString("yyyy-MM-dd");
                        txtSalary.Text = sdr["Salary"].ToString();
                        ddlJobType.SelectedValue = sdr["JobType"].ToString();
                        txtCompany.Text = sdr["CompanyName"].ToString();
                        txtWebsite.Text = sdr["Website"].ToString();
                        txtEmail.Text = sdr["Email"].ToString();
                        txtAddress.Text = sdr["Address"].ToString();
                        txtCountry.Text = sdr["Country"].ToString();
                        ddlState.SelectedValue = sdr["State"].ToString();
                        btnAdd.Text = "Update";
                        linkBack.Visible = true;
                        Session["title"] = "Edit Job";
                        // when we will get the id back to edit the information we want text of save button to change to Update and
                        // we want to show linkbutton once we are on edit menu so we can go back to view jobs
                        // we also want to change title of the page when we are updating from Add job to edit job thats why on page load 
                        // we will load the data if have requested id on edit otherwise it will continue to add job
                    }
                }
                else
                {
                    lblMsg.Text = "Job Not Found";
                    lblMsg.CssClass = "alert alert-danger";
                }
                sdr.Close();
                con.Close();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string type, concatQuery, imagePath = string.Empty;
                bool isValidToExecute = false;
                if (Request.QueryString["id"] != null)
                {
                    if (fuComponyLogo.HasFile)
                    {
                        if (Utils.IsValidExtention(fuComponyLogo.FileName))
                        {
                            concatQuery = "CompanyImage=@CompanyImage, ";
                        }
                        else
                        {
                            concatQuery = string.Empty;
                        }
                    }
                    else
                    {
                        concatQuery = string.Empty;
                    }
                    query = @"Update Jobs set Title=@Title, NoOfPost=@NoOfPost, Description=@Description, Qualification=@Qualification, Experience=@Experience,
                            Specialization=@Specialization, LastDateTOApply=@LastDateTOApply, Salary=@Salary, JobType=@JobType, CompanyName=@CompanyName,
                            " + concatQuery + "Website=@Website, Email=@Email, Address=@Address, Country=@Country, State=@State where JobId=@id";

                    type = "updated";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Title", txtJobTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@NoOfPost", txtNoOfPost.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text.Trim());
                    cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                    cmd.Parameters.AddWithValue("@Specialization", txtSpecialization.Text.Trim());
                    cmd.Parameters.AddWithValue("@LastDateTOApply", txtLastDate.Text.Trim());
                    cmd.Parameters.AddWithValue("@Salary", txtSalary.Text.Trim());
                    cmd.Parameters.AddWithValue("@JobType", ddlJobType.SelectedValue);
                    cmd.Parameters.AddWithValue("@CompanyName", txtCompany.Text.Trim());
                    cmd.Parameters.AddWithValue("@Website", txtWebsite.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@Country", txtCountry.Text.Trim());
                    cmd.Parameters.AddWithValue("@State", ddlState.SelectedValue);
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["id"].ToString());
                    if (fuComponyLogo.HasFile)
                    {
                        if (Utils.IsValidExtention(fuComponyLogo.FileName))
                        {
                            Guid obj = new Guid();
                            imagePath = "Images/" + obj.ToString() + fuComponyLogo.FileName;
                            fuComponyLogo.PostedFile.SaveAs(Server.MapPath("~/Images/") + obj.ToString() + fuComponyLogo.FileName);
                            cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
                            isValidToExecute = true;
                        }
                        else
                        {
                            lblMsg.Text = "Please select .jpg,.jpeg,.png file for Logo";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Please Select the Image Fie";
                        lblMsg.CssClass = "alert alert-danger";
                        //cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
                        //isValidToExecute = true;
                    }
                }
                else
                {
                    query = @"Insert into Jobs values(@Title,@NoOfPost,@Description,@Qualification,@Experience,@Specialization,@LastDateTOApply,
                        @Salary,@JobType,@CompanyName,@CompanyImage,@Website,@Email,@Address,@Country,@State,@CreateDate)";
                    type = "saved";
                    DateTime time = DateTime.Now;
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Title", txtJobTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@NoOfPost", txtNoOfPost.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text.Trim());
                    cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                    cmd.Parameters.AddWithValue("@Specialization", txtSpecialization.Text.Trim());
                    cmd.Parameters.AddWithValue("@LastDateTOApply", txtLastDate.Text.Trim());
                    cmd.Parameters.AddWithValue("@Salary", txtSalary.Text.Trim());
                    cmd.Parameters.AddWithValue("@JobType", ddlJobType.SelectedValue);
                    cmd.Parameters.AddWithValue("@CompanyName", txtCompany.Text.Trim());
                    cmd.Parameters.AddWithValue("@Website", txtWebsite.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@Country", txtCountry.Text.Trim());
                    cmd.Parameters.AddWithValue("@State", ddlState.SelectedValue);
                    cmd.Parameters.AddWithValue("@CreateDate", time.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (fuComponyLogo.HasFile)
                    {
                        if (Utils.IsValidExtention(fuComponyLogo.FileName))
                        {
                            Guid obj = new Guid();
                            imagePath = "Images/" + obj.ToString() + fuComponyLogo.FileName;
                            fuComponyLogo.PostedFile.SaveAs(Server.MapPath("~/Images/") + obj.ToString() + fuComponyLogo.FileName);
                            cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
                            isValidToExecute = true;
                        }
                        else
                        {
                            lblMsg.Text = "Please select .jpg,.jpeg,.png file for Logo";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Please Select the Image Fie";
                        lblMsg.CssClass = "alert alert-danger";
                        //cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
                        //isValidToExecute = true;
                    }
                }

                if (isValidToExecute)
                {
                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        lblMsg.Text = "Job " + type + " Successful . . !";
                        lblMsg.CssClass = "alert alert-success";
                        clear();
                    }
                    else
                    {
                        lblMsg.Text = "Cannot" + type + "the Records, Please Try After Some Time";
                        lblMsg.CssClass = "alert alert-danger";
                    }
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

        // above we have taken if inside try where if we have requested update data then it will go inside if otherwise in else will continue with
        // adding new job 
        //on update we have mention the 'type' variable so on update its value will be 'updated' and on add its value will be 'saved'
        //when we will execute the query we will show that variable dynamically
        // in if part where we have requested id which is clarly telling we are updating the record we are already have image so we are
        // concatinationg the previous image path otherwise if we dont have any image then we can select new
        // we have created seperate class Util to check extentions


        //private bool IsValidExtention(string fileName)
        //{
        //    bool isValid = false;
        //    string[] filExtentions = { ".jpg", ".jpeg", ".png" };
        //    for (int i = 0; i <= filExtentions.Length - 1; i++)
        //    {
        //        if (fileName.Contains(filExtentions[i]))
        //        {
        //            isValid = true;
        //            break;
        //        }
        //    }
        //    return isValid;
        //}
        // this is checking whether the extention is valid or not

        private void clear()
        {
            txtQualification.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtJobTitle.Text = string.Empty;
            txtExperience.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtCompany.Text = string.Empty;
            txtNoOfPost.Text = string.Empty;
            txtSalary.Text = string.Empty;
            txtSpecialization.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            txtLastDate.Text = string.Empty;
            txtCountry.Text = string.Empty;
            ddlJobType.ClearSelection();
            ddlState.ClearSelection();
        }
    }
}