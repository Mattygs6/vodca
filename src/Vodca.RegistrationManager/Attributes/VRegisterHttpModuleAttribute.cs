//-----------------------------------------------------------------------------
// <copyright file="VRegisterHttpModuleAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/10/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Web;

    /// <summary>
    /// The VTaskPipeline Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class VRegisterHttpModuleAttribute : VRegisterAttribute, IVAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VRegisterHttpModuleAttribute"/> class.
        /// </summary>
        /// <param name="httpmoduletype">The HttpModule type.</param>
        public VRegisterHttpModuleAttribute(Type httpmoduletype)
        {
            Ensure.IsNotNull(httpmoduletype, "httpmoduletype");

            var inherits = httpmoduletype.GetInterface(typeof(IHttpModule).FullName);
            if (inherits == null)
            {
                throw new VHttpArgumentException("The type not implements the IHttpModule interface");
            }

            this.ActionType = httpmoduletype;
            this.Order = byte.MaxValue;
            this.MustRunOnApplicationStartup = true;
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
        string IVAction.ActionTypeJson
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}
