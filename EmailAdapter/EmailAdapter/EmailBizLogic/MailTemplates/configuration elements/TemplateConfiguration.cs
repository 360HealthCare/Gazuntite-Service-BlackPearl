using System;
using System.Collections;
using System.Xml.Serialization;


namespace Quivus.Common.MailTemplates.Xml
{
	/// <summary>
	/// Helper class that represents the root
	/// element of an XML declaration document.
	/// </summary>
	[Serializable]
	public class TemplateConfiguration
	{

    #region members

    /// <summary>
    /// The base folder of the templates, if any.
    /// </summary>
    private string baseDirectory;

    /// <summary>
    /// The SMTP server to be used.
    /// </summary>
    private string smtpServer;

    /// <summary>
    /// The available template groups.
    /// </summary>
    private ArrayList templateGroups;

    #endregion


    #region properties

    /// <summary>
    /// The base folder of the templates, if any.
    /// </summary>
    [XmlElement()]
    public string BaseDirectory
    {
      get { return this.baseDirectory; }
      set { this.baseDirectory = value; }
    }

    /// <summary>
    /// The SMTP server to be used.
    /// </summary>
    [XmlElement()]
    public string SmtpServer
    {
      get { return this.smtpServer; }
      set { this.smtpServer = value; }
    }


    /// <summary>
    /// The available template groups.
    /// </summary>
    [XmlArray("Groups")]
    [XmlArrayItem("TemplateGroup", typeof(TemplateGroupElement))]
    public ArrayList TemplateGroups
    {
      get { return this.templateGroups; }
      set { this.templateGroups = value; }
    }

    #endregion


    #region get template group

    /// <summary>
    /// Gets a template group based on the
    /// template ID.
    /// </summary>
    /// <param name="templateId">Alphanumeric group ID.</param>
    /// <returns>A group element that provides access to the
    /// group's templates.</returns>
    /// <exception cref="ArgumentException">Thrown if no
    /// such group exists.</exception>
    public TemplateGroupElement GetGroup(string templateId)
    {
      foreach (TemplateGroupElement group in this.templateGroups)
      {
        if (group.TemplateGroupId.Equals(templateId)) return group;
      }


      string msg = "Unknown template Id: '{0}'";
      msg = String.Format(msg, templateId);
      throw new ArgumentException(msg);
    }

    #endregion

	}
}
