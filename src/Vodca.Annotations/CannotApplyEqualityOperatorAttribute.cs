//-----------------------------------------------------------------------------
// <copyright file="CannotApplyEqualityOperatorAttribute.cs" company="JetBrains">
//     Copyright (c) JetBrains. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Vodca.Annotations
{
    using System;

    /// <summary>
    /// Indicates that the value of marked type (or its derivatives) cannot be compared using '==' or '!=' operators.
    /// There is only exception to compare with <c>null</c>, it is permitted
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public sealed class CannotApplyEqualityOperatorAttribute : Attribute
    {
    }
}