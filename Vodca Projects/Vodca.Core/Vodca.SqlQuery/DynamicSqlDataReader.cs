//-----------------------------------------------------------------------------
// <copyright file="DynamicSqlDataReader.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:         Herbrandson
//  Co-Author:      J.Baltikauskas
//  Date:           12/18/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     Dynamically Adding Load Method from DataSource
    /// </summary>
    /// <typeparam name="TObject">Generic Object</typeparam>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\DynamicSqlDataReader.cs" title="DynamicSqlDataReader.cs" lang="C#" />
    /// </example>
    [GuidAttribute("7BEE9AB7-448B-4F55-BE78-5483BADEE984")]
    internal sealed class DynamicSqlDataReader<TObject>
    {
        /// <summary>
        ///     Delegate for SqlDataReader
        /// </summary>
        private Load handler;

        /// <summary>
        ///     Prevents a default instance of the DynamicSqlDataReader class from being created.
        /// </summary>
        private DynamicSqlDataReader()
        {
        }

        /// <summary>
        ///     The delegate for SqlDataReader
        /// </summary>
        /// <param name="dataRecord">The instance of the SqlDataReader with data</param>
        /// <returns>The generic Object with Load delegate attached</returns>
        private delegate TObject Load(SqlDataReader dataRecord);

        /// <summary>
        ///     Dynamically create and compile method at runtime
        /// </summary>
        /// <param name="dataRecord">IReader with data</param>
        /// <returns>The generic TObject</returns>
        /// <remarks>
        /// <pre>
        /// The method like this will be dynamically create and compile code at runtime.
        /// This would gives the best of both worlds. This code could be dynamic(depends on IReader fields), 
        /// but since it was actually compiled, it would be as fast as classical below.
        /// The downside was that the dynamic code needed to be written using IL (intermediate language) instead of C#.
        /// public Person Load(SqlDataReader reader)
        /// {
        ///     Person person = new Person();
        ///     if (!reader.IsDBNull(0))
        ///     {
        ///         person.ID = (Guid)reader[0];
        ///     }
        ///     if (!reader.IsDBNull(1))
        ///     {
        ///         person.Name = (string)reader[1];
        ///     }
        ///     if (!reader.IsDBNull(2))
        ///     {
        ///         person.Kids = (int)reader[2];
        ///     }
        ///     if (!reader.IsDBNull(3))
        ///     {
        ///         person.Active = (bool)reader[3];
        ///     }
        ///     if (!reader.IsDBNull(4))
        ///     {
        ///         person.DateOfBirth = (DateTime)reader[4];
        ///     }
        ///     return person;
        /// }
        /// </pre>
        /// </remarks>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\DynamicSqlDataReader.cs" title="Ensure.DynamicSqlDataReader.cs" lang="C#" />
        /// </example>
        public static DynamicSqlDataReader<TObject> CreateDynamicMethod(SqlDataReader dataRecord)
        {
            Ensure.IsNotNull(dataRecord, "SqlDataReader dataRecord != null");
            Type type = typeof(TObject);

            var dynamicBuilder = new DynamicSqlDataReader<TObject>();

            var method = new DynamicMethod("DynamicLoadSqlDataReader", MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, type, new[] { DynamicSqlDataReaderProperties.DataRecordType }, type, true);
            ILGenerator generator = method.GetILGenerator();

            // The equivalent in C#
            // Person myPerson;
            LocalBuilder result = generator.DeclareLocal(type);

            // The equivalent in C#
            // myPerson = new Person();
            var constructor = type.GetConstructor(Type.EmptyTypes);

            Ensure.IsTrue(constructor != null, "constructor != null");

            // ReSharper disable AssignNullToNotNullAttribute
            generator.Emit(OpCodes.Newobj, constructor);
            // ReSharper restore AssignNullToNotNullAttribute
            generator.Emit(OpCodes.Stloc, result);

            // Add  System.Collections.Specialized.ListDictionary cache some day
            // ReSharper disable PossibleNullReferenceException
            for (int i = 0; i < dataRecord.FieldCount; i++)
            // ReSharper restore PossibleNullReferenceException
            {
                PropertyInfo propertyInfo = type.GetProperty(
                                                dataRecord.GetName(i),
                                                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.FlattenHierarchy | BindingFlags.Default);

                Label endIfLabel = generator.DefineLabel();

                // Check that TObject has the Setter Property  with the same name as SqlDataReader 
                if (propertyInfo != null)
                {
                    MethodInfo methodinfo = propertyInfo.GetSetMethod();
                    if (methodinfo != null && !methodinfo.IsPrivate)
                    {
                        // The equivalent in C#
                        // if (!mySqlDataReader.IsDBNull(1))
                        // {
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, i);
                        generator.Emit(OpCodes.Callvirt, DynamicSqlDataReaderProperties.IsDbNullMethod);
                        generator.Emit(OpCodes.Brtrue, endIfLabel);

                        // The equivalent in C#
                        // myPerson.Name = (string)mySqlDataReader[1];
                        // OR
                        // myPerson.Age = (byte)mySqlDataReader[1];  
                        // OR
                        // myPerson.ID = (int)mySqlDataReader[1]; 
                        generator.Emit(OpCodes.Ldloc, result);
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, i);
                        generator.Emit(OpCodes.Callvirt, DynamicSqlDataReaderProperties.GetValueMethod);

                        Ensure.IsNotNull(propertyInfo, "propertyInfo.PropertyType");
                        var nullable = propertyInfo.PropertyType.Name.Contains("Nullable") ? TypeOf.GetNullableType(dataRecord.GetFieldType(i)) : dataRecord.GetFieldType(i);

                        /* ReSharper disable AssignNullToNotNullAttribute */
                        Ensure.IsNotNull(nullable, "nullable != null");

                        generator.Emit(OpCodes.Unbox_Any, nullable);
                        /* ReSharper restore AssignNullToNotNullAttribute */

                        generator.Emit(OpCodes.Callvirt, methodinfo);

                        // The equivalent in C#
                        // }
                        generator.MarkLabel(endIfLabel);
                    }
                }
            }

            // The equivalent in C#
            // return myPerson;
            generator.Emit(OpCodes.Ldloc, result);
            generator.Emit(OpCodes.Ret);

            // The code then returns a handler to a delegate. 
            // When this handler is invoked, it calls the dynamically generated code
            Delegate loaddelegate = method.CreateDelegate(typeof(Load));
            dynamicBuilder.handler = (Load)loaddelegate;

            return dynamicBuilder;
        }

        /// <summary>
        ///     Load actual data to the object
        /// </summary>
        /// <param name="dataRecord">The instance of the SqlDataReader with data</param>
        /// <returns>Generic Object with loaded data</returns>
        public TObject Build(SqlDataReader dataRecord)
        {
            return this.handler(dataRecord);
        }
    }
}
