using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string concatQuery, imagePath = string.Empty;
                bool isValidToExecute = false;

                //if (fuComponyLogo.HasFile)
                //{
                //    if (IsValidExtensions(fuComponyLogo.FileName))
                //    {
                //        concatQuery = "";
                //    }
                //    else
                //    {

                //    }
                //}
                //else
                //{

                //}

                query = @"Insert into Jobs values(@Title,@NoOfPost,@Description,@Qualification,@Experience,@Sepcialization,@LastDateTOApply,
                        @Salary,@JobType,@CompanyName,@CompanyImage,@Website,@Email,@Address,@Country,@State,@CreateDate)";
                DateTime time = DateTime.Now;
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Title", txtJobTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@NoOfPost", txtNoOfPost.Text.Trim());
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text.Trim());
                cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                cmd.Parameters.AddWithValue("@Sepcialization", txtSpecialization.Text.Trim());
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
                    if (IsValidExtention(fuComponyLogo.FileName))
                    {
                        Guid obj = new Guid();
                        imagePath = "Images/" + obj.ToString() + fuComponyLogo.FileName;
                        fuComponyLogo.PostedFile.SaveAs(Server.MapPath("~/Images/")+obj.ToString()+fuComponyLogo.FileName);
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
                if (isValidToExecute)
                {
                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        lblMsg.Text = "Job Saved Successful . . !";
                        lblMsg.CssClass = "alert alert-success";
                        clear();
                    }
                    else
                    {
                        lblMsg.Text = "Cannot Save the Records, Please Try After Some Time";
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

        private bool IsValidExtention(string fileName)
        {
            bool isValid = false;
            string[] filExtentions = { ".jpg", ".jpeg", ".png" };
            for (int i = 0; i <= filExtentions.Length - 1; i++)
            {
                if (fileName.Contains(filExtentions[i]))
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }


        private void clear()
        {
            txtQualification.Text = string.Empty;
            txtAddress.Text= string.Empty;
            txtJobTitle.Text= string.Empty;
            txtExperience.Text= string.Empty;
            txtDescription.Text= string.Empty;
            txtEmail.Text= string.Empty;    
            txtCompany.Text= string.Empty;
            txtNoOfPost.Text= string.Empty;
            txtSalary.Text= string.Empty;
            txtSpecialization.Text= string.Empty;
            txtWebsite.Text= string.Empty;
            txtLastDate.Text= string.Empty;
            txtCountry.Text= string.Empty;  
            ddlJobType.ClearSelection();
            ddlState.ClearSelection();
        }
    }
}