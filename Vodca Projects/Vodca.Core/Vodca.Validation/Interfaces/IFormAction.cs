//-----------------------------------------------------------------------------
// <copyright file="IFormAction.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
// Author:      J.Baltikauskas
//   Date:      07/28/2011
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    using Vodca;

    /// <summary>
    /// The required Webservice interface
    /// </summary>
    public interface IFormAction : IValidate
    {
        /// <summary>
        /// Submits this instance.
        /// </summary>
        /// <returns>Submit the data to the server</returns>
        VJsonResponse Submit();
    }
}
