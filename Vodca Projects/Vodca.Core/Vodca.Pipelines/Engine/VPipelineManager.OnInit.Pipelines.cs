//-----------------------------------------------------------------------------
// <copyright file="VPipelineManager.OnInit.Pipelines.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       02/10/2012
//-----------------------------------------------------------------------------
namespace Vodca.Pipelines
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Web;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public sealed partial class VPipelineManager
    {
        /// <summary>
        /// Gets or sets the pipeline folder.
        /// </summary>
        /// <value>
        /// The pipeline folder.
        /// </value>
        private static string PipelineFolder { get; set; }

        /// <summary>
        /// Initializes the pipelines.
        /// </summary>
        /// <param name="context">The context.</param>
        private void InitWebConfigPipelines(HttpApplication context)
        {
            var folder = PipelineFolder.MapPath();
            if (Directory.Exists(folder))
            {
                var files = Directory.GetFiles(folder, "*.config");
                foreach (var file in files)
                {
                    var config = file.DeserializeFromXmlFile<VTaskPipelineConfiguration>(virtualpath: false);
                    if (config.Validate())
                    {
                        Manager.AddVTaskPipeline(config);
                    }
                }
            }
        }

        /// <summary>
        /// Initializes the assembly pipelines.
        /// </summary>
        /// <param name="context">The context.</param>
        private void InitAssemblyEmbeddedPipelines(HttpApplication context)
        {
            foreach (var assembly in Assemblies)
            {
                var attributies = assembly.GetRegisterVTaskPipelineAttributes();
                foreach (var attribute in attributies)
                {
                    var config = attribute.Configuration.GetVTaskPipelineConfiguration();
                    if (config.Validate())
                    {
                        Manager.AddVTaskPipeline(config);
                    }
                }
            }
        }
    }
}