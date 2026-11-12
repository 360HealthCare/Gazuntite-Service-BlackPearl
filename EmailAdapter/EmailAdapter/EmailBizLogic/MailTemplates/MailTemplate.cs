using System;
using System.Globalization;


namespace Quivus.Common.MailTemplates
{
  /// <summary>
  /// Represents a specific template.
  /// </summary>
  public class MailTemplate
  {

    #region members

    /// <summary>
    /// Common ID of all localized templates which
    /// were created for a given subject.
    /// </summary>
    private string templateGroupId;

    /// <summary>
    /// The localization code of the template. In
    /// case it's a default template, this member
    /// references <c>CultureInfo.InvariantCulture</c>.
    /// </summary>
    private CultureInfo locale;

    /// <summary>
    /// The subject of the message.
    /// </summary>
    private string subject;

    /// <summary>
    /// The body of the message.
    /// </summary>
    private string body;

    #endregion


    #region properties

    /// <summary>
    /// Common ID of all localized templates which
    /// were created for a given subject.
    /// </summary>
    public string TemplateGroupId
    {
      get { return this.templateGroupId; }
      set { this.templateGroupId = value; }
    }


    /// <summary>
    /// The localization code of the template. In
    /// case it's a default template, this member
    /// references <c>CultureInfo.InvariantCulture</c>.
    /// </summary>
    public CultureInfo Locale
    {
      get { return this.locale; }
      set { this.locale = value; }
    }


    /// <summary>
    /// The subject of the message.
    /// </summary>
    public string Subject
    {
      get { return this.subject; }
      set { this.subject = value; }
    }


    /// <summary>
    /// The body of the message.
    /// </summary>
    public string Body
    {
      get { return this.body; }
      set { this.body = value; }
    }


    #endregion


    #region initialization

    /// <summary>
    /// Empty default constructor.
    /// </summary>
    public MailTemplate()
    {
    }

    #endregion

  }
}
