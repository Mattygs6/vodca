//-----------------------------------------------------------------------------
// <copyright file="Extensions.HttpCookies.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/28/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Web;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /* ReSharper disable InconsistentNaming */

        /// <summary>
        /// Indicates whether the specified <see cref="System.Web.HttpCookie"/>
        /// is null or empty value.
        /// </summary>
        /// <param name="cookie">The cookie.</param>
        /// <returns>Return true if Count is 0 or it is null</returns>
        public static bool IsNullOrEmpty(this HttpCookie cookie)
        {
            return cookie == null || !string.IsNullOrEmpty(cookie.Value);
        }

        /// <summary>
        ///     Encrypt HttpCookie value string with base 64 digits
        /// </summary>
        /// <param name="cookie">The HTTP cookie</param>
        /// <remarks>
        /// <pre>
        /// <b>Important:</b>
        /// Add keys to the <![CDATA[<appSettings>]]>section in web.config
        /// <![CDATA[
        /// <appSettings>
        ///    <!--Encryption Key and Vector values MUST be 8 chars long exactly-->
        ///    <add key="VEncryption.Key" value="JBaltika"/>
        ///    <add key="VEncryption.Vector" value="IGenuine"/>
        /// </appSettings>
        /// ]]>
        /// </pre>
        /// </remarks>
        public static void Encrypt64(this HttpCookie cookie)
        {
            if (cookie != null)
            {
                cookie.HttpOnly = true;

                string currentvalue = cookie.Value;
                if (!string.IsNullOrEmpty(currentvalue))
                {
                    cookie.Value = currentvalue.Encrypt64();
                }
            }
        }

        /// <summary>
        ///     Encrypt HttpCookie value string using DES algorithm.
        /// </summary>
        /// <param name="cookie">The HTTP cookie</param>
        /// <remarks>
        /// <pre>
        /// <b>Important:</b>
        /// Add keys to the <![CDATA[<appSettings>]]>section in web.config
        /// <![CDATA[
        /// <appSettings>
        ///    <!--Encryption Key and Vector values MUST be 8 chars long exactly-->
        ///    <add key="VEncryption.Key" value="JBaltika"/>
        ///    <add key="VEncryption.Vector" value="IGenuine"/>
        /// </appSettings>
        /// ]]>
        /// </pre>
        /// </remarks>
        public static void EncryptDES(this HttpCookie cookie)
        {
            if (cookie != null)
            {
                cookie.HttpOnly = true;

                string currentvalue = cookie.Value;
                if (!string.IsNullOrEmpty(currentvalue))
                {
                    cookie.Value = currentvalue.EncryptDES();
                }
            }
        }

        /// <summary>
        ///     Decrypt HttpCookie value string with base 64 digits
        /// </summary>
        /// <param name="cookie">The HTTP cookie</param>
        /// <remarks>
        /// <pre>
        /// <b>Important:</b>
        /// Add keys to the <![CDATA[<appSettings>]]>section in web.config
        /// <![CDATA[
        /// <appSettings>
        ///    <!--Encryption Key and Vector values MUST be 8 chars long exactly-->
        ///    <add key="VEncryption.Key" value="JBaltika"/>
        ///    <add key="VEncryption.Vector" value="IGenuine"/>
        /// </appSettings>
        /// ]]>
        /// </pre>
        /// </remarks>
        public static void Decrypt64(this HttpCookie cookie)
        {
            if (cookie != null)
            {
                string currentvalue = cookie.Value;
                if (!string.IsNullOrEmpty(currentvalue))
                {
                    cookie.Value = currentvalue.Decrypt64();
                }
            }
        }

        /// <summary>
        ///     Decrypt HttpCookie value string using DES algorithm.
        /// </summary>
        /// <param name="cookie">The HTTP cookie</param>
        /// <remarks>
        /// <pre>
        /// <b>Important:</b>
        /// Add keys to the <![CDATA[<appSettings>]]>section in web.config
        /// <![CDATA[
        /// <appSettings>
        ///    <!--Encryption Key and Vector values MUST be 8 chars long exactly-->
        ///    <add key="VEncryption.Key" value="JBaltika"/>
        ///    <add key="VEncryption.Vector" value="IGenuine"/>
        /// </appSettings>
        /// ]]>
        /// </pre>
        /// </remarks>
        public static void DecryptDES(this HttpCookie cookie)
        {
            if (cookie != null)
            {
                cookie.HttpOnly = true;

                string currentvalue = cookie.Value;
                if (!string.IsNullOrEmpty(currentvalue))
                {
                    cookie.Value = currentvalue.DecryptDES();
                }
            }
        }

        /* ReSharper restore InconsistentNaming */

        /// <summary>
        /// Creates the encrypted json cookie.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="name">The name.</param>
        /// <returns>An http cookie that is an encrypted base 64 json string</returns>
        public static HttpCookie CreateEncryptedJsonCookie(this object obj, string name)
        {
            name = name.IsNotNullOrEmpty()
                       ? name
                       : "default";

            if (obj != null)
            {
                return new HttpCookie(name, obj.WriteEncryptedJsonBase64());
            }

            return new HttpCookie(name);
        }
    }
}
