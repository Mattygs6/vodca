//-----------------------------------------------------------------------------
// <copyright file="VRegisterRequiredConnectionStringByNameAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/10/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;

    /// <summary>
    /// The Register WebResource Attribute (copies to the file system)
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class VRegisterRequiredConnectionStringByNameAttribute : VRegisterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VRegisterRequiredConnectionStringByNameAttribute"/> class.
        /// </summary>
        /// <param name="connectionstringname">The connection string name.</param>
        /// <param name="exceptionmessage">The exception message.</param>
        public VRegisterRequiredConnectionStringByNameAttribute(string connectionstringname, string exceptionmessage)
        {
            Ensure.IsNotNullOrEmpty(connectionstringname, "connectionstring");

            this.ConnectionStringName = connectionstringname;
            this.ExceptionMessage = exceptionmessage;
            this.MustRunOnApplicationStartup = true;
        }

        /// <summary>
        /// Gets the name of the connection string.
        /// </summary>
        /// <value>
        /// The name of the connection string.
        /// </value>
        public string ConnectionStringName { get; private set; }

        /// <summary>
        /// Gets the exception message.
        /// </summary>
        public string ExceptionMessage { get; private set; }
    }
}
