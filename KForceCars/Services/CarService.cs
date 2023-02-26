using KForceCars.Data;
using KForceCars.Models;
using KForceCars.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KForceCars.Services;

public class CarService : ICarService
{
    private readonly CarDbContext _carDbContext;
    private readonly ILogger<CarService> _logger;
    private const int AcceptMarginErrorPrice = 5000;

    public CarService(CarDbContext carDbContext, ILogger<CarService> logger)
    {
        _carDbContext = carDbContext;
        _logger = logger;
    }

    public async Task<CarModel> GetByIdAsync(long id)
        => await _carDbContext.Car.FirstOrDefaultAsync(x => x.Id == id);

    public IEnumerable<CarModel> GetAsync()
        => _carDbContext.Car.AsEnumerable();

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var car = await GetByIdAsync(id);
            _carDbContext.Car.Remove(car);
            await _carDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return false;
        }
    }

    public async Task<bool> CreateAsync(CarModel carModel)
    {
        try
        {
            _carDbContext.Car.Add(carModel);
            await _carDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return false;
        }
    }

    public async Task<bool> UpdateAsync(CarModel carModel)
    {
        try
        {
            _carDbContext.Car.Update(carModel);
            await _carDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return false;
        }
    }

    public async Task<(bool, decimal)> IsPriceGuessCorrectAsync(long carId, decimal price)
    {
        var car = await GetByIdAsync(carId);

        return (price.IsBetween(
                car.Price - AcceptMarginErrorPrice, 
                car.Price + AcceptMarginErrorPrice),
                car.Price);
    }
}