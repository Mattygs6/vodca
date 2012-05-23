//-----------------------------------------------------------------------------
// <copyright file="VForm.DelegatesAndEvents.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       10/21/2011
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public abstract partial class VForm
    {
        // ReSharper disable EventNeverInvoked

        /// <summary>
        /// Gets or sets the before validation start.
        /// </summary>
        /// <value>
        /// The before validation start.
        /// </value>
        protected Action<VForm, EventArgs> BeforeValidationStart { get; set; }

        /// <summary>
        /// Gets or sets the after validation end.
        /// </summary>
        /// <value>
        /// The after validation end.
        /// </value>
        protected Action<VForm, EventArgs> AfterValidationEnd { get; set; }

        /// <summary>
        /// Gets or sets the after validation arguments initialized.
        /// </summary>
        /// <value>
        /// The after validation arguments initialized.
        /// </value>
        protected Action<VForm, ValidationArgs> AfterValidationArgInit { get; set; }

        // ReSharper restore EventNeverInvoked
    }
}