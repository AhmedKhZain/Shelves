using Shelves.Domain;
using Shelves.Domain.Persistence;
using Shelves.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Shelves.Services.AuthrUseCases
{
    public class AuthrService : IAuthrService
    {
        private readonly ShelvesDbContext _context;
        public AuthrService(ShelvesDbContext context)
        {
            _context = context;
        }

        public async Task<ResultOf<Author>> AddAuthr(string name, DateTime? birthDate, string? bio)
        {
            Author authr = new Author(name, birthDate, bio);
            if (await _context.Authors.AnyAsync(a => a.Name == name && a.BirthDate == birthDate))
            {
                return Error.Validation("Authr already exists");
            }
            _context.Authors.Add(authr);
            await _context.SaveChangesAsync();
            return authr;
        }

        public async Task<ResultOf<Author>> DeleteAuthr(Guid id)
        {
            var authr = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (authr == null)
            {
                return Error.NotFound("Authr not found");
            }
            _context.Authors.Remove(authr);
            await _context.SaveChangesAsync();
            return authr;
        }

        public async Task<ResultOf<List<Author>>> GetAll(string? name, int pageNum = 0, int pageSize = 12)
        {
            var query = _context.Authors.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name.Contains(name));
            }
            
            var authrs = await query.Skip(pageNum * pageSize).Take(pageSize).ToListAsync();
            return authrs;
        }

        public async Task<ResultOf<Author>> GetAuthrById(Guid id)
        {
            var authr = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (authr == null)
            {
                return Error.NotFound("Authr not found");
            }
            return authr;
        }
        

        public async Task<ResultOf<List<Book>>> GetBooksByAuthrId(Guid authrId, int pageNum = 0, int pageSize = 12)
        {
            var books = await _context.Books.Where(b => b.AuthrId == authrId)
                .Skip(pageNum * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return books;
        }

        public async Task<ResultOf<Author>> UpdateAuthr(Guid id, string name, DateTime? birthDate, string? bio)
        {
            var authr = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (authr == null)
            {
                return Error.NotFound("Authr not found");
            }
            authr.Name = name;
            authr.BirthDate = birthDate;
            authr.Bio = bio;
            await _context.SaveChangesAsync();
            return authr;
        }
    }
}
