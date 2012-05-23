//-----------------------------------------------------------------------------
// <copyright file="VFormTypeCacheManager.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/13/2011
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The VForm Property Cache manager
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\Core\VFormTypeCacheManager.cs" title="VFormTypeCacheManager.cs" lang="C#" />
    /// </example>
    [GuidAttribute("EA41243A-59FA-4D46-90C7-FE93B2AC0E1F")]
    internal sealed partial class VFormTypeCacheManager
    {
        /// <summary>
        /// The Cache Container
        /// </summary>
        private static readonly ConcurrentDictionary<Guid, IOrderedEnumerable<PropertyValidator>> Cache = new ConcurrentDictionary<Guid, IOrderedEnumerable<PropertyValidator>>();

        /// <summary>
        /// Gets the ordered properties.
        /// </summary>
        /// <param name="type">The object to validate instance type.</param>
        /// <returns>The ordered property List </returns>
        public static IEnumerable<PropertyValidator> GetOrderedProperties(Type type)
        {
            IOrderedEnumerable<PropertyValidator> properties;
            if (!Cache.TryGetValue(type.GUID, out properties))
            {
                properties = from randomizedproperty in GetPropertyInfo(type) orderby randomizedproperty.FormValidationOrder ascending select randomizedproperty;

                Cache.TryAdd(type.GUID, properties);
            }

            return properties;
        }

        /// <summary>
        /// Gets the property info.
        /// </summary>
        /// <param name="type">The object to validate instance type.</param>
        /// <returns>
        /// The list of the properties with 'ValidateAttribute' attached
        /// </returns>
        private static IEnumerable<PropertyValidator> GetPropertyInfo(Type type)
        {
            var list = new List<PropertyValidator>();

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.FlattenHierarchy | BindingFlags.Default);

            foreach (PropertyInfo info in properties)
            {
                if (info.CanWrite)
                {
                    var validator = new PropertyValidator(info);

                    foreach (object customattribute in info.GetCustomAttributes(true))
                    {
                        var attribute = customattribute as ValidateAttribute;
                        if (attribute != null)
                        {
                            validator.AddValidateAttribute(attribute);
                        }
                    }

                    list.Add(validator);
                }
            }

            return list.ToArray();
        }
    }
}
