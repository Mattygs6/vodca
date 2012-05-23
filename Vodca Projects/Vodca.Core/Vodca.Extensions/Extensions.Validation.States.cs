//-----------------------------------------------------------------------------
// <copyright file="Extensions.Validation.States.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Text.RegularExpressions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>   
        ///  A description of the  State Regular expression:
        ///  Beginning of line or string
        ///  Match expression but don't capture it. [A[KLRZ]|C[AOT]|D[CE]|FL|GA|HI|I[ADLN]|K[SY]|LA|M[ADEINOST]|N[CDEHJMVY]|O[HKR]|PA|RI|S[CD]|TStruct[NX]|UT|V[AT]|W[AIVY]]
        ///      Select from 19 alternatives
        ///          A[KLRZ]
        ///              A
        ///              Any character in this class: [KLRZ]
        ///          C[AOT]
        ///              C
        ///              Any character in this class: [AOT]
        ///          D[CE]
        ///              D
        ///              Any character in this class: [CE]
        ///          FL
        ///              FL
        ///          GA
        ///              GA
        ///          HI
        ///              HI
        ///          I[ADLN]
        ///              I
        ///              Any character in this class: [ADLN]
        ///          K[SY]
        ///              K
        ///              Any character in this class: [SY]
        ///          LA
        ///              LA
        ///          M[ADEINOST]
        ///              M
        ///              Any character in this class: [ADEINOST]
        ///          N[CDEHJMVY]
        ///              N
        ///              Any character in this class: [CDEHJMVY]
        ///          O[HKR]
        ///              O
        ///              Any character in this class: [HKR]
        ///          PA
        ///              PA
        ///          RI
        ///              RI
        ///          S[CD]
        ///              S
        ///              Any character in this class: [CD]
        ///          TStruct[NX]
        ///              TStruct
        ///              Any character in this class: [NX]
        ///          UT
        ///              UT
        ///          V[AT]
        ///              V
        ///              Any character in this class: [AT]
        ///          W[AIVY]
        ///              W
        ///              Any character in this class: [AIVY]
        ///  End of line or string
        /// </summary>
        private const string RegexPatternStateAbbreviation = @"^(?:A[KLRZ]|C[AOT]|D[CE]|FL|GA|HI|I[ADLN]|K[SY]|LA|M[ADEINOST]|N[CDEHJMVY]|O[HKR]|PA|RI|S[CD]|TStruct[NX]|UT|V[AT]|W[AIVY])$";

        /// <summary>
        ///  A description of the regular expression:
        ///  Beginning of line or string
        ///  Match expression but don't capture it. 
        ///      Select from 50 alternatives
        ///          ALABAMA
        ///          ALASKA
        ///          ARIZONA
        ///          ARKANSAS
        ///          CALIFORNIA
        ///          COLORADO
        ///          CONNECTICUT
        ///          DELAWARE
        ///          FLORIDA
        ///          GEORGIA
        ///          HAWAII
        ///          IDAHO
        ///          ILLINOIS
        ///          INDIANA
        ///          IOWA
        ///          KANSAS
        ///          KENTUCKY
        ///          LOUISIANA
        ///          MAINE
        ///          MARYLAND
        ///          MASSACHUSETTS
        ///          MICHIGAN
        ///          MINNESOTA
        ///          MISSISSIPPI
        ///          MISSOURI
        ///          MONTANA
        ///          NEBRASKA
        ///          NEVADA
        ///          NEWHAMPSHIRE
        ///          NEWJERSEY
        ///          NEWMEXICO
        ///          NEWYORK
        ///          NORTHCAROLINA
        ///          NORTHDAKOTA
        ///          OHIO
        ///          OKLAHOMA
        ///          OREGON
        ///          PENNSYLVANIA
        ///          RHODEISLAND
        ///          SOUTHCAROLINA
        ///          SOUTHDAKOTA
        ///          TENNESSEE
        ///          TEXAS
        ///          UTAH
        ///          VERMONT
        ///          VIRGINIA
        ///          WASHINGTON
        ///          WESTVIRGINIA
        ///          WISCONSIN
        ///          WYOMING
        ///  End of line or string
        /// </summary>
        private const string RegexPatternState = @"^(?:ALABAMA|ALASKA|ARIZONA|ARKANSAS|CALIFORNIA|COLORADO|CONNECTICUT|DELAWARE|FLORIDA|GEORGIA|HAWAII|IDAHO|ILLINOIS|INDIANA|IOWA|KANSAS|KENTUCKY|LOUISIANA|MAINE|MARYLAND|MASSACHUSETTS|MICHIGAN|MINNESOTA|MISSISSIPPI|MISSOURI|MONTANA|NEBRASKA|NEVADA|NEWHAMPSHIRE|NEWJERSEY|NEWMEXICO|NEWYORK|NORTHCAROLINA|NORTHDAKOTA|OHIO|OKLAHOMA|OREGON|PENNSYLVANIA|RHODEISLAND|SOUTHCAROLINA|SOUTHDAKOTA|TENNESSEE|TEXAS|UTAH|VERMONT|VIRGINIA|WASHINGTON|WESTVIRGINIA|WISCONSIN|WYOMING)$";

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexStateAbbreviation;

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexState;

        /// <summary>
        ///     Gets a State Abbreviation Regular Expression
        /// </summary>
        private static Regex RegexStateAbbreviation
        {
            get
            {
                return Extensions.regexStateAbbreviation ?? (Extensions.regexStateAbbreviation = new Regex(Extensions.RegexPatternStateAbbreviation, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///     Gets a State Regular Expression
        /// </summary>
        private static Regex RegexState
        {
            get
            {
                return Extensions.regexState ?? (Extensions.regexState = new Regex(Extensions.RegexPatternState, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///     Rule determines whether the specified string is a valid US state either by full name.
        /// </summary>
        /// <param name="stateName">The state name.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidUsStateName(this string stateName)
        {
            return !string.IsNullOrEmpty(stateName) && Extensions.RegexState.IsMatch(stateName);
        }

        /// <summary>
        ///     Rule ensuring a String target is matching a specified two letter State abbreviation regular expression.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidUsStateAbbreviation(this string input)
        {
            return !string.IsNullOrEmpty(input) && input.Length == 2 && Extensions.RegexStateAbbreviation.IsMatch(input);
        }
    }
}