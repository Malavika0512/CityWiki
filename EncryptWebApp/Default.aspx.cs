using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Encryption;
using EncryptionDecryption;

namespace EncryptWebApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Result.Text))
            {
                Result.Text = "First Encrypt before decrypting";
            }
            else
            {
                try
                {
                    string res = EncryptionDecryption.Class1.Decrypt((String)Result.Text);
                    Note.Text = "The decrypted text is";
                    Result.Text = res;
                }
                catch
                {
                    Result.Text = "Some exception occurred";
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(TextBox1.Text))
            {
                Result.Text = "Enter a valid string";
            }
            else
            {
                try
                {
                    string res = EncryptionDecryption.Class1.Encrypt((string)TextBox1.Text);
                    Note.Text = "The encrypted text is:";
                    Result.Text = res;
                }
                catch
                {
                    Result.Text = "Some exception occurred";
                }
            }
        }
    }
}