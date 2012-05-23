//-----------------------------------------------------------------------------
// <copyright file="Extensions.Validation.CreditCard.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///  Will match obvious mistakes
        ///  A description of the Credit Card regular expression:
        ///  Beginning of line or string
        ///  Match expression but don't capture it. [4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6011[0-9]{12}|3(?:0[0-5]|[68][0-9])[0-9]{11}|3[47][0-9]{13}|(?:2131|1800)\\d{11}]
        ///      Select from 6 alternatives
        ///          4[0-9]{12}(?:[0-9]{3})?
        ///              4
        ///              Any character in this class: [0-9], exactly 12 repetitions
        ///              Match expression but don't capture it. [[0-9]{3}], zero or one repetitions
        ///                  Any character in this class: [0-9], exactly 3 repetitions
        ///          5[1-5][0-9]{14}
        ///              5
        ///              Any character in this class: [1-5]
        ///              Any character in this class: [0-9], exactly 14 repetitions
        ///          6011[0-9]{12}
        ///              6011
        ///              Any character in this class: [0-9], exactly 12 repetitions
        ///          3(?:0[0-5]|[68][0-9])[0-9]{11}
        ///              3
        ///              Match expression but don't capture it. [0[0-5]|[68][0-9]]
        ///                  Select from 2 alternatives
        ///                      0[0-5]
        ///                          0
        ///                          Any character in this class: [0-5]
        ///                      [68][0-9]
        ///                          Any character in this class: [68]
        ///                          Any character in this class: [0-9]
        ///              Any character in this class: [0-9], exactly 11 repetitions
        ///          3[47][0-9]{13}
        ///              3
        ///              Any character in this class: [47]
        ///              Any character in this class: [0-9], exactly 13 repetitions
        ///          (?:2131|1800)\\d{11}
        ///              Match expression but don't capture it. [2131|1800]
        ///                  Select from 2 alternatives
        ///                      2131
        ///                          2131
        ///                      1800
        ///                          1800
        ///              Literal \
        ///              d, exactly 11 repetitions
        ///  End of line or string
        /// </summary>
        private const string RegexPatternCreditCard = @"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6011[0-9]{12}|3(?:0[0-5]|[68][0-9])[0-9]{11}|3[47][0-9]{13}|(?:2131|1800)\\d{11})$";

        /// <summary>  
        ///  A description of the Credit Card regular expression:  
        ///  Beginning of line or string
        ///  Match expression but don't capture it. [(?:[3][4|7])(?:\d{13})]
        ///      (?:[3][4|7])(?:\d{13})
        ///          Match expression but don't capture it. [[3][4|7]]
        ///              [3][4|7]
        ///                  Any character in this class: [3]
        ///                  Any character in this class: [4|7]
        ///          Match expression but don't capture it. [\d{13}]
        ///              Any digit, exactly 13 repetitions
        ///  End of line or string
        /// </summary>
        private const string RegexPatternCreditCardAmericanExpress = @"^(?:(?:[3][4|7])(?:\d{13}))$";

        /// <summary>
        ///  A description of the Credit Card  regular expression:    
        ///  Beginning of line or string
        ///  Match expression but don't capture it. [(?:[3](?:[0][0-5]|[6|8]))(?:\d{11,12})]
        ///      (?:[3](?:[0][0-5]|[6|8]))(?:\d{11,12})
        ///          Match expression but don't capture it. [[3](?:[0][0-5]|[6|8])]
        ///              [3](?:[0][0-5]|[6|8])
        ///                  Any character in this class: [3]
        ///                  Match expression but don't capture it. [[0][0-5]|[6|8]]
        ///                      Select from 2 alternatives
        ///                          [0][0-5]
        ///                              Any character in this class: [0]
        ///                              Any character in this class: [0-5]
        ///                          Any character in this class: [6|8]
        ///          Match expression but don't capture it. [\d{11,12}]
        ///              Any digit, between 11 and 12 repetitions
        ///  End of line or string
        /// </summary>
        private const string RegexPatternCreditCardDinersClub = @"^(?:(?:[3](?:[0][0-5]|[6|8]))(?:\d{11,12}))$";

        /// <summary>   
        ///  A description of the Credit Card regular expression:
        ///  Beginning of line or string
        ///  Match expression but don't capture it. [(?:6011)(?:\d{12})]
        ///      (?:6011)(?:\d{12})
        ///          Match expression but don't capture it. [6011]
        ///              6011
        ///                  6011
        ///          Match expression but don't capture it. [\d{12}]
        ///              Any digit, exactly 12 repetitions
        ///  End of line or string
        /// </summary>
        private const string RegexPatternCreditCardDiscover = @"^(?:(?:6011)(?:\d{12}))$";

        /// <summary>
        ///  A description of the Credit Card regular expression:
        ///  Beginning of line or string
        ///  Match expression but don't capture it. [(?:[5][1-5])(?:\d{14})]
        ///      (?:[5][1-5])(?:\d{14})
        ///          Match expression but don't capture it. [[5][1-5]]
        ///              [5][1-5]
        ///                  Any character in this class: [5]
        ///                  Any character in this class: [1-5]
        ///          Match expression but don't capture it. [\d{14}]
        ///              Any digit, exactly 14 repetitions
        ///  End of line or string
        /// </summary>
        private const string RegexPatternCreditCardMasterCard = @"^(?:(?:[5][1-5])(?:\d{14}))$";

        /// <summary>  
        ///  A description of the Credit Card regular expression:     
        ///  Beginning of line or string
        ///  Match expression but don't capture it. [(?:[4])(?:\d{12}|\d{15})]
        ///      (?:[4])(?:\d{12}|\d{15})
        ///          Match expression but don't capture it. [[4]]
        ///              Any character in this class: [4]
        ///          Match expression but don't capture it. [\d{12}|\d{15}]
        ///              Select from 2 alternatives
        ///                  Any digit, exactly 12 repetitions
        ///                  Any digit, exactly 15 repetitions
        ///  End of line or string
        /// </summary>
        private const string RegexPatternCreditCardVisa = @"^(?:(?:[4])(?:\d{12}|\d{15}))$";

        /// <summary>
        ///     Use for obvious errors to find. Other credit card validation uses Luhn credit card validation algorithm.
        ///     Rule determines whether the specified string is a valid Genuine Credit Card like VISA, MASTER, JSB, AMERICA EXPRESS. 
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidCreditCardNumber(string input)
        {
            return !string.IsNullOrEmpty(input) && Regex.IsMatch(input, Extensions.RegexPatternCreditCard);
        }

        /// <summary>
        ///     Uses Luhn credit card validation algorithm. Rule determines whether the specified string is valid credit card. 
        /// See: http://en.wikipedia.org/wiki/Luhn_algorithm
        /// </summary>
        /// <param name="creditCard">The input as credit card string.</param>
        /// <param name="cardtype">The credit card type.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidCreditCardNumber(this string creditCard, CreditCardType cardtype)
        {
            bool success = false;
            if (!string.IsNullOrEmpty(creditCard) && Extensions.IsValidCreditCardFormatValid(creditCard))
            {
                creditCard = Extensions.CleanCreditCardInputString(creditCard);
                switch (cardtype)
                {
                    case CreditCardType.Visa:
                        success = Regex.IsMatch(creditCard, Extensions.RegexPatternCreditCardVisa);
                        break;
                    case CreditCardType.Master:
                        success = Regex.IsMatch(creditCard, Extensions.RegexPatternCreditCardMasterCard);
                        break;
                    case CreditCardType.AmericanExpress:
                        success = Regex.IsMatch(creditCard, Extensions.RegexPatternCreditCardAmericanExpress);
                        break;
                    case CreditCardType.Discover:
                        success = Regex.IsMatch(creditCard, Extensions.RegexPatternCreditCardDiscover);
                        break;
                    case CreditCardType.DinersClub:
                        success = Regex.IsMatch(creditCard, Extensions.RegexPatternCreditCardDinersClub);
                        break;
                }
            }

            return success;
        }

        /// <summary>
        ///     Uses Luhn credit card validation algorithm. Rule determines whether the specified string is an American Express, Discover, MasterCard, or Visa
        ///  See http://en.wikipedia.org/wiki/Luhn_algorithm
        /// </summary>
        /// <param name="creditCard">The credit card.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidCreditCardNumberBigFour(this string creditCard)
        {
            bool success = false;
            if (!string.IsNullOrEmpty(creditCard) && Extensions.IsValidCreditCardFormatValid(creditCard))
            {
                creditCard = Extensions.CleanCreditCardInputString(creditCard);
                success =
                        Regex.IsMatch(creditCard, Extensions.RegexPatternCreditCardVisa) ||
                        Regex.IsMatch(creditCard, Extensions.RegexPatternCreditCardMasterCard) ||
                        Regex.IsMatch(creditCard, Extensions.RegexPatternCreditCardDiscover) ||
                        Regex.IsMatch(creditCard, Extensions.RegexPatternCreditCardAmericanExpress);
            }

            return success;
        }

        /// <summary>
        ///     Cleans the credit card number, returning just the numeric values.
        /// </summary>
        /// <param name="creditCard">The credit card.</param>
        /// <returns>Digits only in string format</returns>
        private static string CleanCreditCardInputString(string creditCard)
        {
            var regex = new Regex(
                                      @"[^0-9]",
                                      RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.Singleline);

            return regex.Replace(creditCard, string.Empty);
        }

        /// <summary>
        ///     Rule determines whether the credit card number, once cleaned, passes the Luhn algorithm.
        /// See: http://en.wikipedia.org/wiki/Luhn_algorithm
        /// </summary>
        /// <param name="creditCardNumber">The credit card number.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsValidCreditCardFormatValid(string creditCardNumber)
        {
            creditCardNumber = Extensions.CleanCreditCardInputString(creditCardNumber);
            if (!string.IsNullOrEmpty(creditCardNumber))
            {
                var numArray = new int[creditCardNumber.Length];
                for (int i = 0; i < numArray.Length; i++)
                {
                    numArray[i] = Convert.ToInt16(creditCardNumber[i].ToString(CultureInfo.InvariantCulture));
                }

                return Extensions.IsValidLuhn(numArray);
            }

            return false;
        }

        /// <summary>
        ///     Rule determines whether the specified int array passes the Luhn algorithm
        /// </summary>
        /// <param name="digits">The int array to evaluate</param>
        /// <returns>
        ///     <c>true</c> if it validates; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsValidLuhn(int[] digits)
        {
            int sum = 0;
            bool alt = false;
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                if (alt)
                {
                    digits[i] *= 2;
                    if (digits[i] > 9)
                    {
                        digits[i] -= 9; // equivalent to adding the target of digits
                    }
                }

                sum += digits[i];
                alt = !alt;
            }

            return sum % 10 == 0;
        }
    }
}