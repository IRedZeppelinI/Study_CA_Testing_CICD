using CatsForAdoption.Application.Features.Cats.Dtos;
using MediatR;

namespace CatsForAdoption.Application.Features.Cats.Queries.GetAllCats;

public class GetAllCatsQuery : IRequest<List<CatDto>>
{
    
}
