using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Math;

namespace BaseLib
{
    public static class BaseExtensions
    {
        public static bool IsEmpty(this string text) => string.IsNullOrEmpty(text);

        public static bool IsNotEmpty(this string text) => !string.IsNullOrEmpty(text);

        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source) => source is null || !source.Any();

        public static bool IsNotEmpty<TSource>(this IEnumerable<TSource> source) => !(source is null) && source.Any();

        public static string ToCsv<T>(this IEnumerable<T> source, string delimiter = ", ")
        {
            if (source.IsEmpty()) return "";

            var str = new StringBuilder(source.First().ToString());
            source.Skip(1).ForEach(s => str.AppendFormat("{0}{1}", delimiter, s));

            return str.ToString();
        }

        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> f)
        {
            foreach (var item in source)
            {
                f(item);
            }
        }

    }
}
