using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Engine
{
    public class DataLoader
    {
        public DataLoader()
        {
            
        }

        public Array getArrayFromCsv(string fileName)
        {
            List<string> contents = CsvReader.ReadListFromCsv(fileName);
            return contents.ToArray();
        }
    }
}
