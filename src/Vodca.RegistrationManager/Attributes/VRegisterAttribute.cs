//-----------------------------------------------------------------------------
// <copyright file="VRegisterAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/10/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// The marker attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    [DebuggerDisplay("Type = {ToString()}, Order = {Order}")]
    public abstract class VRegisterAttribute : Attribute, IVRegisterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VRegisterAttribute"/> class.
        /// </summary>
        protected VRegisterAttribute()
        {
            this.Order = int.MaxValue;
        }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [must run on application startup].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [must run on application startup]; otherwise, <c>false</c>.
        /// </value>
        public bool MustRunOnApplicationStartup { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.GetType().Name;
        }

        /// <summary>
        /// Runs the on application startup.
        /// </summary>
        /// <returns>The flag then to run on application start up or on HttpModule Init method (with httpContext)</returns>
        public virtual bool RunOnApplicationStartup()
        {
            return this.MustRunOnApplicationStartup;
        }
    }
}
