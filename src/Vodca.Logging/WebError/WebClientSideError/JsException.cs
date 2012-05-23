//-----------------------------------------------------------------------------
// <copyright file="JsException.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Xml.Linq;
    using Logging;

    /// <summary>
    ///     The error occurred on the client side
    /// </summary>
    [Serializable]
    public partial class JsException : Exception, IToXElement, IJsException
    {
        /// <summary>
        ///     The default exception status code
        /// </summary>
        public const int DefaultExceptionStatusCode = 1000;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsException"/> class.
        /// </summary>
        public JsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsException"/> class.
        /// </summary>
        /// <param name="proxy">The proxy class.</param>
        internal JsException(IJsException proxy)
        {
            Ensure.IsNotNull(proxy, "proxy");
            this.ErrorDescription = proxy.ErrorDescription;
            this.ErrorLineNumber = proxy.ErrorLineNumber;
            this.ErrorName = proxy.ErrorName;
            this.ErrorNumber = proxy.ErrorNumber;
            this.ErrorUrl = proxy.ErrorUrl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        /// </exception>
        protected JsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            this.ErrorUrl = info.GetString("ErrorUrl");
            this.ErrorLineNumber = info.GetInt32("ErrorLineNumber");
            this.ErrorDescription = info.GetString("ErrorDescription");
            this.ErrorNumber = info.GetInt32("ErrorNumber");
            this.ErrorName = info.GetString("ErrorName");
        }

        /// <summary>
        ///     Gets or sets a Client Error ErrorUrl
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "The raw ErrorUrl is fine here")]
        public string ErrorUrl { get; set; }

        /// <summary>
        ///     Gets or sets a Client Error Line Number
        /// </summary>
        public int? ErrorLineNumber { get; set; }

        /// <summary>
        ///     Gets or sets a Client Error Optional description
        /// </summary>
        public string ErrorDescription { get; set; }

        /// <summary>
        ///     Gets or sets the JavaScript or WebUser defined Error Number
        /// </summary>
        /// <remarks>
        ///     Default is generic user Defined JavaScript Error
        /// </remarks>
        public int? ErrorNumber { get; set; }

        /// <summary>
        ///     Gets or sets a Error Name from try catch block
        /// </summary>
        public string ErrorName { get; set; }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic).
        /// </exception>
        /// <PermissionSet>
        ///     <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/>
        ///     <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter"/>
        /// </PermissionSet>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            base.GetObjectData(info, context);

            info.AddValue("ErrorUrl", this.ErrorUrl);
            info.AddValue("ErrorLineNumber", this.ErrorLineNumber);
            info.AddValue("ErrorDescription", this.ErrorDescription);
            info.AddValue("ErrorNumber", this.ErrorNumber);
            info.AddValue("ErrorName", this.ErrorName);
        }

        #region IToXElement Members

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <param name="rootname">The root element name.</param>
        /// <returns>
        /// The object instance data as XElement
        /// </returns>
        public XElement ToXElement(string rootname = "JsException")
        {
            return new XElement(
                         rootname,
                         new XElement("Url", this.ErrorUrl),
                         new XElement("LineNumber", this.ErrorLineNumber),
                         new XElement("Message", this.Message),
                         new XElement("Description", this.ErrorDescription),
                /* ReSharper disable AssignNullToNotNullAttribute */
                         new XAttribute("ErrorNumber", this.ErrorNumber),
                /* ReSharper restore AssignNullToNotNullAttribute */
                         new XElement("ErrorName", this.ErrorName));
        }

        #endregion

        /// <summary>
        ///     The String representation of the object
        /// </summary>
        /// <returns>Object in well-formed XML string form</returns>
        public override string ToString()
        {
            return this.ToXElement().ToString();
        }

        /// <summary>
        ///     Serves as a hash function for a particular command, suitable for use
        /// in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>A hash code for the current Object.</returns>
        public override int GetHashCode()
        {
            return string.Concat("JsException-", this.ErrorUrl, this.ErrorLineNumber, this.Message, this.ErrorNumber).GetHashCode();
        }
    }
}
