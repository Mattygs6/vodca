//-----------------------------------------------------------------------------
// <copyright file="VFormMessageCacheManager.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/01/2012
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// The Message Cache
    /// </summary>
    public static partial class VFormMessageCacheManager
    {
        /// <summary>
        /// The cache
        /// </summary>
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, string> Cache = new ConcurrentDictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// The thread access synch object
        /// </summary>
        private static readonly object Synch = new object();

        /// <summary>
        /// Resolve the error message event
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The validation args.</param>
        /// <returns>The error message</returns>
        public delegate string ResolveMessageEvent(ValidateAttribute sender, ValidationArgs args);

        /// <summary>
        /// Occurs when resolve error message.
        /// </summary>
        private static event ResolveMessageEvent ResolveErrorMessageById;

        /// <summary>
        /// Adds the resolve message event.
        /// </summary>
        /// <param name="customevent">The custom event.</param>
        public static void AddResolveErrorMessageByIdEvent(ResolveMessageEvent customevent)
        {
            lock (Synch)
            {
                if (ResolveErrorMessageById == null)
                {
                    ResolveErrorMessageById += customevent;
                }
            }
        }

        /// <summary>
        /// Adds the messages.
        /// </summary>
        /// <param name="messages">The messages.</param>
        public static void AddMessages(IEnumerable<IValidateDisplayMessage> messages)
        {
            if (messages != null)
            {
                foreach (var message in messages)
                {
                    string key = string.Concat(message.MessageId, '|', message.MessageLanguage);
                    Cache[key] = message.Message;
                }
            }
        }

        /// <summary>
        /// Adds the messages.
        /// </summary>
        /// <param name="messages">The messages.</param>
        public static void AddMessages(params IValidateDisplayMessage[] messages)
        {
            if (messages != null)
            {
                foreach (var message in messages)
                {
                    string key = string.Concat(message.MessageId, '|', message.MessageLanguage);
                    Cache[key] = message.Message;
                }
            }
        }

        /// <summary>
        /// Tries the resolve error message by id.
        /// </summary>
        /// <param name="validateAttribute">The validate attribute.</param>
        /// <param name="args">The args.</param>
        /// <returns>The validation message</returns>
        internal static string TryResolveErrorMessageById(ValidateAttribute validateAttribute, ValidationArgs args)
        {
            string message = string.Empty;
            if (ResolveErrorMessageById != null)
            {
                message = ResolveErrorMessageById(validateAttribute, args);
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                string key = string.Concat(validateAttribute.MessageId, '|', args.Instance.ContextLanguage);
                if (!Cache.TryGetValue(key, out message))
                {
                    message = args.PropertyName;
                }
            }

            return message;
        }
    }
}