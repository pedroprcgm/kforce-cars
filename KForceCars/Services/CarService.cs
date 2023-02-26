using KForceCars.Data;
using KForceCars.Models;
using Microsoft.EntityFrameworkCore;

namespace KForceCars.Services;

public class CarService : ICarService
{
    private readonly CarDbContext _dbContext;
    private readonly ILogger<CarService> _logger;

    public CarService(CarDbContext dbContext, ILogger<CarService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<CarModel> GetByIdAsync(long id)
        => await _dbContext.Car.FirstOrDefaultAsync(x => x.Id == id);

    public IEnumerable<CarModel> GetAsync()
        => _dbContext.Car.AsEnumerable();

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var car = await GetByIdAsync(id);
            _dbContext.Car.Remove(car);
            await _dbContext.SaveChangesAsync();
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
            _dbContext.Car.Add(carModel);
            await _dbContext.SaveChangesAsync();
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
            _dbContext.Car.Update(carModel);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return false;
        }
    }
}