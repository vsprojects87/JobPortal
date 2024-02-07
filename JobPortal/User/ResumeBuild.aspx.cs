using System;
using System.Configuration;
using System.Data.SqlClient;

namespace JobPortal.User
{
    public partial class ResumeBuild : System.Web.UI.Page
    {
        static string s = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        SqlConnection con = new SqlConnection(s);
        SqlCommand cmd;
        SqlDataReader sdr;
        string query;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    ShowUserInfo();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void ShowUserInfo()
        {
            try
            {

                string query = "Select * from [User] where UserId=@UserId";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", Request.QueryString["id"]);
                con.Open();
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    if (sdr.Read())
                    {
                        txtUserName.Text = sdr["UserName"].ToString();
                        txtFullName.Text = sdr["Name"].ToString();
                        txtEmail.Text = sdr["Email"].ToString();
                        txtMobile.Text = sdr["Mobile"].ToString();
                        txtTenth.Text = sdr["TenthGrade"].ToString();
                        txtTwelth.Text = sdr["TwelthGrade"].ToString();
                        txtGraduation.Text = sdr["GraduationGrade"].ToString();
                        txtPostGraduation.Text = sdr["PostGraduationGrade"].ToString();
                        txtPhd.Text = sdr["Phd"].ToString();
                        txtWork.Text = sdr["WorksOn"].ToString();
                        txtExperience.Text = sdr["Experience"].ToString();
                        txtAddress.Text = sdr["Address"].ToString();
                        ddlState.SelectedValue = sdr["State"].ToString();
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "User Not Found !";
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["id"] != null)
                {
                    string concatQuery, filePath = string.Empty;
                    //bool isValidToExecute = false;
                    bool IsValid = false;
                    if (fuResume.HasFile)
                    {
                        if (Utils.IsValidExtentionForResume(fuResume.FileName))
                        {
                            concatQuery = "Resume=@Resume,";
                            IsValid = true;
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

                    query = @"Update [user] set Username=@Username,Name=@Name, Email=@Email, Mobile=@Mobile, TenthGrade=@TenthGrade, 
                            TwelthGrade=@TwelthGrade, GraduationGrade=@GraduationGrade, PostGraduationGrade=@PostGraduationGrade,Phd=@Phd,
                            WorksOn=@WorksOn,Experience=@Experience," + concatQuery + "Address=@Address,State=@State where UserId=@UserId";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", txtUserName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Name", txtFullName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@TenthGrade", txtTenth.Text.Trim());
                    cmd.Parameters.AddWithValue("@TwelthGrade", txtTwelth.Text.Trim());
                    cmd.Parameters.AddWithValue("@GraduationGrade", txtGraduation.Text.Trim());
                    cmd.Parameters.AddWithValue("@PostGraduationGrade", txtPostGraduation.Text.Trim());
                    cmd.Parameters.AddWithValue("@Phd", txtPhd.Text.Trim());
                    cmd.Parameters.AddWithValue("@WorksOn", txtWork.Text.Trim());
                    cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@State", ddlState.SelectedValue);
                    cmd.Parameters.AddWithValue("@UserId", Request.QueryString["id"]);
                    if (IsValid)
                    {
                        Guid obj = Guid.NewGuid();
                        filePath = "Resumes/" + obj.ToString() + fuResume.FileName;
                        fuResume.PostedFile.SaveAs(Server.MapPath("~/Resumes/") + obj.ToString() + fuResume.FileName);
                        cmd.Parameters.AddWithValue("@resume", filePath);
                        //isValidToExecute = true;
                    }
                    else
                    {
                        IsValid = true;
                    }

                    if (IsValid)
                    {
                        con.Open();
                        int r = cmd.ExecuteNonQuery();
                        if (r > 0)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Resume Details Successful !";
                            lblMsg.CssClass = "alert alert-success";
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Cannot Update the Record, Try later";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }

                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Cannot Update the Record, Try <b>Relogin</b>";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>" + txtUserName.Text.Trim() + " </b> username already exist";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
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
    }
}