using MergeApp.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MergeApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var tuneinService = new TuneInService();
            var mergeService = new MergeService();

            var ranked = await tuneinService.GetJsonResponse();
            var result = mergeService.Merge(ranked);

            Console.WriteLine(result.ToPrettyString());
        }
    }
}
