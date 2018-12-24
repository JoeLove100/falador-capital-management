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
using System.Windows.Shapes;
using MahApps.Metro.SimpleChildWindow;

namespace FaladorTradingSystems.UserControls
{
    /// <summary>
    /// Interaction logic for SettingsChildWindow.xaml
    /// </summary>
    public partial class SettingsChildWindow : ChildWindow
    {
        #region constructor

        public SettingsChildWindow(ISettingsControl settingsControl)
        {
            InitializeComponent();
            InitialiseControls(settingsControl);
            Title = settingsControl.WindowName;
            ControlWidth = settingsControl.Width;
            ControlHeight = settingsControl.Height;
        }

        #endregion

        #region properties

        double ControlHeight { get; }
        double ControlWidth { get; }

        #endregion 

        #region methods

        protected void InitialiseControls(ISettingsControl settingsControl)
        {
            MainHolder.Content = settingsControl;
            ButtonOK.Click += OnClickOk;
            ButtonCancel.Click += OnClickCancel;
        }

        #endregion

        #region event handling

        protected void OnClickOk(object sender, RoutedEventArgs e)
        {
            object settingsToReturn = ((ISettingsControl)MainHolder.Content).GetSettings();
            Close(settingsToReturn);
        }

        protected void OnClickCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
    }
}
