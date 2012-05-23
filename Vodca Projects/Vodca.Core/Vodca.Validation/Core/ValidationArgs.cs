//-----------------------------------------------------------------------------
// <copyright file="ValidationArgs.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/01/2011
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    using System;

    /// <summary>
    /// The Validation args
    /// </summary>
    [Serializable]
    public partial class ValidationArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationArgs"/> class.
        /// </summary>
        /// <param name="propertyname">The property name.</param>
        /// <param name="propertyvalue">The property value.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="collection">The instance name value collection for additional operations comparison for emails and another special cases</param>
        /// <remarks>
        /// Use AbortValidationPipeline property to abort Validation Property
        /// </remarks>
        public ValidationArgs(string propertyname, object propertyvalue, VForm instance, VNameValueCollection collection)
        {
            Ensure.IsTrue(!string.IsNullOrWhiteSpace(propertyname), "property name");
            Ensure.IsTrue(propertyname != null, "property value");
            Ensure.IsTrue(instance != null, "instance");
            Ensure.IsTrue(collection != null, "collection");

            this.PropertyName = propertyname;
            this.PropertyValue = propertyvalue;
            this.Instance = instance;
            this.Collection = collection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationArgs"/> class.
        /// </summary>
        /// <param name="args">The validations args.</param>
        public ValidationArgs(ValidationArgs args)
        {
            Ensure.IsNotNull(args, "ValidationArgs");

            this.PropertyName = args.PropertyName;
            this.PropertyValue = args.PropertyValue;
            this.Instance = args.Instance;
            this.Collection = args.Collection;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ValidationArgs"/> class from being created.
        /// </summary>
        private ValidationArgs()
        {
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        public object PropertyValue { get; private set; }

        /// <summary>
        /// Gets the instance object.
        /// </summary>
        public VForm Instance { get; private set; }

        /* ReSharper disable UnusedAutoPropertyAccessor.Global */

        /// <summary>
        /// Gets or sets a value indicating whether [abort validation pipeline].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [abort validation pipeline]; otherwise, <c>false</c>.
        /// </value>
        public bool AbortValidationPipeline { get; set; }

        /// <summary>
        /// Gets the collection.
        /// </summary>
        public VNameValueCollection Collection { get; private set; }

        /* ReSharper restore UnusedAutoPropertyAccessor.Global */
    }
}
