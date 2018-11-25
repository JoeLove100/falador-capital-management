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

namespace FaladorTradingSystems.Views
{
    /// <summary>
    /// Interaction logic for BacktestingPanel.xaml
    /// </summary>
    public partial class BacktestingPanel : UserControl
    {
        #region constructors
        public BacktestingPanel(BacktestingEngine engine)
        {
            BacktestingEngine = engine;
            InitializeComponent();

            AddEventHandlers();
        }
        #endregion

        #region properties

        BacktestingEngine BacktestingEngine { get; }

        #endregion

        #region methods

        public void AddEventHandlers()
        {
            ButtonBacktest.Click += RunBacktest;
        }

        private void RunBacktest(object sender, RoutedEventArgs e)
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
                BacktestingEngine.GetNaivePortfolio(1000, startDate,
                handler);

            BacktestingEngine.RunBacktest(strategy, portfolio, handler);

        }

        #endregion


    }
}
