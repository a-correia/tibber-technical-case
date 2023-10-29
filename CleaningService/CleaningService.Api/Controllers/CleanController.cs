using AutoMapper;
using CleaningService.Api.Requests;
using CleaningService.Domain.Model;
using CleaningService.Services;

namespace CleaningService.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using System;

[Route("api/v1/tibber-developer-test")]
[ApiController]
public class CleanController : ControllerBase
{
    private readonly ICleanService _cleanService;
    private readonly IMapper _mapper;
    private readonly ILogger<CleanService> _logger;

    public CleanController(ICleanService cleanService, IMapper mapper, ILogger<CleanService> logger)
    {
        _cleanService = cleanService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost("enter-path")]
    public async Task<IActionResult> EnterPath([FromBody] CleanRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var cleanOutput = await _cleanService.Clean(_mapper.Map<CleanInput>(requestDto));
            return Ok(_mapper.Map<CleanResponseDto>(cleanOutput));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "enter-path");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("clean-record/{recordId}")]
    public async Task<IActionResult> GetCleanRecord(int recordId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var cleanOutput = await _cleanService.GetCleanRecordById(recordId);
            if (cleanOutput != null)
            {
                return Ok(_mapper.Map<CleanResponseDto>(cleanOutput));
            }

            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"clean-record/{recordId}");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("clean-records")]
    public async Task<ObjectResult> GetAllCleanRecords()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var cleanOutputs = await _cleanService.GetCleanRecords();
            return Ok(_mapper.Map<List<CleanResponseDto>>(cleanOutputs));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "clean-records");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}