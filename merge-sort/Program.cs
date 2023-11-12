using System.Diagnostics;

namespace merge_sort
{
    public static class Program
    {
        private const int MeasurementIterations = 10;
        private const int ArraySize = 10000000;

        public static void Main()
        {
            var totalSerialTime = 0L;
            var totoalParallelTime = 0L;
            var watch = System.Diagnostics.Stopwatch.StartNew();

            for (var i = 0; i < MeasurementIterations; i++)
            {
                var arraySerial = GenerateRandomArray(ArraySize);
                var arrayParallel = GenerateRandomArray(ArraySize);
                
                watch.Restart();
                MergeSort.SortSerial(arraySerial);
                watch.Stop();
                totalSerialTime += watch.ElapsedMilliseconds;

                watch.Restart();
                MergeSort.SortParallel(arrayParallel, 0);
                watch.Stop();
                totoalParallelTime += watch.ElapsedMilliseconds;

                if (!IsSorted(arraySerial) || !IsSorted(arrayParallel))
                {
                    Console.WriteLine("Error during sorting arrays");
                    Environment.Exit(-1);
                }
            }
            
            watch.Stop();
            
            Console.WriteLine($"Average serial execution time: {totalSerialTime / MeasurementIterations} ms");
            Console.WriteLine($"Average parallel execution time: {totoalParallelTime / MeasurementIterations} ms");
        }

        private static int[] GenerateRandomArray(int length)
        {
            var result = new int[length];
            var rand = new Random();

            for (int i = 0; i < length; i++)
            {
                result[i] = rand.Next(10000);
            }
            
            return result;
        }

        private static bool IsSorted(int[] array)
        {
            for (var i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1])
                {
                    return false;
                }
            }

            return true;
        }
    }    
}

