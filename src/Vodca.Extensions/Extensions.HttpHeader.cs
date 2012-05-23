//-----------------------------------------------------------------------------
// <copyright file="Extensions.HttpHeader.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/12/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Web;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Adds the specified encoding to the response header.
        /// </summary>
        /// <param name="response">An ASP.NET HTTP-response information</param>
        /// <param name="encoding">The ASP.NET page encoding</param>
        public static void SetHeaderEncodingType(this HttpResponse response, string encoding)
        {
            if (response != null)
            {
                response.AppendHeader("Content-encoding", encoding);
            }
        }

        /* ReSharper disable InconsistentNaming */

        /// <summary>
        ///     Enables the IIS7 dynamic compression.
        /// </summary>
        /// <remarks>
        ///     You must enable the IIS 7.0 "dynamic compression before cache" feature.  You can do this by adding the following to your application's web.config file. 
        ///  <![CDATA[
        /// <system.webServer>
        ///     <urlCompression doDynamicCompression="false" dynamicCompressionBeforeCache="true" />
        /// </system.webServer>
        /// ]]>
        /// </remarks>
        /// <param name="request">The http request.</param>
        public static void EnableIIS7DynamicCompression(this HttpRequest request)
        {
            if (request != null && !string.IsNullOrEmpty(request.ServerVariables["SERVER_SOFTWARE"]))
            {
                request.ServerVariables["IIS_EnableDynamicCompression"] = "1";
            }
        }

        /* ReSharper restore InconsistentNaming */
    }
}
