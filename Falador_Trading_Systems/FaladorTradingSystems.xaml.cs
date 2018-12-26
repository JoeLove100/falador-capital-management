using FaladorTradingSystems.MenuItems;
using FaladorTradingSystems.Views;
using MahApps.Metro.Controls;

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
            InitialiseControls();
            DataContext = this;
            AddEventHandlers();
        }

        #endregion

        #region properties

        protected ModelEngine Engine;

        // panel items
        protected LineChartPanel ChartPanelAssetPrice;
        protected BacktestingPanel ChartPanelBacktesting;
        protected NewsfeedPanel ChartPanelNewsfeed;
        protected PortfolioManagementPanel ChartPanelPortfolio;

        // sub menu items
        protected SubMenuPanelBacktest SubMenuBacktest;
        protected SubMenuPanelHistoric SubMenuHistoric;
        protected SubMenuPanelNewsfeed SubMenuNewsfeed;
        protected SubMenuPanelPortfolio SubMenuPortfolio;

        protected SubMenuPanelBacktest SubMenuItem;
        protected ObservableControl MainPanel;

        #endregion

        #region methods

        protected void InitialiseControls()
        {
            // initialise main panels

            ChartPanelAssetPrice = new LineChartPanel();
            ChartPanelAssetPrice.InitialiseChart(Engine.MarketData);
            ChartPanelAssetPrice.PlotLineChart();

            ChartPanelBacktesting = new BacktestingPanel(Engine.BacktestingEngine);
            ChartPanelNewsfeed = new NewsfeedPanel();
            ChartPanelPortfolio = new PortfolioManagementPanel();

            // initialise the sub menu panels

            SubMenuHistoric = new SubMenuPanelHistoric();
            SubMenuBacktest = new SubMenuPanelBacktest();
            SubMenuNewsfeed = new SubMenuPanelNewsfeed();
            SubMenuPortfolio = new SubMenuPanelPortfolio();
        
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
                    ContentControlMain.Content = ChartPanelAssetPrice;
                    ContentControlSubMenu.Content = SubMenuHistoric;
                    break;
                case ViewType.Backtest:
                    ContentControlMain.Content = ChartPanelBacktesting;
                    ContentControlSubMenu.Content = SubMenuBacktest;
                    break;
                case ViewType.News:
                    ContentControlMain.Content = ChartPanelNewsfeed;
                    ContentControlSubMenu.Content = SubMenuNewsfeed;
                    break;
                case ViewType.Portfolio:
                    ContentControlMain.Content = ChartPanelPortfolio;
                    ContentControlSubMenu.Content = SubMenuPortfolio;
                    break;
            }
        }

        #endregion
    }
}
