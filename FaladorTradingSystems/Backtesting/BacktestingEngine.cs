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

        public BacktestingEngine( MarketData marketData)
        {
            Events = new EventStack();
            ExecutionHandler = new NaiveExecutionHandler(Events);
            Data = marketData;
        }

        #endregion

        #region properties

        protected EventStack Events { get; }
        protected IExecutionHandler ExecutionHandler { get; }
        MarketData Data { get; }

        #endregion

        #region public methods
           
        public IPortfolio RunBacktest(IStrategy strategy,
                                IPortfolio portfolio,
                                IDataHandler dataHandler)
        {

            while (true)
            {
                dataHandler.UpdateBars();
                if (!dataHandler.ContinueBacktest) break;

                IEvent latestEvent;

                while (true)
                {
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
                }

                Thread.Sleep(_modelHeartbeat);
            }

            return portfolio;

        }

        public StrategyBuyAndHold GetBuyAndHoldStrategy(IDataHandler dataHandler)
        {
            StrategyBuyAndHold output =
                new StrategyBuyAndHold(dataHandler, Events);

            return output;
        }

        public NaivePortfolio GetNaivePortfolio(
            decimal initialCapital, 
            DateTime startDate,
            IDataHandler dataHandler)
        {
            NaivePortfolio output =
                new NaivePortfolio(initialCapital, startDate,
                dataHandler, Events);

            return output;
        }

        public HistoricSeriesDataHandler GetHistoricDataHandler(DateRange range, 
                                                                List<string> allowableAssets = null)
        {
            MarketData data = Data.GetMarketDataInRange(range, allowableAssets);

            HistoricSeriesDataHandler handler =
                new HistoricSeriesDataHandler(data, Events);

            return handler;
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
            portfolio.GetTradesForSignal(signalEv);
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
