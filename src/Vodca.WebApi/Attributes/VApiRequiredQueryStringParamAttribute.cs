//-----------------------------------------------------------------------------
// <copyright file="VApiRequiredQueryStringParamAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       04/01/2012
//-----------------------------------------------------------------------------
namespace Vodca.WebApi
{
    using System.Net;

    /// <summary>
    ///  The Vodca Web API validation attribute
    /// </summary>
    public sealed class VApiRequiredQueryStringParamAttribute : VApiValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VApiRequiredQueryStringParamAttribute"/> class.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public VApiRequiredQueryStringParamAttribute(string parameter)
        {
            Ensure.IsNotNullOrEmpty(parameter, "parameter");
            this.QueryStringParameterName = parameter;
        }

        /// <summary>
        /// Gets the name of the query string parameter.
        /// </summary>
        /// <value>
        /// The name of the query string parameter.
        /// </value>
        public string QueryStringParameterName { get; private set; }

        /// <summary>
        /// Validates the specified args.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>The true to continue and false otherwise</returns>
        public override bool Validate(VApiArgs args)
        {
            return string.IsNullOrWhiteSpace(args.QueryString[this.QueryStringParameterName]);
        }

        /// <summary>
        /// Called when validation fails.
        /// </summary>
        /// <param name="args">The args.</param>
        public override void OnValidationFailure(VApiArgs args)
        {
            args.Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
            args.Response.AddHeader("X-Validation", "PreconditionFailed - " + this.QueryStringParameterName);
        }
    }
}
