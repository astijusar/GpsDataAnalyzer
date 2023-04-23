using FileReaderLibrary.Interfaces;

namespace FileReaderLibrary
{
    public class FileReaderManager<T> : IFileReaderManager<T>
    {
        private IFileReader<T> _jsonFileReader;
        private IFileReader<T> _csvFileReader;

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
    }
}
