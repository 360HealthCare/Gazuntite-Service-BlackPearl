using System;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

using System.Web;


namespace Quivus.Common.MailTemplates.Xml
{
  /// <summary>
  /// Parses XML configuration files containing
  /// template settings of the running application.
  /// </summary>
  public class ConfigFileParser
  {

    /// <summary>
    /// Parses the XML configuration file that contains the template
    /// declarations.
    /// </summary>
    /// <param name="configFile">Path of the XML configuraton file.</param>
    public static TemplateConfiguration ParseConfiguration(string configFile)
    {
      if (!Path.IsPathRooted(configFile) && IsWebApplication())
      {
        configFile = HttpContext.Current.Server.MapPath(configFile);
       
        
          
      }
      if (!Path.IsPathRooted(configFile) && !IsWebApplication())//Code added by Waqas Baber
      {
          string apPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
         // apPath=System.Web.Hosting.HostingEnvironment.
          //throw new ArgumentException("Waqas Baber's Hit" + configFile);
          configFile = Path.Combine(apPath, configFile);
         
      }//Waqas Baber's code ends here

      //validate file path
      if (!File.Exists(configFile))
      {
        string msg = "No template configuration file found at '{0}'. Check your settings";
        msg = String.Format(msg, new FileInfo(configFile).FullName);
        throw new ArgumentException(msg);
      }


      //perform deserialization
      XmlSerializer serializer = new XmlSerializer(typeof(TemplateConfiguration));
      using (FileStream stream = File.OpenRead(configFile))
      {
        //init configuration object
        TemplateConfiguration configuration;
        configuration = serializer.Deserialize(stream) as TemplateConfiguration;
    
         
          ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////-p-
        //validate the base folder, if set
        string baseDirectory = configuration.BaseDirectory;
        if (baseDirectory != null && !baseDirectory.Equals(String.Empty))
        {
          configuration.BaseDirectory = GetBaseDirectory(baseDirectory).FullName;
        }


        //check the template files
        foreach (TemplateGroupElement group in configuration.TemplateGroups)
        {
          foreach (TemplateElement template in group.Templates)
          {
            FileInfo file = GetFile(configuration.BaseDirectory, template.FileName);
            template.FileName = file.FullName;
          }
        }

        return configuration;
      }
    }


    #region base folder handling

    /// <summary>
    /// Validates the base folder, if any.
    /// </summary>
    /// <param name="baseDirectory">The base folder declaration to be checked.</param>
    /// <returns>A <c>DirectoryInfo</c> pointing to the specified base folder or
    /// <c>null</c> of no base was specified.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid folder
    /// was specified.</exception>
    private static DirectoryInfo GetBaseDirectory(string baseDirectory)
    {

      //get the qualified path for web applications
        if (IsWebApplication())
        {
            baseDirectory = HttpContext.Current.Server.MapPath(baseDirectory);

        }
        else//code added by Waqas Baber here
        {
            string apPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            // apPath=System.Web.Hosting.HostingEnvironment.
            //throw new ArgumentException("Waqas Baber's Hit" + configFile);
            baseDirectory = Path.Combine(apPath, baseDirectory);
        }//Waqas Baber's code ends here
        

      //get base path, if any
      DirectoryInfo baseFolder = new DirectoryInfo(baseDirectory);
      if (!baseFolder.Exists)
      {
        string msg = "Template base directory '{0}' does not exist. Check your settings.";
        msg = String.Format(msg, baseFolder.FullName);
        throw new ArgumentException(msg);
      }

      //return base directory
      return baseFolder;

    }

    #endregion


    #region template file path handling

    /// <summary>
    /// Parses the file path depending on absolute / relative
    /// paths and the optional base path.
    /// </summary>
    /// <param name="baseFolder">Base folder for all templates.</param>
    /// <param name="templatePath">The path of the template. Might
    /// be absolute or relative.</param>
    /// <returns>Returns a <c>FileInfo</c> that points to the path
    /// that was parsed depending on the submitted values.</returns>
    /// <exception cref="ArgumentException">Thrown if the specified
    /// file doesn't exist.</exception>
    private static FileInfo GetFile(string baseFolder, string templatePath)
    {
      FileInfo file;

      if (Path.IsPathRooted(templatePath))
      {
        //ignore base path
        file = new FileInfo(templatePath);
      }
      else
      {
        if (baseFolder != null && !baseFolder.Equals(String.Empty))
        {
          //combine base and relative template path
          string path = Path.Combine(baseFolder, templatePath);
          file = new FileInfo(path);
        }
        else
        {
          //get the qualified path for web applications
          if (IsWebApplication())
          {
            templatePath = HttpContext.Current.Server.MapPath(templatePath);
          }

          //just create a file based on the relative path
          file = new FileInfo(templatePath);
        }
      }

      if (!file.Exists)
      {
        string msg = "Template file not found at '{1}'. Check your mail template settings.";
        msg = String.Format(msg, file.FullName);

        throw new ArgumentException(msg);
      }

      return file;
    }

    #endregion


    /// <summary>
    /// Determines whether it's an ASP.net application or not.
    /// </summary>
    /// <returns><c>true</c> if the class is running within
    /// a web application.</returns>
    private static bool IsWebApplication()
    {
      return (System.Web.HttpContext.Current != null);
    }


  }
}
