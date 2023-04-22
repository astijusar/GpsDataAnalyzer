using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChartLibrary
{
    public class HistogramOptions : ChartOptions
    {
        public int? BinCount { get; set; } = null;
    }
}
