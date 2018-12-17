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

namespace FaladorTradingSystems.MenuItems
{
    /// <summary>
    /// Interaction logic for SubMenuPanel.xaml
    /// </summary>
    public partial class SubMenuPanel : ObservableControl
    {
        #region constructors

        public SubMenuPanel()
        {
            InitialiseSubMenus();
            InitializeComponent();
        }

        #endregion

        #region properties

        protected SubMenuPanelBacktest BacktestSubMenu { get; set; }
        protected SubMenuPanelHistoric HistoricSubMenu { get; set; }
        protected SubMenuPanelNewsfeed NewsfeedSubMenu { get; set; }
        protected SubMenuPanelPortfolio PortfolioSubMenu { get; set; }

        #endregion

        #region method

        protected void InitialiseSubMenus()
        {
            HistoricSubMenu = new SubMenuPanelHistoric();
            BacktestSubMenu = new SubMenuPanelBacktest();
            NewsfeedSubMenu = new SubMenuPanelNewsfeed();
            PortfolioSubMenu = new SubMenuPanelPortfolio();
        }

        public void SetSubMenuItem(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.AssetPrice:
                    DataContext = HistoricSubMenu;
                    break;
                case ViewType.Backtest:
                    DataContext = BacktestSubMenu;
                    break;
                case ViewType.News:
                    DataContext = NewsfeedSubMenu;
                    break;
                case ViewType.Portfolio:
                    DataContext = PortfolioSubMenu;
                    break;
                default:
                    throw new Exception("Unknown view type");
            }




        }



        #endregion 
    }
}
