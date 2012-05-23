//-----------------------------------------------------------------------------
// <copyright file="VEventHandlerList.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Date:       06/20/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.Runtime;
    using System.Security.Permissions;

    /// <summary>
    /// Provides a simple list of delegates. This class cannot be inherited.
    /// </summary>
    /// <remarks>
    /// Modified version of the System.ComponentModel.VEventHandlerList
    /// </remarks>
    [HostProtection(SecurityAction.LinkDemand, SharedState = true)]
    public sealed class VEventHandlerList : IDisposable
    {
        /// <summary>
        /// The list head entry
        /// </summary>
        private ListEntry head;

        /// <summary>
        /// Initializes a new instance of the <see cref="VEventHandlerList"/> class.
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public VEventHandlerList()
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Delegate"/> with the specified key.
        /// </summary>
        /// <param name="key">An delegate to find in the list.</param>
        /// <returns>
        /// The delegate for the specified key, or null if a delegate does not exist.
        /// </returns>
        public Delegate this[string key]
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    ListEntry entry = this.Find(key);

                    if (entry != null)
                    {
                        return entry.Handler;
                    }
                }

                return null;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    ListEntry entry = this.Find(key);
                    if (entry != null)
                    {
                        entry.Handler = value;
                    }
                    else
                    {
                        this.head = new ListEntry(key, value, this.head);
                    }
                }
            }
        }

        /// <summary>
        /// Adds a delegate to the list.
        /// </summary>
        /// <param name="key">The unique key that owns the event.</param>
        /// <param name="value">The delegate to add to the list.</param>
        public void AddHandler(string key, Delegate value)
        {
            if (!string.IsNullOrEmpty(key) && value != null)
            {
                ListEntry entry = this.Find(key);
                if (entry != null)
                {
                    entry.Handler = Delegate.Combine(entry.Handler, value);
                }
                else
                {
                    this.head = new ListEntry(key, value, this.head);
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.head = null;
        }

        /// <summary>
        /// Gets all the unique keys.
        /// </summary>
        /// <returns>The key list</returns>
        public IEnumerable<string> GetKeys()
        {
            ListEntry listEntry = this.head;
            while (listEntry != null)
            {
                yield return listEntry.Key;

                listEntry = listEntry.Next;
            }
        }

        /// <summary>
        /// Gets the delegates.
        /// </summary>
        /// <returns>The list of the delegates</returns>
        public IEnumerable<Delegate> GetDelegates()
        {
            ListEntry listEntry = this.head;
            while (listEntry != null)
            {
                yield return listEntry.Handler;

                listEntry = listEntry.Next;
            }
        }

        /// <summary>
        /// Removes a delegate from the list.
        /// </summary>
        /// <param name="key">The string that owns the event.</param>
        /// <param name="value">The delegate to remove from the list.</param>
        public void RemoveHandler(string key, Delegate value)
        {
            if (!string.IsNullOrEmpty(key) && value != null)
            {
                ListEntry entry = this.Find(key);
                if (entry != null)
                {
                    entry.Handler = Delegate.Remove(entry.Handler, value);
                }
            }
        }

        /// <summary>
        /// Finds the entry by specified key.
        /// </summary>
        /// <param name="key">The unique key.</param>
        /// <returns>The entry record</returns>
        private ListEntry Find(string key)
        {
            ListEntry listEntry = this.head;

            while (listEntry != null)
            {
                if (string.Equals(listEntry.Key, key, StringComparison.InvariantCultureIgnoreCase))
                {
                    return listEntry;
                }

                listEntry = listEntry.Next;
            }

            return null;
        }

        /// <summary>
        /// The list entry 
        /// </summary>
        private sealed class ListEntry
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ListEntry"/> class.
            /// </summary>
            /// <param name="key">The unique key.</param>
            /// <param name="handler">The handler.</param>
            /// <param name="next">The next entry.</param>
            public ListEntry(string key, Delegate handler, ListEntry next)
            {
                this.Next = next;
                this.Key = key;
                this.Handler = handler;
            }

            /// <summary>
            /// Gets or sets the handler.
            /// </summary>
            /// <value>
            /// The handler (delegate).
            /// </value>
            internal Delegate Handler { get; set; }

            /// <summary>
            /// Gets the Unique key.
            /// </summary>
            /// <value>
            /// The Unique key.
            /// </value>
            internal string Key { get; private set; }

            /// <summary>
            /// Gets the next entry.
            /// </summary>
            /// <value>
            /// The next entry.
            /// </value>
            internal ListEntry Next { get; private set; }
        }
    }
}