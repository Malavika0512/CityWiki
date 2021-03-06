﻿using Encryption;
using System;
using System.Web;
using System.Web.UI.WebControls;

namespace CityWiki.Member
{
    public partial class UnauthorizedUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookies = new HttpCookie("StaffCookie");
                cookies.Expires = DateTime.Now.AddMonths(-6);
                Response.Cookies.Add(cookies);
                Session["Username"] = null;
                Session["Role"] = null;
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                Label.Text = ex.Message;
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            LoginService.Service1Client client = new LoginService.Service1Client();


            Result.Text = "";
            try
            {
                // ensures username and password textboxes are not empty
                if (!String.IsNullOrWhiteSpace(Username.Text) && !String.IsNullOrWhiteSpace(Password.Text))
                {
                    string encryptedPassword = Class1.Encrypt(Password.Text);
                        // display rsponse returned by the service
                        string loginResponse = client.search(Username.Text, encryptedPassword, "Member");

                        if (loginResponse.Equals("Success"))
                        {
                            HttpCookie cookies = new HttpCookie("StaffCookie");  // Clearing the cookies if required 
                            cookies.Expires = DateTime.Now.AddMonths(-6);
                            Response.Cookies.Add(cookies);

                            //HttpCookie mycookies = new HttpCookie("MemberCookieId");
                            cookies = new HttpCookie("MemberCookie");
                            cookies["Name"] = Username.Text;            // Store username and password in cookies
                            cookies["Password"] = encryptedPassword;
                            cookies.Expires = DateTime.Now.AddMonths(6);
                            Response.Cookies.Add(cookies);
                            Session["Username"] = Username.Text;   // Storing username is session so that it could be used in welcome page
                            Session["Role"] = "2";
                            Response.Redirect("Member.aspx");
                        }
                        Result.Text = loginResponse;
                }
                else
                {
                    // display error if any of the input textbox is empty
                    Result.Text = "<p style='color: red'>Please enter both username and password.</p>";
                }
            }
            catch (Exception ex)
            {
                // display exception occurred error message
                Result.Text = "<p style='color: red'>Exception Occurred!</p>";
            }

        }

        protected void Home_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}