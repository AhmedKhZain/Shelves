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
        // Check for duplicate National ID
        if (await _context.Clients.AnyAsync(c => c.NationalID == nationalID))
        {
            return Error.Validation(code: "ClientService.AddClient.DuplicateNationalID", message: $"A client with National ID {nationalID} already exists.");
        }

        // Check for duplicate email and is not null or whitespace

        if (!string.IsNullOrWhiteSpace(email) && await _context.Clients.AnyAsync(c => c.Email == email))
        {
            return Error.Validation(code: "ClientService.AddClient.DuplicateEmail", message: $"A client with Email {email} already exists.");
        }

        // Check for duplicate phone number and is not null or whitespace

        if (!string.IsNullOrWhiteSpace(phoneNumber) && await _context.Clients.AnyAsync(c => c.PhoneNumber == phoneNumber))
        {
            return Error.Validation(code: "ClientService.AddClient.DuplicatePhoneNumber", message: $"A client with Phone Number {phoneNumber} already exists.");
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

    public async Task<ResultOf<Client>> GetClientById(Guid id)
    {
        Client? client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        if (client == null)
        {
            return Error.NotFound(code: "ClientNotFound", message: $"Client with ID {id} not found.");
        }
        return client;
    }


    public async Task<ResultOf<Client>> DeleteClient(Guid id)
    {
        var clientResult = await GetClientById(id);
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
