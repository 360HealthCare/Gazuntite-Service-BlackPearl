using System;
using System.Net.Mail;

namespace Quivus.Utility.MailTemplates
{
    /// <summary>
    /// Callback which provides the means to hook into the template
    /// processing. If submitted to the the <see cref="TemplateHandler"/>
    /// controller's <c>Send</c> method, the template plus the generated
    /// <c>MailMessage</c> object is being submitted to the callback before
    /// sending the message to the recipient(s).
    /// </summary>
    public delegate void TemplateCallbackHandler(MailTemplate template, MailMessage message);
}
