using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FaladorTradingSystems.Backtesting.Events;
using FaladorTradingSystems.Backtesting.DataHandling;
using FaladorTradingSystems.Backtesting.Portfolio;
using FaladorTradingSystems.Backtesting.Strategies;
using FaladorTradingSystems.Backtesting.Execution;
using Utils;

namespace FaladorTradingSystems.Backtesting
{
    public class BacktestingEngine

    ///<summary>
    ///main engine class for handling the 
    ///backtesting of strategies
    ///</summary>
    {

        #region constant

        protected readonly int _modelHeartbeat = 10;

        #endregion 


        #region constructor

        public BacktestingEngine(EventStack eventStack, MarketData marketData)
        {
            DataHandler = new HistoricSeriesDataHandler(marketData, eventStack);
            ExecutionHandler = new NaiveExecutionHandler(eventStack);
        }

        #endregion

        #region properties

        protected EventStack Events { get; }
        public IDataHandler DataHandler { get; }
        protected IExecutionHandler ExecutionHandler { get; }

        #endregion

        #region public methods
           
        public void RunBacktest(IStrategy strategy,
                                IPortfolio portfolio)
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
                        HandleMarketEvent(marketEv, strategy, portfolio);
                        continue;
                    case EventType.SignalEvent:
                        SignalEvent signalEv = (SignalEvent)latestEvent;
                        HandleSignalEvent(signalEv, portfolio);
                        continue;
                    case EventType.TradeEvent:
                        TradeEvent tradeEv = (TradeEvent)latestEvent;
                        HandleTradeEvent(tradeEv);
                        break;
                    case EventType.FillEvent:
                        FillEvent fillEv = (FillEvent)latestEvent;
                        HandleFillEvent(fillEv, portfolio);
                        break;
                    default:
                        throw new ArgumentException("Unkown even type!");
                }

                Thread.Sleep(_modelHeartbeat);
            }

        }

        public StrategyBuyAndHold GetBuyAndHoldStrategy()
        {
            StrategyBuyAndHold output =
                new StrategyBuyAndHold(DataHandler);

            return output;
        }

        public NaivePortfolio GetNaivePortfolio(
            double initialCapital, 
            DateTime startDate)
        {
            NaivePortfolio output =
                new NaivePortfolio(initialCapital, startDate,
                DataHandler, Events);

            return output;
        }


        #endregion

        #region protected methods

        protected void HandleMarketEvent(MarketEvent marketEv, 
            IStrategy strategy,
            IPortfolio portfolio)
        {
            strategy.GenerateSignals(marketEv);
            portfolio.UpdateForMarketData(marketEv);
        }

        protected void HandleSignalEvent(SignalEvent signalEv, 
            IPortfolio portfolio)
        {
            portfolio.UpdateForSignals(signalEv);
        }

        protected void HandleTradeEvent(TradeEvent tradeEv)
        {
            ExecutionHandler.GetFillEventForTrade(tradeEv);
        }

        protected void HandleFillEvent(FillEvent fillEv,
            IPortfolio portfolio)
        {
            portfolio.UpdateHoldingsForFill(fillEv);
        }

        #endregion 
    }
}
