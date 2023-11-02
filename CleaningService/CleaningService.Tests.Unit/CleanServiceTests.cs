using AutoMapper;
using CleaningService.Api.Requests;
using CleaningService.Repositories;
using CleaningService.Repositories.Model;
using CleaningService.Services;
using CleaningService.Domain.Model;
using Microsoft.Extensions.Logging;

namespace CleaningService.Test;

using System.Collections.Generic;
using Moq;
using Xunit;

public class CleanServiceTests
{
    [Fact]
    public async void Clean_Should_Return_CleaningRecord()
    {
        // Arrange
        var cleanRepositoryMock = new Mock<ICleanRepository>();
        var mapperMock = new Mock<IMapper>();

        var expectedOutput = new CleanOutput(2, System.DateTime.Now, 4, 0.000123);

        var cleanService =
            new CleanService(cleanRepositoryMock.Object, mapperMock.Object, Mock.Of<ILogger<CleanService>>());
        var cleanInput = new CleanInput()
        {
            Start = new Coordinates() { X = 10, Y = 22 },
            Commands = new List<Command>
            {
                new() { Direction = DirectionEnum.East, Steps = 2 },
                new() { Direction = DirectionEnum.North, Steps = 1 },
            }
        };
        cleanRepositoryMock.Setup(repo => repo.AddCleaningRecord(It.IsAny<CleaningRecord>()))
            .ReturnsAsync((CleaningRecord record) => record);

        mapperMock.Setup(x => x.Map<CleanOutput>(It.IsAny<CleaningRecord>())).Returns(expectedOutput);
        // Act
        var result = await cleanService.Clean(cleanInput);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CleanOutput>(result);
        Assert.Equal(expectedOutput.Commands, result.Commands);
        Assert.Equal(expectedOutput.Result, result.Result);
        Assert.True(result.Duration > 0);
        cleanRepositoryMock.Verify(
            repo => repo.AddCleaningRecord(It.Is<CleaningRecord>(cleaningRecord =>
                cleaningRecord.Commands == expectedOutput.Commands &&
                cleaningRecord.Result == expectedOutput.Result)),
            Times.Once);
    }

    [Fact]
    public void SimulateCleaning_CircularPath()
    {
        // Arrange
        var cleanRepositoryMock = new Mock<ICleanRepository>();
        var cleanService = new CleanService(cleanRepositoryMock.Object, Mock.Of<IMapper>(),
            Mock.Of<ILogger<CleanService>>());
        var start = new Coordinates { X = 0, Y = 0 };
        var commands = new List<Command>
        {
            new Command { Direction = Domain.Model.DirectionEnum.North, Steps = 1 },
            new Command { Direction = Domain.Model.DirectionEnum.East, Steps = 1 },
            new Command { Direction = Domain.Model.DirectionEnum.South, Steps = 1 },
            new Command { Direction = Domain.Model.DirectionEnum.West, Steps = 1 },
        };

        // Act
        var result = cleanService.SimulateCleaning(start, commands);

        // Assert
        Assert.Equal(4, result);
    }

    [Fact]
    public void SimulateCleaning_Circular_Path_2()
    {
        // Arrange
        var cleanRepositoryMock = new Mock<ICleanRepository>();
        var cleanService = new CleanService(cleanRepositoryMock.Object, Mock.Of<IMapper>(),
            Mock.Of<ILogger<CleanService>>());
        var start = new Coordinates { X = 0, Y = 0 };
        
        // {0,0}, {0,1}, {0,0}, {1,0}, {0,0}
        var commands = new List<Command>
        {
            new Command { Direction = Domain.Model.DirectionEnum.North, Steps = 1 },
            new Command { Direction = Domain.Model.DirectionEnum.South, Steps = 1 },
            new Command { Direction = Domain.Model.DirectionEnum.East, Steps = 1 },
            new Command { Direction = Domain.Model.DirectionEnum.West, Steps = 1 }
        };

        // Act
        var result = cleanService.SimulateCleaning(start, commands);

        // Assert
        Assert.Equal(3, result);
    }
}