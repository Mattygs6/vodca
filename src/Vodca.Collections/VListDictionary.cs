//-----------------------------------------------------------------------------
// <copyright file="VListDictionary.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//   Date:      04/30/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Specialized;

    /// <summary>
    /// Vodca List Dictionary
    /// </summary>
    public partial class VListDictionary : ListDictionary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VListDictionary"/> class.
        /// </summary>
        public VListDictionary()
            : base(StringComparer.InvariantCultureIgnoreCase)
        {
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException"/>, and a set operation creates a new element with the specified key.
        /// </returns>
        public object this[string key]
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    return base[key];
                }

                return null;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    base[key] = value;
                }
            }
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Vodca.VListDictionary"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator string(VListDictionary response)
        {
            if (response != null)
            {
                return response.SerializeToJson();
            }

            return string.Empty;
        }

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add. The value can be null for reference types.</param>
        public void Add(string key, object value)
        {
            this[key] = value;
        }
    }
}
