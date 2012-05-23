//-----------------------------------------------------------------------------
// <copyright file="VReflection.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     Microsoft.Web.Infrastructure.dll
//  Date:       03/10/2012
//-----------------------------------------------------------------------------
// Remarks: the original code was copied from Microsoft.Web.Infrastructure.dll and cleanup
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Security;
    using System.Security.Permissions;

    /// <summary>
    /// The Common reflection utilities
    /// </summary>
    [SecurityCritical]
    public static partial class VReflection
    {
        /// <summary>
        /// Binds the method to delegate.
        /// </summary>
        /// <typeparam name="TDelegate">The type of the delegate.</typeparam>
        /// <param name="methodInfo">The method info.</param>
        /// <returns>The delegate</returns>
        [ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
        public static TDelegate BindMethodToDelegate<TDelegate>(MethodInfo methodInfo) where TDelegate : class
        {
            Type[] typeArray;
            Type type;

            ExtractDelegateSignature(typeof(TDelegate), out typeArray, out type);

            string name = "BindMethodToDelegate_" + methodInfo.Name;
            Type returnType = type;
            Type[] parameterTypes = typeArray;

            var method = new DynamicMethod(name, returnType, parameterTypes, true /* RestrictedSkipVisibility */);

            var ilgenerator = method.GetILGenerator();
            for (int i = 0; i < typeArray.Length; i++)
            {
                ilgenerator.Emit(OpCodes.Ldarg, (short)i);
            }

            ilgenerator.Emit(OpCodes.Callvirt, methodInfo);
            ilgenerator.Emit(OpCodes.Ret);

            return method.CreateDelegate(typeof(TDelegate)) as TDelegate;
        }

        /// <summary>
        /// Finds the constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="isStatic">if set to <c>true</c> [is static].</param>
        /// <param name="argumentTypes">The argument types.</param>
        /// <returns>The constructor info</returns>
        public static ConstructorInfo FindConstructor(Type type, bool isStatic, Type[] argumentTypes)
        {
            ConstructorInfo info = type.GetConstructor(GetBindingFlags(isStatic), null, argumentTypes, null);

            return info;
        }

        /// <summary>
        /// Finds the field.
        /// </summary>
        /// <param name="containingType">Type of the containing.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="isStatic">if set to <c>true</c> [is static].</param>
        /// <returns>The field info</returns>
        public static FieldInfo FindField(Type containingType, string fieldName, bool isStatic)
        {
            FieldInfo field = containingType.GetField(fieldName, GetBindingFlags(isStatic));

            return field;
        }

        /// <summary>
        /// Finds the method.
        /// </summary>
        /// <param name="containingType">Type of the containing.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="isStatic">if set to <c>true</c> [is static].</param>
        /// <param name="argumentTypes">The argument types.</param>
        /// <returns>The method info</returns>
        public static MethodInfo FindMethod(Type containingType, string methodName, bool isStatic, Type[] argumentTypes)
        {
            MethodInfo info = containingType.GetMethod(methodName, GetBindingFlags(isStatic), null, argumentTypes, null);

            return info;
        }

        /// <summary>
        /// Makes the delegate.
        /// </summary>
        /// <typeparam name="T">The entity</typeparam>
        /// <param name="method">The method.</param>
        /// <returns>The delegate to the method</returns>
        public static T MakeDelegate<T>(MethodInfo method) where T : class
        {
            return MakeDelegate<T>(null, method);
        }

        /// <summary>
        /// Makes the delegate.
        /// </summary>
        /// <typeparam name="T">The entity</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="method">The method.</param>
        /// <returns>
        /// The created delegate
        /// </returns>
        public static T MakeDelegate<T>(object target, MethodInfo method) where T : class
        {
            return MakeDelegate(typeof(T), target, method) as T;
        }

        /// <summary>
        /// Makes the delegate.
        /// </summary>
        /// <param name="delegateType">Type of the delegate.</param>
        /// <param name="target">The target.</param>
        /// <param name="method">The method.</param>
        /// <returns>The created delegate</returns>
        [ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
        public static Delegate MakeDelegate(Type delegateType, object target, MethodInfo method)
        {
            return Delegate.CreateDelegate(delegateType, target, method);
        }

        /// <summary>
        /// Makes the fast create delegate.
        /// </summary>
        /// <typeparam name="TInstance">The type of the instance.</typeparam>
        /// <typeparam name="TDelegate">The type of the delegate.</typeparam>
        /// <param name="methodInfo">The method info.</param>
        /// <returns>The method delegate</returns>
        [ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
        public static Func<TInstance, TDelegate> MakeFastCreateDelegate<TInstance, TDelegate>(MethodInfo methodInfo)
            where TInstance : class
            where TDelegate : class
        {
            string name = "FastCreateDelegate_" + methodInfo.Name;
            Type returnType = typeof(TDelegate);
            var parameterTypes = new[] { typeof(TInstance) };

            var method = new DynamicMethod(name, returnType, parameterTypes, true /* skipVisibility */);

            ConstructorInfo constructor = typeof(TDelegate).GetConstructor(new[] { typeof(object), typeof(IntPtr) });
            Debug.Assert(constructor != null, "constructor != null");

            ILGenerator ilgenerator = method.GetILGenerator();
            ilgenerator.Emit(OpCodes.Ldarg_0);
            ilgenerator.Emit(OpCodes.Dup);
            ilgenerator.Emit(OpCodes.Ldvirtftn, methodInfo);

            ilgenerator.Emit(OpCodes.Newobj, constructor);
            ilgenerator.Emit(OpCodes.Ret);

            return (Func<TInstance, TDelegate>)method.CreateDelegate(typeof(Func<TInstance, TDelegate>));
        }

        /// <summary>
        /// Makes the fast new object.
        /// </summary>
        /// <typeparam name="TDelegate">The type of the delegate.</typeparam>
        /// <param name="type">The type.</param>
        /// <returns>The entity instance delegate</returns>
        [ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
        public static TDelegate MakeFastNewObject<TDelegate>(Type type) where TDelegate : class
        {
            Type[] typeArray;
            Type type2;
            ExtractDelegateSignature(typeof(TDelegate), out typeArray, out type2);

            Type type3 = type;

            Type[] argumentTypes = typeArray;
            ConstructorInfo con = FindConstructor(type3, false /* isStatic */, argumentTypes);

            string name = "MakeFastNewObject_" + type.Name;
            Type returnType = type2;
            Type[] parameterTypes = typeArray;

            var method = new DynamicMethod(name, returnType, parameterTypes, true /* RestrictedSkipVisibility */);
            ILGenerator ilgenerator = method.GetILGenerator();
            for (int i = 0; i < typeArray.Length; i++)
            {
                ilgenerator.Emit(OpCodes.Ldarg, (short)i);
            }

            ilgenerator.Emit(OpCodes.Newobj, con);
            ilgenerator.Emit(OpCodes.Ret);

            return method.CreateDelegate(typeof(TDelegate)) as TDelegate;
        }

        /// <summary>
        /// Reads the field.
        /// </summary>
        /// <param name="fieldInfo">The field info.</param>
        /// <param name="target">The target.</param>
        /// <returns>The field value</returns>
        [ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
        public static object ReadField(FieldInfo fieldInfo, object target)
        {
            return fieldInfo.GetValue(target);
        }

        /// <summary>
        /// Writes the field.
        /// </summary>
        /// <param name="fieldInfo">The field info.</param>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        [ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
        public static void WriteField(FieldInfo fieldInfo, object target, object value)
        {
            fieldInfo.SetValue(target, value);
        }

        /// <summary>
        /// Gets the binding flags.
        /// </summary>
        /// <param name="isStatic">if set to <c>true</c> [is static].</param>
        /// <returns>The Binding flags</returns>
        private static BindingFlags GetBindingFlags(bool isStatic)
        {
            return ((isStatic ? BindingFlags.Static : BindingFlags.Instance) | BindingFlags.NonPublic) | BindingFlags.Public;
        }

        /// <summary>
        /// Extracts the delegate signature.
        /// </summary>
        /// <param name="delegateType">Type of the delegate.</param>
        /// <param name="argumentTypes">The argument types.</param>
        /// <param name="returnType">Type of the return.</param>
        private static void ExtractDelegateSignature(Type delegateType, out Type[] argumentTypes, out Type returnType)
        {
            MethodInfo method = delegateType.GetMethod("Invoke", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            argumentTypes = Array.ConvertAll(method.GetParameters(), pInfo => pInfo.ParameterType);

            returnType = method.ReturnType;
        }
    }
}
