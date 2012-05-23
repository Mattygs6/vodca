//-----------------------------------------------------------------------------
// <copyright file="VRegisterApiControllerAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//   Date:      03/29/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;

    /// <summary>
    /// The Web API controller registration attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class VRegisterApiControllerAttribute : VRegisterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VRegisterApiControllerAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public VRegisterApiControllerAttribute(Type type)
            : this(type, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VRegisterApiControllerAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="json">The JSON.</param>
        public VRegisterApiControllerAttribute(Type type, string json)
        {
            this.Json = json;
            Ensure.IsNotNull(type, "type");
            this.Type = type;
        }

        /// <summary>
        /// Gets the constructor JSON.
        /// </summary>
        /// <value>
        /// The JSON.
        /// </value>
        public string Json { get; private set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type { get; private set; }
    }
}
