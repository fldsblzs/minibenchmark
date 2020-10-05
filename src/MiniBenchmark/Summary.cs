using System;
using System.Diagnostics;

namespace MiniBenchmark
{
    /// <summary>
    /// Contains the end results for a mini benchmark.
    /// </summary>
    public class Summary
    {
        /// <summary>
        /// Indicates if the mini benchmark was successful.
        /// </summary>
        public bool IsSuccessful { get; }

        /// <summary>
        /// Contains the result of the mini benchmark.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Contains the exception if the mini benchmark was unsuccessful.
        /// </summary>
        public Exception Exception { get; }
        
        /// <summary>
        /// The name of the current mini benchmark.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The number of iterations.
        /// </summary>
        public int NumberOfIterations { get; }

        /// <summary>
        /// Elapsed milliseconds in total from the start of the first iteration to the end of the last one.
        /// </summary>
        public long ElapsedMillisecondsTotal { get; }
        
        /// <summary>
        /// Elapsed milliseconds for a single iteration.
        /// </summary>
        public float ElapsedMillisecondsAverage { get; }

        /// <summary>
        /// Constructor for a successful mini benchmark summary.
        /// </summary>
        /// <param name="stopwatch">The stopwatch instance used by the mini benchmark.</param>
        /// <param name="name">The name of the current mini benchmark.</param>
        /// <param name="numberOfIterations">The number of iterations.</param>
        protected internal Summary(Stopwatch stopwatch, string name, int numberOfIterations)
        {
            IsSuccessful = true;
            Name = name;
            Message = $"Benchmark completed successfully for {name}!";
            NumberOfIterations = numberOfIterations;
            ElapsedMillisecondsTotal = stopwatch.ElapsedMilliseconds;
            ElapsedMillisecondsAverage = (float)stopwatch.ElapsedMilliseconds / numberOfIterations;
        }
        
        /// <summary>
        /// Constructor for a unsuccessful mini benchmark summary.
        /// </summary>
        /// <param name="name">The name of the current mini benchmark.</param>
        /// <param name="exception">The exception caught during the mini benchmark.</param>
        protected internal Summary(string name, Exception exception)
        {
            Name = name;
            Message = "An error occured during the mini benchmark process!";
            Exception = exception;
        }
    }
}