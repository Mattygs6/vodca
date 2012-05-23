//-----------------------------------------------------------------------------
// <copyright file="Extensions.AntiXss.cs" company="genuine">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     Microsoft
//  Modified:   J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Diagnostics;
    using System.Text;

    /// <content>
    ///     This is clone function and code of Microsoft AntiXss library plus a couple extra function for performance and convenience
    ///     Cross-site scripting (XSS) attacks exploit vulnerabilities in Web-based applications that fail to properly validate and/or encode input that is embedded in response data.  
    /// Malicious users can then inject client-side script into response data causing the unsuspecting user's browser to execute the script code.  
    /// The script code will appear to have originated from a trusted-site and may be able to bypass browser protection mechanisms such as security zones.
    /// </content>
    public static partial class Extensions
    {
        /// <summary>
        ///     Encodes input strings for use in JavaScript
        /// </summary>
        /// <param name="input">String as input</param>
        /// <returns>Encoded for JavaScript string</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// string text = "some input".JavaScriptEncode();
        ///     OR
        /// string text = Extensions.JavaScriptEncode(this.Page.Request.QueryString["UserString"]);
        /// // Perform some action on text
        /// </code>
        /// </example>
        [DebuggerHidden]
        public static string JavaScriptEncode(this string input)
        {
            string output;
            if (input == null)
            {
                output = "null";
            }
            else if (input.Length == 0)
            {
                output = "''";
            }
            else
            {
                var builder = new StringBuilder("'", input.Length * 2);
                foreach (char ch in input)
                {
                    if ((((ch > '`') && (ch < '{')) || ((ch > '@') && (ch < '['))) || (((ch == ' ') || ((ch > '/') && (ch < ':'))) || (((ch == '.') || (ch == ',')) || ((ch == '-') || (ch == '_')))))
                    {
                        builder.Append(ch);
                    }
                    else if (ch > '\x007f')
                    {
                        builder.Append(@"\u" + TwoByteHex(ch));
                    }
                    else
                    {
                        builder.Append(@"\x" + SingleByteHex(ch));
                    }
                }

                builder.Append("'");
                output = builder.ToString();
            }

            return output;
        }

        /// <summary>
        ///     Encodes input strings for use in JavaScript
        /// </summary>
        /// <param name="input">String input to encode</param>
        /// <param name="builder">Encodes string and result appends in the StringBuilder instance</param>
        [DebuggerHidden]
        public static void JavaScriptEncode(this string input, StringBuilder builder)
        {
            if (input == null)
            {
                builder.Append("null");
            }
            else if (input.Length == 0)
            {
                builder.Append("''");
            }
            else
            {
                builder.Append("'");
                foreach (char ch in input)
                {
                    if ((((ch > '`') && (ch < '{')) || ((ch > '@') && (ch < '['))) || (((ch == ' ') || ((ch > '/') && (ch < ':'))) || (((ch == '.') || (ch == ',')) || ((ch == '-') || (ch == '_')))))
                    {
                        builder.Append(ch);
                    }
                    else if (ch > '\x007f')
                    {
                        builder.Append(@"\u").Append(TwoByteHex(ch));
                    }
                    else
                    {
                        builder.Append(@"\x").Append(SingleByteHex(ch));
                    }
                }

                builder.Append("'");
            }
        }

        /// <summary>
        ///     Encodes a list of strings into JavaScript array notation, 
        /// with full AntXss encoding of each string in the array
        /// </summary>
        /// <param name="strings">The strings to encode</param>
        /// <returns>A JavaScript array of strings</returns>
        [DebuggerHidden]
        public static string JavaScriptEncode(params string[] strings)
        {
            var builder = new StringBuilder(1024);
            builder.Append("[");
            if (strings != null && strings.Length > 0)
            {
                for (int i = 0; i < strings.Length - 1; i++)
                {
                    JavaScriptEncode(strings[i], builder);
                    builder.Append(", ");
                }

                JavaScriptEncode(strings[strings.Length - 1], builder);
            }

            builder.Append("]");
            return builder.ToString();
        }

        /// <summary>
        ///     Coverts to hex decimal
        /// </summary>
        /// <param name="c">Single Character</param>
        /// <returns>Char as hex target</returns>
        internal static string TwoByteHex(char c)
        {
            uint num = c;
            return num.ToString("x").PadLeft(4, '0');
        }

        /// <summary>
        ///     Coverts to hex decimal
        /// </summary>
        /// <param name="c">Single Character</param>
        /// <returns>Char as hex target</returns>
        internal static string SingleByteHex(char c)
        {
            uint num = c;
            return num.ToString("x").PadLeft(2, '0');
        }
    }
}
