//-----------------------------------------------------------------------------
// <copyright file="VWebConfig.AppSettings.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/24/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Web.Configuration;
    using System.Web.Hosting;

    /// <summary>
    ///     Contains common WebConfigurationManager extension methods
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Configuration\VWebConfig.AppSettings.cs" title="VWebConfig.AppSettings.cs" lang="C#" />
    /// </example>
    public static partial class VWebConfig
    {
        /// <summary>
        ///     Web.config file AppSetting section methods to retrieve the key/value pairs
        /// </summary>
        /// <example>View code: <br />
        /// <code title="web.config" lang="xml">
        /// <![CDATA[
        ///     <appSettings>
        ///         <add key="GoogleAnalyticsKey" value="UA-5391398-1"/>
        ///         <add key="TimeOffset" value="4"/>
        ///         <add key="UrlRewritesAndRedirects" value="/App_Settings/Redirects301.xml"/>
        ///     </appSettings>
        /// ]]>
        /// </code>
        /// <code title=" C# source file" lang="C#">
        ///     string key = VWebConfig.AppSettings.GetKeyAsString("GoogleAnalyticsKey");
        ///     string filepath = VWebConfig.AppSettings.GetKeyAsPhysicalFilePath("UrlRewritesAndRedirects");
        ///     // filepath value equals to "C:\development\WebSite\App_Settings\Redirects301.xml"  
        ///     int? key = VWebConfig.AppSettings.GetKeyAsInt("TimeOffset");
        ///     if(key.HasValue)
        ///     {
        ///         // Do something
        ///     }
        ///     else
        ///     {
        ///         throw new ArgumentException("The 'TimeOffset' key is missing in the web.config");
        ///     }  
        /// </code>
        /// </example>
        public static class AppSettings
        {
            /// <summary>
            ///     Get value from Web.config file AppSettings section.
            /// </summary>
            /// <param name="key">AppSetting section key</param>
            /// <returns>Returns value for AppSetting section key or string.Empty if section key is absent</returns>
            /// <example>View code: <br />
            /// <code title="web.config" lang="xml">
            ///     <appSettings>
            ///         <add key="GoogleAnalyticsKey"  value="UA-5391398-1"/>
            ///     </appSettings>
            /// </code>
            /// <code title="C# source file" lang="C#">
            ///     string key = VWebConfig.AppSettings.GetKeyAsString("GoogleAnalyticsKey");
            /// </code>
            /// </example>
            public static string GetKeyAsString(string key)
            {
                string value = WebConfigurationManager.AppSettings[key];
                return string.IsNullOrWhiteSpace(value) ? string.Empty : value;
            }

            /// <summary>
            ///     Get value from Web.config file AppSettings section.
            /// </summary>
            /// <param name="key">AppSetting section key</param>
            /// <returns>Returns value for AppSetting section key</returns>
            /// <example>View code: <br />
            ///  <code title="web.config" lang="xml">
            ///     <appSettings>
            ///         <add key="TimeOffset"  value="4"/>
            ///     </appSettings>
            /// </code>
            /// <code title="C# source file" lang="C#">
            ///     int? key = VWebConfig.AppSettings.GetKeyAsInt("TimeOffset");
            ///     if(key.HasValue)
            ///     {
            ///         // Do something
            ///     }
            ///     else
            ///     {
            ///         throw new ArgumentException("The 'TimeOffset' key is missing in the web.config");
            ///     }
            /// </code>
            /// </example>
            public static int? GetKeyAsInt(string key)
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
            ///  <code title="web.config" lang="xml">
            ///     <appSettings>
            ///         <add key="Offset"  value="4.5"/>
            ///     </appSettings>
            /// </code>
            /// <code title="C# source file" lang="C#">
            ///     float? key = VWebConfig.AppSettings.GetKeyAsFloat("Offset");
            ///     if(key.HasValue)
            ///     {
            ///         // Do something
            ///     }
            ///     else
            ///     {
            ///         throw new ArgumentException("The 'Offset' key is missing in the web.config");
            ///     }
            /// </code>
            /// </example>
            public static float? GetKeyAsFloat(string key)
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
            ///  <code title="web.config" lang="xml">
            ///     <appSettings>
            ///         <add key="Offset"  value="4.5"/>
            ///     </appSettings>
            /// </code>
            /// <code title="C# source file" lang="C#">
            ///     double? key = VWebConfig.AppSettings.GetKeyAsDouble("Offset");
            ///     if(key.HasValue)
            ///     {
            ///         // Do something
            ///     }
            ///     else
            ///     {
            ///         throw new ArgumentException("The 'Offset' key is missing in the web.config");
            ///     }
            /// </code>
            /// </example>
            public static double? GetKeyAsDouble(string key)
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
            ///  <code title="web.config" lang="xml">
            ///     <appSettings>
            ///         <add key="MembershipKey"  value="3E925433-6E32-4c95-89D0-E675FF9CED33"/>
            ///     </appSettings>
            /// </code>
            /// <code title="C# source file" lang="C#">
            ///     Guid? key = VWebConfig.AppSettings.GetKeyAsGuid("MembershipKey");
            ///     if(key.HasValue)
            ///     {
            ///         // Do something
            ///     }
            ///     else
            ///     {
            ///         throw new ArgumentException("The 'MembershipKey' key is missing in the web.config");
            ///     }
            /// </code>
            /// </example>
            public static Guid? GetKeyAsGuid(string key)
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
            ///  <code title="web.config" lang="xml">
            ///     <appSettings>
            ///         <add key="IsProduction"  value="true"/>
            ///     </appSettings>
            /// </code>
            /// <code title="C# source file" lang="C#">
            ///     bool? key = VWebConfig.AppSettings.GetKeyAsGuid("IsProduction");
            ///     if(key.HasValue)
            ///     {
            ///         // Do something
            ///     }
            ///     else
            ///     {
            ///         throw new ArgumentException("The 'IsProduction' key is missing in the web.config");
            ///     }
            /// </code>
            /// </example>
            public static bool? GetKeyAsBoolean(string key)
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
            /// <code title="web.config" lang="xml">
            ///     <appSettings>
            ///         <add key="AppSettingsFolder"  value="/App_Settings/"/>
            ///     </appSettings>
            /// </code>
            /// <code title="C# source file" lang="C#">
            ///     string folderpath = VWebConfig.AppSettings.GetKeyAsPhysicalFolderPath("AppSettingsFolder");
            ///     // folderpath value equals to something like "C:\development\WebSite\App_Settings\"
            /// </code>
            /// </example>
            public static string GetKeyAsPhysicalFolderPath(string key)
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
            ///  <code title="web.config" lang="xml">
            ///     <appSettings>
            ///         <add key="UrlRewritesAndRedirects"  value="/App_Settings/Redirects301.xml"/>
            ///     </appSettings>
            /// </code>
            /// <code title="C# source file" lang="C#">
            ///     string filepath = VWebConfig.AppSettings.GetKeyAsPhysicalFilePath("UrlRewritesAndRedirects");
            ///     // filepath value equals to "C:\development\WebSite\App_Settings\Redirects301.xml"
            /// </code>
            /// </example>
            public static string GetKeyAsPhysicalFilePath(string key)
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
}
