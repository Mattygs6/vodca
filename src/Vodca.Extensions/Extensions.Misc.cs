//-----------------------------------------------------------------------------
// <copyright file="Extensions.Misc.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       04/24/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the state collection.
        /// </summary>
        /// <returns>The State collection</returns>
        /// <value>The state collection.</value>
        public static VStringDictionary GetStateCollection()
        {
            return new VStringDictionary
                {
                    { "AL", "Alabama" },
                    { "AK", "Alaska" },
                    { "AZ", "Arizona" },
                    { "AR", "Arkansas" },
                    { "CA", "California" },
                    { "CO", "Colorado" },
                    { "CT", "Connecticut" },
                    { "DC", "District of Columbia" },
                    { "DE", "Delaware" },
                    { "FL", "Florida" },
                    { "GA", "Georgia" },
                    { "HI", "Hawaii" },
                    { "ID", "Idaho" },
                    { "IL", "Illinois" },
                    { "IN", "Indiana" },
                    { "IA", "Iowa" },
                    { "KS", "Kansas" },
                    { "KY", "Kentucky" },
                    { "LA", "Louisiana" },
                    { "ME", "Maine" },
                    { "MD", "Maryland" },
                    { "MA", "Massachusetts" },
                    { "MI", "Michigan" },
                    { "MN", "Minnesota" },
                    { "MS", "Mississippi" },
                    { "MO", "Missouri" },
                    { "MT", "Montana" },
                    { "NE", "Nebraska" },
                    { "NV", "Nevada" },
                    { "NH", "New Hampshire" },
                    { "NJ", "New Jersey" },
                    { "NM", "New Mexico" },
                    { "NY", "New York" },
                    { "NC", "North Carolina" },
                    { "ND", "North Dakota" },
                    { "OH", "Ohio" },
                    { "OK", "Oklahoma" },
                    { "OR", "Oregon" },
                    { "PA", "Pennsylvania" },
                    { "PR", "Puerto Rico" },
                    { "RI", "Rhode Island" },
                    { "SC", "South Carolina" },
                    { "SD", "South Dakota" },
                    { "TN", "Tennessee" },
                    { "TX", "Texas" },
                    { "UT", "Utah" },
                    { "VT", "Vermont" },
                    { "VA", "Virginia" },
                    { "WA", "Washington" },
                    { "WV", "West Virginia" },
                    { "WI", "Wisconsin" },
                    { "WY", "Wyoming" }
                };
        }

        /// <summary>
        /// States the long name of the abbreviation to.
        /// </summary>
        /// <param name="stateabbreviation">The state abbreviation.</param>
        /// <returns>The state long name.</returns>
        public static string StateAbbreviationToLongName(string stateabbreviation)
        {
            Ensure.IsNullOrEmptyAndStringMaxLength(stateabbreviation, 2, "State Abbreviation");
            return GetStateCollection()[stateabbreviation];
        }

        /// <summary>
        /// States the list.
        /// </summary>
        /// <param name="propendtext">The propend text.</param>
        /// <returns>The list of State</returns>
        public static IEnumerable<string> StateList(string propendtext = "Select ...")
        {
            if (string.IsNullOrWhiteSpace(propendtext))
            {
                return new[] { "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "District of Columbia", "Delaware", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Puerto Rico", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming" };
            }

            return new[] { propendtext, "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "District of Columbia", "Delaware", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Puerto Rico", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming" };
        }

        /// <summary>
        /// Adds the US state list.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// protected void State_Init(object sender, EventArgs e)
        /// {
        ///    ListControl control = (ListControl)sender;
        ///    control.Items.Add(new ListItem("-select state-", string.Empty));
        ///    control.LoadStateList();
        /// }
        /// </code>
        /// </example>
        public static void LoadStateList(this ListControl control)
        {
            Ensure.IsNotNull(control, "control");

            foreach (var state in GetStateCollection())
            {
                control.Items.Add(new ListItem { Value = state.Key, Text = state.Value });
            }
        }
    }
}
