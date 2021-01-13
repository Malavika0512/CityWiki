using EncryptionDecryption;
using System;

namespace CityWiki.Member
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            UserRegistrationResult.Text = "";
            LoginService.Service1Client client = new LoginService.Service1Client();
            try
            {
                // ensures username, password and image string textboxes are not empty
                if (!String.IsNullOrWhiteSpace(Username.Text) && !String.IsNullOrWhiteSpace(Password.Text) && !String.IsNullOrWhiteSpace(ImageTextBox.Text))
                {
                    if (!Session["ImageString"].Equals(ImageTextBox.Text))    // Comparing input string and random string
                    {
                        UserRegistrationResult.Text = "<p style='color: red'>String entered doesn't match with the image. Please try Again!</p>";
                    }
                    else
                    {
                        string encryptedPassword = Class1.Encrypt(Password.Text);
                        //string response = client.addUser(UserInput.Text, Cryption.Encrypt(PasswordInput.Text), 2);  // Adding the user by manipulating XML file
                        string registrationResponse = client.addtoXML(Username.Text, encryptedPassword, "Member");
                        if (registrationResponse.Equals("Success"))
                        {
                            //  Error.Text = "User has been registered Successfully!";
                            Session["MemberRegistration"] = "Success";
                            UserRegistrationResult.Text = "<p style = 'color: green'>User Successfully Created.</p>";
                            Response.Redirect("Login.aspx");
                        }
                        else
                        {
                            UserRegistrationResult.Text = registrationResponse;
                        }
                    }

                }
                else
                {
                    // display error if any of the input textbox is empty
                    UserRegistrationResult.Text = "<p style='color: red'>Please enter username, password and image string.</p>";
                }
            }
            catch (Exception ex)
            {
                // display exception occurred error message
                UserRegistrationResult.Text = "<p style='color: red'>Exception Occurred!</p>";
            }
        }

        protected void default_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}