//-----------------------------------------------------------------------------
// <copyright file="CompressManager.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/05/2012
//-----------------------------------------------------------------------------
namespace Vodca.YuiCompressor
{
    using System.IO;

    /// <summary>
    /// The JS Compress Manager
    /// </summary>
    public partial class CompressManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompressManager"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="currentdirectory">The currentdirectory.</param>
        public CompressManager(XmlSettings settings, string currentdirectory)
            : this()
        {
            this.XmlSettings = settings;
            this.CurrentDirectory = currentdirectory.EnsureEndsWith(@"\");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressManager"/> class.
        /// </summary>
        /// <param name="settingspath">The settings path.</param>
        /// <param name="isvirtualpath">if set to <c>true</c> is virtualpath.</param>
        public CompressManager(string settingspath, bool isvirtualpath = true)
            : this()
        {
            if (isvirtualpath)
            {
                settingspath = settingspath.MapPath();
            }

            this.XmlSettings = settingspath.DeserializeFromXmlFile<XmlSettings>(virtualpath: false);
            this.CurrentDirectory = Path.GetDirectoryName(settingspath).EnsureEndsWith(@"\");
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="CompressManager"/> class from being created.
        /// </summary>
        private CompressManager()
        {
            this.JsPipeline += this.JsPipelineConsoleLog;
            this.JsPipeline += this.JsPipelineVodcaConsoleLog;
            this.JsPipeline += this.JsFileCompress;
            this.JsPipeline += this.FinalOutput;
            this.JsPipeline += this.SaveFile;

            this.CssPipeline += this.CssFileCompress;
            this.CssPipeline += this.FinalOutput;
            this.CssPipeline += this.SaveFile;
        }

        /// <summary>
        /// Gets the XML settings.
        /// </summary>
        public XmlSettings XmlSettings { get; private set; }

        /// <summary>
        /// Gets the current directory.
        /// </summary>
        public string CurrentDirectory { get; private set; }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns>True if no errors</returns>
        public bool Run()
        {
            foreach (var filegroup in this.XmlSettings.FileGroups)
            {
                var args = new CompressFileGroupArgs(filegroup, this.CurrentDirectory, !this.XmlSettings.DebugVersion);

                switch (filegroup.GetCompressorAction())
                {
                    case CompressorAction.CssCompression:
                        this.CssPipeline(args);
                        break;
                    default:
                        this.JsPipeline(args);
                        break;
                }
            }

            return true;
        }
    }
}
