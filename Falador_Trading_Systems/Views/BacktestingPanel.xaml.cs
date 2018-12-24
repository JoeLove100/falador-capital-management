using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FaladorTradingSystems.Backtesting;
using FaladorTradingSystems.Backtesting.Strategies;
using FaladorTradingSystems.Backtesting.Portfolio;
using FaladorTradingSystems.Backtesting.DataHandling;
using Utils;
using LiveCharts.Wpf;
using LiveCharts;

namespace FaladorTradingSystems.Views
{
    /// <summary>
    /// Interaction logic for BacktestingPanel.xaml
    /// </summary>
    public partial class BacktestingPanel : ObservableControl
    {
        #region constructors
        public BacktestingPanel(BacktestingEngine engine)
        {
            BacktestingEngine = engine;
            InitializeComponent();
            DataContext = this;
            Series = new SeriesCollection();
            AddEventHandlers();
        }

        public BacktestingPanel()
        {
            InitializeComponent();
        }
        #endregion

        #region engine properties

        protected BacktestingEngine BacktestingEngine { get; }
        protected string BacktestString { get; set; }

        #endregion

        #region chart properties

        protected IPortfolio _currentBacktestResult;
        protected string _lastSeriesName;
        protected string[] _labels;
        protected Func<double, string> _formatter;

        public SeriesCollection Series { get; set; }
        public Func<double, string> Formatter 
        {
            get
            {
                return _formatter;
            }

            set
            {
                _formatter = value;
                OnPropertyChanged("Formatter");
            }
        }
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


        #endregion 

        #region  backtest methods

        public void AddEventHandlers()
        {
            ButtonBacktest.Click += HandleBacktest;
        }

        protected void HandleBacktest(object sender, EventArgs e)
        {
            RunBacktest();
            ChartBacktestResult(_currentBacktestResult);
        }

        private void RunBacktest()
        {
            ///<summary>
            ///temp method while I figure out
            ///how the backtesting is actually going
            ///to work
            ///</summary>

            //TODO: these shoudld be arguments you can change in UI
            DateTime startDate = new DateTime(2018, 1, 1);
            DateTime endDate = new DateTime(2018, 6, 30);
            DateRange range = new DateRange(startDate, endDate);

            List<string> allowableAssets = new List<string>() { "Adamantite ore",
            "Bronze bar", "Iron ore", "Rune bar"};

            HistoricSeriesDataHandler handler =
                BacktestingEngine.GetHistoricDataHandler(range, allowableAssets);

            StrategyBuyAndHold strategy = 
                BacktestingEngine.GetBuyAndHoldStrategy(handler);

            NaivePortfolio portfolio =
                BacktestingEngine.GetNaivePortfolio(1e10m, startDate,
                handler);

            IPortfolio result = BacktestingEngine.RunBacktest(strategy, portfolio, handler);

            _currentBacktestResult = result;
            _lastSeriesName = strategy.Name;

        }

        #endregion

        #region chart methods

        protected void ChartBacktestResult(IPortfolio resultingPortfolio)
        {
            ///<summary>
            ///produces a chart of the returns on the 
            ///resulting portfolio
            ///</summary>
            ///


            if (_currentBacktestResult == null)
            {
                return;
            }

            var returnSeries = _currentBacktestResult.GetReturnSeries();

            Labels = DateRange.GetDatesAsStrings(returnSeries.Keys.ToList());
            Series.Clear();
            Series.Add(LineChartPanel.GetPriceDataSeries(returnSeries.Values,
                _lastSeriesName));

            Formatter = value => String.Format("{0:P2}", value);
            OnPropertyChanged("Formatter");
        }


        #endregion 


    }
}
