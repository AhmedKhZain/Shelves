using Shelves.Domain;
using Shelves.Domain.Shared;

namespace Shelves.Services.ClientuseCases;

public interface IClientServce
{
    Task<ResultOf<Client>> AddClient(string name, string nationalID, string? email = null, string? phoneNumber = null);
    Task<ResultOf<List<Client>>> GetAll(string? name, string? email, string? phone, int pageNum = 0, int pageSize = 12);
    Task<ResultOf<Client>> GetClientById(Guid id);
    Task<ResultOf<Client>> DeleteClient(Guid id);
}
