//-----------------------------------------------------------------------------
// <copyright file="VLogError.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Represents a logical application error (as opposed to the actual 
    /// exception it may be representing).
    /// </summary>
    /// <remarks>
    ///  This class will have for ErrorUrl properties as consequence of UrlRewrite/Redirect 
    ///  and for best error analyzing during development/maintenance 
    ///         [ErrorUrl]
    ///         [ErrorRawUrl]
    ///         [ErrorUrlAbsolutePath]
    ///         [ErrorUrlPathAndQuery] 
    /// </remarks>
    [Serializable]
    public abstract partial class VLogError
    {
        /// <summary>
        ///     DateTime then error occurred
        /// </summary>
        private DateTime time = DateTime.UtcNow.ToUniversalTime();

        /// <summary>
        /// Initializes a new instance of the <see cref="VLogError"/> class.
        /// </summary>
        protected VLogError()
        {
            this.ServerTime = DateTime.UtcNow.ToUniversalTime();
            this.ServerTimeTick = this.ServerTime.Ticks;
            this.Id = string.Concat(this.GetType().Name, '/', Guid.NewGuid().ToShortId()).ToLowerInvariant();
            this.ErrorAdditionalData = new Dictionary<string, string>();
        }

        /// <summary>
        ///     Gets or sets a Error Primary Key for Logging
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Gets the WebError type.
        /// </summary>
        /// <value>The error type.</value>
        public string Type
        {
            get
            {
                return this.GetType().FullName;
            }
        }

        /// <summary>
        ///     Gets or sets a Hosts IP Address
        /// </summary>
        public string HostsIpAddress { get; set; }

        /// <summary>
        ///     Gets or sets a Users IP Address
        /// </summary>
        public string UsersIpAddress { get; set; }

        /// <summary>
        ///     Gets or sets date and time (in local time) at which the 
        /// error occurred.
        /// </summary>
        public DateTime ServerTime { get; set; }

        /// <summary>
        /// Gets or sets the server time tick.
        /// </summary>
        /// <value>
        /// The server time tick.
        /// </value>
        public long ServerTimeTick { get; set; }

        /// <summary>
        ///     Gets or sets a page the URI.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "The property Name must mimic ASP.NET property")]
        public string Url { get; set; }

        /// <summary>
        ///     Gets or sets a absolute path of the URI.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "The property Name must mimic ASP.NET property")]
        public string UrlAbsolutePath { get; set; }

        /// <summary>
        ///     Gets or sets a AbsolutePath and Query properties separated by a question mark (?).
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "The property Name must mimic ASP.NET property")]
        public string UrlPathAndQuery { get; set; }

        /// <summary>
        ///     Gets or sets a Error Uri Query
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "The property Name must mimic ASP.NET property")]
        public string UrlQuery { get; set; }

        /// <summary>
        ///     Gets or sets a Error Priority
        /// </summary>
        public VLogErrorTypePriority ErrorPriority { get; set; }

        /// <summary>
        ///     Gets or sets a Error Type
        /// </summary>
        public VLogErrorTypes ErrorType { get; set; }

        /// <summary>
        ///     Gets or sets a brief text describing the error.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     Gets or sets a detailed text describing the error, such as a
        /// stack trace.
        /// </summary>
        public string ErrorDetails { get; set; }

        /// <summary>
        ///     Gets or sets the Error Code. For the ASP.NET errors it will be equal HTTP status code,
        ///     for VLogSqlError Sql number and for client JavaScript message number or user defined.
        /// client the error.
        /// </summary>
        /// <remarks>
        ///     For cases where this value cannot always be reliably determined, 
        /// the value may be reported as zero.
        /// See also http://msdn.microsoft.com/en-us/library/aa937592(SQL.80).aspx
        /// </remarks>
        public int ErrorCode { get; set; }

        /// <summary>
        ///     Gets or sets a link to the help file associated with this exception.
        /// </summary>
        /// <value>The Uniform Resource Name (URN) or Uniform Resource Locator (URL) or message.</value>
        public string ErrorHelp { get; set; }

        /// <summary>
        ///     Gets or sets a collection of key/value pairs that provide additional user-defined information about the exception.
        /// </summary>
        /// <value>a collection of user-defined key/value pairs.</value>
        /// <remarks>The default is an empty collection.</remarks>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "It used for Xml serialization/deserialization")]
        public IDictionary<string, string> ErrorAdditionalData { get; set; }

        /// <summary>
        ///   Serves as a hash function for a particular command, suitable for use
        /// in hashing algorithms and data structures like a SQL search or hash table.  
        /// </summary>
        /// <param name="error">The web error instance</param>
        /// <returns>Hash Code of Url</returns>
        public int GetUrlHashCode(VLogError error)
        {
            return this.Url.ToHashCode();
        }

        /// <summary>
        ///     Serves as a hash function for a particular command, suitable for use
        /// in hashing algorithms and data structures like a SQL search or hash table.
        /// </summary>
        /// <returns>Hash Code Url Absolute Path</returns>
        public int GetUrlAbsolutePathHashCode()
        {
            return this.UrlAbsolutePath.ToHashCode();
        }

        /// <summary>
        ///     Serves as a hash function for a particular command, suitable for use
        /// in hashing algorithms and data structures like a SQL search or hash table.
        /// </summary>
        /// <returns>Hash Code Url Absolute Path</returns>
        public int GetUrlPathAndQueryHashCode()
        {
            return this.UrlPathAndQuery.ToHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Concat(this.ErrorCode, '|', this.ErrorMessage);
        }
    }
}
