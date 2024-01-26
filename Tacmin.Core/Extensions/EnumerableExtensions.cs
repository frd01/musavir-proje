using System;
using System.Collections.Generic;
using System.Linq;

namespace Tacmin.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static List<List<T>> ChunkBy<T>(this IEnumerable<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var t in source)
            {
                action(t);
            }
        }

        public static void Each<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            var i = 0;
            foreach (var t in source)
            {
                action(t, i++);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var t in source)
            {
                action(t);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            var i = 0;
            foreach (var t in source)
            {
                action(t, i++);
            }
        }

        public static IEnumerable<TSource> SelectRecursive<TSource>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TSource>> recursiveSelector)
        {
            var enumerators = new Stack<IEnumerator<TSource>>();
            enumerators.Push(source.GetEnumerator());
            try
            {
                while (enumerators.Count > 0)
                {
                    if (!enumerators.Peek().MoveNext())
                    {
                        enumerators.Pop().Dispose();
                    }
                    else
                    {
                        var tSource = enumerators.Peek().Current;
                        yield return tSource;
                        var tSources = recursiveSelector(tSource);
                        if (tSources == null)
                        {
                            continue;
                        }
                        enumerators.Push(tSources.GetEnumerator());
                    }
                }
            }
            finally
            {
                while (enumerators.Count > 0)
                {
                    enumerators.Pop().Dispose();
                }
            }
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> first, params T[] second)
        {
            return first.Concat(second);
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> first, params T[] second)
        {
            return second.Concat(first);
        }
        public static string Join<T>(this IEnumerable<T> source, string seperator)
        {
            return String.Join(seperator, source);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return (source == null || !source.Any());
        }

        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }

        public static IEnumerable<T> Add<T>(this IEnumerable<T> e, T value)
        {
            foreach (var cur in e)
            {
                yield return cur;
            }

            yield return value;
        }
    }
}
