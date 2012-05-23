//-----------------------------------------------------------------------------
// <copyright file="VRegisterRequiredAppSettingByNameAttribute.cs" company="genuine">
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
    ///  The Ensure the app settings are in web.config
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class VRegisterRequiredAppSettingByNameAttribute : VRegisterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VRegisterRequiredAppSettingByNameAttribute"/> class.
        /// </summary>
        /// <param name="appsettingname">The app settings name.</param>
        /// <param name="exceptionmessage">The exception message.</param>
        public VRegisterRequiredAppSettingByNameAttribute(string appsettingname, string exceptionmessage)
        {
            Ensure.IsNotNullOrEmpty(appsettingname, "app setting name");
            Ensure.IsNotNullOrEmpty(exceptionmessage, "exception message");

            this.AppSettingName = appsettingname;
            this.ExceptionMessage = exceptionmessage;
            this.MustRunOnApplicationStartup = true;
        }

        /// <summary>
        /// Gets the name of the app setting.
        /// </summary>
        /// <value>
        /// The name of the app setting.
        /// </value>
        public string AppSettingName { get; private set; }

        /// <summary>
        /// Gets the exception message.
        /// </summary>
        public string ExceptionMessage { get; private set; }
    }
}
