using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaladorTradingSystems.Backtesting.Events;
using FaladorTradingSystems.Backtesting.DataHandling;
using FaladorTradingSystems.Backtesting.Portfolio;
using FaladorTradingSystems.Backtesting.Strategies;
using FaladorTradingSystems.Backtesting.Execution;

namespace FaladorTradingSystems.Backtesting
{
    public class BacktestingEngine

    ///<summary>
    ///main engine class for handling the 
    ///backtesting of strategies
    ///</summary>
    {
        #region constructor

        public BacktestingEngine(EventStack eventStack, IDataHandler dataHandler, IPortfolio portfolio,
            IStrategy strategy, IExecutionHandler executionHandler)
        {
            DataHandler = dataHandler;
            Portfolio = portfolio;
            Strategy = strategy;
            ExecutionHandler = executionHandler;
        }

        #endregion

        #region properties

        protected EventStack Events { get; }
        protected IDataHandler DataHandler { get; }
        protected IPortfolio Portfolio { get; }
        protected IStrategy Strategy { get; }
        protected IExecutionHandler ExecutionHandler { get; }

        #endregion

        #region methods
           
        public void RunBacktest()
        {
            while (true)
            {
                if (!DataHandler.ContinueBacktest) break;
                DataHandler.UpdateBars();

                IEvent latestEvent;

                try
                {
                    latestEvent = Events.GetEvent();
                }
                catch (InvalidOperationException)
                {
                    break;
                }

                switch (latestEvent.Type)
                {
                    case EventType.MarketEvent:
                        MarketEvent marketEv = (MarketEvent)latestEvent;
                        HandleMarketEvent(marketEv);
                        continue;
                    case EventType.SignalEvent:
                        SignalEvent signalEv = (SignalEvent)latestEvent;
                        HandleSignalEvent(signalEv);
                        continue;
                    case EventType.TradeEvent:
                        TradeEvent tradeEv = (TradeEvent)latestEvent;
                        HandleTradeEvent(tradeEv);
                        break;
                    case EventType.FillEvent:
                        FillEvent fillEv = (FillEvent)latestEvent;
                        HandleFillEvent(fillEv);
                        break;
                    default:
                        throw new ArgumentException("Unkown even type!");
                }


            }

        }

        protected void HandleMarketEvent(MarketEvent marketEv)
        {
            Strategy.GenerateSignals(marketEv);
            Portfolio.UpdateForMarketData(marketEv);
        }

        protected void HandleSignalEvent(SignalEvent singalEv)
        {
            Portfolio.UpdateForSignals(singalEv);
        }

        protected void HandleTradeEvent(TradeEvent tradeEv)
        {
            ExecutionHandler.GetFillEventForTrade(tradeEv);
        }

        protected void HandleFillEvent(FillEvent fillEv)
        {
            Portfolio.UpdateHoldingsForFill(fillEv);
        }

        #endregion 
    }
}
