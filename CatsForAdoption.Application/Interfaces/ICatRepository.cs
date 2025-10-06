using CatsForAdoption.Domain.Entities;

namespace CatsForAdoption.Application.Interfaces;

public interface ICatRepository
{
    Task<List<Cat>> GetAllAsync(CancellationToken cancellationToken);
}
