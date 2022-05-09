using Currency_Converter.Extentions;
using Currency_Converter.Interfaces;
using Currency_Converter.Models;

namespace Currency_Converter.Services
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private List<Rate> _curencyRates = new List<Rate>();
        private CurrencyGraph _currentGraph { get; set; } = new CurrencyGraph();
        public async Task ClearConfiguration()
        {
            try
            {
                await Task.Run(() =>
                {
                    _curencyRates.Clear();
                    _currentGraph = new CurrencyGraph();
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MyHttpResponse<string>> Convert(string fromCurrency, string toCurrency, double amount)
        {
            try
            {
                if (_curencyRates.Count == 0)
                {
                    return new MyHttpResponse<string>(error: "Sorry");
                }
                var result = await Task.Run<double>(() =>
                 {
                     var shortestPath = new BFSAlgorithm().ShortestPathFunction(_currentGraph, fromCurrency.Trim().ToUpper());
                     var rates = shortestPath(toCurrency.Trim().ToUpper());
                     if (rates != null && rates.Count() > 0)
                     {
                         var rate = 1d;
                         foreach (var item in rates)
                             rate *= item.Value;
                         return rate * amount;
                     }
                     else
                     {
                         return -1;
                     }
                 });

                if (result != -1)
                {
                    return new MyHttpResponse<string>(result: fromCurrency + " => " + toCurrency + " = " + result);
                }
                else
                {
                   return new MyHttpResponse<string>(error: "Sorry\n We didn't suppoert this currenry");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateConfiguration(IEnumerable<Rate> conversionRates)
        {
            try
            {
                await Task.Run(() =>
               {
                   if (_curencyRates.Count() == 0)
                   {
                       _curencyRates.AddRange(conversionRates);
                   }
                   else
                   {
                       foreach (var item in conversionRates)
                       {
                           var index = _curencyRates.FindIndex(q =>
                            (q.From == item.From && q.To == item.To) ||
                            (q.From == item.To && q.To == item.From));
                           if (index != -1)
                               _curencyRates[index] = item;
                           else
                               _curencyRates.Add(item);
                       }
                   }
                   _currentGraph = new CurrencyGraph(_curencyRates);
               });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
