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
using FaladorTradingSystems.UserControls;

namespace FaladorTradingSystems.Views
{
    /// <summary>
    /// Interaction logic for LineChartPanel.xaml
    /// </summary>
    public partial class LineChartPanel : ObservableControl
    {
        #region constructor

        public LineChartPanel()
        {
            InitializeComponent();
        }

        #endregion

        #region properties

        private string[] _labels;

        public Func<double, string> Formatter { get; set; }
        public SeriesCollection Series { get; set; }
        public string[] Labels
        {
            get
            {
                return _labels;
            }
            set
            {
                _labels = value;
                OnPropertyChanged("Labels");
            }
        }

        private  MarketData _marketData { get; set; }
        private string _selectedSeries => (string) ComboBoxTicker.SelectedItem;

        #endregion 

        #region public methods

        public void InitialiseChart(MarketData marketData)
        {
            _marketData = marketData;
            Series = new SeriesCollection();

            foreach(AssetDataSeries series in _marketData.Data)
            {
                ComboBoxTicker.Items.Add(series.Name);
            }

            ComboBoxTicker.SelectedIndex = 1;
            DataContext = this;
            InitialiseDateRangeControl();
            AddEventHandlers();
        }


        public void PlotLineChart()
        {
            AssetDataSeries selectedSeries = _marketData.Data[_selectedSeries];
            DateRange selectedRange = DateRangeControl.GetSettings();
            AssetDataSeries priceSeries = selectedSeries.GetSubset(selectedRange);

            Labels = DateRange.GetDatesAsStrings(priceSeries.Keys.ToList());
            OnPropertyChanged("Labels");
            Series.Clear();
            Series.Add( GetPriceDataSeries(priceSeries.GetPricesInOrder(), _selectedSeries));

            Formatter = value => String.Format("{0:0,0}", value);
        }

        public static Series GetPriceDataSeries(IList<decimal> priceValues,
                                                string title)
        {
            Series output = new LineSeries
            {
                PointGeometry = null
            };

            var seriesValues = new ChartValues<decimal>();
            seriesValues.AddRange(priceValues);
            output.Values = seriesValues;
            output.Title = title;

            return output;
        }

        #endregion

        #region private methods

        private void InitialiseDateRangeControl()
        {
            AssetDataSeries series = _marketData.Data[_selectedSeries];
            DateRange range = new DateRange(series.FirstDate, series.LastDate);
            DateRangeControl.InitialiseControls(range);
        }


        private void AddEventHandlers()
        {
            ComboBoxTicker.SelectionChanged += ReplotChart;
            DateRangeControl.AssumptionChanged += ReplotChart;
                
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

