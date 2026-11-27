using Shelves.Domain;
using Shelves.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shelves.Services.AuthrUseCases
{
    public interface IAuthrService
    {

        Task <ResultOf<Author>> AddAuthr(string name, DateTime? birthDate, string? bio);
        Task <ResultOf<List<Author>>> GetAll(string? name, int pageNum = 0, int pageSize = 12);
        Task<ResultOf<Author>> GetAuthrById(Guid id);
        Task<ResultOf<Author>> DeleteAuthr(Guid id);
        Task<ResultOf<Author>> UpdateAuthr(Guid id, string name, DateTime? birthDate, string? bio);
        Task <ResultOf<List<Book>>> GetBooksByAuthrId(Guid authrId, int pageNum = 0, int pageSize = 12);
    }
}
