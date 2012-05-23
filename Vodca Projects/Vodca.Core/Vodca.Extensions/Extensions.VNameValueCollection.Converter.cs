//-----------------------------------------------------------------------------
// <copyright file="Extensions.VNameValueCollection.Converter.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.SessionState;

    /// <content>
    ///     This class solves problems related to NameValueCollection serialization.
    /// A name-values collection implementation suitable for web-based collections 
    /// (like server variables, query strings, forms and cookies) that can also
    /// be written and read as XML.
    ///     NameValueCollection does not directly implement the ICollection interface. 
    /// Instead, NameValueCollection extends NameObjectCollectionBase.  
    /// When you use the XMLSerializer, the XmlSerializer tries to serialize or deserialize the NameValueCollection as 
    /// a generic ICollection. Therefore, it looks for the default Add(System.String). 
    /// In the absence of the Add(system.String) method in 3.5 framework, the exception is thrown.
    /// </content>
    public static partial class Extensions
    {
        /// <summary>
        ///     Converts the non-xml serializable NameValueCollection to xml serializable VNameValueCollection
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>The new VNameValueCollection</returns>
        public static VNameValueCollection ToVNameValueCollection(this NameValueCollection collection)
        {
            return new VNameValueCollection(collection);
        }

        /// <summary>
        ///     Copy HttpCookieCollection to XmlSerializable VKeyValue array.
        /// </summary>
        /// <param name="cookies">HTTP cookies.</param>
        /// <returns>The new VNameValueCollection</returns>
        public static VNameValueCollection ToVNameValueCollection(this HttpCookieCollection cookies)
        {
            var collection = new VNameValueCollection();

            if (cookies != null && cookies.Count > 0)
            {
                for (int i = 0; i < cookies.Count; i++)
                {
                    HttpCookie cookie = cookies[i];
                    collection.Add(new VKeyValue
                                       {
                                           Key = cookie.Name,
                                           Value = cookie.Value
                                       });
                }
            }

            return collection;
        }

        /// <summary>
        ///     Gets Browser Cap Info
        /// </summary>
        /// <param name="capabilities">Browser capabilities</param>
        /// <returns>The new VNameValueCollection</returns>
        public static VNameValueCollection ToVNameValueCollection(this HttpCapabilitiesBase capabilities)
        {
            var browsercollection = new VNameValueCollection();

            if (capabilities != null)
            {
                browsercollection.Add(new VKeyValue("Browser", capabilities.Browser));
                browsercollection.Add(new VKeyValue("EcmaScriptVersion", capabilities.EcmaScriptVersion.ToString()));
                browsercollection.Add(new VKeyValue("MajorVersion", capabilities.MajorVersion.ToString(CultureInfo.InvariantCulture)));
                browsercollection.Add(new VKeyValue("MinorVersion", capabilities.MinorVersion.ToString(CultureInfo.InvariantCulture)));
                browsercollection.Add(new VKeyValue("MSDomVersion", capabilities.MSDomVersion.ToString()));
                browsercollection.Add(new VKeyValue("Platform", capabilities.Platform));
                browsercollection.Add(new VKeyValue("Type", capabilities.Type));
                browsercollection.Add(new VKeyValue("Version", capabilities.Version));
                browsercollection.Add(new VKeyValue("W3CDomVersion", capabilities.W3CDomVersion.ToString()));
                browsercollection.Add(new VKeyValue("ActiveXControls", capabilities.ActiveXControls.ToString(CultureInfo.InvariantCulture)));
                browsercollection.Add(new VKeyValue("AOL", capabilities.AOL.ToString(CultureInfo.InvariantCulture)));
                browsercollection.Add(new VKeyValue("Beta", capabilities.Beta.ToString(CultureInfo.InvariantCulture)));
                browsercollection.Add(new VKeyValue("Cookies", capabilities.Cookies.ToString(CultureInfo.InvariantCulture)));
                browsercollection.Add(new VKeyValue("Crawler", capabilities.Crawler.ToString(CultureInfo.InvariantCulture)));
                browsercollection.Add(new VKeyValue("Frames", capabilities.Frames.ToString(CultureInfo.InvariantCulture)));
            }

            return browsercollection;
        }

        /// <summary>
        /// Gets a Application session state
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>The new VNameValueCollection</returns>
        public static VNameValueCollection ToVNameValueCollection(this HttpSessionState session)
        {
            var results = new VNameValueCollection();

            if (session != null)
            {
                NameObjectCollectionBase.KeysCollection keys = session.Keys;
                if (keys.Count > 0)
                {
                    for (int i = 0; i < keys.Count; i++)
                    {
                        results.Add(new VKeyValue(keys[i], string.Concat(session[i])));
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Gets a Application state
        /// </summary>
        /// <param name="applicationstate">The application state.</param>
        /// <returns>The new VNameValueCollection</returns>
        public static VNameValueCollection ToVNameValueCollection(this HttpApplicationState applicationstate)
        {
            var results = new VNameValueCollection();

            if (applicationstate != null)
            {
                for (int i = 0; i < applicationstate.Count; i++)
                {
                    results.Add(new VKeyValue(applicationstate.GetKey(i), string.Concat(applicationstate.Get(i))));
                }
            }

            return results;
        }
    }
}
