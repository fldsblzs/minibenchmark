using System;

namespace MiniBenchmark.Samples
{
    internal class Helper
    {
        internal static void LogSummaryToConsole(Summary summary)
        {
            Console.WriteLine($"Success: {summary.IsSuccessful}");
            Console.WriteLine(summary.Message);
            Console.WriteLine(summary.Name);

            if (summary.IsSuccessful)
            {
                Console.WriteLine(summary.NumberOfIterations);
                Console.WriteLine(summary.ElapsedMillisecondsTotal);
                Console.WriteLine(summary.ElapsedMillisecondsAverage);
            }
            else
            {
                Console.WriteLine(summary.Exception);
            }
        }
    }
}