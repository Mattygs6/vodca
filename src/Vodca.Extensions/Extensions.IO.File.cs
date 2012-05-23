//-----------------------------------------------------------------------------
// <copyright file="Extensions.IO.File.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       11/02/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Converts a numeric value into a string that represents the number
        /// expressed as a size value in bytes, kilobytes, megabytes, gigabytes,
        /// or terabytes depending on the size. Output is identical to
        /// StrFormatByteSize() in shlwapi.dll. This is a format similar to
        /// the Windows Explorer file Properties page. For example:
        /// 532 -&gt;  532 bytes
        /// 1240 -&gt; 1.21 KB
        /// 235606 -&gt;  230 KB
        /// 5400016 -&gt; 5.14 MB
        /// </summary>
        /// <param name="fileSize">The file size from FileInfo</param>
        /// <returns>
        /// The format similar to the Windows Explorer file Properties page. For example:
        /// 532 -&gt;  532 bytes
        /// 1240 -&gt; 1.21 KB
        /// 235606 -&gt;  230 KB
        /// 5400016 -&gt; 5.14 MB
        /// </returns>
        /// <remarks>
        /// It was surprisingly difficult to emulate the StrFormatByteSize() function
        /// due to a few quirks. First, the function only displays three digits:
        /// - displays 2 decimal places for values under 10            (e.graphics. 2.12 KB)
        /// - displays 1 decimal place for values under 100            (e.graphics. 88.2 KB)
        /// - displays 0 decimal places for values under 1000         (e.graphics. 532 KB)
        /// - jumps to the next unit of measure for values over 1000    (e.graphics. 0.97 MB)
        /// The second quirk: insignificant digits are truncated rather than
        /// rounded. The original function likely uses integer math.
        /// This implementation was tested to 100 TB.
        /// </remarks>
        /// <author>Unitlities.Net</author>
        public static string FileSizeToString(long fileSize)
        {
            if (fileSize < 1024)
            {
                return string.Format("{0} bytes", fileSize);
            }

            double value = fileSize;
            value = value / 1024;
            string unit = "KB";

            if (value >= 1000)
            {
                value = Math.Floor(value);
                value = value / 1024;
                unit = "MB";
            }

            if (value >= 1000)
            {
                value = Math.Floor(value);
                value = value / 1024;
                unit = "GB";
            }

            if (value >= 1000)
            {
                value = Math.Floor(value);
                value = value / 1024;
                unit = "TB";
            }

            if (value < 10)
            {
                value = Math.Floor(value * 100) / 100;
                return string.Format("{0:n2} {1}", value, unit);
            }

            if (value < 100)
            {
                value = Math.Floor(value * 10) / 10;
                return string.Format("{0:n1} {1}", value, unit);
            }

            value = Math.Floor(value * 1) / 1;
            return string.Format("{0:n0} {1}", value, unit);
        }
    }
}