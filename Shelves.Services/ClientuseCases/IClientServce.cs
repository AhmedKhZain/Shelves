using Shelves.Domain;
using Shelves.Domain.Shared;

namespace Shelves.Services.ClientuseCases;

public interface IClientServce
{
    public Task<ResultOf<Client>> AddClient(string name, string nationalID, string? email = null, string? phoneNumber = null);
}
