//-----------------------------------------------------------------------------
// <copyright file="AssemblyInfo.Registration.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/02/2012
//-----------------------------------------------------------------------------

using System.Web;
using Vodca;
using Vodca.Pipelines;
using Vodca.WebApi;

[assembly: PreApplicationStartMethod(typeof(VRegistrationStart), "Run")]

#if REGISTER_REQUIRED

/*
 * Create Physical files and folders from Embedded Web resources 
 */
[assembly: VRegisterRequiredWebFolder("/App_Config/")]
[assembly: VRegisterRequiredWebFolder("/App_Config/Vodca.Pipelines/")]

[assembly: VRegisterRequiredWebFolder("/App_Logs/")]

#endif

/* 
 * Register HttpModules
 *  Note: Initialize Log manager first and VPipeline Manager before rest custom modules 
 */
[assembly: VRegisterHttpModule(typeof(VLog), Order = 100)]
[assembly: VRegisterHttpModule(typeof(VPipelineManager), Order = 200)]
[assembly: VRegisterHttpModule(typeof(VApiManagerModule), Order = 300)]
