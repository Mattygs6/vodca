//-----------------------------------------------------------------------------
// <copyright file="WellKnownXNames.Attributes.Event.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       12/09/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Xml.Linq;

    /// <summary>
    /// Well known Html event attributes
    /// </summary>
    internal static partial class WellKnownXNames
    {
        /// <summary>
        /// Strongly typed html event onclick
        /// </summary>
        public static readonly XName OnClick = XName.Get("onclick");
    }
}
