using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace vldaptest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!(args.Length == 1 && args[0].StartsWith('/')))
            {
                Console.WriteLine("Usage: vldaptest fullFilePath");
                return;
            }

            // read input and load it into a test collection
            var filePath = args[0];
            var s = File.ReadAllText(filePath);

            Rootobject testConfig = JsonConvert.DeserializeObject<Rootobject>(s);

            // Files containing expected output are here
            var expectedPath = testConfig.ExpectedPath;

            // Actual output files will be created under a timestamped folder
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var actualPath = Path.Combine(testConfig.ActualPath, timestamp);
            Directory.CreateDirectory(actualPath);

            // Run tests and collect responses
            foreach (var test in testConfig.Tests)
            {
                var expectedResultPath = Path.Combine(expectedPath, test.FileName);
                var actualResultPath = Path.Combine(actualPath, test.FileName);

                // load expected result
                var expectedResult = File.ReadAllText(expectedResultPath);

                // Run command and capture output to file
                var actualResult = test.Command.Execute();
                File.WriteAllText(actualResultPath, actualResult);

                test.Result = string.Compare(actualResult, expectedResult, StringComparison.Ordinal);
            }

            // Collect test ids for any failed tests
            var failures = testConfig.Tests.Where(t => t.Result != 0)
                            .SelectMany(t => t.Id).ToList<char>();

            var result = failures.Count > 0
                ? $"FAILURE! These tests have failed: {string.Join(", ", failures)}"
                : "SUCCESS!";

            // Report outcome
            Console.WriteLine(result);
        }
    }
}
