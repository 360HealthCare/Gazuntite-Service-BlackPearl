using System;
using System.Configuration;
using System.Globalization;
using System.Net.Mail;

using Quivus.Common.MailTemplates.Xml;
using System.Net;

namespace Quivus.Common.MailTemplates
{
	/// <summary>
	/// Simple controller class that provides access to the
	/// available templates.
	/// </summary>
	public class TemplateHandler
	{

    #region members

    /// <summary>
    /// The template settings of the application.
    /// </summary>
    TemplateConfiguration configuration;

    #endregion


    #region singleton

    /// <summary>
    /// Singleton instance of this controller class.
    /// </summary>
    private static TemplateHandler instance;

    /// <summary>
    /// Gets the singleton instance of this controller class.
    /// </summary>
    public static TemplateHandler Instance
    {
      get { return instance; }
    }

    #endregion


    #region initialization

    /// <summary>
    /// Static constructor, creates the Singleton instance.
    /// </summary>
    static TemplateHandler()
    {
      instance = new TemplateHandler();
    }

    /// <summary>
    /// Inits the controller by parsing the template configuration file.
    /// </summary>
		private TemplateHandler()
		{
      //load the configuration file...
      string configFile = ConfigurationSettings.AppSettings["Evolve.TemplateConfiguration"];      
      configuration = ConfigFileParser.ParseConfiguration(configFile);
		}

    #endregion


    #region indexers

    /// <summary>
    /// Gets a localized template of a given template group. If the
    /// locale is unknown, a template that matches the locale's parent
    /// or a default template is being returned.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown in case of an invalid
    /// template ID or if no locale matches the submitted culture and
    /// no default template is available.</exception>
    public MailTemplate this[string templateId, CultureInfo locale]
    {
      get
      {
        TemplateGroupElement group = configuration.GetGroup(templateId);
        return group.GetTemplate(locale);
      }
    }


    /// <summary>
    /// Gets the default template of a given template group.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown in case of an invalid
    /// template ID or if no default template is available.</exception>
    public MailTemplate this[string templateId]
    {
      get
      {
        return this[templateId, CultureInfo.InvariantCulture];
      }
    }

    #endregion


    #region Send overloads

    /// <summary>
    /// Sends the template to a given recipient.
    /// </summary>
    /// <param name="template">Mail template containing the recipient's message.</param>
    /// <param name="recipient">Single recipient of the message.</param>
    public void Send(MailTemplate template, string recipient)
    {
      //call overload
      Send(template, new string[] { recipient }, null, null, null); 
    }


    /// <summary>
    /// Sends the template to a number of recipients.
    /// </summary>
    /// <param name="template">Mail template containing the recipients' message.</param>
    /// <param name="recipients">Recipients of the message.</param>
    public void Send(MailTemplate template, string[] recipients)
    {
      //call overload
      Send(template, recipients, null, null, null);
    }


    /// <summary>
    /// Sends the template to a number of recipients.
    /// </summary>
    /// <param name="template">Mail template containing the recipients' message.</param>
    /// <param name="to">Recipients of the message.</param>
    /// <param name="cc">Recipients of a carbon copy.</param>
    /// <param name="bcc">Recipients of a blind carbon copy.</param>
    public void Send(MailTemplate template, string[] to, string[] cc, string[] bcc)
    {
      //call overload
      Send(template, to, cc, bcc, null);
    }


    /// <summary>
    /// Sends the template to a given recipient.
    /// </summary>
    /// <param name="template">Mail template containing the recipient's message.</param>
    /// <param name="recipient">Single recipient of the message.</param>
    /// <param name="callback">A callback handler which is being called before the
    /// message is being sent to the recipient(s).</param>
    public void Send(MailTemplate template, string recipient, TemplateCallbackHandler callback)
    {
      //call overload
      Send(template, new string[] {recipient}, null, null, callback);
    }


    /// <summary>
    /// Sends the template to a number of recipients.
    /// </summary>
    /// <param name="template">Mail template containing the recipients' message.</param>
    /// <param name="to">Recipients of the message.</param>
    /// <param name="cc">Recipients of a carbon copy.</param>
    /// <param name="bcc">Recipients of a blind carbon copy.</param>
    /// <param name="callback">A callback handler which is being called before the
    /// message is being sent to the recipient(s).</param>
    public void Send(MailTemplate template, string[] to, string[] cc, string[] bcc,  TemplateCallbackHandler callback)
    {

      //get the group
      TemplateGroupElement group = configuration.GetGroup(template.TemplateGroupId);
      
      //create a new message
      MailMessage msg = new MailMessage();
      msg.From = new MailAddress(group.SenderAddress);
      msg.Body = template.Body;
      msg.Subject = template.Subject;
      //msg.BodyFormat = group.MailFormat;
       
      //msg.To = CreateRecipientList(to);
      msg.To.Add(CreateRecipientList(to));
      if ( cc!=null && cc.Length>0)
      {
          msg.CC.Add(CreateRecipientList(cc));
      }
      if (bcc != null && bcc.Length > 0)
      {
          msg.Bcc.Add(CreateRecipientList(bcc));
      }

      //set SMTP server, if defined
      string smtp = configuration.SmtpServer;

        //Replaced Code to Send Email
      
      SmtpClient client = new SmtpClient(smtp);
      client.EnableSsl = false;
      client.Port = 25;
      client.UseDefaultCredentials = true;
      client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
      NetworkCredential loginInfo = new NetworkCredential("support@quivus.com", "123456");
      client.Credentials = loginInfo;
        
      
      client.Send(msg);

        //Code below is commented by Waqas Baber to use System.Net 
      //if (smtp != null && !smtp.Equals(String.Empty))
      //{
      //  SmtpMail.SmtpServer = smtp;
      //}

      ////submit message to callback handler, if available
      //if (callback != null)
      //{
      //  callback(template, msg);
      //}

      ////send message
      //SmtpMail.Send(msg);
        //Waqas Baber Commits end here, replaced code is above
    }


    /// <summary>
    /// Creates a list of recipients, divided by semicolons or <c>null</c>
    /// in case of an empty list.
    /// </summary>
    /// <param name="recipients">Array of email addresses.</param>
    private string CreateRecipientList(string[] recipients)
    {
      if (recipients == null || recipients.Length == 0) return null;

      System.Text.StringBuilder builder = new System.Text.StringBuilder();
      for (int i=0; i<recipients.Length; i++)
      {
        if (i != 0) builder.Append(";");
        builder.Append(recipients[i]);
      }

      return builder.ToString();
    }



    
    #endregion


	}
}
