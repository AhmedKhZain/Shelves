using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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

    public async Task<ResultOf<Client>> AddClient(string name, string nationalID, string? email = null, string? phoneNumber = null)
    {
        
        var clientcreationresult = Client.MakeNewClient(name, nationalID, email, phoneNumber);
        if (clientcreationresult.IsError)
        {
            return clientcreationresult.Errors;
        }
        _context.Clients.Add(clientcreationresult.Value!);
        await _context.SaveChangesAsync();
        return clientcreationresult;
    }

    public async Task<ResultOf<Client>> GetClientById(string nationalID)
    {
        Client? client = await _context.Clients.FirstOrDefaultAsync(c => c.NationalID == nationalID);
        if (client == null)
        {
            return Error.NotFound(code: "ClientNotFound", message: $"Client with National ID {nationalID} not found.");
        }
        return client;
    }


}
