using KForceCars.Models;
using Microsoft.EntityFrameworkCore;

namespace KForceCars.Data;

public class CarDbContext : DbContext
{
    public CarDbContext(DbContextOptions<CarDbContext> options)
        : base(options)
    {
        if (Car.Any()) return;
        var defaultCars = new List<CarModel>
        {
            new CarModel
                {Id = 1, Make = "Audi", Model = "R8", Year = 2018, Doors = 2, Color = "Red", Price = 79995},
            new CarModel
                {Id = 2, Make = "Tesla", Model = "3", Year = 2018, Doors = 4, Color = "Black", Price = 54995},
            new CarModel
            {
                Id = 3, Make = "Porsche", Model = " 911 991", Year = 2020, Doors = 2, Color = "White",
                Price = 155000
            },
            new CarModel
            {
                Id = 4, Make = "Mercedes-Benz", Model = "GLE 63S", Year = 2021, Doors = 5, Color = "Blue",
                Price = 83995
            },
            new CarModel
                {Id = 5, Make = "BMW", Model = "X6 M", Year = 2020, Doors = 5, Color = "Silver", Price = 62995},
        };
        Car.AddRange(defaultCars);
        SaveChanges();
    }

    public virtual DbSet<CarModel> Car => Set<CarModel>();
}