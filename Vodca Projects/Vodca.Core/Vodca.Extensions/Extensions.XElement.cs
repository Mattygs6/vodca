//-----------------------------------------------------------------------------
// <copyright file="Extensions.XElement.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       02/04/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Xml.Linq;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the XElement attribute with null reference checking.
        /// </summary>
        /// <param name="xelement">The XElement instance.</param>
        /// <param name="xname">The XName of the attribute.</param>
        /// <returns>The XElement value or empty string</returns>
        public static string GetAttribute(this XElement xelement, XName xname)
        {
            if (xname != null && xelement != null)
            {
                XAttribute attribute = xelement.Attribute(xname);
                if (attribute != null)
                {
                    return attribute.Value;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the XElement attribute with null reference checking.
        /// </summary>
        /// <param name="xelement">The XElement instance.</param>
        /// <param name="name">The name of attribute.</param>
        /// <returns>The XElement value or empty string</returns>
        public static string GetAttribute(this XElement xelement, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                XName xname = XName.Get(name);

                return xelement.GetAttribute(xname);
            }

            return string.Empty;
        }

        /// <summary>
        /// Sets the or add attribute value.
        /// </summary>
        /// <param name="xelement">The XElement instance.</param>
        /// <param name="xname">The name of attribute.</param>
        /// <param name="value">The value for the attribute.</param>
        public static void SetOrAddAttributeValue(this XElement xelement, XName xname, string value)
        {
            if (xelement != null && xname != null && !string.IsNullOrWhiteSpace(value))
            {
                XAttribute attribute = xelement.Attribute(xname);
                var newvalue = string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
                if (attribute != null)
                {
                    attribute.Value = newvalue;
                }
                else
                {
                    var newattribute = new XAttribute(xname, newvalue);
                    xelement.Add(newattribute);
                }
            }
        }

        /// <summary>
        /// Sets the or add attribute value.
        /// </summary>
        /// <param name="xelement">The XElement instance.</param>
        /// <param name="name">The name of attribute.</param>
        /// <param name="value">The value for the attribute.</param>
        public static void SetOrAddAttributeValue(this XElement xelement, string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value))
            {
                XName xname = XName.Get(name);

                xelement.SetOrAddAttributeValue(xname, value);
            }
        }

        /// <summary>
        /// Sets the or add child element value.
        /// </summary>
        /// <param name="xelement">The XElement.</param>
        /// <param name="name">The child XElement name.</param>
        /// <param name="value">The child XElement value.</param>
        /// <param name="ascdata">if set to <c>true</c> as XCData.</param>
        public static void SetOrAddChildElementValue(this XElement xelement, string name, string value, bool ascdata = false)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value))
            {
                XName xname = XName.Get(name);

                xelement.SetOrAddChildElementValue(xname, value, ascdata);
            }
        }

        /// <summary>
        /// Sets the or add child element value.
        /// </summary>
        /// <param name="xelement">The XElement.</param>
        /// <param name="name">The child XElement name.</param>
        /// <param name="value">The child XElement value.</param>
        /// <param name="ascdata">if set to <c>true</c> as XCData.</param>
        public static void SetOrAddChildElementValue(this XElement xelement, XName name, string value, bool ascdata = false)
        {
            if (xelement != null && name != null && !string.IsNullOrWhiteSpace(value))
            {
                XElement element = xelement.Element(name);
                if (element != null)
                {
                    if (ascdata)
                    {
                        var xcdata = element.FirstNode as XCData;
                        if (xcdata == null)
                        {
                            element.Value = string.Empty;
                            element.AddFirst(new XCData(value));
                        }
                        else
                        {
                            xcdata.Value = value;
                        }
                    }
                    else
                    {
                        element.Value = value;
                    }
                }
                else
                {
                    if (ascdata)
                    {
                        var newelement = new XElement(name, new XCData(value));
                        xelement.Add(newelement);
                    }
                    else
                    {
                        var newelement = new XElement(name, value);
                        xelement.Add(newelement);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the or add child element value.
        /// </summary>
        /// <param name="xelement">The XElement.</param>
        /// <param name="name">The child XElement name.</param>
        /// <param name="value">The child XElement value.</param>
        public static void SetOrAddChildElementValue(this XElement xelement, string name, object value)
        {
            if (xelement != null && !string.IsNullOrWhiteSpace(name) && value.IsNotNull())
            {
                XName xname = XName.Get(name);
                xelement.SetOrAddChildElementValue(xname, value);
            }
        }

        /// <summary>
        /// Sets the or add child element value.
        /// </summary>
        /// <param name="xelement">The XElement.</param>
        /// <param name="name">The child XElement name.</param>
        /// <param name="value">The child XElement value.</param>
        public static void SetOrAddChildElementValue(this XElement xelement, XName name, object value)
        {
            if (xelement != null && name != null && value.IsNotNull())
            {
                XElement element = xelement.Element(name);
                if (element != null)
                {
                    string s = string.Concat(value);
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        element.Value = s;
                    }
                }
                else
                {
                    var newelement = new XElement(name, value);
                    xelement.Add(newelement);
                }
            }
        }

        /// <summary>
        /// Gets the XElement with null reference checking.
        /// </summary>
        /// <param name="xelement">The XElement instance.</param>
        /// <param name="name">The name of element.</param>
        /// <returns>The XElement value or empty string</returns>
        public static string GetChildElementValue(this XElement xelement, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                XName xname = XName.Get(name);

                return xelement.GetChildElementValue(xname);
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the XElement with null reference checking.
        /// </summary>
        /// <param name="xelement">The XElement instance.</param>
        /// <param name="name">The name of element.</param>
        /// <returns>The XElement value or empty string</returns>
        public static string GetChildElementValue(this XElement xelement, XName name)
        {
            if (xelement != null && name != null)
            {
                XElement elemtent = xelement.Element(name);
                if (elemtent != null)
                {
                    return elemtent.Value;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Mimics the jQuery Data functionality, Gets the Data attribute
        /// </summary>
        /// <param name="xelement">The XElement.</param>
        /// <param name="name">The name.</param>
        /// <returns>The data attribute value</returns>
        public static string Data(this XElement xelement, string name)
        {
            if (xelement != null)
            {
                return xelement.GetAttribute("data-" + name);
            }

            return null;
        }

        /// <summary>
        /// Mimics the jQuery Data functionality, Sets the Data attribute
        /// </summary>
        /// <param name="xelement">The XElement.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The XElement instance</returns>
        public static XElement Data(this XElement xelement, string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                string attr = "data-" + name;

                if (string.IsNullOrWhiteSpace(value))
                {
                    return xelement.RemoveAttribute(attr);
                }

                xelement.SetOrAddAttributeValue(attr, value);
            }

            return xelement;
        }

        /// <summary>
        /// Removes the attribute.
        /// </summary>
        /// <param name="xelement">The XElement.</param>
        /// <param name="name">The name.</param>
        /// <returns>The XElement without </returns>
        public static XElement RemoveAttribute(this XElement xelement, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                XName xname = XName.Get(name);

                xelement.RemoveAttribute(xname);
            }

            return xelement;
        }

        /// <summary>
        /// Removes the attribute.
        /// </summary>
        /// <param name="xelement">The XElement.</param>
        /// <param name="xname">The XName.</param>
        /// <returns>The XElement instance</returns>
        public static XElement RemoveAttribute(this XElement xelement, XName xname)
        {
            if (xname != null)
            {
                XAttribute attribute = xelement.Attribute(xname);
                if (attribute != null)
                {
                    attribute.Remove();
                }
            }

            return xelement;
        }
    }
}
