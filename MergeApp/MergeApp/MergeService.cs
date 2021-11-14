using MergeApp.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace MergeApp
{
    public class MergeService
    {
        public Dictionary<string, object> Merge(List<Ranked> rankeds)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            var vals = rankeds.Select(item => item.Vals).ToArray();
            foreach (var val in vals)
            {
                var pairsWithoutSkipped = val.Where(item => !item.Key.StartsWith("skip"));
                AddOrUpdateVals(result, pairsWithoutSkipped);
            }
            return result;
        }

        private static void AddOrUpdateVals(
            Dictionary<string, object> result,
            IEnumerable<KeyValuePair<string, object>> pairsWithoutSkipped)
        {
            foreach (var pair in pairsWithoutSkipped)
            {
                if (result.ContainsKey(pair.Key))
                {
                    result[pair.Key] = pair.Value;
                }
                else
                {
                    result.Add(pair.Key, pair.Value);
                }
            }
        }
    }
}
