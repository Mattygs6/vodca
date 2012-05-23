//-----------------------------------------------------------------------------
// <copyright file="Extensions.ToJContainer.cs" company="genuine">
//     Copyright (c) M.Gramolini All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       02/19/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Xml.Linq;
    using Vodca.SDK.Newtonsoft.Json.Linq;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Converts to the JObject.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>The JContainer</returns>
        [CLSCompliant(false)]
        public static JContainer ToJContainer(this IEnumerable target)
        {
            if (target != null)
            {
                var jobjarray = new JArray();

                foreach (var item in target)
                {
                    jobjarray.Add(item.ToJContainer());
                }

                return jobjarray;
            }

            return null;
        }

        /// <summary>
        /// Converts to the JObject.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>The JContainer</returns>
        [CLSCompliant(false)]
        public static JContainer ToJContainer(this ICollection target)
        {
            if (target != null)
            {
                var jobjarray = new JArray();

                foreach (var item in target)
                {
                    jobjarray.Add(item.ToJContainer());
                }

                return jobjarray;
            }

            return null;
        }

        /// <summary>
        /// Converts to the JObject.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>The JContainer</returns>
        [CLSCompliant(false)]
        public static JContainer ToJContainer(this NameValueCollection target)
        {
            if (target != null)
            {
                var jobj = new JObject();

                foreach (var key in target.AllKeys)
                {
                    jobj.Add(new JProperty(key, target[key]));
                }

                return jobj;
            }

            return null;
        }

        /// <summary>
        /// Converts to the JObject.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>The JContainer</returns>
        [CLSCompliant(false)]
        public static JContainer ToJContainer(this XElement target)
        {
            if (target != null)
            {
                var jobj = new JObject();

                foreach (var attr in target.Attributes())
                {
                    jobj.Add(new JProperty(string.Concat('@', attr.Name.LocalName), attr.Value));
                }

                var textNodes = target.Nodes().OfType<XText>();

                foreach (var node in textNodes)
                {
                    string name;
                    switch (node.NodeType)
                    {
                        case System.Xml.XmlNodeType.CDATA:
                            name = "#cdata-section";
                            break;
                        case System.Xml.XmlNodeType.Text:
                            name = "#text";
                            break;
                        default:
                            name = "#other";
                            break;
                    }

                    jobj.Add(new JProperty(name, node.Value));
                }

                var multiresults = target.Elements()
                                    .GroupBy(e => e.Name.LocalName)
                                    .Where(e => e.Count() > 1)
                                    .SelectMany(e => e);

                string prevName = string.Empty;
                JArray jarr = null;

                foreach (var xe in multiresults)
                {
                    if (!xe.Name.LocalName.Equals(prevName))
                    {
                        if (jarr != null && !string.IsNullOrWhiteSpace(prevName))
                        {
                            jobj.Add(new JProperty(prevName, jarr));
                        }

                        jarr = new JArray();
                    }

                    if (jarr != null)
                    {
                        jarr.Add(xe.ToJContainer());
                    }

                    prevName = xe.Name.LocalName;
                }

                if (jarr != null && !string.IsNullOrWhiteSpace(prevName))
                {
                    jobj.Add(new JProperty(prevName, jarr));
                }

                var distinctresults = target.Elements()
                                        .GroupBy(e => e.Name.LocalName)
                                        .Where(e => e.Count() == 1)
                                        .SelectMany(e => e);

                foreach (var xe in distinctresults)
                {
                    jobj.Add(new JProperty(xe.Name.LocalName, xe.ToJContainer()));
                }

                return jobj;
            }

            return null;
        }

        /// <summary>
        /// Converts to the JObject.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>The JContainer</returns>
        [CLSCompliant(false)]
        public static JContainer ToJContainer(this object target)
        {
            if (target != null)
            {
                var obj = target as IToJContainer;
                if (obj != null)
                {
                    return obj.ToJContainer();
                }

                return JObject.FromObject(target);
            }

            return null;
        }
    }
}