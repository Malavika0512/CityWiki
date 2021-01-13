using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CityWiki
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Logout.Visible = false;
            Session["MemberRegistration"] = "";
            Session["StaffRegistration"] = "";
            ApplicationStartTime.Text = "Application was last used at : " + Application["StartTime"];
            AccessRequest.Text = "Number of Access Requests : " + (int)Application["AccessRequest"];

            HttpCookie memberCookies = Request.Cookies["MemberCookie"];
            HttpCookie staffCookies = Request.Cookies["StaffCookie"];
            if (memberCookies != null) {
                Session["Role"] = "2";
                Session["Username"] = memberCookies["Name"];
            }
            if(Session["Role"] != null && Session["Role"].ToString() == "2")
            {
                //Response.Redirect("Member/Member.aspx");
                welcome.Text = "Logged in as Member : " + Session["Username"].ToString();
                memberRegistration.Visible = false;
                memberLogin.Text = "Member Services";
                staffLogin.Text = "Staff Services";
                Logout.Visible = true;

            }
            else if (staffCookies != null)
            {
                Session["Role"] = "3";
                Session["Username"] = staffCookies["Name"];
            }
            if (Session["Role"] != null && Session["Role"].ToString() == "3")
            {
                //Response.Redirect("Staff/StaffPage.aspx");
                welcome.Text = "Logged in as Staff : " + Session["Username"].ToString();
                memberRegistration.Visible = false;
                memberLogin.Text = "Member Services";
                staffLogin.Text = "Staff Services";
                Logout.Visible = true;
            }
        }

        protected void memberLogin_Click(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookies = Request.Cookies["MemberCookie"];
                HttpCookie cookie = Request.Cookies["StaffCookie"];
                if ((cookie != null) && (Session["Role"].ToString() == "3"))
                {
                    Response.Redirect("Member/UnauthorizedUser.aspx");
                }
                else if (cookies == null || Session["Role"] == null || Session["Role"].ToString() != "2")
                {
                    Response.Redirect("Member/Login.aspx");
                }
                else if (Session["Role"].ToString() == "2")
                {
                    Response.Redirect("Member/Member.aspx");
                }
            }
            catch (Exception ex)
            {
                Result.Text = ex.Message;
            }
        }

        protected void memberRegistration_Click(object sender, EventArgs e)
        {
            Response.Redirect("Member/Register.aspx");
        }

        protected void staffLogin_Click(object sender, EventArgs e)
        {
            try
            {

                HttpCookie myCookies = Request.Cookies["StaffCookie"];
                HttpCookie cookie = Request.Cookies["MemberCookie"];
                if ((cookie != null) && (Session["Role"].ToString() == "2"))
                {
                    Response.Redirect("Staff/UnauthorizedUser.aspx");
                }

                else if ((myCookies == null) || (Session["Role"].ToString() != "3"))
                {
                    Response.Redirect("Staff/Login.aspx");
                }
                
                else if (Session["Role"].ToString() == "3")
                {
                    Response.Redirect("Staff/StaffPage.aspx");
                }

            }
            catch (Exception ex)
            {
                Result.Text = ex.Message;
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookies = new HttpCookie("StaffCookie");
                if(cookies != null)
                {
                    cookies.Expires = DateTime.Now.AddMonths(-6);
                    Response.Cookies.Add(cookies);
                }
                cookies = new HttpCookie("MemberCookie");
                if (cookies != null)
                {
                    cookies.Expires = DateTime.Now.AddMonths(-6);
                    Response.Cookies.Add(cookies);
                }
                Session["Username"] = null;
                Session["Role"] = null;
                Response.Redirect("Default.aspx");

            }
            catch (Exception ex)
            {
                Result.Text = ex.Message;
            }
        }
    }
}