using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.BL.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task GetWeatherStringJson() // not void! https://stackoverflow.com/questions/14205590/await-a-async-void-method-call-for-unit-testing
        {
            try
            {
                IWeather weather = new Weather();
                string forecastJson = await weather.GetWeather();
            }
            catch (Exception ex) // ProtocolException
            {
                // The exception will be caught because you've awaited
                // the call in an async method.
            }
            
        }
    }
}
