//-----------------------------------------------------------------------------
// <copyright file="NameValuePairList.cs" company="genuine">
//     Copyright (c) Simon Mourier. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:         HtmlAgilityPack V1.0 - Simon Mourier
//  Modifications:  J.Baltikauskas
//   Date:          04/20/2012
//-----------------------------------------------------------------------------
namespace Vodca.HtmlAgilityPack
{
    using System.Collections.Generic;

    /// <summary>
    /// The name value pair list.
    /// </summary>
    internal class NameValuePairList
    {
        /// <summary>
        /// The _all pairs.
        /// </summary>
        private readonly List<KeyValuePair<string, string>> allPairs;

        /// <summary>
        /// The _pairs with name.
        /// </summary>
        private readonly Dictionary<string, List<KeyValuePair<string, string>>> pairsWithName;

        /// <summary>
        /// Initializes a new instance of the <see cref="NameValuePairList"/> class.
        /// </summary>
        internal NameValuePairList()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NameValuePairList"/> class.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        internal NameValuePairList(string text)
        {
            this.Text = text;
            this.allPairs = new List<KeyValuePair<string, string>>();
            this.pairsWithName = new Dictionary<string, List<KeyValuePair<string, string>>>();

            this.Parse(text);
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; internal set; }

        /// <summary>
        /// The get name value pairs value.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="name">The name.</param>
        /// <returns>
        /// The name value pairs value.
        /// </returns>
        internal static string GetNameValuePairsValue(string text, string name)
        {
            var l = new NameValuePairList(text);
            return l.GetNameValuePairValue(name);
        }

        /// <summary>
        /// The get name value pair value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// The name value pair value.
        /// </returns>
        internal string GetNameValuePairValue(string name)
        {
            Ensure.IsNotNull(name, "name");

            List<KeyValuePair<string, string>> al = this.GetNameValuePairs(name);
            if (al.Count == 0)
            {
                return string.Empty;
            }

            // return first item
            return al[0].Value.Trim();
        }

        /// <summary>
        /// The get name value pairs.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The name value pairs</returns>
        internal List<KeyValuePair<string, string>> GetNameValuePairs(string name)
        {
            if (name == null)
            {
                return this.allPairs;
            }

            return this.pairsWithName.ContainsKey(name)
                       ? this.pairsWithName[name]
                       : new List<KeyValuePair<string, string>>();
        }

        /// <summary>
        /// The parse.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        private void Parse(string text)
        {
            this.allPairs.Clear();
            this.pairsWithName.Clear();
            if (text == null)
            {
                return;
            }

            string[] p = text.Split(';');
            foreach (string pv in p)
            {
                if (pv.Length == 0)
                {
                    continue;
                }

                string[] onep = pv.Split(new[] { '=' }, 2);
                if (onep.Length == 0)
                {
                    continue;
                }

                var nvp = new KeyValuePair<string, string>(onep[0].Trim().ToLower(), onep.Length < 2 ? string.Empty : onep[1]);

                this.allPairs.Add(nvp);

                // index by name
                List<KeyValuePair<string, string>> al;
                if (!this.pairsWithName.ContainsKey(nvp.Key))
                {
                    al = new List<KeyValuePair<string, string>>();
                    this.pairsWithName[nvp.Key] = al;
                }
                else
                {
                    al = this.pairsWithName[nvp.Key];
                }

                al.Add(nvp);
            }
        }
    }
}