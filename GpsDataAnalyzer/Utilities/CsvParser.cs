using GpsDataAnalyzer.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GpsDataAnalyzer.Utilities
{
    public class CsvParser<T> : ICsvParser<T>
    {
        public T ParseLine(string line)
        {
            var elements = line.Split(',');
            var instance = Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties();

            for (int i = 0; i < elements.Length; i++)
            {
                if (i < properties.Length)
                {
                    try
                    {
                        var convertedValue = Convert.ChangeType(elements[i].Trim(), properties[i].PropertyType, CultureInfo.InvariantCulture);

                        properties[i].SetValue(instance, convertedValue);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Error converting element '{elements[i]}' to type '{properties[i].PropertyType.FullName}': {ex.Message}");
                    }
                }
            }

            return instance;
        }
    }
}
