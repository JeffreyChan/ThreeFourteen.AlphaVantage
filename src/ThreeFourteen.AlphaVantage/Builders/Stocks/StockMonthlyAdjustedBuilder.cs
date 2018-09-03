﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeFourteen.AlphaVantage.Model;
using ThreeFourteen.AlphaVantage.Response;
using ThreeFourteen.AlphaVantage.Service;

namespace ThreeFourteen.AlphaVantage.Builders.Stocks
{
    public class StockMonthlyAdjustedBuilder : BuilderBase, IHaveData<TimeSeriesAdjustedEntry>
    {
        public StockMonthlyAdjustedBuilder(IAlphaVantageService service, string symbol)
            : base(service)
        {
            SetField(ParameterFields.Symbol, symbol);
        }

        protected override string[] RequiredFields => new[] { ParameterFields.Symbol };

        protected override Function Function => Function.Stocks.TimeSeriesMonthlyAdjusted;

        public Task<Result<TimeSeriesAdjustedEntry>> GetAsync()
        {
            return GetDataAsync(Parse);
        }

        private IEnumerable<TimeSeriesAdjustedEntry> Parse(JToken token)
        {
            var properties = token as JProperty;
            return properties.First.Children()
                .Select(x => ((JProperty)x).ToTimeSeriesAdjusted())
                .ToList();
        }
    }
}
