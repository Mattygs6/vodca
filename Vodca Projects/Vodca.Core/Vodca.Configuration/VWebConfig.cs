//-----------------------------------------------------------------------------
// <copyright file="VWebConfig.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/24/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections;
    using System.Configuration;
    using System.Web.Configuration;

    /// <summary>
    /// Contains common WebConfigurationManager extension methods
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Configuration\VWebConfig.AppSettings.cs" title="VWebConfig.cs" lang="C#" />
    /// </example>
    public static partial class VWebConfig
    {
        /// <summary>
        /// Gets the authentication section.
        /// </summary>
        /// <value>The authentication section.</value>
        public static AuthenticationSection AuthenticationSection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/authentication") as AuthenticationSection;
            }
        }

        /// <summary>
        /// Gets the authorization section.
        /// </summary>
        /// <value>The authorization section.</value>
        public static AuthorizationSection AuthorizationSection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/authorization") as AuthorizationSection;
            }
        }

        /// <summary>
        /// Gets the client target section.
        /// </summary>
        /// <value>The client target section.</value>
        public static ClientTargetSection ClientTargetSection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/clientTarget") as ClientTargetSection;
            }
        }

        /// <summary>
        /// Gets the compilation section.
        /// </summary>
        /// <value>The compilation section.</value>
        public static CompilationSection CompilationSection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/compilation") as CompilationSection;
            }
        }

        /// <summary>
        /// Gets the connection strings section.
        /// </summary>
        /// <value>The connection strings section.</value>
        public static ConnectionStringsSection ConnectionStringsSection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("connectionStrings") as ConnectionStringsSection;
            }
        }

        /// <summary>
        /// Gets the globalization section.
        /// </summary>
        /// <value>The globalization section.</value>
        public static GlobalizationSection GlobalizationSection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/globalization") as GlobalizationSection;
            }
        }

        /// <summary>
        /// Gets the HTTP cookies section.
        /// </summary>
        /// <value>The HTTP cookies section.</value>
        public static HttpCookiesSection HttpCookiesSection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/httpCookies") as HttpCookiesSection;
            }
        }

        /// <summary>
        /// Gets the HTTP modules section.
        /// </summary>
        /// <value>The HTTP modules section.</value>
        public static HttpModulesSection HttpModulesSection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/httpModules") as HttpModulesSection;
            }
        }

        /// <summary>
        /// Gets the HTTP runtime section.
        /// </summary>
        /// <value>The HTTP runtime section.</value>
        public static HttpRuntimeSection HttpRuntimeSection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/httpRuntime") as HttpRuntimeSection;
            }
        }

        /// <summary>
        /// Gets the identity section.
        /// </summary>
        /// <value>The identity section.</value>
        public static IdentitySection IdentitySection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/identity") as IdentitySection;
            }
        }

        /// <summary>
        /// Gets the machine key section.
        /// </summary>
        /// <value>The machine key section.</value>
        public static MachineKeySection MachineKeySection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/machineKey") as MachineKeySection;
            }
        }

        /// <summary>
        /// Gets the membership section.
        /// </summary>
        /// <value>The membership section.</value>
        public static MembershipSection MembershipSection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/membership") as MembershipSection;
            }
        }

        /// <summary>
        /// Gets the output cache section.
        /// </summary>
        /// <value>The output cache section.</value>
        public static OutputCacheSection OutputCacheSection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/outputCache") as OutputCacheSection;
            }
        }

        /// <summary>
        /// Gets the pages section.
        /// </summary>
        /// <value>The pages section.</value>
        public static PagesSection PagesSection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/pages") as PagesSection;
            }
        }

        /// <summary>
        /// Gets the profile section.
        /// </summary>
        /// <value>The profile section.</value>
        public static ProfileSection ProfileSection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/profile") as ProfileSection;
            }
        }

        /// <summary>
        /// Gets the session page state section.
        /// </summary>
        /// <value>The session page state section.</value>
        public static SessionPageStateSection SessionPageStateSection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/sessionPageState") as SessionPageStateSection;
            }
        }

        /// <summary>
        /// Gets the URL mappings section.
        /// </summary>
        /// <value>The URL mappings section.</value>
        public static UrlMappingsSection UrlMappingsSection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/urlMappings") as UrlMappingsSection;
            }
        }

        /// <summary>
        /// Gets the XHTML conformance section.
        /// </summary>
        /// <value>The XHTML conformance section.</value>
        public static XhtmlConformanceSection XhtmlConformanceSection
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/xhtmlConformance") as XhtmlConformanceSection;
            }
        }

        /// <summary>
        /// Gets the web controls.
        /// </summary>
        /// <value>The web controls.</value>
        public static Hashtable WebControls
        {
            get
            {
                return WebConfigurationManager.GetWebApplicationSection("system.web/webControls") as Hashtable;
            }
        }
    }
}
