//-----------------------------------------------------------------------------
// <copyright file="VLogErrorCode.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       09/25/2008
//-----------------------------------------------------------------------------
namespace Vodca.Logging
{
    using System;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// Web Error Data Storage
    /// </summary>
    [Serializable]
    public sealed partial class VLogErrorCode : IToXElement
    {
        /// <summary>
        ///     Initializes a new instance of the VLogErrorCode class.
        /// </summary>
        /// <param name="code">Http/Error code</param>
        /// <param name="message">Http/Error message</param>
        public VLogErrorCode(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VLogErrorCode"/> class.
        /// </summary>
        /// <param name="type">The type of the error.</param>
        internal VLogErrorCode(VLogErrorTypes type)
        {
            switch (type)
            {
                case VLogErrorTypes.ServerSideIIS:
                    this.Code = 500;
                    this.ExcludeBrowserCapabilities = true;
                    break;

                case VLogErrorTypes.ServerSideSql:
                    this.Code = 500;
                    this.ExcludeBrowserCapabilities = true;
                    this.ExcludeServerVariables = true;
                    this.ExcludeQueryStringVariables = true;
                    this.ExcludeFormVariables = true;
                    this.ExcludeApplicationStateVariables = true;
                    this.ExcludeCookies = true;
                    this.ExcludeSessionStateVariables = true;
                    this.ExcludeContextItems = true;
                    break;

                case VLogErrorTypes.ClientSide:
                    this.Code = JsException.DefaultExceptionStatusCode;
                    this.ExcludeBrowserCapabilities = false;
                    this.ExcludeServerVariables = true;
                    this.ExcludeQueryStringVariables = true;
                    this.ExcludeFormVariables = true;
                    this.ExcludeApplicationStateVariables = true;
                    this.ExcludeCookies = true;
                    this.ExcludeSessionStateVariables = true;
                    this.ExcludeContextItems = true;
                    break;
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="VLogErrorCode"/> class from being created.
        /// </summary>
        private VLogErrorCode()
        {
        }

        /// <summary>
        /// Gets or sets the type of the action. This property used by .
        /// </summary>
        /// <value>
        /// The type of the action.
        /// </value>
        [XmlAttribute]
        public string ActionType { get; set; }

        /// <summary>
        /// Gets or sets the action type JSON.
        /// </summary>
        /// <value>
        /// The action type JSON.
        /// </value>
        [XmlAttribute]
        public string ActionTypeJson { get; set; }

        /// <summary>
        ///     Gets or sets a value of WebError Message
        /// </summary>
        [XmlAttribute]
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating WebError Type Priority 
        /// </summary>
        [XmlAttribute("Priority")]
        public VLogErrorTypePriority Priority { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating Http Code 
        /// </summary>
        [XmlAttribute]
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether exclude header from logging.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [exclude header]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool ExcludeHeader { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether to exclude Session State Variables from logging
        /// </summary>
        [XmlAttribute]
        public bool ExcludeSessionStateVariables { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether to exclude Application State Variables from logging
        /// </summary>
        [XmlAttribute]
        public bool ExcludeCookies { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether to exclude Application State Variables from logging
        /// </summary>
        [XmlAttribute]
        public bool ExcludeApplicationStateVariables { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether to exclude Form Variables from logging
        /// </summary>
        [XmlAttribute]
        public bool ExcludeFormVariables { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether to exclude Query String Variables from logging 
        /// </summary>
        [XmlAttribute]
        public bool ExcludeQueryStringVariables { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether to exclude Server Variables from Logging
        /// </summary>
        [XmlAttribute]
        public bool ExcludeServerVariables { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [exclude context items].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [exclude context items]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool ExcludeContextItems { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether to exclude error from Logging
        /// </summary>
        [XmlAttribute]
        public bool ExcludeFromLogging { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether to add Browser Capability Section
        /// </summary>
        [XmlAttribute]
        public bool ExcludeBrowserCapabilities { get; set; }

        #region Static operators
        /// <summary>
        ///     The equality operator (==) returns true if the values of its operands are equal, false otherwise.
        /// </summary>
        /// <param name="one">Firsts Object typeof WebErrorCode</param>
        /// <param name="two">Second Object typeof WebErrorCode</param>
        /// <returns>True if the values of its operands are equal</returns>
        public static bool operator ==(VLogErrorCode one, VLogErrorCode two)
        {
#pragma warning disable 183
            if (two is VLogErrorCode && one is VLogErrorCode)
#pragma warning restore 183
            {
                return one.Code == two.Code;
            }

            // Use .NET comparison then one side is NULL
            return object.Equals(one, two);
        }

        /// <summary>
        ///     The equality operator (!=) returns true if the values of its operands are NOT equal, false otherwise.
        /// </summary>
        /// <param name="one">First Object typeof WebErrorCode</param>
        /// <param name="two">Object Object typeof WebErrorCode</param>
        /// <returns>True if the values of its operands are equal</returns>
        public static bool operator !=(VLogErrorCode one, VLogErrorCode two)
        {
#pragma warning disable 183
            if (two is VLogErrorCode && one is VLogErrorCode)
#pragma warning restore 183
            {
                return one.Code != two.Code;
            }

            // Use .NET comparison then one side is NULL
            return !object.Equals(one, two);
        }
        #endregion

        #region IToXElement Members

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <param name="rootname">The root element name.</param>
        /// <returns>
        /// The object instance data as XElement
        /// </returns>
        public XElement ToXElement(string rootname = "VLogErrorCode")
        {
            return new XElement(
                rootname,
                new XElement("ActionType", this.ActionType),
                new XElement("ActionTypeJson", this.ActionTypeJson),
                new XElement("Code", this.Code),
                new XElement("Message", this.Message),
                new XElement("Priority", this.Priority),
                new XElement("ExcludeFromLogging", this.ExcludeFromLogging),
                new XElement("ExcludeServerVariables", this.ExcludeServerVariables),
                new XElement("ExcludeQueryStringVariables", this.ExcludeQueryStringVariables),
                new XElement("ExcludeFormVariables", this.ExcludeFormVariables),
                new XElement("ExcludeApplicationStateVariables", this.ExcludeApplicationStateVariables),
                new XElement("ExcludeCookies", this.ExcludeCookies),
                new XElement("ExcludeSessionStateVariables", this.ExcludeSessionStateVariables),
                new XElement("ExcludeContextItems", this.ExcludeContextItems),
                new XElement("ExcludeHeader", this.ExcludeHeader),
                new XElement("ExcludeBrowserCapabilities", this.ExcludeBrowserCapabilities));
        }

        #endregion

        #region ToString()

        /// <summary>
        ///     The String representation of the object
        /// </summary>
        /// <returns>Object in well-formed XML string form</returns>
        public override string ToString()
        {
            return this.ToXElement().ToString();
        }

        #endregion

        #region Equals, GetHashCode, operator ==, operator !=

        /// <summary>
        ///     Determines whether the specified Object typeof WebErrorCode is equal to the current Object.
        /// </summary>
        /// <param name="obj">The Object to compare with the current Object.</param>
        /// <returns>true if the specified Object is equal to the current Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var weberrorlookuptable = obj as VLogErrorCode;
            return this == weberrorlookuptable;
        }

        /// <summary>
        ///     Serves as a hash function for a particular command, suitable for use
        /// in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>A hash code for the current Object(WebErrorCode).</returns>
        public override int GetHashCode()
        {
            return this.Code.GetHashCode();
        }

        #endregion
    }
}
