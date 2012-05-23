//-----------------------------------------------------------------------------
// <copyright file="IToJContainer.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       02/19/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using Vodca.SDK.Newtonsoft.Json.Linq;

    /// <summary>
    /// Convert object to JContainer
    /// </summary>
    /// <example>View code: <br />
    /// <code lang="C#" title="Example">
    /// <![CDATA[
    /// public JContainer ToJContainer()
    /// {
    ///    var root = new JObject();
    ///
    ///    root.Add(new JProperty("Id", this.Id));
    ///    root.Add(new JProperty("DateCreated", this.DateCreated));
    ///    root.Add(new JProperty("Approved", this.Approved));
    ///    root.Add(new JProperty("TwitterEntryInner", this.TwitterEntryInner.ToJContainer()));
    ///
    ///    return root;
    /// }
    /// ]]>
    /// </code>
    /// </example>
    [CLSCompliant(false)]
    public interface IToJContainer
    {
        /// <summary>
        /// Converts current object data to JObject
        /// </summary>
        /// <returns>The object instance data as JObject</returns>
        [CLSCompliant(false)]
        JContainer ToJContainer();
    }
}