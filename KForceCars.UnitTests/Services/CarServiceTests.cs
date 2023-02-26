using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using KForceCars.Data;
using KForceCars.Models;
using KForceCars.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KForceCars.UnitTests.Services;

public class CarServiceTests
{
    private readonly Mock<ILogger<CarService>> _loggerMock;
    private readonly CarDbContext _carDbContextMock;

    public CarServiceTests()
    {
        var options = new DbContextOptionsBuilder<CarDbContext>()
            .UseInMemoryDatabase(databaseName: "KForceCarsTesting")
            .Options;
        _carDbContextMock = new CarDbContext(options);
        _loggerMock = new Mock<ILogger<CarService>>();
    }

    private CarService CarService => new(
        _carDbContextMock,
        _loggerMock.Object
    );

    [Fact]
    public async Task GivenACompleteCarModel_WhenAddingACar_ThenResultShouldBeTrue()
    {
        // Arrange
        var initialCount = _carDbContextMock.Car.Count();
        var instance = CarService;

        // Act
        var car = new Fixture().Create<CarModel>();
        var result = await instance.CreateAsync(car);

        // Assert
        result.Should().BeTrue();
        _carDbContextMock.Car.Should().HaveCount(initialCount + 1);
    }

    [Fact]
    public async Task GivenANullCarModel_WhenAddingACar_ThenResultShouldBeFalse()
    {
        // Arrange
        var initialCount = _carDbContextMock.Car.Count();
        var instance = CarService;

        // Act
        var result = await instance.CreateAsync(null);

        // Assert
        result.Should().BeFalse();
        _carDbContextMock.Car.Should().HaveCount(initialCount);
    }

    [Fact]
    public async Task GivenACompleteCarModel_WhenUpdatingACar_ThenResultShouldBeTrue()
    {
        // Arrange
        var initialCount = _carDbContextMock.Car.Count();
        var instance = CarService;
        const string newModel = "Testing model";
        var car = await _carDbContextMock.Car.FirstOrDefaultAsync();
        car.Model = newModel;

        // Act
        var result = await instance.UpdateAsync(car);

        // Assert
        result.Should().BeTrue();
        _carDbContextMock.Car.Should().HaveCount(initialCount);
        var carUpdated = await _carDbContextMock.Car.FirstOrDefaultAsync(x => x.Id == car.Id);
        carUpdated.Should().NotBeNull();
        carUpdated.Model.Should().Be(newModel);
    }

    [Fact]
    public async Task GivenANullCarModel_WhenUpdatingACar_ThenResultShouldBeFalse()
    {
        // Arrange
        var initialCount = _carDbContextMock.Car.Count();
        var instance = CarService;

        // Act
        var result = await instance.UpdateAsync(null);

        // Assert
        result.Should().BeFalse();
        _carDbContextMock.Car.Should().HaveCount(initialCount);
    }

    [Fact]
    public async Task GivenAnExistingCarModel_WhenDeletingACar_ThenResultShouldBeTrue()
    {
        // Arrange
        var initialCount = _carDbContextMock.Car.Count();
        var instance = CarService;
        var car = await _carDbContextMock.Car.FirstOrDefaultAsync();

        // Act
        var result = await instance.DeleteAsync(car.Id);

        // Assert
        result.Should().BeTrue();
        _carDbContextMock.Car.Should().HaveCount(initialCount - 1);
        var carUpdated = await _carDbContextMock.Car.FirstOrDefaultAsync(x => x.Id == car.Id);
        carUpdated.Should().BeNull();
    }

    [Fact]
    public async Task GivenANonExistingCarModel_WhenDeletingACar_ThenResultShouldBeFalse()
    {
        // Arrange
        var initialCount = _carDbContextMock.Car.Count();
        var instance = CarService;

        // Act
        var result = await instance.DeleteAsync(0);

        // Assert
        result.Should().BeFalse();
        _carDbContextMock.Car.Should().HaveCount(initialCount);
    }

    [Fact]
    public async Task GivenACorrectGuess_WhenGuessingACarPrice_ThenResultShouldBeTrue()
    {
        // Arrange
        var instance = CarService;
        var car = await _carDbContextMock.Car.FirstOrDefaultAsync();
        
        // Act
        var (isCorrectResult, carPriceResult) = await instance.IsPriceGuessCorrectAsync(car.Id, car.Price);

        // Assert
        isCorrectResult.Should().BeTrue();
        carPriceResult.Should().Be(car.Price);
    }
    
    [Fact]
    public async Task GivenAnIncorrectGuess_WhenGuessingACarPrice_ThenResultShouldBeFalse()
    {
        // Arrange
        var instance = CarService;
        var car = await _carDbContextMock.Car.FirstOrDefaultAsync();
        
        // Act
        var guessed = car.Price - 10000;
        var (isCorrectResult, carPriceResult) = 
            await instance.IsPriceGuessCorrectAsync(car.Id, guessed);

        // Assert
        isCorrectResult.Should().BeFalse();
        carPriceResult.Should().BeGreaterThan(guessed);
    }
}