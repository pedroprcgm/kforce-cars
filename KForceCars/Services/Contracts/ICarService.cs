using KForceCars.Models;

namespace KForceCars.Services.Contracts;

public interface ICarService
{
    Task<CarModel> GetByIdAsync(long id);
    IEnumerable<CarModel> GetAsync();
    Task<bool> DeleteAsync(long id);
    Task<bool> CreateAsync(CarModel carModel);
    Task<bool> UpdateAsync(CarModel carModel);
    Task<(bool, decimal)> IsPriceGuessCorrectAsync(long carId, decimal price);
}