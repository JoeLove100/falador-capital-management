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
using Utils;

namespace FaladorTradingSystems.UserControls
{
    /// <summary>
    /// Interaction logic for WindowDateRange.xaml
    /// </summary>
    public partial class DateRangeSettingsControl : UserControl, ISettingsControl
    {
        #region constants

        protected const string _name = "Selected date range";

        #endregion 

        #region constructor 

        public DateRangeSettingsControl()
        {
            WindowName = _name;
            InitializeComponent();
        }

        #endregion

        public string WindowName { get; }

        double ISettingsControl.Height => Height;

        double ISettingsControl.Width => Width;

        #region methods

        public void SetSettings(object settings)
        {
            DateRange currentRange = (DateRange)settings;
            DatePickerStart.Value = currentRange.Start;
            DatePickerEnd.Value = currentRange.End;
        }


        public object GetSettings()
        {
            DateTime startDate = DatePickerStart.Value.GetValueOrDefault();
            DateTime endDate = DatePickerEnd.Value.GetValueOrDefault();

            return new DateRange(startDate, endDate);
        }

        #endregion

    }
}
