using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiniBenchmark.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            // // Sample #1
            // var successfulSummary1 = Lets.Go(() =>
            // {
            //     var tempFilePath = Path.GetTempFileName();
            //     if (File.Exists(tempFilePath))
            //     {
            //         File.Delete(tempFilePath);
            //     }
            // });
            // Helper.LogSummaryToConsole(successfulSummary1);
            //
            // // Sample #2
            // var successfulSummary2 = Lets.Go(() => Sample1());
            // Helper.LogSummaryToConsole(successfulSummary2);
            //
            // // Sample #3
            // var successfulSummary3 = Lets.Go(async () => 
            //     await Sample2("https://via.placeholder.com/500x500.png"));
            // Helper.LogSummaryToConsole(successfulSummary3);

            // // Sample #4
            // var unsuccessfulSummary1 = Lets.Go(
            //     () =>Sample3(1500, HttpStatusCode.Forbidden),
            //     "Exception sample",
            //     5);
            // Helper.LogSummaryToConsole(unsuccessfulSummary1);
            
            // Sample #5
            var unsuccessfulSummary2 = Lets.Go(
                () => Sample4("https://via.placeholder.com/500x500.png", true));
            Helper.LogSummaryToConsole(unsuccessfulSummary2);
        }

        /// <summary>
        /// A sample method to benchmark.
        /// </summary>
        private static void Sample1()
        {
            var random = new Random();
            var bytes = new byte[1000];
            random.NextBytes(bytes);
        }

        /// <summary>
        /// A sample method to benchmark.
        /// </summary>
        private static async Task<bool> Sample2(string url)
        {
            using var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync(url);
            var content = await responseMessage.Content.ReadAsByteArrayAsync();

            return content.Length > 100;
        }

        /// <summary>
        /// A sample method to benchmark.
        /// </summary>
        private static string Sample3(int delay, HttpStatusCode statusCode)
        {
            int sum = default;
            for (int i = 0; i < 1000; i++)
            {
                sum += i;
            }
          
            if (statusCode == HttpStatusCode.Unauthorized || 
                statusCode == HttpStatusCode.BadRequest ||
                statusCode == HttpStatusCode.Forbidden)
            {
                throw new Exception("Error!");
            }

            return "OK";
        }

        /// <summary>
        /// A sample method to benchmark.
        /// </summary>
        private static async Task<string> Sample4(string url, bool isTest)
        {
            using var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync(url);
            _ = await responseMessage.Content.ReadAsByteArrayAsync();

            if (isTest)
            {
                throw new Exception("Error!");
            }

            return string.Empty;
        }
    }
}