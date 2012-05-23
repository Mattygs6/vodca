//-----------------------------------------------------------------------------
// <copyright file="Extensions.Uri.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/23/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text;
    using System.Web;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Leaves all alphanumeric, '/' and '-' chars in string
        /// </summary>
        /// <param name="input">User input</param>
        /// <returns>User input with Url Rewrite extension less chars only left</returns>
        /// <remarks>Not 100% prof the normalization of the string. The normalization is complex thing and the method normalization will take care of most cases. </remarks>
        /// <seealso href="http://blogs.msdn.com/b/michkap/archive/2007/05/14/2629747.aspx"/>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "This case must be Url as string")]
        public static string RemoveNonUrlChars(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                input = input.Trim().ToLowerInvariant();

                /* Normalize */
                byte[] tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(input);
                input = Encoding.UTF8.GetString(tempBytes);

                var builder = new StringBuilder(input.Length);
                for (int i = 0; i < input.Length; i++)
                {
                    char current = input[i];

                    if (char.IsLetterOrDigit(current) || '/'.Equals(current) || '.'.Equals(current))
                    {
                        builder.Append(current);
                    }
                    else if (char.IsWhiteSpace(current))
                    {
                        if (!'-'.Equals(input[i - 1]))
                        {
                            builder.Append('-');
                        }
                    }
                    else if ('_'.Equals(current))
                    {
                        builder.Append('-');
                    }
                    else if ('-'.Equals(current))
                    {
                        if (i != 0 && i + 1 != input.Length && !'-'.Equals(input[i - 1]))
                        {
                            builder.Append('-');
                        }
                    }
                }

                builder.Replace("//", "/");

                builder.Replace("..", ".");
                builder.Replace("--", "-");

                builder.Replace("/.", "/");
                builder.Replace("./", "/");

                builder.Replace("-/", "/");
                builder.Replace("/-", "/");

                builder.Replace(".-", ".");
                builder.Replace("-.", ".");

                builder.Replace("//", "/");

                return builder.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Converts the string to URL.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The formatted string</returns>
        public static string ToUrl(this string input)
        {
            return input.RemoveNonUrlChars();
        }

        /// <summary>
        ///     Gets HttpPostedFile and removes non web chars from it.
        /// </summary>
        /// <param name="postedfile">The posted file object</param>
        /// <returns>The file name without special chars</returns>
        public static string RemoveNonUrlChars(this HttpPostedFile postedfile)
        {
            if (postedfile != null)
            {
                string filename = Path.GetFileNameWithoutExtension(postedfile.FileName);
                string extention = Path.GetExtension(postedfile.FileName);

                return filename.RemoveNonLetterOrDigitChars() + extention;
            }

            return string.Empty;
        }

        /// <summary>
        ///     Get the current Uri to https
        /// </summary>
        /// <param name="uri">Request URI (a uniform resource identifier)</param>
        /// <returns>
        ///     A new instance of Uri with https schema
        /// </returns>
        public static Uri ToHttps(this Uri uri)
        {
            if (uri != null && uri.Port != 433)
            {
                var newuri = new UriBuilder(uri)
                {
                    Scheme = Uri.UriSchemeHttps,
                    Port = 443
                };

                return newuri.Uri;
            }

            return uri;
        }

        /// <summary>
        /// Get current Https URI to HTTP.
        /// </summary>
        /// <param name="uri">The uniform resource identifier (URI).</param>
        /// <returns>A new instance of Uri with http schema</returns>
        public static Uri ToHttp(this Uri uri)
        {
            if (uri != null && uri.Port != 433)
            {
                var newuri = new UriBuilder(uri)
                {
                    Scheme = Uri.UriSchemeHttp,
                    Port = 80
                };

                return newuri.Uri;
            }

            return uri;
        }

        /// <summary>
        /// Converts The Uri Builder to href attribute value
        /// </summary>
        /// <param name="uribuilder">The URI builder.</param>
        /// <returns>The HREF attribute source</returns>
        public static string ToHref(this UriBuilder uribuilder)
        {
            if (uribuilder != null)
            {
                if (string.Equals(uribuilder.Host, "localhost", StringComparison.InvariantCultureIgnoreCase))
                {
                    return uribuilder.Path.ToLowerInvariant();
                }

                return uribuilder.ToString().ToLowerInvariant();
            }

            return string.Empty;
        }
    }
}
