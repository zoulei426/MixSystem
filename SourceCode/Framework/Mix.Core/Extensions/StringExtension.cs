using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Mix.Core
{
    /// <summary>
    /// StringExtensions
    /// </summary>
    public static class StringExtension
    {
        private static readonly char[] Delimeters = { ' ', '-', '_' };

        /// <summary>
        /// Converts to snakecase.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public static string ToSnakeCase(this string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return SymbolsPipe(
                source,
                '_',
                (s, disableFrontDelimeter) =>
                {
                    if (disableFrontDelimeter)
                    {
                        return new char[] { char.ToLowerInvariant(s) };
                    }

                    return new char[] { '_', char.ToLowerInvariant(s) };
                });
        }

        /// <summary>
        /// Symbolses the pipe.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="mainDelimeter">The main delimeter.</param>
        /// <param name="newWordSymbolHandler">The new word symbol handler.</param>
        /// <returns></returns>
        private static string SymbolsPipe(
            string source,
            char mainDelimeter,
            Func<char, bool, char[]> newWordSymbolHandler)
        {
            var builder = new StringBuilder();

            bool nextSymbolStartsNewWord = true;
            bool disableFrontDelimeter = true;
            foreach (var symbol in source)
            {
                if (Delimeters.Contains(symbol))
                {
                    if (symbol == mainDelimeter)
                    {
                        builder.Append(symbol);
                        disableFrontDelimeter = true;
                    }

                    nextSymbolStartsNewWord = true;
                }
                else if (!char.IsLetterOrDigit(symbol))
                {
                    builder.Append(symbol);
                    disableFrontDelimeter = true;
                    nextSymbolStartsNewWord = true;
                }
                else
                {
                    if (nextSymbolStartsNewWord || char.IsUpper(symbol))
                    {
                        builder.Append(newWordSymbolHandler(symbol, disableFrontDelimeter));
                        disableFrontDelimeter = false;
                        nextSymbolStartsNewWord = false;
                    }
                    else
                    {
                        builder.Append(symbol);
                    }
                }
            }

            return builder.ToString();
        }

        #region ICN

        /// <summary>
        /// 1男0女
        /// </summary>
        /// <param name="identityCard"></param>
        /// <returns></returns>
        public static int? GetGenderByICN(this string identityCard)
        {
            if (identityCard.Length != 15 && identityCard.Length != 18)
            {
                Trace.WriteLine($"身份证{identityCard}不合法！");
                return null;
            }

            string gender = string.Empty;
            if (identityCard.Length == 18)
            {
                gender = identityCard.Substring(14, 3);
            }
            if (identityCard.Length == 15)
            {
                gender = identityCard.Substring(12, 3);
            }

            if (int.Parse(gender) % 2 == 0)//性别代码为偶数是女性奇数为男性
            {
                return 0;
                //return "女";
            }
            else
            {
                return 1;
                //return "男";
            }
        }

        #endregion ICN
    }
}