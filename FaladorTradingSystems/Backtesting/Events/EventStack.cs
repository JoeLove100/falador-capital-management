using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaladorTradingSystems.Backtesting.Events;

namespace FaladorTradingSystems.Backtesting.Events
{
    /// <summary>
    /// holder class to store queue
    /// of events for backtests
    /// </summary>
    public class EventStack
    {
        #region constructor

        public EventStack()
        {
            _lockObject = new object();
        }

        #endregion


        #region properties

        protected Stack<IEvent> _eventStack;
        protected object _lockObject { get; }


        #endregion

        #region methods

        public IEvent GetEvent()
        {
            lock (_lockObject)
            {
                return _eventStack.Pop();
            }
        }

        public void PutEvent(IEvent ev)
        {
            lock (_lockObject)
            {
                _eventStack.Push(ev);
            }
        }

        #endregion 
    }
}
