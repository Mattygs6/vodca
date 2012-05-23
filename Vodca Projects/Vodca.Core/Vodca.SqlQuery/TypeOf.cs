//-----------------------------------------------------------------------------
// <copyright file="TypeOf.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       12/31/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;

    /// <summary>
    ///      Obtain the System.Type object for a nullable types
    /// </summary>
    /// <remarks>
    ///     The Class Required for Dynamics IReader to handle Sql Nullable types
    /// </remarks>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\TypeOf.cs" title="TypeOf.cs" lang="C#" />
    /// </example>
    [System.Runtime.InteropServices.GuidAttribute("0DD7ADFF-979E-4998-98E5-0155F9140601")]
    internal static class TypeOf
    {
        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type Int32Nullable = typeof(int?);

        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type Int64Nullable = typeof(long?);

        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type BooleanNullable = typeof(bool?);

        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type FloatNullable = typeof(float?);

        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type DoubleNullable = typeof(double?);

        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type ByteNullable = typeof(byte?);

        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type DateTimeNullable = typeof(DateTime?);

        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type GuidNullable = typeof(Guid?);

        /// <summary>
        ///     Obtain the System.Type object for a type
        /// </summary>
        public static readonly Type DecimalNullable = typeof(decimal?);

        /// <summary>
        ///     Obtain the object as a Nullable type
        /// </summary>
        /// <param name="type">The type of DbReader field</param>
        /// <returns>The Nullable type to the corresponding type</returns>
        public static Type GetNullableType(Type type)
        {
            switch (type.Name)
            {
                case "Object":
                case "String":
                    break;

                case "Int32":
                    type = Int32Nullable;
                    break;

                case "Int64":
                    type = Int64Nullable;
                    break;

                case "Boolean":
                    type = BooleanNullable;
                    break;

                case "Double":
                    type = DoubleNullable;
                    break;

                case "DateTime":
                    type = DateTimeNullable;
                    break;

                case "Byte":
                    type = ByteNullable;
                    break;

                case "Guid":
                    type = GuidNullable;
                    break;

                case "Single":
                    type = FloatNullable;
                    break;

                case "Decimal":
                    type = DecimalNullable;
                    break;
            }

            return type;
        }
    }
}
