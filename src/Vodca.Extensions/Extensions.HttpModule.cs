//-----------------------------------------------------------------------------
// <copyright file="Extensions.HttpModule.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/06/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Finds the running instance of IHttpModule
        /// </summary>
        /// <typeparam name="TObject">The type of the IHttpModule instance</typeparam>
        /// <returns>The instance of IHttpModule otherwise Null</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It's tries to find the Http Module in web.config section and return to the user specified instance")]
        public static TObject FindHttpModule<TObject>() where TObject : class, IHttpModule
        {
            HttpModuleCollection collection = HttpContext.Current.ApplicationInstance.Modules;

            return (from string modulename in collection select collection[modulename]).OfType<TObject>().FirstOrDefault();
        }

        /// <summary>
        ///     Finds the running instance of IHttpModule
        /// </summary>
        /// <typeparam name="TObject">The type of the IHttpModule instance</typeparam>
        /// <param name="modulename">The IHttpModule name in web.config</param>
        /// <returns>The instance of IHttpModule if found otherwise Null </returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It's tries to find the Http Module in web.config section and return to the user specified instance")]
        public static TObject FindHttpModule<TObject>(string modulename) where TObject : class, IHttpModule
        {
            if (!string.IsNullOrWhiteSpace(modulename))
            {
                var module = HttpContext.Current.ApplicationInstance.Modules[modulename] as TObject;

                if (module != null)
                {
                    return module;
                }
            }

            return default(TObject);
        }

        /// <summary>
        ///     Finds the running instance of IHttpModule
        /// </summary>
        /// <typeparam name="TObject">The type of the IHttpModule instance</typeparam>
        /// <param name="applicationinstance">The base class for applications that are defined by the user in the Global.asax file</param>
        /// <param name="modulename">The IHttpModule name in web.config</param>
        /// <returns>The instance of IHttpModule if found otherwise Null </returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It's tries to find the Http Module in web.config section and return to the user specified instance")]
        public static TObject FindHttpModule<TObject>(this HttpApplication applicationinstance, string modulename) where TObject : class, IHttpModule
        {
            if (!string.IsNullOrWhiteSpace(modulename))
            {
                var module = applicationinstance.Modules[modulename] as TObject;
                if (module != null)
                {
                    return module;
                }
            }

            return default(TObject);
        }
    }
}
