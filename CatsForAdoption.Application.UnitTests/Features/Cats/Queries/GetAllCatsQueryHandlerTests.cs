using CatsForAdoption.Application.Common.Mappings;
using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using CatsForAdoption.Application.Interfaces;
using CatsForAdoption.Domain.Entities;
using CatsForAdoption.Application.Features.Cats.Queries.GetAllCats;
using CatsForAdoption.Application.Features.Cats.Dtos;

namespace CatsForAdoption.Application.UnitTests.Features.Cats.Queries;

public class GetAllCatsQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<ICatRepository> _mockCatRepository;

    public GetAllCatsQueryHandlerTests()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        }, NullLoggerFactory.Instance);

        _mapper = mapperConfig.CreateMapper();
        _mockCatRepository = new Mock<ICatRepository>();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_WhenCatsExist_MustReturnListOfCatDTO()
    {
        var catsFromDb = new List<Cat>
        {
            new Cat(name: "", breed: "TesteBreed", birthDate: new DateOnly(2020, 10, 05), adoptionCenterId: 1),
            new Cat(name: "TesteCat2", breed: "TesteBreed", birthDate: new DateOnly(2010, 11, 22), adoptionCenterId: 1),
            new Cat(name: "TesteCat3", breed: "TesteBreed", birthDate: new DateOnly(2018, 08, 11), adoptionCenterId: 2)            
        };

        _mockCatRepository.Setup(repo => repo
            .GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(catsFromDb);

        var handler = new GetAllCatsQueryHandler(_mockCatRepository.Object, _mapper);


        var result = await handler.Handle(new GetAllCatsQuery(), new CancellationToken());


        Assert.NotNull(result);
        Assert.IsType<List<CatDto>>(result);
        Assert.Equal(3, result.Count);

        var secondCatDto = result[1];
        Assert.Equal("TesteCat2", secondCatDto.Name);

        _mockCatRepository.Verify(repo => repo
            .GetAllAsync(It.IsAny<CancellationToken>()),
            Times.Once());
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_WhenCats_NOT_Exist_MustReturnEmptyListOfCatDTO()
    {
        var catsFromDb = new List<Cat>();
        

        _mockCatRepository.Setup(repo => repo
            .GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(catsFromDb);

        var handler = new GetAllCatsQueryHandler(_mockCatRepository.Object, _mapper);


        var result = await handler.Handle(new GetAllCatsQuery(), new CancellationToken());


        Assert.NotNull(result);
        Assert.IsType<List<CatDto>>(result);
        Assert.Empty(result);
                

        _mockCatRepository.Verify(repo => repo
            .GetAllAsync(It.IsAny<CancellationToken>()),
            Times.Once());
    }
}
