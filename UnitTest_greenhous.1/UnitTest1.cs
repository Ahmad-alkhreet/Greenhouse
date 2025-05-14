using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;

public class SensorServiceTests
{
    private readonly Mock<SensorRepository> _mockSensorRepo;
    private readonly Mock<ILogger<SensorService>> _mockLogger;
    private readonly SensorService _sensorService;

    public SensorServiceTests()
    {
        _mockSensorRepo = new Mock<SensorRepository>(null);
        _mockLogger = new Mock<ILogger<SensorService>>();

        _sensorService = new SensorService(_mockSensorRepo.Object);
    }

    [Fact]
    public async Task AddSensorAsync_ShouldCallRepositoryAddAsync()
    {
        // Arrange
        var location = "Test";
        var type = "Temp";
        var buildingId = 1;

        // Act
        await _sensorService.AddSensorAsync(location, type, buildingId);

        // Assert
        _mockSensorRepo.Verify(repo =>
            repo.AddAsync(It.Is<Sensor>(s =>
                s.GetLocation() == location &&
                s.GetType() == type &&
                s.GetBuildingId() == buildingId
            )),
            Times.Once);
    }

    [Fact]
    public async Task GetAllSensorsAsync_ShouldReturnSensors()
    {
        // Arrange
        var sensors = new List<Sensor> { new Sensor(1, "A", "T", 1) };
        _mockSensorRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(sensors);

        // Act
        var result = await _sensorService.GetAllSensorsAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("A", result[0].GetLocation());
    }

    [Fact]
    public async Task DeleteSensorAsync_ShouldCallDeleteAsync()
    {
        // Act
        await _sensorService.DeleteSensorAsync(1);

        // Assert
        _mockSensorRepo.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task UpdateSensorAsync_ShouldUpdateSensor_IfExists()
    {
        // Arrange
        var id = 1;
        var location = "UpdatedLoc";
        var type = "UpdatedType";
        var buildingId = 2;
        var existingSensor = new Sensor(id, "OldLoc", "OldType", 1);

        _mockSensorRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existingSensor);

        // Act
        await _sensorService.UpdateSensorAsync(id, location, type, buildingId);

        // Assert
        _mockSensorRepo.Verify(r =>
            r.UpdateAsync(It.Is<Sensor>(s =>
                s.GetId() == id &&
                s.GetLocation() == location &&
                s.GetType() == type &&
                s.GetBuildingId() == buildingId
            )), Times.Once);
    }
}
