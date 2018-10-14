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

namespace FaladorTradingSystems
{
    /// <summary>
    /// Interaction logic for DateRangeBar.xaml
    /// </summary>
    public partial class DateRangeBar : ObservableControl
    {
        #region constructors

        public DateRangeBar()
        {

        }

        #endregion

        #region properties

        protected DateTime MinDateStart { get; set; }
        protected DateTime MaxDateEnd { get; set; }
        protected DateTime MinDateEnd => ((DateTime)DateTimeUpDownEnd.Value).AddDays(-1);
        protected DateTime MaxDateStart => ((DateTime)DateTimeUpDownStart.Value).AddDays(1);

        #endregion

        #region methods

        public void InitialiseControls(DateRange dateRange)
        {
            MinDateStart = dateRange.Start;
            MaxDateEnd = dateRange.End;
            DateTimeUpDownStart.Value = dateRange.Start;
            DateTimeUpDownEnd.Value = dateRange.End;

            InitialiseSlider(dateRange);
            AddEventHandlers();
        }

        public DateRange GetSettings()
        {
            DateTime startDate = DateTimeUpDownStart.Value.GetValueOrDefault();
            DateTime endDate = DateTimeUpDownEnd.Value.GetValueOrDefault();
            DateRange output = new DateRange(startDate, endDate);
            return output;
        }

        private void InitialiseSlider(DateRange dateRange)
        {
            DateRangeSlider.Minimum = dateRange.Start.ToOADate();
            DateRangeSlider.LowerValue = DateRangeSlider.Minimum;

            DateRangeSlider.Maximum = dateRange.End.ToOADate();
            DateRangeSlider.HigherValue = DateRangeSlider.Maximum;
        }

        #endregion

        #region event handlers

        protected void AddEventHandlers()
        {
            DateTimeUpDownEnd.ValueChanged += HandleEndValueChanged;
            DateTimeUpDownStart.ValueChanged += HandleStartValueChanged;
            DateRangeSlider.PreviewMouseUp += HandleRangeSliderChanged;
        }

        protected void HandleRangeSliderChanged(object sender, RoutedEventArgs e)
        {
            if (IsBeingUpdated) return;

            IsBeingUpdated = true;
            DateTimeUpDownEnd.Value = DateTime.FromOADate(DateRangeSlider.HigherValue);
            DateTimeUpDownStart.Value = DateTime.FromOADate(DateRangeSlider.LowerValue);
            IsBeingUpdated = false;

            RaiseAssumptionChangedEvent();
        }

        protected void HandleStartValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (IsBeingUpdated) return;
            IsBeingUpdated = true;
            DateRangeSlider.LowerValue = DateTimeUpDownStart.Value.GetValueOrDefault().ToOADate();
            RaiseAssumptionChangedEvent();
            IsBeingUpdated = false;
        }

        protected void HandleEndValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (IsBeingUpdated) return;
            IsBeingUpdated = true;
            DateRangeSlider.HigherValue = DateTimeUpDownEnd.Value.GetValueOrDefault().ToOADate();
            RaiseAssumptionChangedEvent();
            IsBeingUpdated = false;
        }


        #endregion
    }
}
