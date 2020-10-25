using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mix.Core
{
    /// <summary>
    /// 守卫
    /// </summary>
    public static class Guards
    {
        /// <summary>
        /// Throws if null.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ThrowIfNull(params object[] parameters)
        {
            if (parameters.Any(item => item == null))
                throw new ArgumentNullException();
        }

        /// <summary>
        /// Throws if null or empty.
        /// </summary>
        /// <param name="strings">The strings.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ThrowIfNullOrEmpty(params string[] strings)
        {
            if (strings.Any(string.IsNullOrEmpty))
                throw new ArgumentNullException();
        }

        /// <summary>
        /// Throws if null or empty.
        /// </summary>
        /// <param name="strings">The strings.</param>
        public static void ThrowIfNullOrEmpty(IEnumerable<string> strings)
        {
            ThrowIfNull(strings);
            ThrowIfNullOrEmpty(strings.ToArray());
        }

        /// <summary>
        /// Throws if file not found.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <exception cref="FileNotFoundException">Can not found the specified file path.</exception>
        public static void ThrowIfFileNotFound(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Can not found the specified file path. ", path);
        }

        /// <summary>
        /// Throws if folder not fount.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <exception cref="DirectoryNotFoundException">Can not found the specified path {path}.</exception>
        public static void ThrowIfFolderNotFount(string path)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"Can not found the specified path {path}. ");
        }

        /// <summary>
        /// Throws if invalid path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <exception cref="DirectoryNotFoundException">The specified path is not a valid file or directory.  ({path})</exception>
        public static void ThrowIfInvalidPath(string path)
        {
            if (!File.Exists(path) && !Directory.Exists(path))
                throw new DirectoryNotFoundException($"The specified path is not a valid file or directory.  ({path})");
        }

        /// <summary>
        /// Throws if not.
        /// </summary>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void ThrowIfNot(bool condition)
        {
            if (!condition)
                throw new InvalidOperationException();
        }

        /// <summary>
        /// Throws if not.
        /// </summary>
        /// <param name="condition">The condition.</param>
        public static void ThrowIfNot(Func<bool> condition)
        {
            ThrowIfNull(condition);
            ThrowIfNot(condition());
        }
    }
}