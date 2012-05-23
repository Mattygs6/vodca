//-----------------------------------------------------------------------------
// <copyright file="VCacheTime.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author     J.Baltikauskas
//  Date       08/31/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    /// <summary>
    ///     Cache Time In Minutes
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Web\Cache\VCacheTime.cs" title="VCacheTime.cs" lang="C#" />
    /// </example>
    [System.Serializable]
    public enum VCacheTime
    {
        /// <summary>
        ///     No Cache time
        /// </summary>
        /// <value>No Cache time</value>
        None = 0,

        /// <summary>
        ///     Cache for 1 minute
        /// </summary>
        /// <value>Cache for 1 minute</value>
        BelowLow = 1,

        /// <summary>
        ///     Cache for 5 minute
        /// </summary>
        /// <value>Cache for 5 minute</value>
        Low = 5,

        /// <summary>
        ///     Cache for 10 minutes
        /// </summary>
        /// <value>Cache for 10 minutes</value>
        BelowNormal = 10,

        /// <summary>
        ///     Cache for 20 minutes
        /// </summary>
        /// <value>Cache for 20 minutes</value>
        Normal = 20,

        /// <summary>
        ///     Cache for 30 minutes
        /// </summary>
        /// <value>Cache for 30 minutes</value>
        AboveNormal = 30,

        /// <summary>
        ///     Cache for 1 hour
        /// </summary>
        /// <value>Cache for 1 hour</value>
        High = 60,

        /// <summary>
        ///     Cache for 4 hours
        /// </summary>
        /// <value>Cache for 4 hours</value>
        AboveHigh = 240,

        /// <summary>
        ///     Cache for 1 day
        /// </summary>
        /// <value>Cache for 1 day</value>
        Day = 1440
    }
}
