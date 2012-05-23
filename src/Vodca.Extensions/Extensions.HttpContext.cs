//-----------------------------------------------------------------------------
// <copyright file="Extensions.HttpContext.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       03/05/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Web;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Ends the request.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void EndRequest(this HttpContext context)
        {
            if (context != null)
            {
                context.Response.Flush();
                context.ApplicationInstance.CompleteRequest();
            }
        }

        /// <summary>
        /// Redirects the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="redirectLocation">The redirect location.</param>
        /// <param name="statusCode">The status code.</param>
        public static void Redirect(this HttpContext context, string redirectLocation, int statusCode = 302)
        {
            if (context != null)
            {
                context.Response.StatusCode = statusCode;
                context.Response.RedirectLocation = redirectLocation;
                context.EndRequest();
            }
        }

        /// <summary>
        /// Redirects the permanent.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="redirectLocation">The redirect location.</param>
        public static void RedirectPermanent(this HttpContext context, string redirectLocation)
        {
            context.Redirect(redirectLocation, 301);
        }

        /// <summary>
        /// Reads the base64 cookie.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <param name="encrypted">if set to <c>true</c> [encrypted].</param>
        /// <returns>The Object</returns>
        public static TObject ReadBase64Cookie<TObject>(this HttpContext context, string name, bool encrypted = false) where TObject : class
        {
            if (context != null && !string.IsNullOrWhiteSpace(name))
            {
                var cookie = context.Request.Cookies[name];

                if (cookie != null && !string.IsNullOrWhiteSpace(cookie.Value))
                {
                    return encrypted
                        ? cookie.Value.DecryptDES().DeserializeFromJson<TObject>()
                        : cookie.Value.DecodeBase64().DeserializeFromJson<TObject>();
                }
            }

            return null;
        }

        /// <summary>
        /// Writes the base64 cookie.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="encrypt">if set to <c>true</c> [encrypt].</param>
        public static void WriteBase64Cookie(this HttpContext context, string name, object obj, bool encrypt = false)
        {
            if (context != null && !string.IsNullOrWhiteSpace(name) && obj != null)
            {
                HttpCookie cookie = encrypt
                                        ? new HttpCookie(name, obj.SerializeToJson().EncryptDES())
                                        : new HttpCookie(name, obj.SerializeToJson().EncodeBase64());

                context.Response.SetCookie(cookie);
            }
        }
    }
}
