using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderLibrary.Interfaces
{
    public interface ICsvParser<T>
    {
        T ParseLine(string line);
    }
}
