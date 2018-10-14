using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LiveCharts.Wpf;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Configurations;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using Utils;

namespace FaladorTradingSystems
{
    /// <summary>
    /// Interaction logic for LineChartPanel.xaml
    /// </summary>
    public partial class LineChartPanel : UserControl
    {
        #region constructor

        public LineChartPanel()
        {
            InitializeComponent();
        }

        #endregion

        #region properties

        public Func<double, string> Formatter { get; set; }
        public SeriesCollection Series { get; set; }
        public string[] Labels { get; set; }

        private  AssetPriceSeriesCollection _seriesCollection { get; set; }
        private string _selectedSeries => (string) ComboBoxTicker.SelectedItem;

        #endregion 

        #region public methods

        public void InitialiseChart(AssetPriceSeriesCollection collection)
        {
            _seriesCollection = collection;
            Series = new SeriesCollection();

            foreach(AssetPriceSeries series in _seriesCollection)
            {
                ComboBoxTicker.Items.Add(series.Name);
            }

            ComboBoxTicker.SelectedIndex = 1;
            InitialiseDateRangeControl();
            //DataContext = this;
            AddEventHandlers();
        }


        public void PlotLineChart()
        {
            AssetPriceSeries selectedSeries = _seriesCollection[_selectedSeries];
            DateRange selectedRange = DateRangeControl.GetSettings();
            AssetPriceSeries priceSeries = selectedSeries.GetSubset(selectedRange);

            Labels = GetDateArray(priceSeries.Keys.ToList());
            Series.Clear();
            Series.Add( GetPriceDataSeries(priceSeries.Values.ToList()));
            Formatter = value => String.Format("{0:0,0}", value);
        }


        #endregion

        #region private methods

        private void InitialiseDateRangeControl()
        {
            AssetPriceSeries series = _seriesCollection[_selectedSeries];
            DateRange range = new DateRange(series.FirstDate, series.LastDate);
            DateRangeControl.InitialiseControls(range);
        }

        private string[] GetDateArray(List<DateTime> dates)
        {
            List<string> output = new List<string>();

            for(int i = 0; i < dates.Count; i++)
            {
                output.Add(dates[i].ToShortDateString());
            }

            return output.ToArray<string>();
        }

        private Series GetPriceDataSeries(List<double> priceValues)
        {
            Series output = new LineSeries
            {
                PointGeometry = null
            };
            
            var seriesValues = new ChartValues<double>();
            seriesValues.AddRange(priceValues);
            output.Values = seriesValues;
            output.Title = _selectedSeries;

            return output;
        }

        private void AddEventHandlers()
        {
            ComboBoxTicker.SelectionChanged += ReplotChart;
            DateRangeControl.SettingsChangedEvent += ReplotChart;
                
        }

        #endregion

        #region Events

        private void ReplotChart(object sender, EventArgs e)
        { 
            AssetPriceChart.Series.Clear();
            PlotLineChart();
            AssetPriceChart.Update();
        }

        #endregion 

    }
}

