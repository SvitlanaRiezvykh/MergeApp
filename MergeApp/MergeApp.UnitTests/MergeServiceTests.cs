using MergeApp.Contracts;
using System.Collections.Generic;
using Xunit;

namespace MergeApp.UnitTests
{
    public class MergeServiceTests
    {
        [Fact]
        public void MergeEmptyRanked()
        {
            var service = new MergeService();
            var rankedInput = new List<Ranked>() { };

            var result = service.Merge(rankedInput);

            Assert.Empty(result);
        }

        [Fact]
        public void MergeWithOneRankedWithSkippedVal()
        {
            var service = new MergeService();
            var rankedInput = new List<Ranked>()
            {
                new Ranked()
                {
                    Priority = 1,
                    Vals = new Dictionary<string,object>()
                    {
                        { "timeout", "4s"},
                        { "startup_delay", "5m"},
                        { "skip_percent_active", 1.2},
                    }
                },
            };

            var result = service.Merge(rankedInput);

            var expectedOutput = new Dictionary<string, object>()
            {
                { "timeout", "4s"},
                { "startup_delay", "5m"},
            };
            Assert.Equal(expectedOutput, result);
        }

        [Fact]
        public void MergeWithOneRankedWithoutSkippedVal()
        {
            var service = new MergeService();
            var rankedInput = new List<Ranked>()
            {
                new Ranked()
                {
                    Priority = 0,
                    Vals = new Dictionary<string,object>()
                    {
                        { "label", "testing"},
                        { "startup_delay", "5m"},
                        { "percent_active", 0.2},
                    }
                },
            };

            var result = service.Merge(rankedInput);

            var expectedOutput = new Dictionary<string, object>()
            {
                { "label", "testing"},
                { "startup_delay", "5m"},
                { "percent_active", 0.2},
            };
            Assert.Equal(expectedOutput, result);
        }

        [Fact]
        public void MergeRanked()
        {
            var service = new MergeService();
            var rankedInput = new List<Ranked>()
            {
                new Ranked()
                {
                    Priority = 2,
                    Vals = new Dictionary<string,object>()
                    {
                        {"timeout", "3s"},
                        {"num_threads", 500},
                        {"buffer_size", 4000},
                        {"use_sleep", true}
                    }
                },
                new Ranked()
                {
                    Priority = 1,
                    Vals = new Dictionary<string,object>()
                    {
                        { "timeout", "2s"},
                        { "startup_delay", "2m"},
                        { "skip_percent_active", 0.2},
                    }
                },
                new Ranked()
                {
                    Priority = 0,
                    Vals = new Dictionary<string,object>()
                    {
                        { "num_threads", 300},
                        { "buffer_size", 3000},
                        { "label", "testing"},
                    }
                }
            };

            var result = service.Merge(rankedInput);

            var expectedOutput = new Dictionary<string, object>()
            {
                {"timeout", "2s" },
                { "num_threads", 300 },
                { "buffer_size", 3000 },
                { "use_sleep", true },
                { "startup_delay", "2m" },
                { "label", "testing" },
            };
            Assert.Equal(expectedOutput, result);
        }
    }
}
