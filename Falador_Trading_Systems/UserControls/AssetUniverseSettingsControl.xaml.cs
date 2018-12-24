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
using Utils;

namespace FaladorTradingSystems.UserControls
{
    /// <summary>
    /// Interaction logic for AssetUniverseSettingsControl.xaml
    /// </summary>
    public partial class AssetUniverseSettingsControl : ObservableControl, ISettingsControl
    {
        #region constants

        protected const string _name = "Selected assets";

        #endregion

        #region constructor

        public AssetUniverseSettingsControl()
        {
            InitializeComponent();
            InitialiseControls();
            AddEventHandlers();
        }

        #endregion

        #region properties

        public string WindowName { get; }

        double ISettingsControl.Height => Height;

        double ISettingsControl.Width => Width;

        #endregion

        #region methods 

        protected void InitialiseControls()
        {
            List<string> allAssets = AllAssets.GetAll();

            foreach(string asset in allAssets)
            {
                ListBoxNotSelected.Items.Add(asset);
            }
        }

        public object GetSettings()
        {
            return ListBoxSelected.Items;
        }

        public void SetSettings(object settings)
        {
            IsBeingUpdated = true;

            var selectedAssets = (List<string>)settings;

            foreach(string asset in selectedAssets)
            {
                ListBoxSelected.Items.Add(asset);
                ListBoxNotSelected.Items.Remove(asset);
            }

            IsBeingUpdated = false;
        }

        #endregion

        #region event handlers

        protected void AddEventHandlers()
        {
            ButtonSelect.Click += OnSelectClick;
            ButtonDeselect.Click += OnDeselectClick;
        }

        private void OnSelectClick(object sender, RoutedEventArgs e)
        {
            if (IsBeingUpdated) return;

            string assetToSelect = (string) ListBoxNotSelected.SelectedItem;

            if (assetToSelect == null) return;

            ListBoxSelected.Items.Add(assetToSelect);
            ListBoxNotSelected.Items.Remove(assetToSelect);
        }

        private void OnDeselectClick(object sender, RoutedEventArgs e)
        {
            if (IsBeingUpdated) return;

            string assetToDeselct = (string)ListBoxSelected.SelectedItem;

            if (assetToDeselct == null) return;

            ListBoxSelected.Items.Remove(assetToDeselct);
            ListBoxNotSelected.Items.Add(assetToDeselct);
        }

        #endregion
    }
}
