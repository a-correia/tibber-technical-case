using CleaningService.Repositories.Model;

namespace CleaningService.Api.Requests;

using AutoMapper;
using Domain = CleaningService.Domain.Model;

public class CleaningProfile : Profile
{
    public CleaningProfile()
    {
        CreateMap<CleanRequestDto, Domain.CleanInput>();
        CreateMap<CommandDto, Domain.Command>();
        CreateMap<CoordinatesDto, Domain.Coordinates>();
        CreateMap<DirectionEnumDto, Domain.DirectionEnum>();
        CreateMap<CleaningRecord, Domain.CleanOutput>();
        CreateMap<Domain.CleanOutput,Domain.CleanResponseDto>();
    }
}