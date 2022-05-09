using Currency_Converter.Interfaces;
using Currency_Converter.Models;
using Microsoft.AspNetCore.Mvc;

namespace Currency_Converter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {

        private readonly ICurrencyConverter _currencyConverter;
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ICurrencyConverter currencyConverter, ILogger<CurrencyController> logger)
        {
            _currencyConverter = currencyConverter;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string fromCurrency, string toCurrency, double amount)
        {
            try
            {
                var result= await _currencyConverter.Convert(fromCurrency, toCurrency, amount);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(IEnumerable<Rate> conversionRates)
        {
            try
            {
                await _currencyConverter.UpdateConfiguration(conversionRates);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            try
            {
                await _currencyConverter.ClearConfiguration();
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}