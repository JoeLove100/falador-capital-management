using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaladorTradingSystems.Backtesting.DataHandling;
using FaladorTradingSystems.Backtesting.Events;
using Utils;

namespace FaladorTradingSystems.Backtesting.Portfolio
{
    /// <summary>
    /// basic portfolio system which naively
    /// instructs fixed trade amounts on 
    /// all trades based on strategies
    /// </summary>
    public class NaivePortfolio : IPortfolio
    {
        #region constructors

        public NaivePortfolio(decimal initialCapital,
            DateTime initialDate,
            IDataHandler handler,
            EventStack eventStack)
        {
            _initialCapital = initialCapital;
            _initialDate = initialDate;
            _handler = handler;
            _eventStack = eventStack;

            InitialiseAllocation(initialCapital);
            InitialiseValuations(initialCapital);
        }

        #endregion

        #region properties

        protected decimal _initialCapital { get; }
        protected DateTime _initialDate { get; }
        protected IDataHandler _handler { get; }
        protected EventStack _eventStack { get; set; }

        public AssetAllocation CurrentAllocation { get; set; }
        public SortedList<DateTime, AssetAllocation> AllocationHistory { get; set; }
        public PortfolioValuation CurrentValuation { get; set; }
        public SortedList<DateTime, PortfolioValuation> ValuationHistory { get; set; }


        #endregion

        #region public methods

        public void InitialiseAllocation(decimal initialCapital)
        {
            CurrentAllocation = new AssetAllocation(_handler.AllAssets, initialCapital);
            //AllocationHistory = new SortedList<DateTime, AssetAllocation>
            //{
            //    { _initialDate, CurrentAllocation }
            //};

            AllocationHistory = new SortedList<DateTime, AssetAllocation>();
        }

        public void InitialiseValuations(decimal initialCapital)
        {
            CurrentValuation = new PortfolioValuation(_handler.AllAssets, initialCapital);
            //ValuationHistory = new SortedList<DateTime, PortfolioValuation>
            //{
            //    { _initialDate, CurrentValuation }
            //};

            ValuationHistory = new SortedList<DateTime, PortfolioValuation>();
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

            Dictionary<string, decimal> lastPrices = _handler.GetLastPrices();

            PortfolioValuation newValuation = GetValuationFromHoldings(newAssetAllocation,
                lastPrices, CurrentValuation.FreeCash, CurrentValuation.Commision);
            ValuationHistory.Add(_handler.CurrentDate, newValuation);
            
        }

        public void UpdateHoldingsForFill(FillEvent fillEvent)
        {
            UpdateAllocationForFill(fillEvent);
            UpdateValuationsForFill(fillEvent);
        }

        public void GetTradesForSignal(SignalEvent signalEvent)
        {
            TradeEvent tradeEvent = GenerateTradeFromSignal(signalEvent);
            _eventStack.PutEvent(tradeEvent);
        }

        public SortedList<DateTime, decimal> GetReturnSeries()
        {
            var valuations = new SortedList<DateTime, decimal>();

            foreach(KeyValuePair<DateTime, PortfolioValuation> kvp in ValuationHistory)
            {
                valuations.Add(kvp.Key, kvp.Value.GetTotal());
            }

            var output = (SortedList<DateTime, decimal>) valuations.PriceToScaledCumulativeReturns(100);

            return output;
        }

        #endregion

        #region protected methods

        protected PortfolioValuation GetValuationFromHoldings(AssetAllocation allocation,
            Dictionary<string, decimal> lastPrices, decimal freeCash, decimal commission)
        {
            {
                List<string> assets = new List<string>();
                List<decimal> values = new List<decimal>();

                foreach (KeyValuePair<string, decimal> holding in allocation)
                {
                    decimal positionValue = holding.Value * lastPrices[holding.Key];
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
            decimal assetPrice = _handler.GetLatestBars(fillEvent.Ticker)[0].Price;
            decimal fillCost = fillEvent.Quantity * assetPrice;

            switch (fillEvent.OrderType)
            {
                case OrderType.Buy:
                    CurrentValuation[fillEvent.Ticker] += fillCost;
                    CurrentValuation.FreeCash -= fillCost;
                    CurrentAllocation.FreeCash -= fillCost;
                    break;
                case OrderType.Sell:
                    CurrentValuation[fillEvent.Ticker] -= fillCost;
                    CurrentValuation.FreeCash += fillCost;
                    CurrentAllocation.FreeCash += fillCost;
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
                    decimal buyQuantity = Math.Floor(100 * signalEvent.Strenth);
                    TradeEvent buyTrade = new TradeEvent(DateTime.Now,
                        signalEvent.Ticker, OrderType.Buy, buyQuantity);
                    return buyTrade;

                case SignalDirection.Short:
                    decimal sellQuantity = Math.Floor(100 * signalEvent.Strenth);
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
            decimal exitQuantity = CurrentAllocation[ticker];
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
