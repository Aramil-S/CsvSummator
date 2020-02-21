using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace CsvSummator
{
    public static class Memory
    {
        public static ConcurrentQueue<KeyValuePair<DateTime, int>> YearlyQueue { get; set; } = new ConcurrentQueue<KeyValuePair<DateTime, int>>();
        public static Dictionary<int, int> ValuesPerYear { get; set; } = new Dictionary<int, int>();

        public static ConcurrentQueue<KeyValuePair<string, int>> CategoryQueue { get; set; } = new ConcurrentQueue<KeyValuePair<string, int>>();
        public static Dictionary<string, int> ValuesPerCategory { get; set; } = new Dictionary<string, int>();

        public static ConcurrentQueue<long> SumQueue { get; set; } = new ConcurrentQueue<long>();
        public static long ValuesSummary { get; set; } = 0;

        public static bool ClearData()
        {
            try
            {
                YearlyQueue  = new ConcurrentQueue<KeyValuePair<DateTime, int>>();
                ValuesPerYear.Clear();
                CategoryQueue = new ConcurrentQueue<KeyValuePair<string, int>>();
                ValuesPerCategory.Clear();
                SumQueue = new ConcurrentQueue<long>();
                ValuesSummary = 0;
                return true;
            }
            catch { return false; }
        }
    }
}
