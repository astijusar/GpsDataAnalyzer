using GpsDataAnalyzer.Models;
using GpsDataAnalyzer.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsDataAnalyzer.Utilities
{
    public class FileReaderManager<T> : IFileReaderManager<T>
    {
        private IFileReader<T> _jsonFileReader;
        private IFileReader<T> _csvFileReader;
        private IFileReader<GpsData> _binaryFileReader;

        public IFileReader<T> JsonFile
        {
            get
            {
                if (_jsonFileReader == null)
                    _jsonFileReader = new JsonFileReader<T>();

                return _jsonFileReader;
            }
        }

        public IFileReader<T> CsvFile
        {
            get
            {
                if (_csvFileReader == null)
                {
                    var csvParser = new CsvParser<T>();
                    _csvFileReader = new CsvFileReader<T>(csvParser);
                }

                return _csvFileReader;
            }
        }

        public IFileReader<GpsData> BinaryFile
        {
            get
            {
                if (_binaryFileReader == null)
                {
                    var binaryParser = new BinaryParser();
                    _binaryFileReader = new BinaryFileReader(binaryParser);
                }

                return _binaryFileReader;
            }
        }
    }
}
