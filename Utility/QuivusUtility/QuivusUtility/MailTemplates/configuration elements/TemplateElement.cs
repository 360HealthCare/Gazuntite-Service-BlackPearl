using System;
using System.Xml.Serialization;
using System.Globalization;

namespace Quivus.Utility.MailTemplates.Xml
{
    /// <summary>
    /// Helper class used to get the declaration for
    /// a given template from the XML configuration file.
    /// </summary>
    [Serializable]
    public class TemplateElement
    {

        #region members

        /// <summary>
        /// Defines how the template is localized.
        /// </summary>
        private CultureInfo locale = null;

        /// <summary>
        /// The subject of the template message.
        /// </summary>
        private string subject;

        /// <summary>
        /// The file name of the template. May be
        /// relative or absolute.
        /// </summary>
        private string fileName;

        /// <summary>
        /// Whether this template should be used if
        /// an template in a language that is not
        /// available is requested.
        /// </summary>
        private bool isDefault = false;

        /// <summary>
        /// The contents of the template file. Defaults
        /// to <c>null</c> while not set.
        /// </summary>
        private string body = null;

        #endregion


        #region properties

        /// <summary>
        /// Defines how the template is localized.
        /// </summary>
        [XmlIgnore()]
        public CultureInfo Locale
        {
            get { return this.locale; }
        }


        /// <summary>
        /// Internal property, used for deserialization purpose
        /// only.
        /// </summary>
        [XmlAttribute("Locale")]
        public string CultureName
        {
            get { return this.locale.Name; }
            set { if (!isDefault) locale = new CultureInfo(value); }
        }


        /// <summary>
        /// The subject of the template message.
        /// </summary>
        [XmlElement()]
        public string Subject
        {
            get { return this.subject; }
            set { this.subject = value; }
        }


        /// <summary>
        /// The file name of the template. May be
        /// relative or absolute.
        /// </summary>
        [XmlElement("File")]
        public string FileName
        {
            get { return this.fileName; }
            set { this.fileName = value; }
        }


        /// <summary>
        /// Whether this template should be used if
        /// an template in a language that is not
        /// available is requested.
        /// </summary>
        [XmlAttribute()]
        public bool IsDefault
        {
            get { return this.isDefault; }
            set
            {
                this.isDefault = value;
                //if it's the default culture, set the
                //culture to invariant
                if (value)
                {
                    locale = CultureInfo.InvariantCulture;
                }
            }
        }


        /// <summary>
        /// The contents of the template file. Defaults
        /// to <c>null</c> while not set.
        /// </summary>
        [XmlIgnore()]
        public string Body
        {
            get { return this.body; }
            set { this.body = value; }
        }

        #endregion

    }
}
