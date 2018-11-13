using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace FaladorTradingSystems
{
    internal static class AssetPriceDataLoader
    {
        #region methods

        public static MarketData LoadData(string dataFileLocation)
        {
            List<string> dataFromCsv = CsvReader.ReadListFromCsv(dataFileLocation);
            MarketData output = ConvertToAssetPriceSeries(dataFromCsv);

            return output;
        }

        private static MarketData ConvertToAssetPriceSeries(List<string> rawDataFromCsv)
        {
            string[] seriesNames = (string[]) GetFormattedSeriesNames(rawDataFromCsv[0].Split(','));
            List<DateTime> dates = new List<DateTime>();
            var seriesCollection = new AssetDataSeriesCollection();
            
            for(int i =1; i < seriesNames.Length; i++)
            {
                AssetDataSeries series = new AssetDataSeries(seriesNames[i]);
                seriesCollection.Add(series);
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
                        Bar bar = new Bar(double.Parse(priceEntry[j]));
                        seriesCollection[seriesNames[j]].Add(date, bar);
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                    }
                }

            }

            MarketData output = new MarketData(seriesCollection);

            return output;
        }


        private static IList<string> GetFormattedSeriesNames(IList<string> names)
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
