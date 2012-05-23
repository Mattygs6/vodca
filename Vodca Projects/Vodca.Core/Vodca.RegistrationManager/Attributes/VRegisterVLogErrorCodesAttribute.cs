//-----------------------------------------------------------------------------
// <copyright file="VRegisterVLogErrorCodesAttribute.cs" company="genuine">
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
    /// The VLog Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class VRegisterVLogErrorCodesAttribute : VRegisterAttribute, IVAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VRegisterVLogErrorCodesAttribute"/> class.
        /// </summary>
        /// <param name="executabletype">The executable type.</param>
        public VRegisterVLogErrorCodesAttribute(Type executabletype)
            : this(executabletype, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VRegisterVLogErrorCodesAttribute"/> class.
        /// </summary>
        /// <param name="executabletype">The class type to execute.</param>
        /// <param name="json">The JSON.</param>
        public VRegisterVLogErrorCodesAttribute(Type executabletype, string json)
        {
            Ensure.IsNotNull(executabletype, "executable type");
            this.ActionTypeJson = json;

            this.Instance = executabletype.GetInstance(json) as IVExecute;
            Ensure.IsNotNull(this.Instance, "The type not implements the IVExecute interface");

            this.ActionType = executabletype;
            this.Order = byte.MaxValue;
        }

        /// <summary>
        /// Gets the type of the HTTP module.
        /// </summary>
        /// <value>
        /// The type of the HTTP module.
        /// </value>
        public Type ActionType { get; private set; }

        /// <summary>
        /// Gets the constructor JSON.
        /// </summary>
        /// <value>
        /// The JSON.
        /// </value>
        public string ActionTypeJson { get; private set; }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public IVExecute Instance { get; private set; }
    }
}
