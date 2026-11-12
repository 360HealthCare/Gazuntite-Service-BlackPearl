using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.IO;

namespace QuivusManagerService.CustomeControls
{
    public static class TempCtrlUtil
    {
        /// <summary>
        /// Provides an overload for web controls to return the html representation of the final rendered output in string format.
        /// </summary>
        public static string RenderControl(Control control)
        {
            StringBuilder sb = new StringBuilder();

            using (StringWriter sw = new StringWriter(sb))
            using (HtmlTextWriter tw = new HtmlTextWriter(sw))
                control.RenderControl(tw);

            return sb.ToString();
        }

        public static string createHtmlPage(string fileName,string content, string logoFileName)
        {
            string directory = @"D:\SoapNote\Staging\";
            string address = @"http://www.quivus.com/soapNoteStaging/" + fileName;
            try
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                StreamWriter SW;
                SW = File.CreateText(directory+fileName);
                string logoPrintScript = "<script language='JavaScript'>document.getElementById('ctl00_header_logo').setAttribute(\"src\", \"Images/" + logoFileName + "\");</script>";
                SW.Write(start + content + logoPrintScript + end);
                SW.Close();

            }
            catch
            {
                throw;
            }
            return address;
        }

        public static string createHtmlPage(string content)
        {
            return start + content + end;
        }

        private static string start = "<html>\r\n<head>\r\n<title>SOAP NOTE</title>\r\n<meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\">\r\n<script language='JavaScript'>\r\n\t document.write(\"<div id='non-printable'><a href='#' target='_blank'><img src='Images/printbutton.gif' alt='Print' /></a></div>\");\r\n window.onload = function()\r\n{\r\n\tif (window.print)\r\n\t{\r\n\t\twindow.print();\r\n\t\tdocument.getElementById('print').style.display='none';\r\n\t}\r\n\t}\r\n</script>\r\n</head>\r\n<body>\r\n<div id=\"main\">\r\n";
        private static string end = "</div>\r\n</body>\r\n</html>";
            
    }
}