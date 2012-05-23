//-----------------------------------------------------------------------------
// <copyright file="Extensions.Session.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Web.SessionState;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Session utility. Use for reference type TObject Object Types (classes).
        /// </summary>
        /// <typeparam name="TObject">Target object type</typeparam>
        /// <param name="session">Provides access to session-state values as well as session-level settings and lifetime management methods</param>
        /// <param name="name">The key name of the session target.</param>
        /// <returns>Session name as class object</returns>
        /// <example>View code: <br />
        /// <![CDATA[
        ///     SettingsClass id = Extensions.ValueAs<SettingsClass>("id");
        /// ]]>
        /// </example> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This method is utility to convert generic object in the session-collection to user specified.")]
        public static TObject ValueAs<TObject>(this HttpSessionState session, string name)
        {
            Ensure.IsNotNullOrEmpty(name, "The name of the item in the session-state collection can't be NULL or Empty!");

            TObject result = default(TObject);

            object value = session[name];

            if (value is TObject)
            {
                result = (TObject)value;
            }

            return result;
        }

        /// <summary>
        ///     Session utility.
        /// </summary>
        /// <param name="session">Provides access to session-state values as well as session-level settings and lifetime management methods</param>
        /// <param name="name">The key name of the session target.</param>
        /// <returns>Session name as Nullable int</returns>
        /// <example>View code: <br />
        ///     int? id = Session.ValueAsInt("id");
        /// </example> 
        public static int? ValueAsInt(this HttpSessionState session, string name)
        {
            return session.ValueAs<int?>(name);
        }

        /// <summary>
        ///     Session utility.
        /// </summary>
        /// <param name="session">Provides access to session-state values as well as session-level settings and lifetime management methods</param>
        /// <param name="name">The key name of the session target.</param>
        /// <returns>Session name as Nullable long</returns>
        /// <example>View code: <br />
        ///     long? id = Session.ValueAsLong("id");
        /// </example> 
        public static long? ValueAsLong(this HttpSessionState session, string name)
        {
            return session.ValueAs<long?>(name);
        }

        /// <summary>
        ///     Session utility.
        /// </summary>
        /// <param name="session">Provides access to session-state values as well as session-level settings and lifetime management methods</param>
        /// <param name="name">The key name of the session target.</param>
        /// <returns>Session name as Nullable double</returns>
        /// <example>View code: <br />
        ///     Double? id = Session.ValueAsDouble("id");
        /// </example> 
        public static double? ValueAsDouble(this HttpSessionState session, string name)
        {
            return session.ValueAs<double?>(name);
        }

        /// <summary>
        ///     Session utility.
        /// </summary>
        /// <param name="session">Provides access to session-state values as well as session-level settings and lifetime management methods</param>
        /// <param name="name">The key name of the session target.</param>
        /// <returns>Session name as Nullable Boolean</returns>
        /// <example>View code: <br />
        ///     bool? id = Session.ValueAsBoolean("iscurrent");
        /// </example> 
        public static bool? ValueAsBoolean(this HttpSessionState session, string name)
        {
            return session.ValueAs<bool?>(name);
        }

        /// <summary>
        ///     Session utility.
        /// </summary>
        /// <param name="session">Provides access to session-state values as well as session-level settings and lifetime management methods</param>
        /// <param name="name">The key name of the session target.</param>
        /// <returns>Session name as Nullable DateTime</returns>
        /// <example>View code: <br />
        ///     DateTime? id = Session.ValueAsDateTime("datetime");
        /// </example> 
        public static DateTime? ValueAsDateTime(this HttpSessionState session, string name)
        {
            return session.ValueAs<DateTime?>(name);
        }

        /// <summary>
        ///     Session utility.
        /// </summary>
        /// <param name="session">Provides access to session-state values as well as session-level settings and lifetime management methods</param>
        /// <param name="name">The key name of the session target.</param>
        /// <returns>Session name as Nullable Guid</returns>
        /// <example>View code: <br />
        ///     Guid? id = Session.ValueAsGuid("guid");
        /// </example> 
        public static Guid? ValueAsGuid(this HttpSessionState session, string name)
        {
            return session.ValueAs<Guid?>(name);
        }
    }
}
