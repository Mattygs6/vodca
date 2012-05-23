//-----------------------------------------------------------------------------
// <copyright file="IVWebSettings.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       03/27/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    /// <summary>
    /// Allows coder to explicitly set folder path to save config to
    /// </summary>
    public interface IVWebSettings
    {
        /// <summary>
        /// Gets the folderpath.
        /// </summary>
        /// <returns>The folder path</returns>
        string GetFolderPath();
    }
}
