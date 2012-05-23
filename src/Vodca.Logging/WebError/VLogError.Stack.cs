//-----------------------------------------------------------------------------
// <copyright file="VLogError.Stack.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Inspired by:        http://stackoverflow.com/questions/20198/how-does-the-asp-net-yellow-screen-of-death-display-code
//  Date:               10/10/2010
//-----------------------------------------------------------------------------
namespace Vodca.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Web;
    using System.Web.Hosting;

    /// <summary>
    ///     Represents a logical application error (as opposed to the actual exception it may be representing).
    /// </summary>
    public abstract partial class VLogError
    {
        /// <summary>
        /// Gets or sets the stack data.
        /// </summary>
        /// <value>The stack data.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "The .NET Xml serialization")]
        public IDictionary<string, string> ErrorStackData { get; set; }

        /// <summary>
        /// Generates the formatted stack trace.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <remarks>The similar to ASP.NET error</remarks>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SetExceptionAddtionalInformation(Exception exception)
        {
            var exceptionStack = new Stack<Exception>();

            // create exception stack
            for (Exception e = exception; e != null; e = e.InnerException)
            {
                exceptionStack.Push(e);
            }

            if (this.ErrorStackData == null)
            {
                this.ErrorStackData = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            }

            try
            {
                this.ErrorStackData["ExceptionSource"] = exception.GetErrorSource();
                this.ErrorStackData["ExceptionMessage"] = exception.GetErrorMessage();
                this.ErrorStackData["ExceptionTypeName"] = exception.GetErrorTypeName();
                this.ErrorStackData["ExceptionHelpLink"] = exception.HelpLink;

                if (exceptionStack.Count != 0)
                {
                    // render exception type and message
                    Exception ex = exceptionStack.Pop();

                    // Load stack trace
                    var stackTrace = new StackTrace(ex, true);
                    for (int frame = 0; frame < stackTrace.FrameCount; frame++)
                    {
                        StackFrame stackFrame = stackTrace.GetFrame(frame);
                        MethodBase method = stackFrame.GetMethod();
                        Type declaringType = method.DeclaringType;

                        if (declaringType != typeof(Ensure) && stackFrame.GetFileLineNumber() > 0)
                        {
                            this.ErrorStackData["HostingEnvironmentApplicationPhysicalPath"] = HostingEnvironment.ApplicationPhysicalPath;
                            this.ErrorStackData["HttpRuntimeAppDomainAppId"] = HttpRuntime.AppDomainAppId;
                            this.ErrorStackData["EnvironmentMachineName"] = Environment.MachineName;

                            this.ErrorStackData["CurrentCultureDisplayName"] = Thread.CurrentThread.CurrentCulture.DisplayName;

                            if (declaringType != null)
                            {
                                this.ErrorStackData["TypeGUID"] = declaringType.GUID.ToShortId();
                                this.ErrorStackData["TypeFullName"] = declaringType.FullName;
                                this.ErrorStackData["TypeAssemblyQualifiedName"] = declaringType.AssemblyQualifiedName;
                                this.ErrorStackData["TypeNamespace"] = declaringType.Namespace;

                                this.ErrorStackData["AssemblyName"] = Path.GetFileName(declaringType.Assembly.Location);
                            }

                            var csfilename = stackFrame.GetFileName();

                            if (!string.IsNullOrWhiteSpace(csfilename))
                            {
                                this.ErrorStackData["StackFrameFileName"] = stackFrame.GetFileName();
                                this.ErrorStackData["StackFrameFileNameVirtualPath"] = csfilename.Replace(HostingEnvironment.ApplicationPhysicalPath, "/").Replace('\\', '/');
                            }

                            this.ErrorStackData["StackFrameFileLineNumber"] = stackFrame.GetFileLineNumber().ToString(CultureInfo.InvariantCulture);
                            this.ErrorStackData["StackFrameShortFileName"] = Path.GetFileName(stackFrame.GetFileName());

                            var trace = new StringBuilder(64);

                            if (declaringType != null)
                            {
                                trace.Append(declaringType.Namespace).Append('.').Append(declaringType.Name).Append('.').Append(method.Name);
                            }

                            this.ErrorStackData["MethodBaseName"] = trace.ToString();

                            trace.Append('(');

                            // get parameter information 
                            ParameterInfo[] parameters = method.GetParameters();
                            for (int paramIndex = 0; paramIndex < parameters.Length; paramIndex++)
                            {
                                trace.Append(((paramIndex != 0) ? "," : string.Empty) + parameters[paramIndex].ParameterType.Name + " " + parameters[paramIndex].Name);
                            }

                            trace.Append(')');

                            this.ErrorStackData["MethodBaseFullName"] = trace.ToString();

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                VLog.Logger.InfoException("GetExceptionAddtionalInformation", ex);
            }
        }
    }
}
