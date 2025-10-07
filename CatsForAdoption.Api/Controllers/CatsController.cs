using CatsForAdoption.Application.Features.Cats.Dtos;
using CatsForAdoption.Application.Features.Cats.Queries.GetAllCats;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatsForAdoption.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatsController : Controller
{
    private readonly IMediator _mediator;

    public CatsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CatDto>>> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllCatsQuery();
        var cats = await _mediator.Send(query, cancellationToken);

        return Ok(cats);
    }
}
