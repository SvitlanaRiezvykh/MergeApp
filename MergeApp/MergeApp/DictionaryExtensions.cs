using System;
using System.Collections.Generic;
using System.Linq;

namespace MergeApp
{
    public static class DictionaryExtensions
    {
        public static string ToPrettyString<T, V>(this IDictionary<T, V> keyValuePairs)
        {
            var lines = keyValuePairs.Select(kvp => kvp.Key + ": " + kvp.Value.ToString());
            var linesString = string.Join(Environment.NewLine, lines);

            return $"Output: {Environment.NewLine}{{{Environment.NewLine}{linesString}{Environment.NewLine}}}";
        }
    }
}
