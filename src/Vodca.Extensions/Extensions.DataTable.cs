//-----------------------------------------------------------------------------
// <copyright file="Extensions.DataTable.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/16/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Data;
    using System.IO;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Export Data View to the Xml Table which can be passed to Excel
        /// </summary>
        /// <param name="view">The data in the memory table</param>
        /// <returns>The data formatted as Xml</returns>
        public static string ExportToXmlTable(this DataView view)
        {
            if (view != null)
            {
                return view.Table.ExportToXmlTable();
            }

            return string.Empty;
        }

        /// <summary>
        ///     Export Data Table to the Xml Table which can be passed to Excel
        /// </summary>
        /// <param name="table">The data in the memory table</param>
        /// <returns>The data formatted as Xml</returns>
        public static string ExportToXmlTable(this DataTable table)
        {
            if (table != null)
            {
                // Allocated 8kb initial size
                var builder = new StringBuilder(8096);
                builder.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>");

                using (var writer = new StringWriter(builder))
                {
                    table.WriteXml(writer, XmlWriteMode.IgnoreSchema);
                }

                return builder.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        ///     Export Data Table to the Xml Table which can be passed to Excel
        /// </summary>
        /// <param name="table">The data in the memory table</param>
        /// <returns>The data formatted as Xml</returns>
        public static byte[] ExportToXmlTableAsBytes(this DataTable table)
        {
            if (table != null)
            {
                string results = table.ExportToXmlTable();
                var ecnoding = new ASCIIEncoding();

                return ecnoding.GetBytes(results);
            }

            return null;
        }
    }
}
