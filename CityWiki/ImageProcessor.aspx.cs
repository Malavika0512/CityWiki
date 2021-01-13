using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CityWiki
{
    public partial class ImageProcessor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            ImageVerificationService.ServiceClient client = new ImageVerificationService.ServiceClient();
            string imageString;
            if (Session["ImageString"] == null)
            {
                imageString = client.GetVerifierString("6"); 
                Session["ImageString"] = imageString;
            }
            else
            {
                imageString = Session["ImageString"].ToString();
            }
            Stream imageStream = client.GetImage(imageString);
            System.Drawing.Image image = System.Drawing.Image.FromStream(imageStream);
            Response.ContentType = "image/jpeg";
            image.Save(Response.OutputStream, ImageFormat.Jpeg);
        }
    }
}