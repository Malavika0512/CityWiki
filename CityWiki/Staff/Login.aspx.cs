using Encryption;
using System;
using System.Web;

namespace CityWiki.Staff
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session != null && Session["StaffRegistration"] != null && Session["StaffRegistration"].Equals("Success"))
            {
                Label1.Text = "Staff successfully registered! Please login to access your account.";
                Session["StaffRegistration"] = "";
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                LoginService.Service1Client client = new LoginService.Service1Client();
                Result.Text = "";
                if (!String.IsNullOrWhiteSpace(Username.Text) && !String.IsNullOrWhiteSpace(Password.Text))
                {
                    string encryptedPassword = Class1.Encrypt(Password.Text);
                    //string loginResponse = client.searchUser(username_input.Text, Cryption.Encrypt(passsword_input.Text), 2);  // Check whether user and password are correct by searching in users.xml file
                    string loginResponse = client.search(Username.Text, encryptedPassword, "Staff");
                    if (loginResponse.Equals("Success"))
                    {
                        HttpCookie cookies = new HttpCookie("MemberCookie");  // Clearing the cookies if required 
                        cookies.Expires = DateTime.Now.AddMonths(-6);
                        Response.Cookies.Add(cookies);

                        cookies = new HttpCookie("StaffCookie");
                        cookies["Name"] = Username.Text;            // Store username and password in cookies
                        cookies["Password"] = encryptedPassword;
                        cookies.Expires = DateTime.Now.AddMonths(6);
                        Response.Cookies.Add(cookies);
                        Session["Role"] = "3";
                        Session["Username"] = Username.Text;   // Storing username is session so that it could be used in welcome page                      
                        Response.Redirect("StaffPage.aspx");
                    }
                    else
                    {
                        Result.Text = loginResponse;
                    }
                }
                else
                {
                    // display error if any of the input textbox is empty
                    Result.Text = "<p style='color: red'>Please enter both username and password.</p>";
                }
            }
            catch (Exception e1)
            {
                Result.Text = e1.Message;
            }
        }

        protected void default_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}