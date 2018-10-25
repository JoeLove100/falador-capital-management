using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Utils
{
    public class CsvReader
    {

        public static List<string> ReadListFromCsv(string csvFilePath)
        {
            ///<summary>
            ///this class reads in an array of csv to a list, via some code 
            ///I stole from Stack Exchange and don't fully understand- should
            ///probably replace with a package version at some point
            ///</summary>

            List<string> listA = new List<string>();
            List<string> listB = new List<string>();

            using (var reader = new StreamReader(csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    listA.Add(values[0]);
                }
            }

            return listA;
        }

    }
}
