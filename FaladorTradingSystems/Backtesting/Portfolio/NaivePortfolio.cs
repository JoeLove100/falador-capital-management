using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaladorTradingSystems.DataHandling;
using FaladorTradingSystems.Events;
using Utils;

namespace FaladorTradingSystems.Portfolio 
{
    /// <summary>
    /// basic portfolio system which naively
    /// instructs fixed trade amounts on 
    /// all trades based on strategies
    /// </summary>
    public class NaivePortfolio : IPortfolio
    {
        #region constructors

        public NaivePortfolio(double initialCapital,
            DateTime initialDate,
            DataHandler handler,
            SortedList<DateTime, IEvent> eventQueue)
        {
            _initialCapital = initialCapital;
            _initialDate = initialDate;
            _handler = handler;
            _eventQueue = eventQueue;

            InitialiseAllocation();
            InitialiseValuations();
        }

        #endregion

        #region properties

        protected double _initialCapital { get; }
        protected DateTime _initialDate { get; }
        protected DataHandler _handler { get; }
        protected SortedList<DateTime, IEvent> _eventQueue { get; set; }

        public AssetAllocation CurrentAllocation { get; set; }
        public SortedList<DateTime, AssetAllocation> AllocationHistory { get; set; }
        public PortfolioValuation CurrentValuation { get; set; }
        public SortedList<DateTime, PortfolioValuation> ValuationHistory { get; set; }


        #endregion

        #region public methods

        public void InitialiseAllocation()
        {
            CurrentAllocation = new AssetAllocation(_handler.AllAssets);
            AllocationHistory = new SortedList<DateTime, AssetAllocation>
            {
                { _initialDate, CurrentAllocation }
            };
        }

        public void InitialiseValuations()
        {
            CurrentValuation = new PortfolioValuation(_handler.AllAssets, _initialCapital);
            ValuationHistory = new SortedList<DateTime, PortfolioValuation>
            {
                { _initialDate, CurrentValuation }
            };
        }

        public void UpdateForMarketData(MarketEvent marketEvent)
        {
            Dictionary<string, Bar[]> bars = new Dictionary<string, Bar[]>();

            foreach (string asset in _handler.AllAssets)
            {
                bars.Add(asset, _handler.GetLatestBars(asset, 1));
            }

            AssetAllocation newAssetAllocation = CurrentAllocation.Clone();
            AllocationHistory.Add(_handler.CurrentDate, newAssetAllocation);

            Dictionary<string, double> lastPrices = _handler.GetLastPrices();

            PortfolioValuation newValuation = GetValuationFromHoldings(newAssetAllocation,
                lastPrices, CurrentValuation.FreeCash, CurrentValuation.Commision);
            ValuationHistory.Add(_handler.CurrentDate, newValuation);
            
        }

        public void UpdateHoldingsForFill(FillEvent fillEvent)
        {
            UpdateAllocationForFill(fillEvent);
            UpdateHoldingsForFill(fillEvent);
        }

        public void UpdateForSignals(SignalEvent signalEvent)
        {
            TradeEvent tradeEvent = GenerateTradeFromSignal(signalEvent);
            _eventQueue.Add(tradeEvent.DateTimeGenerated, tradeEvent);
        }

        #endregion

        #region protected methods

        protected PortfolioValuation GetValuationFromHoldings(AssetAllocation allocation,
            Dictionary<string, double> lastPrices, double freeCash, double commission)
        {
            {
                List<string> assets = new List<string>();
                List<double> values = new List<double>();

                foreach (KeyValuePair<string, double> holding in allocation)
                {
                    double positionValue = holding.Value * lastPrices[holding.Key];
                    assets.Add(holding.Key);
                    values.Add(positionValue);
                }

                PortfolioValuation valuation = new PortfolioValuation(assets, values,
                    freeCash, commission);

                return valuation;
            }
        }

        protected void UpdateAllocationForFill(FillEvent fillEvent)
        {
            switch (fillEvent.OrderType)
            {
                case OrderType.Buy:
                    CurrentAllocation[fillEvent.Ticker] += fillEvent.Quantity;
                    break;
                case OrderType.Sell:
                    CurrentAllocation[fillEvent.Ticker] -= fillEvent.Quantity;
                    break;
                default:
                    throw new ArgumentException($"Uknown order type placed " +
                        $"for{fillEvent.Ticker} as at " +
                        $"{fillEvent.DateTimeFilled.ToShortDateString()}");
            }  
        }

        protected void UpdateValuationsForFill(FillEvent fillEvent)
        {
            double assetPrice = _handler.GetLatestBars(fillEvent.Ticker)[0].Price;
            double fillCost = fillEvent.Quantity * assetPrice;

            switch (fillEvent.OrderType)
            {
                case OrderType.Buy:
                    CurrentValuation[fillEvent.Ticker] += assetPrice;
                    break;
                case OrderType.Sell:
                    CurrentValuation[fillEvent.Ticker] -= assetPrice;
                    break;
                default:
                    throw new ArgumentException($"Unknown order type placed" +
                        $"for {fillEvent.Ticker} as at " +
                        $"{fillEvent.DateTimeFilled.ToShortDateString()}");
            }
        }

        protected TradeEvent GenerateNaiveTrade(SignalEvent signalEvent)
        {
            ///<summary>
            ///basic ordering mechanism, with no 
            ///dynamic sizing or risk management
            ///considerations
            ///</summary>

            switch (signalEvent.Direction)
            {
                case SignalDirection.Long:
                    double buyQuantity = Math.Floor(100 * signalEvent.Strenth);
                    TradeEvent buyTrade = new TradeEvent(DateTime.Now,
                        signalEvent.Ticker, OrderType.Buy, buyQuantity);
                    return buyTrade;

                case SignalDirection.Short:
                    double sellQuantity = Math.Floor(100 * signalEvent.Strenth);
                    TradeEvent sellTrade = new TradeEvent(DateTime.Now,
                        signalEvent.Ticker, OrderType.Buy, sellQuantity);
                    return sellTrade;

                case SignalDirection.Exit:
                    TradeEvent exitTrade = GetExitTradeEvent(signalEvent.Ticker);
                    return exitTrade;
                default:
                    throw new ArgumentException($"Uknown signal type was passed" +
                        $"for {signalEvent.Ticker} as at" +
                        $"{signalEvent.DateTimeGenerated.ToShortDateString()}");
            }
        }

        protected TradeEvent GetExitTradeEvent(string ticker)
        {
            double exitQuantity = CurrentAllocation[ticker];
            if (exitQuantity < 0)
            {
                TradeEvent exitBuyTrade = new TradeEvent(DateTime.Now,
                   ticker, OrderType.Buy, -exitQuantity);
                return exitBuyTrade;
            }
            else
            {
                TradeEvent exitBuyTrade = new TradeEvent(DateTime.Now,
                ticker, OrderType.Buy, -exitQuantity);
                return exitBuyTrade;
            }
        }

        protected TradeEvent GenerateTradeFromSignal(SignalEvent signalEvent)
        {
            TradeEvent tradeEvent = GenerateNaiveTrade(signalEvent);
            return tradeEvent;
        }

        #endregion

    }
}
