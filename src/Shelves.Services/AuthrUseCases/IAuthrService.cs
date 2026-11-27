using Shelves.Domain;
using Shelves.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shelves.Services.AuthrUseCases
{
    public interface IAuthrService
    {

        Task <ResultOf<Authr>> AddAuthr(string name, DateTime? birthDate, string? bio);
        Task <ResultOf<List<Authr>>> GetAll(string? name, int pageNum = 0, int pageSize = 12);
        Task<ResultOf<Authr>> GetAuthrById(Guid id);
        Task<ResultOf<Authr>> DeleteAuthr(Guid id);
        Task<ResultOf<Authr>> UpdateAuthr(Guid id, string name, DateTime? birthDate, string? bio);
        Task <ResultOf<List<Book>>> GetBooksByAuthrId(Guid authrId, int pageNum = 0, int pageSize = 12);
    }
}
