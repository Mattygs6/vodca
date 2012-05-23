//-----------------------------------------------------------------------------
// <copyright file="CreditCardType.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///    Acceptable Credit Card Types 
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "None target isn't necessary")]
    [Serializable]
    public enum CreditCardType
    {
        /// <summary>
        ///     Visa card. Default
        /// </summary>
        Visa = 0,

        /// <summary>
        ///    Master card 
        /// </summary>
        Master = 1,

        /// <summary>
        ///     American Express Card
        /// </summary>
        AmericanExpress = 2,

        /// <summary>
        ///     Discovery Card
        /// </summary>
        Discover = 4,

        /// <summary>
        ///     Diners Club Card
        /// </summary>
        DinersClub = 8
    }
}
