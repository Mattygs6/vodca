//-----------------------------------------------------------------------------
// <copyright file="ValidateAttribute.Methods.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/01/2011
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    using System;
#if DEBUG   
    using System.Linq;
    using System.Reflection;
#endif
    using System.Web;

    /// <content>
    ///     Abstract class for all validation attributes.
    /// </content>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\Core\Validate.Attribute.Methods.cs" title="Validate.Attribute.Methods.cs" lang="C#" />
    /// </example>
    public abstract partial class ValidateAttribute
    {
        /// <summary>
        /// Failed the condition.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <remarks>This method throws and catches the Http exception. This way all stack will </remarks>
        protected static void LogFailedCondition(string message)
        {
#if !DEBUG
            try
            {
#endif
                throw new HttpException(message);
#if !DEBUG
            }
            catch (HttpException exception)
            {
                Vodca.VLog.LogException(exception);
            }
#endif
        }

        /// <summary>
        /// Compare two property values as strings.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="comparepropertyname">The compare property name.</param>
        /// <param name="args">The validation args.</param>
        /// <param name="caseinsensitive">if set to <c>true</c> [case insensitive].</param>
        /// <returns>
        /// The true if strings are equals, otherwise false
        /// </returns>
        protected bool CompareValueAsStrings(string input, string comparepropertyname, ValidationArgs args, bool caseinsensitive = true)
        {
#if DEBUG
            /* Development only: Do with reflection to ensure property exists */
            Type type = args.Instance.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.FlattenHierarchy | BindingFlags.Default);
            PropertyInfo propertyinfo = (from property in properties where string.Equals(property.Name, comparepropertyname, StringComparison.OrdinalIgnoreCase) select property).FirstOrDefault();
            Ensure.IsTrue(propertyinfo != null, string.Concat("The Property '", comparepropertyname, "' not found to compare!"));
#endif

            string valuetocompare = args.Collection[comparepropertyname];

            if (caseinsensitive)
            {
                return string.Equals(input, valuetocompare, StringComparison.OrdinalIgnoreCase);
            }

            return string.Equals(input, valuetocompare);
        }
    }
}