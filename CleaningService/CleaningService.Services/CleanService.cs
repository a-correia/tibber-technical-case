using System.Diagnostics;
using AutoMapper;
using CleaningService.Domain.Model;
using CleaningService.Repositories;
using CleaningService.Repositories.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace CleaningService.Services;

public class CleanService : ICleanService
{
    private readonly ICleanRepository _cleanRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CleanService> _logger;


    public CleanService(ICleanRepository cleanRepository, IMapper mapper, ILogger<CleanService> logger)
    {
        _cleanRepository = cleanRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public CleanOutput Clean(CleanInput cleanInput)
    {
        _logger.LogInformation("Starting cleaning");
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var nPlaces = SimulateCleaning(cleanInput.Start, cleanInput.Commands);

        stopwatch.Stop();

        var duration = Math.Round(stopwatch.Elapsed.TotalSeconds, 6);
        var cleaningRecord = new CleaningRecord(cleanInput.Commands.Count, DateTime.UtcNow, nPlaces, duration);

        var record = _cleanRepository.AddCleaningRecord(cleaningRecord);
        _logger.LogInformation("Cleaning completed successfully in {Duration}", duration);

        return _mapper.Map<CleanOutput>(record);
    }

    public List<CleanOutput> GetCleanRecords()
    {
        _logger.LogInformation("Getting all cleaning records");
        return _mapper.Map<List<CleanOutput>>(_cleanRepository.GetAllCleaningRecords());
    }

    public CleanOutput GetCleanRecordById(int id)
    {
        _logger.LogInformation("Getting cleaning record by ID: {RecordId}", id);
        return _mapper.Map<CleanOutput>(_cleanRepository.GetCleaningRecordById(id));
    }

    public int SimulateCleaning(Coordinates start, List<Command> commands)
    {
        var uniquePlaces = new HashSet<Coordinates>();
        var currentPosition = start;
        uniquePlaces.Add(currentPosition);

        foreach (var command in commands)
        {
            switch (command.Direction)
            {
                case DirectionEnum.North:
                    CalculateUniquePlaces(currentPosition, command.Steps, uniquePlaces);
                    currentPosition.X += command.Steps;
                    break;
                case DirectionEnum.South:
                    CalculateUniquePlaces(currentPosition, command.Steps, uniquePlaces);
                    currentPosition.X -= command.Steps;
                    break;
                case DirectionEnum.East:
                    CalculateUniquePlaces(currentPosition, command.Steps, uniquePlaces);
                    currentPosition.Y += command.Steps;
                    break;
                case DirectionEnum.West:
                    CalculateUniquePlaces(currentPosition, command.Steps, uniquePlaces);
                    currentPosition.Y -= command.Steps;
                    break;
            }
        }

        return uniquePlaces.Count;
    }

    private static void CalculateUniquePlaces(Coordinates currentPosition, int steps, HashSet<Coordinates> uniquePlaces)
    {
        for (int i = 0; i < steps; i++)
        {
            uniquePlaces.Add(new Coordinates { X = currentPosition.X, Y = currentPosition.Y });
        }
    }
}