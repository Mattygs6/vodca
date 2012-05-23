//-----------------------------------------------------------------------------
// <copyright file="YuiControl.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       02/05/2012
//-----------------------------------------------------------------------------
namespace Vodca.Controls
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.Adapters;
    using System.Web.UI.WebControls;
    using Vodca.YuiCompressor;

    /// <summary>
    /// Vodca YUI compressor  Control
    /// </summary>
    /// <example>
    /// <code lang="xml" title="web.config">
    /// The example config: <br/>
    /// <pages>
    ///   <controls>
    ///     <add tagPrefix="vodca" namespace="Vodca.Controls" assembly="Vodca.Core" />
    ///   </controls>
    ///   <namespaces>
    ///     <add namespace="Vodca"/>
    ///   </namespaces>
    /// </pages>
    /// </code>
    /// </example>
    [DefaultProperty("Path")]
    [ParseChildren(false), PersistChildren(false)]
    [ControlBuilder(typeof(LiteralControlBuilder))]
    public sealed partial class YuiControl : Control
    {
        /// <summary>
        /// Gets or sets the Google account.
        /// </summary>
        /// <value>
        /// The Google account.
        /// </value>
        [Bindable(false)]
        [Category("Vodca")]
        [DefaultValue("")]
        [Localizable(false)]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the physical folder.
        /// </summary>
        /// <value>
        /// The physical folder.
        /// </value>
        private string PhysicalFolder { get; set; }

        /// <summary>
        /// Applies the style properties defined in the page style sheet to the control.
        /// </summary>
        /// <param name="page">The <see cref="T:System.Web.UI.Page"/> containing the control.</param>
        /// <exception cref="T:System.InvalidOperationException">The style sheet is already applied.</exception>
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
        public override Control FindControl(string id)
        {
            throw new InvalidOperationException("The Method is not supported!");
        }

        /// <summary>
        /// Binds a data source to the invoked server control and all its child controls.
        /// </summary>
        public override void DataBind()
        {
            throw new InvalidOperationException("The Method is not supported!");
        }

        /// <summary>
        /// Sets input focus to a control.
        /// </summary>
        public override void Focus()
        {
            throw new InvalidOperationException("The Method is not supported!");
        }

        /// <summary>
        /// Binds a data source to the invoked server control and all its child controls with an option to raise the <see cref="E:System.Web.UI.Control.DataBinding"/> event.
        /// </summary>
        /// <param name="raiseOnDataBinding">true if the <see cref="E:System.Web.UI.Control.DataBinding"/> event is raised; otherwise, false.</param>
        protected override void DataBind(bool raiseOnDataBinding)
        {
            // Don't do anything
        }

        /// <summary>
        /// Binds a data source to the server control's child controls.
        /// </summary>
        protected override void DataBindChildren()
        {
            // Don't do anything
        }

        /// <summary>
        /// Determines whether the server control contains child controls. If it does not, it creates child controls.
        /// </summary>
        protected override void EnsureChildControls()
        {
            // Don't do anything
        }

        /// <summary>
        /// Causes tracking of view-state changes to the server control so they can be stored in the server control's <see cref="T:System.Web.UI.StateBag"/> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState"/> property.
        /// </summary>
        protected override void TrackViewState()
        {
            // Don't do anything
        }

        /// <summary>
        ///      Good programming practice
        /// </summary>
        /// <returns>Empty Control collection. No children.</returns>
        protected override ControlCollection CreateControlCollection()
        {
            return new EmptyControlCollection(this);
        }

        /// <summary>
        ///     Good programming practice
        /// </summary>
        /// <param name="obj">An Object that represents the parsed element.</param>
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
        protected override object SaveViewState()
        {
            return null;
        }

        /// <summary>
        /// Restores control-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveControlState"/> method.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Object"/> that represents the control state to be restored.</param>
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
        protected override ControlAdapter ResolveAdapter()
        {
            return null;
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
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
            if (!string.IsNullOrWhiteSpace(this.Path))
            {
                var settings = this.Path.DeserializeFromXmlFile<XmlSettings>();
                if (settings != null)
                {
                    this.PhysicalFolder = this.Path.GetDirectory();
                    writer.WriteLine();

#if DEBUG
                    writer.WriteLine(@"<!-- Yui control begin -->");
                    this.RenderDebug(settings, writer);
                    writer.WriteLine(@"<!-- Yui control end -->");
#else
                    this.RenderRelease(settings, writer);
#endif
                }
            }
        }

        /* ReSharper disable UnusedMember.Local */

        /// <summary>
        /// Renders the debug.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="writer">The writer.</param>
        /// <example>
        /// The example config:
        /// <code lang="xml">
        /// <![CDATA[
        /// <?xml version="1.0" encoding="utf-16"?>
        /// <yuicompressor debug="false">
        ///  <filegroups>
        ///    <filegroup minfilename="VForms.min.js">
        ///      <files>
        ///        <file>VForms.js</file>
        ///        <file>VForms.vc.js</file>
        ///      </files>
        ///    </filegroup>
        ///    <filegroup minfilename="Test-VForms.min.js">
        ///      <files>
        ///        <file>VForms.js</file>
        ///        <file>VForms.vc.js</file>
        ///      </files>
        ///    </filegroup>
        ///  </filegroups>
        /// </yuicompressor>
        /// ]]>
        /// </code>
        /// </example>
        private void RenderDebug(XmlSettings settings, HtmlTextWriter writer)
        {
            var tick = DateTime.UtcNow.ToIsoDateFormat(ignoretime: false);
            foreach (var filegroup in settings.FileGroups)
            {
                var action = filegroup.GetCompressorAction();
                if (action == CompressorAction.CssCompression)
                {
                    foreach (var file in filegroup.Files)
                    {
                        var tag = VTag.Link.AddAttribute("ref", "stylesheet").AddAttribute("type", "text/css");
                        tag.AddAttribute("href", string.Concat(this.PhysicalFolder, file, "?v=", tick));
                        writer.WriteLine(tag);
                    }
                }
                else
                {
                    foreach (var file in filegroup.Files)
                    {
                        var tag = VTag.Script.AddAttribute("type", "text/javascript");
                        tag.AddAttribute("src", string.Concat(this.PhysicalFolder, file, "?v=", tick));
                        writer.WriteLine(tag);
                    }
                }
            }
        }

        /// <summary>
        /// Renders the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="writer">The writer.</param>
        private void RenderRelease(XmlSettings settings, HtmlTextWriter writer)
        {
            foreach (var filegroup in settings.FileGroups)
            {
                var action = filegroup.GetCompressorAction();
                var path = this.PhysicalFolder + filegroup.MinifiedFileName;
                string version = string.Empty;
                if (File.Exists(path))
                {
                    version = "?v=" + File.GetLastWriteTimeUtc(path).ToIsoDateFormat(ignoretime: false);
                }

                if (action == CompressorAction.CssCompression)
                {
                    var tag = VTag.Link.AddAttribute("ref", "stylesheet").AddAttribute("type", "text/css");
                    tag.AddAttribute("href", string.Concat(this.PhysicalFolder, filegroup.MinifiedFileName, version));
                    writer.WriteLine(tag);
                }
                else
                {
                    var tag = VTag.Script.AddAttribute("type", "text/javascript");
                    tag.AddAttribute("src", string.Concat(this.PhysicalFolder, filegroup.MinifiedFileName, version));
                    writer.WriteLine(tag);
                }
            }
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

        /* ReSharper restore UnusedMember.Local */
    }
}
