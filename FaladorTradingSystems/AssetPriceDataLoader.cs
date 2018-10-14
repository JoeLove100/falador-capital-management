using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Engine
{
    internal static class AssetPriceDataLoader
    {
        #region methods

        public static AssetPriceSeriesCollection LoadData(string dataFileLocation)
        {
            List<string> dataFromCsv = CsvReader.ReadListFromCsv(dataFileLocation);
            AssetPriceSeriesCollection output = ConvertToAssetPriceSeries(dataFromCsv);

            return output;
        }

        private static AssetPriceSeriesCollection ConvertToAssetPriceSeries(List<string> rawDataFromCsv)
        {
            string[] seriesNames = GetFormattedSeriesNames(rawDataFromCsv[0].Split(','));
            List<DateTime> dates = new List<DateTime>();
            var output = new AssetPriceSeriesCollection();
            
            for(int i =1; i < seriesNames.Length; i++)
            {
                AssetPriceSeries series = new AssetPriceSeries(seriesNames[i]);
                output.Add(series);
            }    

            for(int i = 1; i < rawDataFromCsv.Count; i++)
            {
                string[] priceEntry = rawDataFromCsv[i].Split(',');

                DateTime date = DateTime.Parse(priceEntry[0]);
                dates.Add(date);

                for(int j = 1; j < priceEntry.Length; j++)
                {
                    try
                    {
                        if (priceEntry[j] == "") continue;
                        output[seriesNames[j]].Add(date, double.Parse(priceEntry[j]));
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                    }
                }

            }

            return output;
        }


        private static string[] GetFormattedSeriesNames(string[] names)
        {
            var output = new List<string>();

            foreach(string name in names)
                {
                    string formattedName = name.Replace('_', ' ');
                    formattedName = formattedName.Replace("'", "");
                    output.Add(formattedName);
                }

            return output.ToArray();
        }



        #endregion 
    }
}
