//-----------------------------------------------------------------------------
// <copyright file="Extensions.AjaxWebservice.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    /// <content>
    ///     A robust templating solution for lightweight AJAX.
    /// The four steps required to accomplish this will be: 
    ///         building the user control, 
    ///         rendering the control as HTML, 
    ///         providing progress indication, 
    ///         and using ASP.NET AJAX to request and inject that HTML.      
    /// </content>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This is library of extensions methods!")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Loads a System.Web.UI.Control object from a file based on a specified virtual path.
        /// </summary>
        /// <typeparam name="TObject">The object inherited from UserControl</typeparam>
        /// <param name="virtualpath">The virtual Path to the control</param>
        /// <returns>The loaded control instance</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// <![CDATA[
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string LoadHelloWorldControlWithParams()
        /// {
        ///     HelloWorld world = Extensions.LoadControl<HelloWorld>("/Examples/AjaxWebServices/HelloWorld.ascx");
        ///     world.UserName = "Jimbo Jet"; // Set the property
        ///     return Extensions.RenderControlAsHtml(world);
        /// }   
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It's load the user control from provided path")]
        public static TObject LoadControl<TObject>(this string virtualpath) where TObject : UserControl
        {
            if (virtualpath.FileExists() && virtualpath.EndsWith(".ascx"))
            {
                // Create a new Page and add the control to it.
                using (var page = new Page())
                {
                    page.EnableViewState = false;
                    page.Controls.Clear();

                    return page.LoadControl(virtualpath) as TObject;
                }
            }

            return null;
        }

        /// <summary>
        /// Renders the control as HTML.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>The control html</returns>
        public static string RenderControlAsHtml(this Control control)
        {
            return RenderControlAsHtml(new[] { control });
        }

        /// <summary>
        ///     Renders a control to a string - useful for AJAX partials.
        /// Works only with non-post back controls
        /// </summary>
        /// <param name="controls">Instance of the any ASP.NET control</param>
        /// <returns>Return rendered Control HTML, as a string.</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// /* Declaration: */
        ///     LiteralControl one = new LiteralControl("Hello world");
        ///     UserControl usercontrol = this.Page.LoadControl("/App_UserControls/WebUserControl1.ascx");
        ///     .....
        ///     LiteralControl last = new LiteralControl("Last Control");
        ///  /* Call:  */ 
        ///     string str = Extensions.RenderControlAsHtml(one);
        ///         OR    
        ///     string str = Extensions.RenderControlAsHtml(one, usercontrol, ..., last);
        /// </code>
        /// </example>
        public static string RenderControlAsHtml(this IEnumerable<Control> controls)
        {
            if (controls != null)
            {
                var html = new StringBuilder(1024);
                StringWriter stringwriter = null;
                try
                {
                    stringwriter = new StringWriter(html);

                    var xhtmltextwriter = new XhtmlTextWriter(stringwriter);

                    // Create a new Page and add the control to it.
                    using (var page = new Page())
                    {
                        page.EnableViewState = false;
                        page.Controls.Clear();
                        foreach (Control item in controls)
                        {
                            page.Controls.Add(item);
                        }

                        HttpContext.Current.Server.Execute(page, xhtmltextwriter, false);
                    }
                }
                finally
                {
                    if (stringwriter != null)
                    {
                        stringwriter.Dispose();
                    }
                }

                return html.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        ///     Renders a control to a string - useful for AJAX partials.
        /// Works only with non-post back controls
        /// </summary>
        /// <param name="virtualpath">The virtual Path to the control</param>
        /// <returns> Return rendered Control HTML, as a string.</returns>
        /// <example>View code: <br />
        ///      string str = Extensions.RenderControlAsHtml("/App_UserControls/WebUserControl1.ascx");
        /// </example>
        public static string RenderControlAsHtml(this string virtualpath)
        {
            if (!string.IsNullOrWhiteSpace(virtualpath) && virtualpath.FileExists() && virtualpath.EndsWith(".ascx"))
            {
                var html = new StringBuilder(1024);
                StringWriter stringwriter = null;
                try
                {
                    stringwriter = new StringWriter(html);

                    var xhtmltextwriter = new XhtmlTextWriter(stringwriter);

                    // Create a new Page and add the control to it.
                    using (var page = new Page())
                    {
                        page.EnableViewState = false;
                        page.Controls.Clear();
                        page.Controls.Add(page.LoadControl(virtualpath));

                        HttpContext.Current.Server.Execute(page, xhtmltextwriter, false);
                    }
                }
                finally
                {
                    if (stringwriter != null)
                    {
                        stringwriter.Dispose();
                    }
                }

                return html.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        ///     Renders a control to a string - useful for AJAX partials.
        /// Works only with non-post back controls
        /// </summary>
        /// <param name="usercontroltype">The type of the control</param>
        /// <param name="parameters"> 
        ///     An array of arguments that match in number, order, and type the parameters
        /// of the constructor to invoke. If parameters is an empty array or null, the
        /// constructor that takes no parameters (the default constructor) is invoked.
        /// </param>
        /// <returns> Return rendered Control HTML, as a string.</returns>
        /// <example>View code: <br />
        ///      string str = Extensions.RenderControlAsHtml(typeof(WebUserControl1), new object[]{someid , someday }");
        /// </example>
        public static string RenderControlAsHtml(Type usercontroltype, object[] parameters)
        {
            if (usercontroltype != null && parameters != null)
            {
                var html = new StringBuilder(1024);

                StringWriter stringwriter = null;
                try
                {
                    stringwriter = new StringWriter(html);

                    var xhtmltextwriter = new XhtmlTextWriter(stringwriter);

                    // Create a new Page and add the control to it.
                    using (var page = new Page())
                    {
                        page.EnableViewState = false;
                        page.Controls.Clear();
                        page.Controls.Add(page.LoadControl(usercontroltype, parameters));

                        HttpContext.Current.Server.Execute(page, xhtmltextwriter, false);
                    }
                }
                finally
                {
                    if (stringwriter != null)
                    {
                        stringwriter.Dispose();
                    }
                }

                return html.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        ///     Renders a control to a string - useful for AJAX partials.
        /// Works only with non-post back controls
        /// </summary>
        /// <param name="control">Instance of the any ASP.NET control</param>
        /// <returns> Return rendered Control HTML, as a string.</returns>
        /// <example>View code: <br />
        /// <code lang="C#">
        ///  /* Declaration: */
        ///     LiteralControl one = new LiteralControl("Hello world");
        ///     UserControl usercontrol = this.Page.LoadControl("~/App_UserControls/WebUserControl1.ascx");
        ///  /* Call: */  
        ///     string str = one.RenderControlAsJson();
        ///         OR    
        ///     string str = usercontrol.RenderControlAsJson();
        /// </code>
        /// </example>
        public static string RenderControlAsJson(this Control control)
        {
            return JavaScriptEncode(RenderControlAsHtml(control));
        }

        /// <summary>
        ///     Renders a control to a string - useful for AJAX partials.
        /// Works only with non-post back controls
        /// </summary>
        /// <param name="controls">Instance of the any ASP.NET control</param>
        /// <returns> Return rendered Control HTML, as a string.</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// /* Declaration: */
        ///     LiteralControl one = new LiteralControl("Hello world");
        ///     UserControl usercontrol = this.Page.LoadControl("~/App_UserControls/WebUserControl1.ascx");
        /// /* Examples: */  
        ///     string str = Extensions.RenderControlAsJson(one);
        ///         OR    
        ///     string str = Extensions.RenderControlAsJson(one, usercontrol);
        /// </code>   
        /// </example>
        public static string RenderControlAsJson(params Control[] controls)
        {
            return JavaScriptEncode(RenderControlAsHtml(controls));
        }

        /// <summary>
        ///     Renders a control to a JSON string - useful for AJAX partials.
        /// Works only with non-post back controls
        /// </summary>
        /// <param name="virtualpath">The virtual Path to the control</param>
        /// <returns> Return rendered Control HTML, as a JSON string.</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        ///     string json = Extensions.RenderControlAsJson("/App_UserControls/WebUserControl1.ascx");
        /// </code>   
        /// </example>
        public static string RenderControlAsJson(string virtualpath)
        {
            return JavaScriptEncode(RenderControlAsHtml(virtualpath));
        }
    }
}
