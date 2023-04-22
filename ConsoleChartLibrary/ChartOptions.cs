using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChartLibrary
{
    public abstract class ChartOptions
    {
        public string Title { get; set; } = null;
        public string XLabel { get; set; } = null;
        public string YLabel { get; set; } = null;
    }
}
