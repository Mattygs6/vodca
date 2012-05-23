//-----------------------------------------------------------------------------
// <copyright file="Extensions.XmlAndXslt.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       08/01/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Caching;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using System.Xml.Xsl;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Load Xml file from Cache if exists, if not will load from file system
        /// </summary>
        /// <param name="virtualPath">The XML virtual path to convert to an application-relative path</param>
        /// <param name="cachetime">The cache time.</param>
        /// <returns>Loaded XmlTextReader</returns>
        /// <remarks>
        ///     The CacheTime parameter works together with CacheDependency (when this resource changes, the cached object becomes obsolete and is removed from the cache.)
        /// </remarks>
        /// <example>View code: <br />
        /// <code title="Usage" lang="C#">
        /// var xml = "/App_Xml/States.xml".LoadXmlTextReader(CacheTime.Normal);
        /// /* Do something */
        /// </code> 
        /// </example>
        /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.XmlAndXslt.cs" title="C# Source File" lang="C#" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The System.Web.Caching.CacheDependency class monitors the dependency relationships so that when any of them changes, the cached item will be automatically removed.")]
        public static XmlTextReader LoadXmlTextReader(this string virtualPath, VCacheTime cachetime = VCacheTime.AboveHigh)
        {
            XmlTextReader xmldoc;

            if (virtualPath.FileExists())
            {
                string xmlabsolutepath = virtualPath.MapPath();
                var cache = HttpRuntime.Cache;

                // Could be the case then context is null like call from Unit Test DLL.
                if (cache != null)
                {
                    string cachekey = string.Concat("LoadXmlTextReader-", xmlabsolutepath);

                    xmldoc = cache[cachekey] as XmlTextReader;
                    if (xmldoc == null)
                    {
                        xmldoc = new XmlTextReader(cachekey);

                        if (cachetime > VCacheTime.None)
                        {
                            cache.Insert(cachekey, xmldoc, new CacheDependency(xmlabsolutepath), DateTime.Now.AddMinutes((double)cachetime), Cache.NoSlidingExpiration);
                        }
                    }
                }
                else
                {
                    xmldoc = new XmlTextReader(xmlabsolutepath);
                }
            }
            else
            {
                throw new HttpException((int)System.Net.HttpStatusCode.NotFound, string.Format("The XML file {0} doesn't exist on file system: ", virtualPath.MapPath()));
            }

            return xmldoc;
        }

        /// <summary>
        /// Load Xml file from Cache if exists, if not will load from file system
        /// </summary>
        /// <param name="virtualPath">The XML virtual path to convert to an application-relative path</param>
        /// <param name="cachetime">The cache time.</param>
        /// <returns>Loaded Xml document</returns>
        /// <remarks>
        ///     The CacheTime parameter works together with CacheDependency (when this resource changes, the cached object becomes obsolete and is removed from the cache.)
        /// </remarks>
        /// <example>View code: <br />
        /// <code title="Usage" lang="C#">
        /// /* CacheTime.Normal equals 20 min */
        /// var xml = "/App_Xml/States.xml".LoadXml(CacheTime.Normal);
        /// /* Do something */
        /// </code> 
        /// </example> 
        /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.XmlAndXslt.cs" title="C# Source File" lang="C#" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The System.Web.Caching.CacheDependency class monitors the dependency relationships so that when any of them changes, the cached item will be automatically removed.")]
        public static XmlDocument LoadXml(this string virtualPath, VCacheTime cachetime = VCacheTime.AboveHigh)
        {
            XmlDocument xmldoc;

            if (virtualPath.FileExists())
            {
                // Converts a virtual path to an application absolute path.
                string xmlabsolutepath = virtualPath.MapPath();

                var cache = HttpRuntime.Cache;

                // Could be the case then context is null like call from Unit Test DLL.
                if (cache != null)
                {
                    string cachekey = string.Concat("LoadXml-", xmlabsolutepath);

                    xmldoc = cache[cachekey] as XmlDocument;

                    if (xmldoc == null)
                    {
                        xmldoc = new XmlDocument();
                        xmldoc.Load(xmlabsolutepath);

                        if (cachetime > VCacheTime.None)
                        {
                            cache.Insert(cachekey, xmldoc, new CacheDependency(xmlabsolutepath), DateTime.Now.AddMinutes((double)cachetime), Cache.NoSlidingExpiration);
                        }
                    }
                }
                else
                {
                    xmldoc = new XmlDocument();
                    xmldoc.Load(xmlabsolutepath);
                }
            }
            else
            {
                throw new HttpException((int)System.Net.HttpStatusCode.NotFound, string.Format("The XML file {0} doesn't exist on file system: ", virtualPath.MapPath()));
            }

            return xmldoc;
        }

        /// <summary>
        /// Load Xml file from Cache if exists, if not will load from file system
        /// </summary>
        /// <param name="virtualPath">The XML virtual path to convert to an application-relative path</param>
        /// <param name="cachetime">The cache time.</param>
        /// <returns>Loaded XPathDocument</returns>
        /// <remarks>
        ///     The CacheTime parameter works together with CacheDependency (when this resource changes, the cached object becomes obsolete and is removed from the cache.)
        /// </remarks>
        /// <example>View code: <br />
        /// <code title="Usage" lang="C#">
        /// /* CacheTime.Normal equals 20 min */
        /// var xml = "/App_Xml/States.xml".LoadXPathDocument(CacheTime.Normal);
        /// /* Do something */
        /// </code> 
        /// </example>
        /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.XmlAndXslt.cs" title="C# Source File" lang="C#" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The System.Web.Caching.CacheDependency class monitors the dependency relationships so that when any of them changes, the cached item will be automatically removed.")]
        public static XPathDocument LoadXPathDocument(this string virtualPath, VCacheTime cachetime = VCacheTime.AboveHigh)
        {
            XPathDocument xmldoc;
            if (virtualPath.FileExists())
            {
                // Converts a virtual path to an application absolute path.
                string xmlabsolutepath = virtualPath.MapPath();

                var cache = HttpRuntime.Cache;

                // Could be the case then context is null like call from Unit Test DLL.
                if (cache != null)
                {
                    string cachekey = string.Concat("LoadXPathDocument-", xmlabsolutepath);

                    xmldoc = cache[cachekey] as XPathDocument;
                    if (xmldoc == null)
                    {
                        xmldoc = new XPathDocument(cachekey);

                        if (cachetime > VCacheTime.None)
                        {
                            cache.Insert(cachekey, xmldoc, new CacheDependency(xmlabsolutepath), DateTime.Now.AddMinutes((double)cachetime), Cache.NoSlidingExpiration);
                        }
                    }
                }
                else
                {
                    xmldoc = new XPathDocument(xmlabsolutepath);
                }
            }
            else
            {
                throw new HttpException((int)System.Net.HttpStatusCode.NotFound, string.Format("The XML file {0} doesn't exist on file system: ", virtualPath.MapPath()));
            }

            return xmldoc;
        }

        /// <summary>
        /// Load Xml file from Cache if exists, if not will load from file system
        /// </summary>
        /// <param name="virtualPath">XSLT file virtual path</param>
        /// <param name="cachetime">The caching time.</param>
        /// <returns>Loaded XPathDocument</returns>
        /// <remarks>
        ///     Default cache time is CacheTime.AboveHigh (1 day) with CacheDependency (when this resource changes, the cached object becomes obsolete and is removed from the cache.)
        /// </remarks>
        /// <example>View code: <br />
        /// <code title="Usage" lang="C#">
        /// /* CacheTime.Normal equals 20 min */
        /// var xml = "/App_Xml/States.xml".LoadXslt(CacheTime.Normal);
        /// /* Do something */
        /// </code> 
        /// </example>
        /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.XmlAndXslt.cs" title="C# Source File" lang="C#" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The System.Web.Caching.CacheDependency class monitors the dependency relationships so that when any of them changes, the cached item will be automatically removed.")]
        public static XslCompiledTransform LoadXslt(this string virtualPath, VCacheTime cachetime = VCacheTime.AboveHigh)
        {
            // Create instance of XstTransform object
            XslCompiledTransform transform;

            if (virtualPath.FileExists())
            {
                string xmlabsolutepath = virtualPath.MapPath();

                var cache = HttpRuntime.Cache;

                // Could be the case then context is null like call from Unit Test DLL.
                if (cache != null)
                {
                    string cachekey = string.Concat("LoadXslt-", xmlabsolutepath);

                    transform = cache[cachekey] as XslCompiledTransform;
                    if (transform == null)
                    {
                        // Create instance of XstTransform object
                        transform = new XslCompiledTransform();
                        transform.Load(xmlabsolutepath);

                        if (cachetime > VCacheTime.None)
                        {
                            cache.Insert(cachekey, transform, new CacheDependency(xmlabsolutepath), DateTime.Now.AddMinutes((double)cachetime), Cache.NoSlidingExpiration);
                        }
                    }
                }
                else
                {
                    transform = new XslCompiledTransform();
                    transform.Load(xmlabsolutepath);
                }
            }
            else
            {
                throw new HttpException((int)System.Net.HttpStatusCode.NotFound, string.Format("The XML file {0} doesn't exist on file system: ", virtualPath.MapPath()));
            }

            return transform;
        }

        /// <summary>
        /// Loads the file.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <param name="cachetime">The cache time.</param>
        /// <returns>The file content</returns>
        public static string LoadTextFile(this string virtualPath, VCacheTime cachetime = VCacheTime.AboveHigh)
        {
            if (!string.IsNullOrWhiteSpace(virtualPath) && virtualPath.FileExists())
            {
                string fileabsolutepath = virtualPath.MapPath();

                var cache = HttpRuntime.Cache;

                string cachekey = string.Concat("LoadFile-", fileabsolutepath);

                var file = cache[cachekey] as string;
                if (string.IsNullOrWhiteSpace(file))
                {
                    file = File.ReadAllText(fileabsolutepath);
                    if (cachetime > VCacheTime.None)
                    {
                        cache.Insert(cachekey, file, new CacheDependency(fileabsolutepath), DateTime.Now.AddMinutes((double)cachetime), Cache.NoSlidingExpiration);
                    }
                }

                return file;
            }

            return string.Empty;
        }

        /// <summary>
        /// Load XElement from Cache if exists, if not will load from file system.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <param name="cachetime">The cache time.</param>
        /// <returns>Loaded XElement</returns>
        /// <remarks>
        ///     The CacheTime parameter works together with CacheDependency (when this resource changes, the cached object becomes obsolete and is removed from the cache.)
        /// </remarks>
        /// <example>View code: <br />
        /// <code title="Usage" lang="C#">
        /// /* CacheTime.Normal equals 20 min */
        /// var xml = "/App_Xml/States.xml".LoadXElement(CacheTime.Normal);
        /// /* Do something */
        /// </code> 
        /// </example>
        /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.XmlAndXslt.cs" title="C# Source File" lang="C#" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The System.Web.Caching.CacheDependency class monitors the dependency relationships so that when any of them changes, the cached item will be automatically removed.")]
        public static XElement LoadXElement(this string virtualPath, VCacheTime cachetime = VCacheTime.AboveHigh)
        {
            XElement root;

            if (virtualPath.FileExists())
            {
                string xmlabsolutepath = virtualPath.MapPath();

                var cache = HttpRuntime.Cache;

                // Could be the case then context is null like call from Unit Test DLL.
                if (cache != null)
                {
                    string cachekey = string.Concat("LoadXElement-", xmlabsolutepath);

                    root = cache[cachekey] as XElement;
                    if (root == null)
                    {
                        root = XElement.Load(xmlabsolutepath);

                        if (cachetime > VCacheTime.None)
                        {
                            cache.Insert(cachekey, root, new CacheDependency(xmlabsolutepath), DateTime.Now.AddMinutes((double)cachetime), Cache.NoSlidingExpiration);
                        }
                    }
                }
                else
                {
                    root = XElement.Load(xmlabsolutepath);
                }
            }
            else
            {
                throw new HttpException((int)System.Net.HttpStatusCode.NotFound, string.Format("The XML file {0} doesn't exist on file system: ", virtualPath.MapPath()));
            }

            return root;
        }

        /// <summary>
        /// Load XDocument from Cache if exists, if not will load from file system.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <param name="cachetime">The cache time.</param>
        /// <returns>Loaded XDocument</returns>
        /// <remarks>
        ///     The CacheTime parameter works together with CacheDependency (when this resource changes, the cached object becomes obsolete and is removed from the cache.)
        /// </remarks>
        /// <example>View code: <br />
        /// <code title="Usage" lang="C#">
        /// /* CacheTime.Normal equals 20 min */
        /// var xml = "/App_Xml/States.xml".LoadXDocument(CacheTime.Normal);
        /// /* Do something */
        /// </code> 
        /// </example>
        /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.XmlAndXslt.cs" title="C# Source File" lang="C#" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The System.Web.Caching.CacheDependency class monitors the dependency relationships so that when any of them changes, the cached item will be automatically removed.")]
        public static XDocument LoadXDocument(this string virtualPath, VCacheTime cachetime = VCacheTime.AboveHigh)
        {
            XDocument root;

            if (virtualPath.FileExists())
            {
                string xmlabsolutepath = virtualPath.MapPath();

                var cache = HttpRuntime.Cache;

                // Could be the case then context is null like call from Unit Test DLL.
                if (cache != null)
                {
                    string cachekey = string.Concat("LoadXElement-", xmlabsolutepath);

                    root = cache[cachekey] as XDocument;
                    if (root == null)
                    {
                        root = XDocument.Load(xmlabsolutepath);

                        if (cachetime > VCacheTime.None)
                        {
                            cache.Insert(cachekey, root, new CacheDependency(xmlabsolutepath), DateTime.Now.AddMinutes((double)cachetime), Cache.NoSlidingExpiration);
                        }
                    }
                }
                else
                {
                    root = XDocument.Load(xmlabsolutepath);
                }
            }
            else
            {
                throw new HttpException((int)System.Net.HttpStatusCode.NotFound, string.Format("The XML file {0} doesn't exist on file system: ", virtualPath.MapPath()));
            }

            return root;
        }
    }
}
