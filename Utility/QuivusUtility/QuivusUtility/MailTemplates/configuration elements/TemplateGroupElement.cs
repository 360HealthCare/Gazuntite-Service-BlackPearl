using System;
using System.Collections;
using System.Xml.Serialization;
using System.Globalization;
using System.IO;
//using System.Web.Mail;

namespace Quivus.Utility.MailTemplates.Xml
{
    /// <summary>
    /// Helper class used to get the declarations
    /// for a template group from the XML configuration
    /// file.
    /// </summary>
    [Serializable]
    public class TemplateGroupElement
    {

        #region members

        /// <summary>
        /// The ID that references the group.
        /// </summary>
        private string templateGroupId;

        /// <summary>
        /// Contains a list of <see cref="TemplateElement"/>
        /// objects that belong to this group.
        /// </summary>
        private ArrayList templates;

        /// <summary>
        /// The sender address used for a mail message.
        /// </summary>
        private string senderAddress;


        /// <summary>
        /// Whether the mail is in text or HTML format.
        /// </summary>
        //   private MailFormat mailFormat = MailFormat.Text;

        #endregion


        #region properties

        /// <summary>
        /// The ID that references the group.
        /// </summary>
        [XmlAttribute()]
        public string TemplateGroupId
        {
            get { return this.templateGroupId; }
            set { this.templateGroupId = value; }
        }


        /// <summary>
        /// The sender address used for a mail message.
        /// </summary>
        [XmlAttribute("Sender")]
        public string SenderAddress
        {
            get { return this.senderAddress; }
            set { this.senderAddress = value; }
        }


        /// <summary>
        /// Whether the mail is in text or HTML format.
        /// </summary>
        //[XmlAttribute("MailFormat")]
        //public MailFormat MailFormat
        //{
        //  get { return this.mailFormat; }
        //  set { this.mailFormat = value; }
        //}


        /// <summary>
        /// Contains a list of <see cref="TemplateElement"/>
        /// objects that belong to this group.
        /// </summary>
        [XmlArray("Templates")]
        [XmlArrayItem("Template", typeof(TemplateElement))]
        public ArrayList Templates
        {
            get { return this.templates; }
            set { this.templates = value; }
        }

        #endregion


        #region get template

        /// <summary>
        /// Gets the template for a given locale.
        /// </summary>
        /// <param name="locale">Requested template locale.</param>
        /// <returns>The appropriate template. If the locale is
        /// unknown, a template with an available parent culture or
        /// - if available - the default template will be used.</returns>
        /// <exception cref="ArgumentException">Thrown if no template
        /// that matches the locale and no default template is
        /// available.</exception>
        public MailTemplate GetTemplate(CultureInfo locale)
        {
            TemplateElement element = FindTemplate(locale);
            if (element == null)
            {
                string msg = "No matching matching template in template group '{0}' found for locale '{1}'";
                msg = String.Format(msg, this.templateGroupId, locale.Name);
                throw new ArgumentException(msg);
            }

            //create a template object
            MailTemplate template = new MailTemplate();
            template.TemplateGroupId = this.templateGroupId;
            template.Locale = element.Locale;
            template.Subject = element.Subject;

            //read the template file if it's the first request
            if (element.Body == null)
            {
                ReadTemplateFile(element);
            }

            template.Body = element.Body;

            return template;
        }



        /// <summary>
        /// Gets the appropriate template for the given locale
        /// or <c>null</c> if no matching localized template and
        /// no default template exist.
        /// </summary>
        /// <param name="locale">Defines the locale of the template.</param>
        /// <returns>A template that matches the locale a close as
        /// possible.</returns>
        private TemplateElement FindTemplate(CultureInfo locale)
        {
            while (true)
            {
                //loop over templates
                //(no performance hit compared to dictionaries for this amount of items)
                foreach (TemplateElement element in this.templates)
                {
                    if (element.Locale.Equals(locale)) return element;
                }

                if (locale != CultureInfo.InvariantCulture)
                {
                    //if the locale is not available, try the locale's parent...
                    locale = locale.Parent;
                }
                else
                {
                    //if we're at the top (invariant culture) and its not available,
                    //we can't go up
                    break;
                }
            }


            //if we arrive here, neither a localized template that matches
            //the submitted locale nor a default template (invariant culture) exists.
            return null;
        }

        #endregion


        #region read template file

        /// <summary>
        /// Reads the template file that was assigned to a template
        /// element and assigns the file's content to the elements
        /// <see cref="TemplateElement.Body"/> property.
        /// </summary>
        /// <param name="element">Requested Template element.</param>
        private void ReadTemplateFile(TemplateElement element)
        {
            using (StreamReader reader = new StreamReader(element.FileName, System.Text.Encoding.Default))
            {
                element.Body = reader.ReadToEnd();
                reader.Close();
            }
        }

        #endregion


    }
}
