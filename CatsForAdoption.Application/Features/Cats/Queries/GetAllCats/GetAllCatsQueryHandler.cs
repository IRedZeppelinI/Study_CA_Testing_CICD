using AutoMapper;
using CatsForAdoption.Application.Features.Cats.Dtos;
using CatsForAdoption.Application.Interfaces;
using MediatR;

namespace CatsForAdoption.Application.Features.Cats.Queries.GetAllCats;

public class GetAllCatsQueryHandler : IRequestHandler<GetAllCatsQuery, List<CatDto>>
{
    private readonly ICatRepository _catRepository;
    private readonly IMapper _mapper;

    public GetAllCatsQueryHandler(ICatRepository catRepository, IMapper mapper)
    {
        _catRepository = catRepository;
        _mapper = mapper;
    }

    public async Task<List<CatDto>> Handle(GetAllCatsQuery request, CancellationToken cancellationToken)
    {
        var cats = await _catRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<CatDto>>(cats);
    }
}
