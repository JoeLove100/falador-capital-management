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
using FaladorTradingSystems.Backtesting.Strategies;

namespace FaladorTradingSystems.UserControls
{
    /// <summary>
    /// Interaction logic for StrategySettingsControl.xaml
    /// </summary>
    public partial class StrategySettingsControl : UserControl, ISettingsControl
    {
        #region constants

        protected const string _name = "Selected strategy type";

        #endregion 


        #region constructors

        public StrategySettingsControl()
        {
            InitializeComponent();
            InitialiseControls();
            WindowName = _name;
        }

        #endregion

        #region properties

        public string WindowName { get; }

        double ISettingsControl.Height => Height;

        double ISettingsControl.Width => Width;

        #endregion

        #region methods

        public void InitialiseControls()
        {
            ComboBoxStrategy.ItemsSource = Enum.GetValues(typeof(StrategyType));
        }

        public void SetSettings(object settings)
        {
            StrategyType strategy = (StrategyType)settings;
            ComboBoxStrategy.SelectedItem = strategy;
        }

        public object GetSettings()
        {
            return ComboBoxStrategy.SelectedItem;
        }

        #endregion 



    }

}
