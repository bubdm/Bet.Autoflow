using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bet.Autoflow
{
    internal static class Guard
    {
        /// <summary>
        /// Validates that the value is not null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument">The argument.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        [DebuggerHidden]
        internal static T ArgumentIsNotNull<T>(T argument, string description)
            where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(description);
            }

            return argument;
        }

        /// <summary>
        /// Validates if the value is the greater than zero.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument">The argument.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        [DebuggerHidden]
        public static T ArgumentGreaterThanZero<T>(T argument, string description)
        {
            if (Convert.ToInt32(argument) < 1)
            {
                throw new ArgumentOutOfRangeException(description);
            }

            return argument;
        }

        /// <summary>
        /// Validates if the date is initialized.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">DateTime has not been initialized</exception>
        [DebuggerHidden]
        public static DateTime ArgumentDateIsInitialized(DateTime argument, string description = null)
        {
            if (argument == default)
            {
                throw new ArgumentException( description ?? "DateTime has not been initialized");
            }

            return argument;
        }

        /// <summary>
        /// Determines whether the dictionary contains the key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="search">The search.</param>
        /// <param name="key">The key.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        [DebuggerHidden]
        public static Dictionary<T, K> ContainsKey<T, K>(Dictionary<T, K> search,
            T key,
            string description)
        {
            if (search.ContainsKey(key) == false)
            {
                throw new KeyNotFoundException(description);
            }

            return search;
        }

        /// <summary>
        /// Validates the specified condition.
        /// </summary>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.ArgumentException"></exception>
        [DebuggerHidden]
        public static void That(bool condition, string message)
        {
            if (condition == false)
            {
                throw new ArgumentException(message);
            }
        }
    }
}
