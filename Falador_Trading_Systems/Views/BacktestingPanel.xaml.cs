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


            StrategyBuyAndHold strategy = 
                BacktestingEngine.GetBuyAndHoldStrategy();

            DateTime startDate = new DateTime(2018, 1, 1);
            NaivePortfolio portfolio =
                BacktestingEngine.GetNaivePortfolio(1000, startDate);

            BacktestingEngine.RunBacktest(strategy, portfolio);

        }

        #endregion


    }
}
