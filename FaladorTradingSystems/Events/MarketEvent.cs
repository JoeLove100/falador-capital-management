﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaladorTradingSystems.Events
{
    /// <summary>
    /// Event which represents an event being
    /// generated by the arrival of new market data
    /// </summary>
    public class MarketEvent : IEvent
    {
        #region constructor
        public MarketEvent()
        {
            Type = EventType.MarktetEvent;
        }
        #endregion

        #region properties
        public EventType Type { get; }
        #endregion 
    }
}
