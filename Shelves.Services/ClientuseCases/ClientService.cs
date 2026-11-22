using Microsoft.EntityFrameworkCore;
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

    public async Task<ResultOf<List<Client>>> GetAll(
        string? name,
        string? email,
        string? phone,
        int pageNum = 0,
        int pageSize = 12)
    {
        var query = _context.Clients.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(e => e.Name.Contains(name));

        if (!string.IsNullOrWhiteSpace(email))
            query = query.Where(e => e.Email.Contains(email));

        if (!string.IsNullOrWhiteSpace(phone))
            query = query.Where(e => e.PhoneNumber.Contains(phone));




        var clients = await query
                    .Skip(pageNum * pageSize).Take(pageSize)
                    .ToListAsync();

        if (clients.Count == 0)
            return Error.NotFound(code: "ClientService.GetAll.NotFound");

        return clients;

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


    public async Task<ResultOf<Client>> DeleteClient(string nationalID)
    {
        var clientResult = await GetClientById(nationalID);
        if (clientResult.IsError)
        {
            return clientResult.Errors;
        }
        var client = clientResult.Value!;
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return client;
    }

}
