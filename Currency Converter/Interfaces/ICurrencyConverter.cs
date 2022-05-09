using Currency_Converter.Models;

namespace Currency_Converter.Interfaces
{
    public interface ICurrencyConverter
    {

        /// <summary>
        /// Clears any prior configuration.
        /// </summary>
        public Task ClearConfiguration();

        /// <summary>
        /// Updates the configuration. Rates are inserted or replaced internally.
        /// </summary>
        public Task UpdateConfiguration(IEnumerable<Rate> conversionRates);

        /// <summary>
        /// Converts the specified amount to the desired currency.
        /// </summary>
        public Task<MyHttpResponse<string>> Convert(string fromCurrency, string toCurrency, double amount);

    }
}
