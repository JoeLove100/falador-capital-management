using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace FaladorTradingSystems
{
    public abstract class ObservableControl : UserControl, INotifyPropertyChanged
    {

        #region property changed
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

        #region raising assumption change

        public event EventHandler<AssumptionChangedEventArgs> AssumptionChanged;

        protected bool IsBeingUpdated { get; set; }

        protected void RaiseAssumptionChangedEvent()
        {
            if (IsBeingUpdated) return;

            AssumptionChanged?.Invoke(this, new AssumptionChangedEventArgs());
        }

        #endregion 
    }

    #region assumption changed event 

    public class AssumptionChangedEventArgs : EventArgs
    {
        //might add properties to this later...
    }

    #endregion 
}
