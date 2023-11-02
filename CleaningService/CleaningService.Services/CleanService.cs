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

    public async Task<CleanOutput> Clean(CleanInput cleanInput)
    {
        _logger.LogInformation("Starting cleaning");
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var nPlaces = SimulateCleaning(cleanInput.Start, cleanInput.Commands);

        stopwatch.Stop();

        var duration = Math.Round(stopwatch.Elapsed.TotalSeconds, 6);
        var cleaningRecord = new CleaningRecord(cleanInput.Commands.Count, DateTime.UtcNow, nPlaces, duration);

        var record = await _cleanRepository.AddCleaningRecord(cleaningRecord);
        _logger.LogInformation("Cleaning completed successfully in {Duration}", duration);

        return _mapper.Map<CleanOutput>(record);
    }

    public async Task<List<CleanOutput>> GetCleanRecords()
    {
        _logger.LogInformation("Getting all cleaning records");
        var cleanOutputs = await _cleanRepository.GetAllCleaningRecords();
        return _mapper.Map<List<CleanOutput>>(cleanOutputs);
    }

    public async Task<CleanOutput> GetCleanRecordById(int id)
    {
        _logger.LogInformation("Getting cleaning record by ID: {RecordId}", id);
        var cleaningRecordById = await _cleanRepository.GetCleaningRecordById(id);
        return _mapper.Map<CleanOutput>(cleaningRecordById);
    }

    public int SimulateCleaning(Coordinates start, List<Command> commands)
    {
        var uniquePlaces = new HashSet<Coordinates>();
        uniquePlaces.Add(new Coordinates { X = start.X, Y = start.Y });
        
        var currentPosition = start;
        _logger.LogInformation(String.Join("\n", uniquePlaces));

        foreach (var command in commands)
        {
            for (int i = 0; i < command.Steps; i++)
            {
                switch (command.Direction)
                {
                    case DirectionEnum.North:
                        currentPosition.Y += 1;
                        break;
                    case DirectionEnum.South:
                        currentPosition.Y -= 1;
                        break;
                    case DirectionEnum.East:
                        currentPosition.X += 1;
                        break;
                    case DirectionEnum.West:
                        currentPosition.X -= 1;
                        break;
                }
                uniquePlaces.Add(new Coordinates { X = currentPosition.X, Y = currentPosition.Y });
            }
        }

        _logger.LogInformation(String.Join("\n", uniquePlaces));
        return uniquePlaces.Count;
    }
}