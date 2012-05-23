//-----------------------------------------------------------------------------
// <copyright file="Extensions.Param.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       09/08/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Web;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Param string utility. Designed to use inside Web DLL's mostly and legacy support for ASP.NET 2.0.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>The param as Nullable byte</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        ///     byte? id = "id".ParamAsByte();
        ///     byte? id = Extensions.ParamAsByte("id");
        /// </code>
        /// </example>
        public static byte? ParamAsByte(string param)
        {
            string value = HttpContext.Current.Request.Params[param];
            return value.ConvertToByte();
        }

        /// <summary>
        ///     Param string utility.
        /// </summary>
        /// <param name="request">The HTTP values sent by a client during a Web request</param>
        /// <param name="param">The param.</param>
        /// <returns>The param as Nullable byte</returns>
        /// <example>View code: <br />
        ///     byte? id = this.Page.Request.ParamAsByte("id");
        /// </example>
        public static byte? ParamAsByte(this HttpRequest request, string param)
        {
            string value = request.Params[param];
            return value.ConvertToByte();
        }

        /// <summary>
        ///     Param string utility. Designed to use inside Web DLL's mostly and legacy support for ASP.NET 2.0.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>The param as Nullable int</returns>
        /// <example>View code: <br />
        ///     int? id = Extensions.ParamAsInt("id");
        /// </example>
        public static int? ParamAsInt(string param)
        {
            string value = HttpContext.Current.Request.Params[param];
            return value.ConvertToInt();
        }

        /// <summary>
        ///     Param string utility.
        /// </summary>
        /// <param name="request">The HTTP values sent by a client during a Web request</param>
        /// <param name="param">The param.</param>
        /// <returns>The param as Nullable int</returns>
        /// <example>View code: <br />
        ///     int? id = this.Page.Request.ParamAsInt("id");
        /// </example>
        public static int? ParamAsInt(this HttpRequest request, string param)
        {
            string value = request.Params[param];
            return value.ConvertToInt();
        }

        /// <summary>
        ///     Param string utility. Designed to use inside Web DLL's mostly and legacy support for ASP.NET 2.0.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>The param as Nullable long</returns>
        /// <example>View code: <br />
        ///     long? id = Extensions.ParamAsLong("id");
        /// </example>
        public static long? ParamAsLong(string param)
        {
            string value = HttpContext.Current.Request.Params[param];
            return value.ConvertToLong();
        }

        /// <summary>
        ///     Param string utility.
        /// </summary>
        /// <param name="request">The HTTP values sent by a client during a Web request</param>
        /// <param name="param">The param.</param>
        /// <returns>The param as Nullable long</returns>
        /// <example>View code: <br />
        ///     long? id = this.Page.Request.ParamAsLong("id");
        /// </example>
        public static long? ParamAsLong(this HttpRequest request, string param)
        {
            string value = request.Params[param];
            return value.ConvertToLong();
        }

        /// <summary>
        ///     Param string utility. Designed to use inside Web DLL's mostly and legacy support for ASP.NET 2.0.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>The param as Nullable double</returns>
        /// <example>View code: <br />
        ///     double? id = Extensions.ParamAsDouble("id");
        /// </example>
        public static double? ParamAsDouble(string param)
        {
            string value = HttpContext.Current.Request.Params[param];
            return value.ConvertToDouble();
        }

        /// <summary>
        ///     Param string utility.
        /// </summary>
        /// <param name="request">The HTTP values sent by a client during a Web request</param>
        /// <param name="param">The param.</param>
        /// <returns>The param as Nullable double</returns>
        /// <example>View code: <br />
        ///     double? id = this.Page.Request.ParamAsDouble("id");
        /// </example>
        public static double? ParamAsDouble(this HttpRequest request, string param)
        {
            string value = request.Params[param];
            return value.ConvertToDouble();
        }

        /// <summary>
        ///     Param string utility. Designed to use inside Web DLL's mostly and legacy support for ASP.NET 2.0.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>The param as Nullable DateTime</returns>
        /// <example>View code: <br />
        ///     DateTime? id = Extensions.ParamAsDateTime("datetime");
        /// </example>
        public static DateTime? ParamAsDateTime(string param)
        {
            string value = HttpContext.Current.Request.Params[param];
            return value.ConvertToDateTime();
        }

        /// <summary>
        ///     Param string utility.
        /// </summary>
        /// <param name="request">The HTTP values sent by a client during a Web request</param>
        /// <param name="param">The param.</param>
        /// <returns>The param as Nullable DateTime</returns>
        /// <example>View code: <br />
        ///     DateTime? id = this.Page.Request.ParamAsDateTime("datetime");
        /// </example>
        public static DateTime? ParamAsDateTime(this HttpRequest request, string param)
        {
            string value = request.Params[param];
            return value.ConvertToDateTime();
        }

        /// <summary>
        ///     Param string utility. Designed to use inside Web DLL's mostly and legacy support for ASP.NET 2.0.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>Param as Nullable Boolean</returns>
        /// <example>View code: <br />
        ///     bool? id = Extensions.ParamAsBoolean("iscurrent");
        /// </example>
        public static bool? ParamAsBoolean(string param)
        {
            string value = HttpContext.Current.Request.Params[param];
            return value.ConvertToBoolean();
        }

        /// <summary>
        ///     Param string utility.
        /// </summary>
        /// <param name="request">The HTTP values sent by a client during a Web request</param>
        /// <param name="param">The param.</param>
        /// <returns>Param as Nullable Boolean</returns>
        /// <example>View code: <br />
        ///     bool? id = this.Page.Request.ParamAsBoolean("iscurrent");
        /// </example>
        public static bool? ParamAsBoolean(this HttpRequest request, string param)
        {
            string value = request.Params[param];
            return value.ConvertToBoolean();
        }

        /// <summary>
        ///     Param string utility. Designed to use inside Web DLL's mostly and legacy support for ASP.NET 2.0.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns>Param as Nullable Guid</returns>
        /// <example>View code: <br />
        ///     Guid? id = Extensions.ParamAsGuid("guid");
        /// </example>
        public static Guid? ParamAsGuid(string param)
        {
            string value = HttpContext.Current.Request.Params[param];
            return value.ConvertToGuid();
        }

        /// <summary>
        ///     Param string utility.
        /// </summary>
        /// <param name="request">The HTTP values sent by a client during a Web request</param>
        /// <param name="param">The param.</param>
        /// <returns>Param as Nullable Guid</returns>
        /// <example>View code: <br />
        ///     Guid? id = this.Page.Request.ParamAsGuid("guid");
        /// </example>
        public static Guid? ParamAsGuid(this HttpRequest request, string param)
        {
            string value = request.Params[param];
            return value.ConvertToGuid();
        }
    }
}
