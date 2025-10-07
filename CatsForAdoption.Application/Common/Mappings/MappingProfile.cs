using AutoMapper;
using CatsForAdoption.Application.Features.Cats.Dtos;
using CatsForAdoption.Domain.Entities;

namespace CatsForAdoption.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Cat, CatDto>();
    }
}
