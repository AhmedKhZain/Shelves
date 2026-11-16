using Shelves.Domain;
using Shelves.Domain.Persistence;
using Shelves.Domain.Shared;

namespace Shelves.Services.ClientuseCases;

public class ClientService : IClientServce
{
    private readonly ShelvesDbContext _context;

    public ClientService(ShelvesDbContext context)
    {
        _context = context;
    }

    public Task<ResultOf<Client>> AddClient(string name, string nationalID, string? email = null, string? phoneNumber = null)
    {



    }
}
