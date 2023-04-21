using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsDataAnalyzer.Utilities.Interfaces
{
    public interface IBinaryParser<T>
    {
        T ParseBytes(byte[] bytes);
    }
}
