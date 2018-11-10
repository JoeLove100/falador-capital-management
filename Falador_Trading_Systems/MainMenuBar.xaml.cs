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

namespace FaladorTradingSystems
{
    /// <summary>
    /// Interaction logic for MainMenuBar.xaml
    /// </summary>
    public partial class MainMenuBar : ObservableControl
    {
        #region constructor
        public MainMenuBar()
        {
            InitializeComponent();
            InitialiseControls();
            AddEventHandlers();
        }
        #endregion

        #region properties

        private string SelectedButton { get; set; }

        #endregion

        #region methods

        public void InitialiseControls()
        {
            SelectedButton = ButtonAssetPrice.Name;
            SetButtonColours();
        }

        private void SetButtonColours()
        {
            foreach(Control button in StackPanelMain.Children)
            {
                if(button.Name == SelectedButton)
                {
                    button.Background = new SolidColorBrush(Colors.LightSalmon);
                }
                else
                {
                    button.Background = new SolidColorBrush(Colors.White);
                }
            }
        }

        #endregion 

        #region event handlers

        private void AddEventHandlers()
        {
            foreach(Control control in StackPanelMain.Children)
            {
                Button button = (Button)control;
                button.Click += OnButtonClicked;
            }
        }

        private void OnButtonClicked(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button) sender;
            SelectedButton = clickedButton.Name;

            SetButtonColours();

            RaiseAssumptionChangedEvent();
        }

        #endregion

    }
}
