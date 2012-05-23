//-----------------------------------------------------------------------------
// <copyright file="Extensions.Serialize.Json.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Vodca.SDK.Newtonsoft.Json;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Serialize DataView to the JavaScript Notation Object (JSON)
        /// </summary>
        /// <param name="view">Data View to serialize to JSON</param>
        /// <returns>Serialized JSON string</returns>
        public static string SerializeToJson(this DataView view)
        {
            if (view != null)
            {
                if (view.Count > 0)
                {
                    var rows = new List<object[]>();
                    for (int i = 0; i < view.Count; i++)
                    {
                        rows.Add(view[i].Row.ItemArray);
                    }

                    return JsonConvert.SerializeObject(rows);
                }

                return "{}";
            }

            return null;
        }

        /// <summary>
        ///     Serialize DataTable to the JavaScript Notation Object (JSON)
        /// </summary>
        /// <param name="table">Data Table to serialize to JSON</param>
        /// <returns>Serialized JSON string</returns>
        public static string SerializeToJson(this DataTable table)
        {
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    var rows = (from DataRow row in table.Rows select row.ItemArray).ToList();

                    return JsonConvert.SerializeObject(rows);
                }

                return "{}";
            }

            return null;
        }
    }
}
