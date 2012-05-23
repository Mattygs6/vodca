//-----------------------------------------------------------------------------
// <copyright file="GoogleAnalyticsControl.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       04/20/2011
//-----------------------------------------------------------------------------
namespace Vodca.Controls
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.Adapters;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Google Analytics Control
    /// </summary>
    [DefaultProperty("Account")]
    [ParseChildren(false), PersistChildren(false)]
    [ControlBuilder(typeof(LiteralControlBuilder))]
    public sealed partial class GoogleAnalyticsControl : Control
    {
        /// <summary>
        /// The Google Js code
        /// </summary>
        private const string JsTemplate = @"   
<script type=""text/javascript"">
    var _gaq = _gaq || [];
    _gaq.push(['_setAccount', '#ACCOUNT#']);
    #SUBDOMAIN#
    _gaq.push(['_trackPageview']);

    (function () {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
    })();
</script>
";

        /// <summary>
        /// Gets or sets the Google account.
        /// </summary>
        /// <value>
        /// The Google account.
        /// </value>
        [Bindable(false)]
        [Category("Google Account")]
        [DefaultValue("")]
        [Localizable(false)]
        public string Account { get; set; }

        /// <summary>
        /// Gets or sets the sub domain.
        /// </summary>
        /// <value>
        /// The sub domain.
        /// </value>
        [Bindable(false)]
        [Category("Google Account")]
        [Localizable(false)]
        public string SubDomain { get; set; }

        // ReSharper disable UnusedMember.Local

        /// <summary>
        /// Applies the style properties defined in the page style sheet to the control.
        /// </summary>
        /// <param name="page">The <see cref="T:System.Web.UI.Page"/> containing the control.</param>
        /// <exception cref="T:System.InvalidOperationException">The style sheet is already applied.</exception>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.UI.XControl\XControl.DesignerAndDebug.cs" title="XControl.DesignerAndDebug.cs" lang="C#" />
        /// </example>
        public override void ApplyStyleSheetSkin(Page page)
        {
            // Don't do anything
        }

        /// <summary>
        /// Determines if the server control contains any child controls.
        /// </summary>
        /// <returns>
        /// true if the control contains other controls; otherwise, false.
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.UI.XControl\XControl.DesignerAndDebug.cs" title="XControl.DesignerAndDebug.cs" lang="C#" />
        /// </example>
        public override bool HasControls()
        {
            return false;
        }

        /// <summary>
        /// Searches the current naming container for a server control with the specified <paramref name="id"/> parameter.
        /// </summary>
        /// <param name="id">The identifier for the control to be found.</param>
        /// <returns>
        /// The specified control, or null if the specified control does not exist.
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.UI.XControl\XControl.DesignerAndDebug.cs" title="XControl.DesignerAndDebug.cs" lang="C#" />
        /// </example>
        public override Control FindControl(string id)
        {
            throw new InvalidOperationException("The Method is not supported!");
        }

        /// <summary>
        /// Binds a data source to the invoked server control and all its child controls.
        /// </summary>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.UI.XControl\XControl.DesignerAndDebug.cs" title="XControl.DesignerAndDebug.cs" lang="C#" />
        /// </example>
        public override void DataBind()
        {
            throw new InvalidOperationException("The Method is not supported!");
        }

        /// <summary>
        /// Sets input focus to a control.
        /// </summary>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.UI.XControl\XControl.DesignerAndDebug.cs" title="XControl.DesignerAndDebug.cs" lang="C#" />
        /// </example>
        public override void Focus()
        {
            throw new InvalidOperationException("The Method is not supported!");
        }

        /// <summary>
        /// Binds a data source to the invoked server control and all its child controls with an option to raise the <see cref="E:System.Web.UI.Control.DataBinding"/> event.
        /// </summary>
        /// <param name="raiseOnDataBinding">true if the <see cref="E:System.Web.UI.Control.DataBinding"/> event is raised; otherwise, false.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.UI.XControl\XControl.DesignerAndDebug.cs" title="XControl.DesignerAndDebug.cs" lang="C#" />
        /// </example>
        protected override void DataBind(bool raiseOnDataBinding)
        {
            // Don't do anything
        }

        /// <summary>
        /// Binds a data source to the server control's child controls.
        /// </summary>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.UI.XControl\XControl.DesignerAndDebug.cs" title="XControl.DesignerAndDebug.cs" lang="C#" />
        /// </example>
        protected override void DataBindChildren()
        {
            // Don't do anything
        }

        /// <summary>
        /// Determines whether the server control contains child controls. If it does not, it creates child controls.
        /// </summary>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.UI.XControl\XControl.DesignerAndDebug.cs" title="XControl.DesignerAndDebug.cs" lang="C#" />
        /// </example>
        protected override void EnsureChildControls()
        {
            // Don't do anything
        }

        /// <summary>
        /// Causes tracking of view-state changes to the server control so they can be stored in the server control's <see cref="T:System.Web.UI.StateBag"/> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState"/> property.
        /// </summary>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.UI.XControl\XControl.DesignerAndDebug.cs" title="XControl.DesignerAndDebug.cs" lang="C#" />
        /// </example>
        protected override void TrackViewState()
        {
            // Don't do anything
        }

        /// <summary>
        ///      Good programming practice
        /// </summary>
        /// <returns>Empty Control collection. No children.</returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.UI.XControl\XControl.DesignerAndDebug.cs" title="XControl.DesignerAndDebug.cs" lang="C#" />
        /// </example>
        protected override ControlCollection CreateControlCollection()
        {
            return new EmptyControlCollection(this);
        }

        /// <summary>
        ///     Good programming practice
        /// </summary>
        /// <param name="obj">An Object that represents the parsed element.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.UI.XControl\XControl.DesignerAndDebug.cs" title="XControl.DesignerAndDebug.cs" lang="C#" />
        /// </example>
        protected override void AddParsedSubObject(object obj)
        {
            // Don't do anything
        }

        /// <summary>
        /// Saves any server control view-state changes that have occurred since the time the page was posted back to the server.
        /// </summary>
        /// <returns>
        /// Returns the server control's current view state. If there is no view state associated with the control, this method returns null.
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.UI.XControl\XControl.DesignerAndDebug.cs" title="XControl.DesignerAndDebug.cs" lang="C#" />
        /// </example>
        protected override object SaveViewState()
        {
            return null;
        }

        /// <summary>
        /// Restores control-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveControlState"/> method.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Object"/> that represents the control state to be restored.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.UI.XControl\XControl.DesignerAndDebug.cs" title="XControl.DesignerAndDebug.cs" lang="C#" />
        /// </example>
        protected override void LoadControlState(object savedState)
        {
            // Don't do anything  
        }

        /// <summary>
        /// Gets the control adapter responsible for rendering the specified control.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.UI.Adapters.ControlAdapter"/> that will render the control.
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.UI.XControl\XControl.DesignerAndDebug.cs" title="XControl.DesignerAndDebug.cs" lang="C#" />
        /// </example>
        protected override ControlAdapter ResolveAdapter()
        {
            return null;
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.UI.XControl\XControl.DesignerAndDebug.cs" title="XControl.DesignerAndDebug.cs" lang="C#" />
        /// </example>
        protected override void CreateChildControls()
        {
            // Don't do anything
        }

        /// <summary>
        /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter"/> object, which writes the content to be rendered on the client.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"/> object that receives the server control content.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteLine();
            writer.Write("<!-- Seo tracking --> ");
#if !DEBUG
            string js = JsTemplate.Replace("#ACCOUNT#", this.Account).Replace("#SUBDOMAIN#", this.GetSubDomain());
            writer.Write(js);
#else
            writer.WriteLine();
            writer.Write("<!-- Google Analytics Control will be render here on release only --> ");
#endif
            writer.WriteLine("<!-- end seo tracking -->");
        }

        /// <summary>
        /// Determines if the server control holds only literal content.
        /// </summary>
        /// <returns>
        /// true if the server control contains solely literal content; otherwise false.
        /// </returns>
        private new bool IsLiteralContent()
        {
            return true;
        }

        // ReSharper restore UnusedMember.Local

        /// <summary>
        /// Gets the sub domain.
        /// </summary>
        /// <returns>returns the SubDomain for the site</returns>
        private string GetSubDomain()
        {
            if (!string.IsNullOrWhiteSpace(this.SubDomain))
            {
                return string.Format("_gaq.push(['_setDomainName', '{0}'])", this.SubDomain);
            }

            return string.Empty;
        }
    }
}
