using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaleyLab.Extension
{
    /// <summary>
    /// Assertion extension class.
    /// Provides methods for object assertion.
    /// </summary>
    public static class AssertionExtensions
    {
        /// <summary>
        /// Indicates whether an object instance is null.
        /// </summary>
        /// <param name="instance">Object instance</param>
        /// <returns>True or false</returns>
        public static bool IsNull(this object instance)
        {
            return instance == null;
        }

        /// <summary>
        /// Indicates whether an object instance is not null.
        /// </summary>
        /// <param name="instance">Object instance</param>
        /// <returns>True or false</returns>
        public static bool IsNotNull(this object instance)
        {
            return instance != null;
        }

        /// <summary>
        /// Indicates whether a enumerable collection is null or empty.
        /// </summary>
        /// <typeparam name="T">Collection instance type</typeparam>
        /// <param name="enumerable">Enumerable instance</param>
        /// <returns>True or false</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        /// <summary>
        /// Indicates whether a enumerable collection is not null or empty.
        /// </summary>
        /// <typeparam name="T">Collection instance type</typeparam>
        /// <param name="enumerable">Enumerable instance</param>
        /// <returns>True or false</returns>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }

        /// <summary>
        /// Indicates whether a guid is empty. 
        /// </summary>
        /// <param name="guid">Guid instance</param>
        /// <returns>True or false</returns>
        public static bool IsEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        /// <summary>
        /// Indicates whether a guid is not empty. 
        /// </summary>
        /// <param name="guid">Guid instance</param>
        /// <returns>True or false</returns>
        public static bool IsNotEmpty(this Guid guid)
        {
            return guid != Guid.Empty;
        }

        /// <summary>
        /// Indicates whether a string is null or empty.
        /// </summary>
        /// <param name="str">String instance</param>
        /// <returns>True or false</returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Indicates whether a string is not null or empty.
        /// </summary>
        /// <param name="str">String instance</param>
        /// <returns>True or false</returns>
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Asserts an object if null that throws specified exception.
        /// </summary>
        /// <param name="instance">Object instance</param>
        /// <param name="exception">Instance of spcified exception </param>
        public static void ThrowsExIfNull(this object instance, Exception exception)
        {
            if (instance.IsNull()) { throw exception; }
        }

        /// <summary>
        /// Asserts an object if null throws <see cref="System.ArgumentNullException">System.ArgumentNullException</see> with argument name.
        /// </summary>
        /// <param name="instance">Object instance</param>
        /// <param name="name"></param>
        public static void ThrowsArgumentNullExIfNull(this object instance,string name)
        {
            if (instance.IsNull()) { throw new ArgumentNullException(name); }
        }

        /// <summary>
        /// Asserts an object if null that throws argument exception.
        /// </summary>
        /// <param name="instance">Object instance</param>
        /// <param name="message">Argument exception message</param>
        public static void ThrowsArgumentExIfNull(this object instance,string message)
        {
            if (instance.IsNull()) { throw new ArgumentException(message); }
        }

        /// <summary>
        /// Asserts an object if null that throws invalid operation exception.
        /// </summary>
        /// <param name="instance">Object instance</param>
        /// <param name="message">Invalid operation exception message</param>
        public static void ThrowsInvalidOpExIfNull(this object instance, string message)
        {
            if (instance.IsNull()) { throw new InvalidOperationException(message); }
        }

        /// <summary>
        /// Asserts a string if null or empty that throws argument exception.
        /// </summary>
        /// <param name="str">String instance</param>
        /// <param name="message">Argument exception message</param>
        public static void ThrowArgumentExIfNullOrEmpty(this string str, string message)
        {
            if (str.IsNullOrEmpty()) { throw new ArgumentException(message); }
        }

        /// <summary>
        /// Asserts an enumerable collection if null or empty that throws argument exception.
        /// </summary>
        /// <typeparam name="T">Collection instance type</typeparam>
        /// <param name="enumerable">Enumerable instance</param>
        /// <param name="message">Argument exception message</param>
        public static void ThrowsArgumentExIfNullOrEmpty<T>(this IEnumerable<T> enumerable, string message)
        {
            if (enumerable.IsNullOrEmpty()) { throw new ArgumentException(message); }
        }

        /// <summary>
        /// Asserts a guid if empty that throws argument exception.
        /// </summary>
        /// <param name="guid">Guid instance</param>
        /// <param name="message">Argument exception message</param>
        public static void ThrowsArgumentExIfEmpty(this Guid guid,string message)
        {
            if (guid.IsEmpty()) { throw new ArgumentException(message); }
        }
    }
}
