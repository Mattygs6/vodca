//-----------------------------------------------------------------------------
// <copyright file="VJsonCookie.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       03/07/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Web;

    /// <summary>
    /// Base class for storing data to cookies as an object
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    public abstract class VJsonCookie<TObject> where TObject : VJsonCookie<TObject>, new()
    {
        /// <summary>
        /// Descendant class object used to get Cookie name if null or empty in method calls
        /// </summary>
        private static readonly TObject Descendant = new TObject();

        /// <summary>
        /// Gets the name of the cookie.
        /// </summary>
        /// <code lang="C#" title="Example">
        /// <![CDATA[
        /// public override string GetCookieName()
        /// {
        ///     return "HelloWorld";
        /// }
        /// ]]>
        /// </code>
        /// <remarks>
        /// Prevents the need to use Reflection and Attributes to get custom cookie names
        /// </remarks>
        /// <returns>The cookie name</returns>
        public static string ResolveCookieName()
        {
            var name = Descendant.GetCookieName();
            if (!string.IsNullOrWhiteSpace(name))
            {
                return name;
            }

            return typeof(TObject).Name;
        }

        /// <summary>
        /// Reads the cookie.
        /// </summary>
        /// <returns>
        /// The Object
        /// </returns>
        public static TObject ReadCookie()
        {
            var context = HttpContext.Current;

            if (context != null)
            {
                var cookie = context.Request.Cookies[ResolveCookieName()];

                if (cookie != null && !string.IsNullOrWhiteSpace(cookie.Value))
                {
                    return Descendant.UseEncryption()
                        ? cookie.Value.DecryptDES().DeserializeFromJson<TObject>()
                        : cookie.Value.DecodeBase64().DeserializeFromJson<TObject>();
                }
            }

            return new TObject();
        }

        /// <summary>
        /// Deletes the cookie.
        /// </summary>
        public static void DeleteCookie()
        {
            var context = HttpContext.Current;

            if (context != null)
            {
                var cookie = context.Request.Cookies[ResolveCookieName()];

                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    context.Response.Cookies.Add(cookie);
                }
            }
        }

        /// <summary>
        /// Writes the cookie.
        /// </summary>
        /// <returns>
        /// The created cookie
        /// </returns>
        public virtual HttpCookie WriteCookie()
        {
            var context = HttpContext.Current;

            if (context != null)
            {
                HttpCookie cookie = this.UseEncryption()
                      ? new HttpCookie(ResolveCookieName(), this.SerializeToJson().EncryptDES())
                      : new HttpCookie(ResolveCookieName(), this.SerializeToJson().EncodeBase64());

                context.Response.SetCookie(cookie);

                return cookie;
            }

            return null;
        }

        /// <summary>
        /// Gets the name of the cookie. If not implemented, will use the Descendant class name.
        /// </summary>
        /// <returns>
        /// The cookie name to use
        /// </returns>
        public virtual string GetCookieName()
        {
            return this.GetType().Name;
        }

        /// <summary>
        /// Uses the encryption.
        /// </summary>
        /// <returns>
        /// True or false to use encryption
        /// </returns>
        public virtual bool UseEncryption()
        {
            return false;
        }
    }
}
