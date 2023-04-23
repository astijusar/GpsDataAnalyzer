using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderLibrary.Interfaces
{
    public interface IFileReaderManager<T>
    {
        IFileReader<T> JsonFile { get; }
        IFileReader<T> CsvFile { get; }
    }
}
