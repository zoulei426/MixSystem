using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Mix.Windows.Core
{
    /// <summary>
    /// FileLocator
    /// </summary>
    /// <seealso cref="System.IEquatable{T}" />
    [DebuggerDisplay("{" + nameof(FullPath) + "}")]
    public class FileLocator : IEquatable<FileLocator>
    {
        /// <summary>
        /// Gets a regular expression for splitting the file full path string.
        /// In the right case, the group will have four elements:
        /// [0]: FullPath
        /// [1]: FolderName
        /// [2]: FileName
        /// [3]: FileExtension
        /// </summary>
        private static readonly Regex RegexFileLocation = new Regex(@"^([\\/]?(?:\w:)?(?:[^\\/]+?[\\/])*?)([^\\/]+?(?:\.(\w+?))?)?$", RegexOptions.Compiled);

        /// <summary>
        /// Gets a string representing the full path of the file.
        /// </summary>
        public string FullPath { get; }

        /// <summary>
        /// Gets a string representing the folder where the file is located.
        /// </summary>
        public string FolderPath { get; }

        /// <summary>
        /// Gets a string representing the file name.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Gets a string representing the file extension.
        /// </summary>
        public string FileExtension { get; }

        /// <summary>
        /// Initializes a instance of <see cref="FileLocator"/> with specified file full path.
        /// </summary>
        /// <param name="fileFullPath">A string representing the full path of the file. </param>
        public FileLocator(string fileFullPath)
        {
            var matchResult = RegexFileLocation.Match(fileFullPath);

            if (matchResult.Groups == null || matchResult.Groups.Count != 4)
                throw new ArgumentException($"The file path is not valid: {fileFullPath}", fileFullPath);

            FullPath = matchResult.Groups[0].Value;
            var temp = matchResult.Groups[1].Value;
            if (!string.IsNullOrEmpty(temp)) FolderPath = temp.Remove(temp.Length - 1); // Remove the "\" or "/" at the end.
            FileName = matchResult.Groups[2].Value;
            FileExtension = matchResult.Groups[3].Value.ToLower();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => FullPath;

        #region Implements Equals

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(FileLocator other) => !Equals(other, null) && string.Equals(FullPath, other.FullPath);

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj) => ReferenceEquals(this, obj) || Equals(obj as FileLocator);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(FileLocator left, FileLocator right)
        {
            if (left is null || right is null)
                return Equals(left, right);

            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(FileLocator left, FileLocator right) => !(left == right);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode() => FullPath != null ? FullPath.GetHashCode() : 0;

        #endregion Implements Equals

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="FileLocator"/>.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator FileLocator(string filePath) => string.IsNullOrEmpty(filePath) ? null : new FileLocator(filePath);

        /// <summary>
        /// Performs an implicit conversion from <see cref="FileLocator"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="fileLocation">The file location.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator string(FileLocator fileLocation) => fileLocation?.FullPath;
    }
}