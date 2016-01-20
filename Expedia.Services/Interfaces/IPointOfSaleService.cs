using System.Threading.Tasks;
using Expedia.Entities.AppEnvContext;

namespace Expedia.Services.Interfaces
{
    public interface IPointOfSaleService
    {
        Task<PointOfSale[]> GetPointsOfSale();
        Task<PointOfSale> GetCurrentPointOfSale();
        Task SetCurrentPointOfSale(string countryId);
        Task<PointOfSaleCulture[]> GetCultures();
        Task<PointOfSaleCulture> GetCurrentCulture();
        void SetCurrentCulture(string cultureCode);
    }
}
