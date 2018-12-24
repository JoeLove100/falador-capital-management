using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaladorTradingSystems.UserControls
{
    /// <summary>
    /// interface for UI objects which 
    /// edit settings for the main 
    /// panels
    /// </summary>

    public interface ISettingsControl
    {
        object GetSettings();

        void SetSettings(object settings);

        string WindowName { get; }

        double Height { get; }
        double Width { get; }

    }
}
