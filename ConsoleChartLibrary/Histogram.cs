using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleChartLibrary
{
    public class Histogram<T, K> where T : IComparable<T>
    {
        public Dictionary<T, K> Data { get; set; }
        public HistogramOptions Options { get; set; }

        public void Render()
        {
            var bins = Options.BinCount;
            int binWidth = 0;

            if (bins == null)
            {
                bins = calculateBinCount();
                binWidth = calcualteBinWidth(bins);
            }
            else
            {
                binWidth = calcualteBinWidth(bins);
            }

            if (binWidth == 1)
            {
                printChartVertically();
            }
            else
            {
                printChartHorizontally(bins, binWidth);
            }
        }

        private void printChartVertically()
        {
            var sortedData = Data.OrderBy(x => x.Key);
            var keys = sortedData.Select(x => x.Key);

            var maxVal = Data.Values.Max();
            var minVal = Data.Values.Min();

            int maxHeight = 10;
            int maxWidth = 80;

            double scale = (double)maxWidth / (dynamic)maxVal;

            Console.WriteLine($"{Options.Title}".PadLeft((Console.WindowWidth - Options.Title.Length) / 2));
            Console.WriteLine();

            for (var i = maxHeight; i > 0; i--)
            {
                foreach (var key in keys)
                {
                    int barHeight = (int)((dynamic)Data[key] * scale / maxHeight);
                    if (barHeight >= i || (i == 1 && (dynamic)Data[key] != 0))
                    { 
                        Console.Write("██".PadRight(maxWidth / Data.Count()));
                    }
                    else
                    {
                        Console.Write("  ".PadRight(maxWidth / Data.Count()));
                    }
                }

                if (i == maxHeight)
                {
                    if (Options.YLabel != null)
                    {
                        Console.WriteLine($"{maxVal} {Options.YLabel}");
                    }
                    else
                    {
                        Console.Write($"{maxVal}");
                    }
                }
                else if (i == 1)
                {
                    Console.Write($"{minVal}");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            foreach (var key in keys)
            {
                if ((dynamic)key < 10)
                {
                    Console.Write($"0{Convert.ToString(key)}");
                }
                else
                {
                    Console.Write($"{Convert.ToString(key)}");
                }

                Console.Write("  ");
            }
            Console.WriteLine();
            Console.WriteLine();

            if (Options.XLabel != null)
            {
                Console.WriteLine($"{Options.XLabel}".PadLeft((Console.WindowWidth - Options.Title.Length) / 2));
                Console.WriteLine();
            }
        }

        private void printChartHorizontally(int? bins, int binWidth)
        {
            var sortedData = Data.OrderBy(x => x.Key);
            var minVal = Data.Values.Min();
            var maxVal = Data.Values.Max();

            Console.WriteLine($"{Options.Title}".PadLeft((Console.WindowWidth - Options.Title.Length) / 2));
            Console.WriteLine();

            if (Options.YLabel != null)
            {
                Console.WriteLine(Options.YLabel);
            }

            for (int i = 0; i < bins; i++)
            {
                var start = i * binWidth;
                var end = start + binWidth;
                var binLabel = $"[{start} - {end}]";

                T startT = (T)Convert.ChangeType(start, typeof(T));
                T endT = (T)Convert.ChangeType(end, typeof(T));

                var count = sortedData.Where(x => x.Key.CompareTo(startT) >= 0 && x.Key.CompareTo(endT) < 0)
                    .Sum(x => (dynamic)x.Value);

                var histogramBar = new string('▓', (int)((double)count / (dynamic)maxVal * 45));
                var line = $"{binLabel.PadRight(15)} | {histogramBar} {count}";

                Console.WriteLine(line);
            }

            if (Options.XLabel != null)
            {
                Console.WriteLine($"{Options.XLabel}".PadLeft((Console.WindowWidth - Options.Title.Length) / 2));
            }

            Console.WriteLine();
        }

        private int calculateBinCount()
        {
            var elemCount = Data.Count();

            return (int)Math.Ceiling(Math.Sqrt(elemCount));
        }

        private int calcualteBinWidth(int? bins)
        {
            var maxVal = Data.Keys.Max();
            var minVal = Data.Keys.Min();

            double binWidth = Convert.ToDouble(maxVal) - Convert.ToDouble(minVal);

            return (int)Math.Ceiling((decimal)(binWidth / bins));
        }
    }
}
