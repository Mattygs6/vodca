//-----------------------------------------------------------------------------
// <copyright file="VWebConfig.SqlCacheDependency.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/24/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Web.Configuration;

    /// <summary>
    ///     Contains common WebConfigurationManager extension methods
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Configuration\VWebConfig.SqlCacheDependency.cs" title="VWebConfig.SqlCacheDependency.cs" lang="C#" />
    /// </example>
    public static partial class VWebConfig
    {
        /// <summary>
        ///     Web.config SqlCacheDependency  sections  
        /// </summary>
        public static class SqlCacheDependency
        {
            /// <summary>
            ///     Gets the SQL cache dependency section.
            /// </summary>
            /// <returns>The SqlCacheDependencySection or NULL</returns>
            public static SqlCacheDependencySection GetSqlCacheDependencySection()
            {
                return (SqlCacheDependencySection)WebConfigurationManager.GetWebApplicationSection("system.web/caching/sqlCacheDependency");
            }

            /// <summary>
            ///     Determines whether is enabled SQL cache dependency enabled.
            /// </summary>
            /// <returns>
            ///     <c>true</c> if is enabled SQL cache dependency; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsEnabledSqlCacheDependency()
            {
                var section = GetSqlCacheDependencySection();

                return section != null && section.Enabled;
            }
        }
    }
}
