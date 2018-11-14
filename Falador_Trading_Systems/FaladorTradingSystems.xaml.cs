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
using FaladorTradingSystems;
using Utils;
using MahApps.Metro.Controls;
using FaladorTradingSystems.Views;
using FaladorTradingSystems.MenuItems;

namespace FaladorTradingSystems
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region constructor

        public MainWindow()
        {
            InitializeComponent();
            Engine = new ModelEngine();
            InitialiseViews();
            AddEventHandlers();
        }

        #endregion

        #region properties

        protected ModelEngine Engine;

        protected LineChartPanel ChartPanelAssetPrice;
        protected BacktestingPanel ChartPanelBacktesting;
        protected NewsfeedPanel ChartPanelNewsfeed;
        protected PortfolioManagementPanel ChartPanelPortfolio;

        #endregion

        #region methods

        protected void InitialiseViews()
        {
            ChartPanelAssetPrice = new LineChartPanel();
            ChartPanelAssetPrice.InitialiseChart(Engine.MarketData);
            ChartPanelAssetPrice.PlotLineChart();

            ChartPanelBacktesting = new BacktestingPanel();
            ChartPanelNewsfeed = new NewsfeedPanel();
            ChartPanelPortfolio = new PortfolioManagementPanel();
        }


        #endregion

        #region events

        public void AddEventHandlers()
        {
            MenuItemMain.AssumptionChanged += ChangeView;
        }

        private void ChangeView(object sender, AssumptionChangedEventArgs e)
        {
            ViewType viewType = MenuItemMain.GetSelectedView();
            switch (viewType)
            {
                case ViewType.AssetPrice:
                    DataContext = ChartPanelAssetPrice;
                    break;
                case ViewType.Backtest:
                    DataContext = ChartPanelBacktesting;
                    break;
                case ViewType.News:
                    DataContext = ChartPanelNewsfeed;
                    break;
                case ViewType.Portfolio:
                    DataContext = ChartPanelPortfolio;
                    break;
            }
        }

        #endregion
    }
}
