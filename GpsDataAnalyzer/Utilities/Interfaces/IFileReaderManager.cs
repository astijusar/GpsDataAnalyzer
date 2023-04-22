using GpsDataAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsDataAnalyzer.Utilities.Interfaces
{
    public interface IFileReaderManager<T>
    {
        IFileReader<T> JsonFile { get; }
        IFileReader<T> CsvFile { get; }
        IFileReader<GpsData> BinaryFile { get; }
    }
}
