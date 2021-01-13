using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace UserRegistration
{
    public class Service1 : IService1
    {
        public string addtoXML(string username, string password, string filename)
        {
            String path = "";
            if (filename == "Member")
            {
                path = HttpContext.Current.Server.MapPath("~/App_Data/Member.xml");
            }
            else
            {
                path = HttpContext.Current.Server.MapPath("~/App_Data/Staff.xml");
            }

            //Loading the xml file
            XDocument doc = XDocument.Load(path);
            foreach (XElement member in doc.Element(filename + "s").Elements())
            {

                var uname = member.Element("username").Value;
                if (uname == username)
                {
                    return "<p style = 'color: red' > Username already exists</p>";
                }
                else
                {
                    continue;
                }

            }
            //Creating the root element
            XElement root = new XElement(filename);

            //Creating all the child elements to the root element
            root.Add(new XElement("username", username));
            root.Add(new XElement("password", password));

            if (filename == "Member")
            {
                //Adding all the elements to the root <Members> element
                doc.Element("Members").Add(root);
            }
            else
            {
                //Adding all the elements to the root <Staffs> element
                doc.Element("Staffs").Add(root);
            }
            doc.Save(path);
            return "Success";

        }

        public string search(string username, string password, string filename)
        {
            string path = HttpContext.Current.Server.MapPath("~/App_Data/Member.xml");
            if (filename == "Staff")
            {
                path = HttpContext.Current.Server.MapPath("~/App_Data/Staff.xml");
            }

            //Loading the xml file
            XDocument doc = XDocument.Load(path);
            XElement foundEl = doc.Element(filename + "s").Element(filename);
            if (foundEl == null)
            {
                return "<p style = 'color: red'> Username or Password Incorrect</p>";

            }
            try
            {
                foreach (XElement member in doc.Element(filename + "s").Elements())
                {

                    var uname = member.Element("username").Value;
                    var pwd = member.Element("password").Value;
                    if (uname == username && pwd == password)
                    {
                        return "Success";
                    }
                    else
                    {
                        continue;
                    }

                }
                return "<p style = 'color: red'> Username or Password Incorrect</p>";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}
