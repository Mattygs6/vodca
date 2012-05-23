//-----------------------------------------------------------------------------
// <copyright file="Extensions.QueryString.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Web;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Query the string utility. Designed to use inside Web DLL's mostly and legacy support for ASP.NET 2.0.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>Query string param as Nullable byte</returns>
        /// <example>View code: <br />    
        /// byte? id = Extensions.QueryStringAsByte("id");
        /// </example>
        public static byte? QueryStringAsByte(string param)
        {
            string value = HttpContext.Current.Request.QueryString[param];
            return string.IsNullOrEmpty(value) ? null : value.ConvertToByte();
        }

        /// <summary>
        ///     Query the string utility. Designed to use for ASP.NET 3.5.
        /// </summary>
        /// <param name="request">The HTTP values sent by a client during a Web request</param>
        /// <param name="param">The param.</param>
        /// <returns>Query string param as Nullable byte</returns>
        /// <example>View code: <br />
        ///     byte? id = this.Page.Request.QueryStringAsByte("id");
        /// </example>
        public static byte? QueryStringAsByte(this HttpRequest request, string param)
        {
            string value = request.QueryString[param];
            return string.IsNullOrEmpty(value) ? null : value.ConvertToByte();
        }

        /// <summary>
        ///     Query the string utility. Designed to use inside Web DLL's mostly and legacy support for ASP.NET 2.0.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>Query string param as Nullable int</returns>
        /// <example>View code: <br />
        ///     int? id = Extensions.QueryStringAsInt("id");
        /// </example>
        public static int? QueryStringAsInt(string param)
        {
            string value = HttpContext.Current.Request.QueryString[param];
            return string.IsNullOrEmpty(value) ? null : value.ConvertToInt();
        }

        /// <summary>
        ///     Query the string utility. Designed to use for ASP.NET 3.5.
        /// </summary>
        /// <param name="request">The HTTP values sent by a client during a Web request</param>
        /// <param name="param">The param.</param>
        /// <returns>Query string param as Nullable int</returns>
        /// <example>View code: <br />
        ///     int? id = this HttpRequest request, .QueryStringAsInt("id");
        /// </example>
        public static int? QueryStringAsInt(this HttpRequest request, string param)
        {
            string value = request.QueryString[param];
            return string.IsNullOrEmpty(value) ? null : value.ConvertToInt();
        }

        /// <summary>
        ///     Query the string utility. Designed to use inside Web DLL's mostly and legacy support for ASP.NET 2.0.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>Query string param as Nullable long</returns>
        /// <example>View code: <br />
        ///     long? id = Extensions.QueryStringAsLong("id");
        /// </example>
        public static long? QueryStringAsLong(string param)
        {
            string value = HttpContext.Current.Request.QueryString[param];
            return string.IsNullOrEmpty(value) ? null : value.ConvertToLong();
        }

        /// <summary>
        ///     Query the string utility. Designed to use for ASP.NET 3.5.
        /// </summary>
        /// <param name="request">The HTTP values sent by a client during a Web request.</param>
        /// <param name="param">The param.</param>
        /// <returns>Query string param as Nullable long</returns>
        /// <example>View code: <br />
        ///     long? id = this.Page.Request.QueryStringAsLong("id");
        /// </example>
        public static long? QueryStringAsLong(this HttpRequest request, string param)
        {
            string value = request.QueryString[param];
            return string.IsNullOrEmpty(value) ? null : value.ConvertToLong();
        }

        /// <summary>
        ///     Query the string utility. Designed to use inside Web DLL's mostly and legacy support for ASP.NET 2.0.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>Query string param as Nullable double</returns>
        /// <example>View code: <br />
        ///     Double? id = Extensions.QueryStringAsDouble("id");
        /// </example>
        public static double? QueryStringAsDouble(string param)
        {
            string value = HttpContext.Current.Request.QueryString[param];
            return string.IsNullOrEmpty(value) ? null : value.ConvertToDouble();
        }

        /// <summary>
        ///     Query the string utility. Designed to use for ASP.NET 3.5.
        /// </summary>
        /// <param name="request">The HTTP values sent by a client during a Web request</param>
        /// <param name="param">The param.</param>
        /// <returns>Query string param as Nullable double</returns>
        /// <example>View code: <br />
        ///     double? id = this.Page.Request.QueryStringAsDouble("id");
        /// </example>
        public static double? QueryStringAsDouble(this HttpRequest request, string param)
        {
            string value = request.QueryString[param];
            return string.IsNullOrEmpty(value) ? null : value.ConvertToDouble();
        }

        /// <summary>
        ///     Query the string utility. Designed to use inside Web DLL's mostly and legacy support for ASP.NET 2.0.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>Query string param as Nullable float</returns>
        /// <example>View code: <br />
        ///     float? id = Extensions.QueryStringAsFloat("id");
        /// </example>
        public static float? QueryStringAsFloat(string param)
        {
            string value = HttpContext.Current.Request.QueryString[param];
            return string.IsNullOrEmpty(value) ? null : value.ConvertToFloat();
        }

        /// <summary>
        ///     Query the string utility. Designed to use for ASP.NET 3.5.
        /// </summary>
        /// <param name="request">The HTTP values sent by a client during a Web request</param>
        /// <param name="param">The param.</param>
        /// <returns>Query string param as Nullable float</returns>
        /// <example>View code: <br />
        ///     float? id = this.Page.Request.QueryStringAsFloat("id");
        /// </example>
        public static float? QueryStringAsFloat(this HttpRequest request, string param)
        {
            string value = request.QueryString[param];
            return string.IsNullOrEmpty(value) ? null : value.ConvertToFloat();
        }

        /// <summary>
        ///     Query the string utility. Designed to use inside Web DLL's mostly and legacy support for ASP.NET 2.0.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>Query string param as Nullable DateTime</returns>
        /// <example>View code: <br />
        ///     DateTime? id = Extensions.QueryStringAsDateTime("datetime");
        /// </example>
        public static DateTime? QueryStringAsDateTime(string param)
        {
            string value = HttpContext.Current.Request.QueryString[param];
            return string.IsNullOrEmpty(value) ? null : value.ConvertToDateTime();
        }

        /// <summary>
        ///     Query the string utility. Designed to use for ASP.NET 3.5.
        /// </summary>
        /// <param name="request">The HTTP values sent by a client during a Web request</param>
        /// <param name="param">The param.</param>
        /// <returns>Query string param as Nullable DateTime</returns>
        /// <example>View code: <br />
        ///     DateTime? id = this.Page.Request.QueryStringAsDateTime("datetime");
        /// </example>
        public static DateTime? QueryStringAsDateTime(this HttpRequest request, string param)
        {
            string value = request.QueryString[param];
            return string.IsNullOrEmpty(value) ? null : value.ConvertToDateTime();
        }

        /// <summary>
        ///     Query the string utility. Designed to use inside Web DLL's mostly and legacy support for ASP.NET 2.0.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>Query string param as Nullable Boolean</returns>
        /// <example>View code: <br />
        ///     bool? id = Extensions.QueryStringAsBoolean("iscurrent");
        /// </example>
        public static bool? QueryStringAsBoolean(string param)
        {
            string value = HttpContext.Current.Request.QueryString[param];
            return string.IsNullOrEmpty(value) ? null : value.ConvertToBoolean();
        }

        /// <summary>
        ///     Query the string utility. Designed to use for ASP.NET 3.5.
        /// </summary>
        /// <param name="request">The HTTP values sent by a client during a Web request</param>
        /// <param name="param">The param.</param>
        /// <returns>Query string param as Nullable Boolean</returns>
        /// <example>View code: <br />
        ///     bool? id = this.Page.Request.QueryStringAsBoolean("iscurrent");
        /// </example>
        public static bool? QueryStringAsBoolean(this HttpRequest request, string param)
        {
            string value = request.QueryString[param];
            return string.IsNullOrEmpty(value) ? null : value.ConvertToBoolean();
        }

        /// <summary>
        ///     Query the string utility. Designed to use inside Web DLL's mostly and legacy support for ASP.NET 2.0.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>Query string param as Nullable Guid</returns>
        /// <example>View code: <br />
        ///     Guid? id = Extensions.QueryStringAsGuid("id");
        /// </example>
        public static Guid? QueryStringAsGuid(string param)
        {
            string value = HttpContext.Current.Request.QueryString[param];
            return string.IsNullOrEmpty(value) ? null : value.ConvertToGuid();
        }

        /// <summary>
        ///     Query the string utility.Designed to use for ASP.NET 3.5.
        /// </summary>
        /// <param name="request">The HTTP values sent by a client during a Web request</param>
        /// <param name="param">The param.</param>
        /// <returns>Query string param as Nullable Guid</returns>
        /// <example>View code: <br />
        ///     Guid? id = this.Page.Request.QueryStringAsGuid("id");
        /// </example>
        public static Guid? QueryStringAsGuid(this HttpRequest request, string param)
        {
            string value = request.QueryString[param];
            return string.IsNullOrEmpty(value) ? null : value.ConvertToGuid();
        }
    }
}
