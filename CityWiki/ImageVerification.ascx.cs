using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CityWiki
{
    public partial class ImageVerification : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Image.ImageUrl = "ImageProcessor.aspx";
        }
    }
}