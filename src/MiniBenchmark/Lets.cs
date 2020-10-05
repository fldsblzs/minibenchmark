using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniBenchmark
{
    /// <summary>
    /// The class with the Go methods for running the mini benchmarks.
    /// </summary>
    public static class Lets
    {
        /// <summary>
        /// Runs the mini benchmark. By default the name of the mini benchmark is the passed method's name, the number of iterations is 10.
        /// </summary>
        /// <param name="action">The method to test.</param>
        /// <returns>The summary of the mini benchmark.</returns>
        public static Summary Go(Action action) => Go(action, action.Method.Name, 10);

        /// <summary>
        /// Runs the mini benchmark. By default the number of iterations is 10.
        /// </summary>
        /// <param name="action">The method to test.</param>
        /// <param name="name">The name of the current mini benchmark.</param>
        /// <returns>The summary of the mini benchmark.</returns>
        public static Summary Go(Action action, string name) => Go(action, name, 10);

        /// <summary>
        /// Runs the mini benchmark.
        /// </summary>
        /// <param name="action">The method to test.</param>
        /// <param name="name">The name of the current mini benchmark.</param>
        /// <param name="numberOfIterations">The number of iterations.</param>
        /// <returns>The summary of the mini benchmark.</returns>
        public static Summary Go(Action action, string name, int numberOfIterations)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            try
            {
                action.Invoke();
            }
            catch (Exception exception)
            {
                return new Summary(name, exception);
            }

            var stopwatch = Stopwatch.StartNew();

            for (var i = 0; i < numberOfIterations; i++)
            {
                action.Invoke();
            }

            stopwatch.Stop();

            return new Summary(stopwatch, name, numberOfIterations);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="name"></param>
        /// <param name="numberOfIterations"></param>
        /// <param name="path"></param>
        public static void GoAndSaveSummary(
            Action action,
            string name,
            int numberOfIterations,
            string path)
        {
            var summary = Go(action, name, numberOfIterations);

            SaveAsJson(path, summary);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Summary Go(Func<Task> func) => Go(func, func.Method.Name, 10);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Summary Go(Func<Task> func, string name) => Go(func, name, 10);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="name"></param>
        /// <param name="numberOfIterations"></param>
        /// <returns></returns>
        public static Summary Go(Func<Task> func, string name, int numberOfIterations)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            try
            {
                func.Invoke().GetAwaiter().GetResult();
            }
            catch (Exception exception)
            {
                return new Summary(name, exception);
            }

            var stopwatch = Stopwatch.StartNew();

            for (var i = 0; i < numberOfIterations; i++)
            {
                func.Invoke().GetAwaiter().GetResult();
            }

            stopwatch.Stop();

            return new Summary(stopwatch, name, numberOfIterations);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="name"></param>
        /// <param name="numberOfIterations"></param>
        /// <param name="path"></param>
        public static void GoAndSaveSummary(
            Func<Task> func,
            string name,
            int numberOfIterations,
            string path)
        {
            var summary = Go(func, name, numberOfIterations);

            SaveAsJson(path, summary);
        }

        private static void SaveAsJson(string path, Summary summary)
        {
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                throw new Exception($"Invalid path: {path}.");
            }

            var jsonString = JsonSerializer.Serialize(summary);
            File.WriteAllText(path, jsonString);
        }
    }
}