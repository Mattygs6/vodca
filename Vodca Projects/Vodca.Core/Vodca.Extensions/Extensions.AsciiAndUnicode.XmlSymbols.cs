//-----------------------------------------------------------------------------
// <copyright file="Extensions.AsciiAndUnicode.XmlSymbols.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Modified by:     J.Baltikauskas
//  Date:            12/23/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Concurrent;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Xml Symbol hash table holder
        /// </summary>
        private static ConcurrentDictionary<char, string> xmlSymbols;

        /// <summary>
        ///     Gets Xml Symbol hash table without Html entities
        /// </summary>
        public static ConcurrentDictionary<char, string> XmlSymbols
        {
            get
            {
                if (Extensions.xmlSymbols == null)
                {
                    Extensions.xmlSymbols = new ConcurrentDictionary<char, string>();

                    xmlSymbols.TryAdd('¡', "&#161");
                    xmlSymbols.TryAdd('¢', "&#162");
                    xmlSymbols.TryAdd('£', "&#163");
                    xmlSymbols.TryAdd('¤', "&#164");
                    xmlSymbols.TryAdd('¥', "&#165");
                    xmlSymbols.TryAdd('¦', "&#166");
                    xmlSymbols.TryAdd('§', "&#167");
                    xmlSymbols.TryAdd('¨', "&#168");
                    xmlSymbols.TryAdd('©', "&#169");
                    xmlSymbols.TryAdd('ª', "&#170");
                    xmlSymbols.TryAdd('«', "&#171");
                    xmlSymbols.TryAdd('¬', "&#172");
                    xmlSymbols.TryAdd('®', "&#174");
                    xmlSymbols.TryAdd('¯', "&#175");
                    xmlSymbols.TryAdd('°', "&#176");
                    xmlSymbols.TryAdd('±', "&#177");
                    xmlSymbols.TryAdd('²', "&#178");
                    xmlSymbols.TryAdd('³', "&#179");
                    xmlSymbols.TryAdd('´', "&#180");
                    xmlSymbols.TryAdd('µ', "&#181");
                    xmlSymbols.TryAdd('¶', "&#182");
                    xmlSymbols.TryAdd('•', "&#183");
                    xmlSymbols.TryAdd('¸', "&#184");
                    xmlSymbols.TryAdd('¹', "&#185");
                    xmlSymbols.TryAdd('º', "&#186");
                    xmlSymbols.TryAdd('»', "&#187");
                    xmlSymbols.TryAdd('¼', "&#188");
                    xmlSymbols.TryAdd('½', "&#189");
                    xmlSymbols.TryAdd('¾', "&#190");
                    xmlSymbols.TryAdd('¿', "&#191");
                    xmlSymbols.TryAdd('À', "&#192");
                    xmlSymbols.TryAdd('Á', "&#193");
                    xmlSymbols.TryAdd('Â', "&#194");
                    xmlSymbols.TryAdd('Ã', "&#195");
                    xmlSymbols.TryAdd('Ä', "&#196");
                    xmlSymbols.TryAdd('Å', "&#197");
                    xmlSymbols.TryAdd('Æ', "&#198");
                    xmlSymbols.TryAdd('Ç', "&#199");
                    xmlSymbols.TryAdd('È', "&#200");
                    xmlSymbols.TryAdd('É', "&#201");
                    xmlSymbols.TryAdd('Ê', "&#202");
                    xmlSymbols.TryAdd('Ë', "&#203");
                    xmlSymbols.TryAdd('Ì', "&#204");
                    xmlSymbols.TryAdd('Í', "&#205");
                    xmlSymbols.TryAdd('Î', "&#206");
                    xmlSymbols.TryAdd('Ï', "&#207");
                    xmlSymbols.TryAdd('Ð', "&#208");
                    xmlSymbols.TryAdd('Ñ', "&#209");
                    xmlSymbols.TryAdd('Ò', "&#210");
                    xmlSymbols.TryAdd('Ó', "&#211");
                    xmlSymbols.TryAdd('Ô', "&#212");
                    xmlSymbols.TryAdd('Õ', "&#213");
                    xmlSymbols.TryAdd('Ö', "&#214");
                    xmlSymbols.TryAdd('×', "&#215");
                    xmlSymbols.TryAdd('Ø', "&#216");
                    xmlSymbols.TryAdd('Ù', "&#217");
                    xmlSymbols.TryAdd('Ú', "&#218");
                    xmlSymbols.TryAdd('Û', "&#219");
                    xmlSymbols.TryAdd('Ü', "&#220");
                    xmlSymbols.TryAdd('Ý', "&#221");
                    xmlSymbols.TryAdd('Þ', "&#222");
                    xmlSymbols.TryAdd('ß', "&#223");
                    xmlSymbols.TryAdd('à', "&#224");
                    xmlSymbols.TryAdd('á', "&#225");
                    xmlSymbols.TryAdd('â', "&#226");
                    xmlSymbols.TryAdd('ã', "&#227");
                    xmlSymbols.TryAdd('ä', "&#228");
                    xmlSymbols.TryAdd('å', "&#229");
                    xmlSymbols.TryAdd('æ', "&#230");
                    xmlSymbols.TryAdd('ç', "&#231");
                    xmlSymbols.TryAdd('è', "&#232");
                    xmlSymbols.TryAdd('é', "&#233");
                    xmlSymbols.TryAdd('ê', "&#234");
                    xmlSymbols.TryAdd('ë', "&#235");
                    xmlSymbols.TryAdd('ì', "&#236");
                    xmlSymbols.TryAdd('í', "&#237");
                    xmlSymbols.TryAdd('î', "&#238");
                    xmlSymbols.TryAdd('ï', "&#239");
                    xmlSymbols.TryAdd('ð', "&#240");
                    xmlSymbols.TryAdd('ñ', "&#241");
                    xmlSymbols.TryAdd('ò', "&#242");
                    xmlSymbols.TryAdd('ó', "&#243");
                    xmlSymbols.TryAdd('ô', "&#244");
                    xmlSymbols.TryAdd('õ', "&#245");
                    xmlSymbols.TryAdd('ö', "&#246");
                    xmlSymbols.TryAdd('÷', "&#247");
                    xmlSymbols.TryAdd('ø', "&#248");
                    xmlSymbols.TryAdd('ù', "&#249");
                    xmlSymbols.TryAdd('ú', "&#250");
                    xmlSymbols.TryAdd('û', "&#251");
                    xmlSymbols.TryAdd('ü', "&#252");
                    xmlSymbols.TryAdd('ý', "&#253");
                    xmlSymbols.TryAdd('þ', "&#254");
                    xmlSymbols.TryAdd('ÿ', "&#255");
                    xmlSymbols.TryAdd('Œ', "&#338");
                    xmlSymbols.TryAdd('œ', "&#339");
                    xmlSymbols.TryAdd('Š', "&#352");
                    xmlSymbols.TryAdd('š', "&#353");
                    xmlSymbols.TryAdd('Ÿ', "&#376");
                    xmlSymbols.TryAdd('ƒ', "&#402");
                    xmlSymbols.TryAdd('ˆ', "&#710");
                    xmlSymbols.TryAdd('˜', "&#732");
                    xmlSymbols.TryAdd('Α', "&#913");
                    xmlSymbols.TryAdd('Β', "&#914");
                    xmlSymbols.TryAdd('Γ', "&#915");
                    xmlSymbols.TryAdd('Δ', "&#916");
                    xmlSymbols.TryAdd('Ε', "&#917");
                    xmlSymbols.TryAdd('Ζ', "&#918");
                    xmlSymbols.TryAdd('Η', "&#919");
                    xmlSymbols.TryAdd('Θ', "&#920");
                    xmlSymbols.TryAdd('Ι', "&#921");
                    xmlSymbols.TryAdd('Κ', "&#922");
                    xmlSymbols.TryAdd('Λ', "&#923");
                    xmlSymbols.TryAdd('Μ', "&#924");
                    xmlSymbols.TryAdd('Ν', "&#925");
                    xmlSymbols.TryAdd('Ξ', "&#926");
                    xmlSymbols.TryAdd('Ο', "&#927");
                    xmlSymbols.TryAdd('Π', "&#928");
                    xmlSymbols.TryAdd('Ρ', "&#929");
                    xmlSymbols.TryAdd('Σ', "&#931");
                    xmlSymbols.TryAdd('Τ', "&#932");
                    xmlSymbols.TryAdd('Υ', "&#933");
                    xmlSymbols.TryAdd('Φ', "&#934");
                    xmlSymbols.TryAdd('Χ', "&#935");
                    xmlSymbols.TryAdd('Ψ', "&#936");
                    xmlSymbols.TryAdd('Ω', "&#937");
                    xmlSymbols.TryAdd('α', "&#945");
                    xmlSymbols.TryAdd('β', "&#946");
                    xmlSymbols.TryAdd('γ', "&#947");
                    xmlSymbols.TryAdd('δ', "&#948");
                    xmlSymbols.TryAdd('ε', "&#949");
                    xmlSymbols.TryAdd('ζ', "&#950");
                    xmlSymbols.TryAdd('η', "&#951");
                    xmlSymbols.TryAdd('θ', "&#952");
                    xmlSymbols.TryAdd('ι', "&#953");
                    xmlSymbols.TryAdd('κ', "&#954");
                    xmlSymbols.TryAdd('λ', "&#955");
                    xmlSymbols.TryAdd('μ', "&#956");
                    xmlSymbols.TryAdd('ν', "&#957");
                    xmlSymbols.TryAdd('ξ', "&#958");
                    xmlSymbols.TryAdd('ο', "&#959");
                    xmlSymbols.TryAdd('π', "&#960");
                    xmlSymbols.TryAdd('ρ', "&#961");
                    xmlSymbols.TryAdd('ς', "&#962");
                    xmlSymbols.TryAdd('σ', "&#963");
                    xmlSymbols.TryAdd('τ', "&#964");
                    xmlSymbols.TryAdd('υ', "&#965");
                    xmlSymbols.TryAdd('φ', "&#966");
                    xmlSymbols.TryAdd('χ', "&#967");
                    xmlSymbols.TryAdd('ψ', "&#968");
                    xmlSymbols.TryAdd('ω', "&#969");
                    xmlSymbols.TryAdd('ϑ', "&#977");
                    xmlSymbols.TryAdd('ϒ', "&#978");
                    xmlSymbols.TryAdd('ϖ', "&#982");
                    xmlSymbols.TryAdd('–', "&#8211");
                    xmlSymbols.TryAdd('—', "&#8212");
                    xmlSymbols.TryAdd('‘', "&#8216");
                    xmlSymbols.TryAdd('’', "&#8217");
                    xmlSymbols.TryAdd('‚', "&#8218");
                    xmlSymbols.TryAdd('“', "&#8220");
                    xmlSymbols.TryAdd('”', "&#8221");
                    xmlSymbols.TryAdd('„', "&#8222");
                    xmlSymbols.TryAdd('†', "&#8224");
                    xmlSymbols.TryAdd('‡', "&#8225");
                    xmlSymbols.TryAdd('…', "&#8230");
                    xmlSymbols.TryAdd('‰', "&#8240");
                    xmlSymbols.TryAdd('′', "&#8242");
                    xmlSymbols.TryAdd('″', "&#8243");
                    xmlSymbols.TryAdd('‹', "&#8249");
                    xmlSymbols.TryAdd('›', "&#8250");
                    xmlSymbols.TryAdd('‾', "&#8254");
                    xmlSymbols.TryAdd('⁄', "&#8260");
                    xmlSymbols.TryAdd('€', "&#8364");
                    xmlSymbols.TryAdd('™', "&#8482");
                    xmlSymbols.TryAdd('℠', "&#8480");
                    xmlSymbols.TryAdd('←', "&#8592");
                    xmlSymbols.TryAdd('↑', "&#8593");
                    xmlSymbols.TryAdd('→', "&#8594");
                    xmlSymbols.TryAdd('↓', "&#8595");
                    xmlSymbols.TryAdd('↔', "&#8596");
                    xmlSymbols.TryAdd('∂', "&#8706");
                    xmlSymbols.TryAdd('∏', "&#8719");
                    xmlSymbols.TryAdd('∑', "&#8721");
                    xmlSymbols.TryAdd('−', "&#8722");
                    xmlSymbols.TryAdd('√', "&#8730");
                    xmlSymbols.TryAdd('∞', "&#8734");
                    xmlSymbols.TryAdd('∩', "&#8745");
                    xmlSymbols.TryAdd('∫', "&#8747");
                    xmlSymbols.TryAdd('≈', "&#8776");
                    xmlSymbols.TryAdd('≠', "&#8800");
                    xmlSymbols.TryAdd('≡', "&#8801");
                    xmlSymbols.TryAdd('≤', "&#8804");
                    xmlSymbols.TryAdd('≥', "&#8805");
                    xmlSymbols.TryAdd('〈', "&#9001");
                    xmlSymbols.TryAdd('〉', "&#9002");
                    xmlSymbols.TryAdd('◊', "&#9674");
                    xmlSymbols.TryAdd('♠', "&#9824");
                    xmlSymbols.TryAdd('♣', "&#9827");
                    xmlSymbols.TryAdd('♥', "&#9829");
                }

                return Extensions.xmlSymbols;
            }
        }

        /// <summary>
        ///     Encodes input strings for use in Xml
        /// </summary>
        /// <param name="input">String input to HtmlEncode</param>
        /// <returns>Encoded  string</returns>
        /// <remarks>
        ///     Intended use for Control method 'public override void RenderControl(HtmlTextWriter writer)' 
        /// </remarks>
        public static string ToXmlExtendedSymbols(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                ConcurrentDictionary<char, string> xmlsymbols = Extensions.XmlSymbols;
                var builder = new StringBuilder(string.Empty, input.Length + 128);

                foreach (char ch in input)
                {
                    if (xmlsymbols.ContainsKey(ch))
                    {
                        builder.Append(xmlsymbols[ch]);
                    }
                    else
                    {
                        builder.Append(ch);
                    }
                }

                return builder.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        ///     Encodes input strings for use in Xml
        /// </summary>
        /// <param name="input">String input to HtmlEncode</param>
        /// <returns>Encoded  string</returns>
        /// <remarks>
        ///     Intended use for Control method 'public override void RenderControl(HtmlTextWriter writer)' 
        /// </remarks>
        public static string ToXmlExtendedSymbols(this StringBuilder input)
        {
            ConcurrentDictionary<char, string> xmlsymbols = Extensions.XmlSymbols;
            var builder = new StringBuilder(string.Empty, input.Length + 128);

            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                if (xmlsymbols.ContainsKey(ch))
                {
                    builder.Append(xmlsymbols[ch]);
                }
                else
                {
                    builder.Append(ch);
                }
            }

            return builder.ToString();
        }
    }
}
