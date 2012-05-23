//-----------------------------------------------------------------------------
// <copyright file="AssertionConditionAttribute.cs" company="JetBrains">
//     Copyright (c) JetBrains. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Vodca.Annotations
{
    using System;

    /// <summary>
    /// Indicates the condition parameter of the assertion method. 
    /// The method itself should be marked by <see cref="AssertionMethodAttribute"/> attribute.
    /// The mandatory argument of the attribute is the assertion type.
    /// </summary>
    /// <seealso cref="AssertionConditionType"/>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class AssertionConditionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionConditionAttribute"/> class.
        /// </summary>
        /// <param name="conditionType">Specifies condition type</param>
        public AssertionConditionAttribute(AssertionConditionType conditionType)
        {
            this.ConditionType = conditionType;
        }

        /// <summary>
        /// Gets the type of the condition.
        /// </summary>
        /// <value>
        /// The type of the condition.
        /// </value>
        public AssertionConditionType ConditionType { get; private set; }
    }
}