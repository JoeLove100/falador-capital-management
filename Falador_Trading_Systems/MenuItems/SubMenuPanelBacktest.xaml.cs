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
using FaladorTradingSystems.PanelSettings;
using FaladorTradingSystems.Backtesting.Strategies;
using FaladorTradingSystems.UserControls;
using MahApps.Metro.Controls;
using MahApps.Metro.SimpleChildWindow;

namespace FaladorTradingSystems.MenuItems
{
    /// <summary>
    /// Interaction logic for SubMenuPanelBacktest.xaml
    /// </summary>
    public partial class SubMenuPanelBacktest : ObservableControl
    {
        #region constructors

        public SubMenuPanelBacktest()
        {
            SetDefaultSettings();
            InitializeComponent();
            AddEventHandlers();

        }

        #endregion

        #region properties

        protected BacktestSettings Settings;

        #endregion

        #region methods

        public BacktestSettings GetSettings()
        {
            return Settings.Clone();
        }

        protected void SetDefaultSettings()
        {
            DateTime defaultStart = new DateTime(2018, 1, 1);
            DateTime defaultEnd = DateTime.Now;

            DateRange defaultPeriod = new DateRange(defaultStart, defaultEnd);

            List<string> defaultAssetUniverse = new List<string>() { "Adamantite ore",
            "Bronze bar", "Iron ore", "Rune bar"};

            Settings = new BacktestSettings(defaultPeriod, StrategyType.BuyAndHold,
                defaultAssetUniverse);
        }

        #endregion

        #region event handling

        protected void AddEventHandlers()
        {
            ButtonAssets.Click += OnAssetsClicked;
            ButtonStrategy.Click += OnStrategyClicked;
            ButtonDates.Click += OnDatesClicked;
        }

        private async void OnDatesClicked(object sender, RoutedEventArgs e)
        {
            DateRange selectedDates = Settings.Period.Clone();
            DateRangeSettingsControl dateControl =
                new DateRangeSettingsControl();
            dateControl.SetSettings(selectedDates);

            object newSettings =
                await ((MetroWindow)Application.Current.MainWindow).ShowChildWindowAsync<DateRange>(
                    new SettingsChildWindow(dateControl));

            if(newSettings != null)
            {
                Settings.Period = (DateRange) newSettings;
            }
        }

        protected async void OnStrategyClicked(object sender, RoutedEventArgs e)
        {
            StrategyType selectedStrategy = Settings.Strategy;
            StrategySettingsControl dateControl =
                new StrategySettingsControl();
            dateControl.SetSettings(selectedStrategy);

            object newSettings =
                await((MetroWindow)Application.Current.MainWindow).ShowChildWindowAsync<StrategyType>(
                    new SettingsChildWindow(dateControl));

            if (newSettings != null)
            {
                Settings.Strategy = (StrategyType)newSettings;
            }
        }

        protected async void OnAssetsClicked(object sender, RoutedEventArgs e)
        {
            List<string> selectedUniverse = Settings.AssetUniverse;
            AssetUniverseSettingsControl assetUniverseControl =
                new AssetUniverseSettingsControl();
            assetUniverseControl.SetSettings(selectedUniverse);

            object newSettings =
                await((MetroWindow)Application.Current.MainWindow).ShowChildWindowAsync<List<string>>(
                    new SettingsChildWindow(assetUniverseControl));

            if (newSettings != null)
            {
                Settings.AssetUniverse = (List<string>)newSettings;
            }
        }

        #endregion
    }
}
