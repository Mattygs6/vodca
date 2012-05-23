//-----------------------------------------------------------------------------
// <copyright file="Extensions.Type.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       02/07/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Vodca.SDK.Newtonsoft.Json;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="json">The constructor JSON.</param>
        /// <returns>The instance of the type</returns>
        public static object GetInstance(this Type type, string json = null)
        {
            if (type != null)
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    return type.CreateInstance();
                }

                return JsonConvert.DeserializeObject(json, type);
            }

            return null;
        }

        /// <summary>
        /// Tries the get instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="json">The JSON.</param>
        /// <returns>The instance of the type</returns>
        public static object TryGetInstance(this Type type, string json = null)
        {
            if (type != null)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(json))
                    {
                        return type.CreateInstance();
                    }

                    return JsonConvert.DeserializeObject(json, type);
                }
                catch (Exception exception)
                {
                    exception.LogException();
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="json">The JSON.</param>
        /// <returns>The instance of the type</returns>
        public static object GetInstance<TObject>(this string json) where TObject : class, new()
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return new TObject();
            }

            return JsonConvert.DeserializeObject<TObject>(json);
        }
    }
}