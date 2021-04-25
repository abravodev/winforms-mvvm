using System;
using System.Collections.Generic;
using System.Linq;

namespace WinformsTools.Common.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds the elements of the specified collection to the end of the <paramref name="source"/>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="collection">The collection whose elements should be added to the end of the <paramref name="source"/></param>
        public static void AddRange<TSource>(this ICollection<TSource> source, IEnumerable<TSource> collection)
        {
            foreach (var item in collection)
            {
                source.Add(item);
            }
        }

        /// <summary>
        /// Replaces the content of <paramref name="source"/> with <paramref name="collection"/>, 
        /// using <see cref="ICollection{T}.Clear"/> and <see cref="ICollection{T}.Add(T)"/>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="collection"></param>
        public static void Replace<TSource>(this ICollection<TSource> source, IEnumerable<TSource> collection)
        {
            source.Clear();
            source.AddRange(collection);
        }

        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source) => !source.Any();

        public static string Joined<TSource>(this IEnumerable<TSource> source, string separator = ",") 
            => string.Join(separator, source);

        public static string[] Split(this string source, string separator)
            => source?.Split(new string[] { separator }, StringSplitOptions.None) ?? throw new ArgumentNullException(nameof(source));
    }
}
