using System.Threading.Tasks;

namespace Demo.BL
{
    public interface IWeather
    {
        Task<string> GetWeather();
    }
}