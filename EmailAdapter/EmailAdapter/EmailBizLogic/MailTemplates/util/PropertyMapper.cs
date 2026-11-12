using System;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Quivus.Common.MailTemplates
{
	/// <summary>
	/// Replaces content placeholders of a given template content
	/// with object properties.
	/// </summary>
	/// <example>
	/// Let's say you have a template that looks like this:
	/// <code>
	/// Hello #?FirstName?# #?LastName?#
	/// 
	/// We have registered you with user name #?UserName?#.
	/// All postings will be sent to this address: #?EmailAddress#?
	/// </code>
	/// 
	/// Now let's assume you have an object with properties that match
	/// the above placeholders.
	/// <code>
	/// public class Account
	/// {
	///   public string FirstName
	///   {
	///     get { ... }
	///   }
	///   
	///   public string LastName
	///   {
	///     get { ... }
	///   }
	///   
	///   [...]
	/// }
	/// </code>
	/// 
	/// In this case, the mapper replaces all matching placeholders with
	/// the corresponding property value. <b>Note:</b>If there are placeholders
	/// that cannot be mapped (no corrsponding property or simply misspelling),
	/// they will be silently ignored. Properties containing <c>null</c> values
	/// are returned as empty strings.
	/// </example>
	public class PropertyMapper
	{

    #region members & properties

    /// <summary>
    /// Business object with properties that match
    /// placeholders content which is parsed.
    /// </summary>
    private object entity;


    /// <summary>
    /// Gets or sets the entity that contains
    /// properties that match placeholders in the
    /// parsed content.
    /// </summary>
    public object Entity
    {
      get { return this.entity; }
      set { this.entity = value; }
    }

    #endregion


    #region intialization

    public PropertyMapper(object entity)
    {
      this.entity = entity;
    }

    #endregion



    /// <summary>
    /// Maps a business object's properties to the placeholders
    /// on a given template's body and subject.
    /// </summary>
    /// <param name="template">Mail template object.</param>
    public void MapTemplate(MailTemplate template)
    {
      template.Body = MapContent(template.Body);
      template.Subject = MapContent(template.Subject);
    }


    /// <summary>
    /// Maps a business object's properties to placeholders
    /// of the submitted content.
    /// </summary>
    /// <param name="content">A string containing placeholder tags. Check
    /// the class summary to get the proper placeholder format.</param>
    /// <returns></returns>
    public string MapContent(string content)
    {
      StringBuilder builder = new StringBuilder(content);
      string pattern = @"#\?(?'property'\S+?)\?#";
      return Regex.Replace(content, pattern, new MatchEvaluator(RegexMatchEvaluation), RegexOptions.ExplicitCapture); 
    }


    /// <summary>
    /// Retrieves the property name from a placeholder and
    /// tries to get a property value from the business
    /// object.
    /// </summary>
    /// <param name="match">Regex match.</param>
    /// <returns>The property value, if existing. If the property
    /// is <c>null</c>, <c>String.Empty</c> is being returned. If
    /// no property is available, the unchanged placeholder is
    /// returned.</returns>
    private string RegexMatchEvaluation(Match match)
    {
      //get the property name (named group of the regex)
      string propertyName = match.Groups["property"].Value;
      
      //try to get a property handle from the business object
      PropertyInfo pi = this.entity.GetType().GetProperty(propertyName);
      
      //do not replace anything if no such property exists
      if (pi == null) return match.Value;

      //return the property value
      object propertyValue = pi.GetValue(entity, null);
      if (propertyValue != null)
      {
        return propertyValue.ToString();
      }
      else
      {
        return "";
      }
    }

	}
}
