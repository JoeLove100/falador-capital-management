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
using Engine;
using Utils;
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
            InitialiseLineChart();
        }

        #endregion

        #region properties

        protected ModelEngine Engine;

        #endregion

        #region methods

        protected void InitialiseLineChart()
        {
            ChartPanelAssetPrice.InitialiseChart(Engine.AssetPriceData);
            ChartPanelAssetPrice.PlotLineChart();
        }


        #endregion

        #region events

        public void AddEventHandlers()
        {
            
        }

        #endregion 
    }
}
