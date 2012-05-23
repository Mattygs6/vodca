//-----------------------------------------------------------------------------
// <copyright file="Extensions.AppSettings.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Web.Configuration;
    using System.Web.Hosting;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Get value from Web.config file AppSettings section.
        /// </summary>
        /// <param name="key">AppSetting section key</param>
        /// <returns>Returns value for AppSetting section key or string.Empty if section key is absent</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// <![CDATA[
        /// -------------------------------------------------------
        /// Web.config 
        ///     <appSettings>
        ///         <add key="GoogleAnalyticsKey"  value="UA-5391398-1"/>
        ///     </appSettings>
        /// -------------------------------------------------------
        /// C# source file
        ///     string key = Extensions.GetAppSettingKeyAsString("GoogleAnalyticsKey");
        /// -------------------------------------------------------
        /// ]]>
        /// </code>
        /// </example>
        public static string GetAppSettingKeyAsString(string key)
        {
            return WebConfigurationManager.AppSettings[key] ?? string.Empty;
        }

        /// <summary>
        ///     Get value from Web.config file AppSettings section.
        /// </summary>
        /// <param name="key">AppSetting section key</param>
        /// <returns>Returns value for AppSetting section key</returns>
        /// <example>View code: <br />
        /// <pre>
        /// <![CDATA[
        /// -------------------------------------------------------
        /// Web.config 
        ///     <appSettings>
        ///         <add key="TimeOffset"  value="4"/>
        ///     </appSettings>
        /// -------------------------------------------------------
        /// C# source file
        ///     int? key = Extensions.GetAppSettingKeyAsInt("TimeOffset");
        ///     if(key.HasValue)
        ///     {
        ///         // Do something
        ///     }
        ///     else
        ///     {
        ///         throw new ArgumentException("The 'TimeOffset' key is missing in the web.config");
        ///     }
        /// -------------------------------------------------------
        /// ]]>
        /// </pre>
        /// </example>
        public static int? GetAppSettingKeyAsInt(string key)
        {
            string value = WebConfigurationManager.AppSettings[key];

            return value.ConvertToInt();
        }

        /// <summary>
        ///     Get value from Web.config file AppSettings section.
        /// </summary>
        /// <param name="key">AppSetting section key</param>
        /// <returns>Returns value for AppSetting section key</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// <![CDATA[
        /// -------------------------------------------------------
        /// Web.config
        ///     <appSettings>
        ///         <add key="Offset"  value="4.5"/>
        ///     </appSettings>
        /// -------------------------------------------------------
        /// C# source file
        ///     float? key = Extensions.GetAppSettingKeyAsFloat("Offset");
        ///     if(key.HasValue)
        ///     {
        ///         // Do something
        ///     }
        ///     else
        ///     {
        ///         throw new ArgumentException("The 'Offset' key is missing in the web.config");
        ///     }
        /// -------------------------------------------------------
        /// ]]>
        /// </code>
        /// </example>
        public static float? GetAppSettingKeyAsFloat(string key)
        {
            string value = WebConfigurationManager.AppSettings[key];

            return value.ConvertToFloat();
        }

        /// <summary>
        ///     Get value from Web.config file AppSettings section.
        /// </summary>
        /// <param name="key">AppSetting section key</param>
        /// <returns>Returns value for AppSetting section key</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// <![CDATA[
        /// -------------------------------------------------------
        /// Web.config
        ///     <appSettings>
        ///         <add key="Offset"  value="4.5"/>
        ///     </appSettings>
        /// -------------------------------------------------------
        /// C# source file
        ///     double? key = Extensions.GetAppSettingKeyAsDouble("Offset");
        ///     if(key.HasValue)
        ///     {
        ///         // Do something
        ///     }
        ///     else
        ///     {
        ///         throw new ArgumentException("The 'Offset' key is missing in the web.config");
        ///     }
        /// -------------------------------------------------------
        /// ]]>
        /// </code>
        /// </example>
        public static double? GetAppSettingKeyAsDouble(string key)
        {
            string value = WebConfigurationManager.AppSettings[key];

            return value.ConvertToDouble();
        }

        /// <summary>
        ///     Get value from Web.config file AppSettings section.
        /// </summary>
        /// <param name="key">AppSetting section key</param>
        /// <returns>Returns value for AppSetting section key</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// <![CDATA[
        /// -------------------------------------------------------
        /// Web.config
        ///     <appSettings>
        ///         <add key="MembershipKey"  value="3E925433-6E32-4c95-89D0-E675FF9CED33"/>
        ///     </appSettings>
        /// -------------------------------------------------------
        /// C# source file
        ///     Guid? key = Extensions.GetAppSettingKeyAsGuid("MembershipKey");
        ///     if(key.HasValue)
        ///     {
        ///         // Do something
        ///     }
        ///     else
        ///     {
        ///         throw new ArgumentException("The 'MembershipKey' key is missing in the web.config");
        ///     }
        /// -------------------------------------------------------
        /// ]]> 
        /// </code>
        /// </example>
        public static Guid? GetAppSettingKeyAsGuid(string key)
        {
            string value = WebConfigurationManager.AppSettings[key];

            return value.ConvertToGuid();
        }

        /// <summary>
        ///     Get value from Web.config file AppSettings section.
        /// </summary>
        /// <param name="key">AppSetting section key</param>
        /// <returns>Returns value for AppSetting section key</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// <![CDATA[
        /// -------------------------------------------------------
        /// Web.config
        ///     <appSettings>
        ///         <add key="IsProduction"  value="true"/>
        ///     </appSettings>
        /// -------------------------------------------------------
        /// C# source file
        ///     bool? key = Extensions.GetAppSettingKeyAsGuid("IsProduction");
        ///     if(key.HasValue)
        ///     {
        ///         // Do something
        ///     }
        ///     else
        ///     {
        ///         throw new ArgumentException("The 'IsProduction' key is missing in the web.config");
        ///     }
        /// -------------------------------------------------------
        /// ]]> 
        /// </code>
        /// </example>
        public static bool? GetAppSettingKeyAsBoolean(string key)
        {
            string value = WebConfigurationManager.AppSettings[key];

            return value.ConvertToBoolean();
        }

        /// <summary>
        ///     Get value from Web.config file AppSettings section.
        /// </summary>
        /// <param name="key">AppSetting section key</param>
        /// <returns>Returns value as Physical Folder Path for AppSetting section key</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// <![CDATA[
        /// -------------------------------------------------------
        /// Web.config
        ///     <appSettings>
        ///         <add key="AppSettingsFolder"  value="/App_Settings/"/>
        ///     </appSettings>
        /// -------------------------------------------------------
        /// C# source file
        ///     string folderpath = Extensions.GetAppSettingKeyAsPhysicalFolderPath("AppSettingsFolder");
        ///     // folderpath value equals to "C:\development\WebSite\App_Settings\"
        /// -------------------------------------------------------
        /// ]]> 
        /// </code>
        /// </example>
        public static string GetAppSettingKeyAsPhysicalFolderPath(string key)
        {
            string value = WebConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value))
            {
                throw new VHttpArgumentNullException("The AppSetting Sections doesn't contain key = " + key);
            }

            return HostingEnvironment.MapPath(value);
        }

        /// <summary>
        ///     Get value from Web.config file AppSettings section.
        /// </summary>
        /// <param name="key">AppSetting section key</param>
        /// <returns>Returns value as Physical File Path for AppSetting section key</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// <![CDATA[
        /// -------------------------------------------------------
        /// Web.config
        ///     <appSettings>
        ///         <add key="UrlRewritesAndRedirects"  value="/App_Settings/Redirects301.xml"/>
        ///     </appSettings>
        /// -------------------------------------------------------
        /// C# source file
        ///     string filepath = Extensions.GetAppSettingKeyAsPhysicalFilePath("UrlRewritesAndRedirects");
        ///     // filepath value equals to something like "C:\development\WebSite\App_Settings\Redirects301.xml"
        /// -------------------------------------------------------
        /// ]]> 
        /// </code>
        /// </example>
        public static string GetAppSettingKeyAsPhysicalFilePath(string key)
        {
            string value = WebConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value))
            {
                throw new VHttpArgumentNullException("The AppSettings Sections doesn't contain key = " + key);
            }

            return HostingEnvironment.MapPath(value);
        }
    }
}
