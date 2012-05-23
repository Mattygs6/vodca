//-----------------------------------------------------------------------------
// <copyright file="VRegistrationManagerActionAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       02/10/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;

    /// <summary>
    /// The VTaskPipeline Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class VRegistrationManagerActionAttribute : VRegisterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VRegistrationManagerActionAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public VRegistrationManagerActionAttribute(Type type)
            : this(type, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VRegistrationManagerActionAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="json">The JSON.</param>
        public VRegistrationManagerActionAttribute(Type type, string json)
        {
            this.Json = json;
            Ensure.IsNotNull(type, "type");

            this.Method = type.TryGetInstance(this.Json) as IVRegisterAction;
            this.MustRunOnApplicationStartup = true;
        }

        /// <summary>
        /// Gets the constructor JSON.
        /// </summary>
        /// <value>
        /// The JSON.
        /// </value>
        public string Json { get; private set; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IVRegisterAction Method { get; private set; }
    }
}
