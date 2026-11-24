using Microsoft.EntityFrameworkCore;
using Shelves.Domain;
using Shelves.Domain.Persistence;
using Shelves.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shelves.Services.BookUseCase
{
    public class BookService : IBookService
    {

        private readonly ShelvesDbContext _dbContext;
        public BookService(ShelvesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task  <ResultOf<Book>> AddBook(string title, string description, int totalCopies, decimal reservationPricePerDay, Guid? authrId)
        {
            Book book = new Book(title, description, totalCopies, reservationPricePerDay, authrId);
            if (await _dbContext.Books.FirstOrDefaultAsync(b => b.Title == title) != null)
            {
                return Error.Validation(code: "BookService.AddBook.Foundthesametitle", message: $"Book with the same title {title} already exists.");
            }

            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<ResultOf<Book>> DeleteBook(Guid id)
        {
           Book? book = _dbContext.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return Error.NotFound(code: "BookService.DeleteBook.NotFound", message: $"Book with id {id} not found.");
            }
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            return book ;
        }

        public async Task<ResultOf<List<Book>>> GetAll(string? title = null, string? description = null, int pageNum = 0, int pageSize = 12)
        {
            var query = _dbContext.Books.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(e => e.Title.Contains(title));

            if (!string.IsNullOrWhiteSpace(description))
                query = query.Where(e => e.Description.Contains(description));




            var books = await query
                        .Skip(pageNum * pageSize).Take(pageSize)
                        .ToListAsync();

            if (books.Count == 0)
                return Error.NotFound(code: "BookService.GetAll.NotFound");

            return books;

        }

        public async Task<ResultOf<Book>> GetBookById(Guid id)
        {
            var book = await _dbContext.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return Error.NotFound(code: "BookService.GetBookById.NotFound", message: $"Book with id {id} not found.");
            }
            return book;
        }

        public async Task<ResultOf<Book>> ReservingBook(Guid bookId)
        {
            Book? book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            if (book == null)
            {
                return Error.NotFound(code: "BookService.ReservingBook.InvalidInput", message: "Book ID  is not found.");
            }

            book.CurrentAvailableCopies -= 1;
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<ResultOf<Book>> ReturningBook(Guid bookId)
        {
            Book? book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            if (book == null)
            {
                return Error.NotFound(code: "BookService.ReturningBook.notfound", message: "Book ID  is not found.");
            }

            book.CurrentAvailableCopies += 1;
            await _dbContext.SaveChangesAsync();
            return book;
        }

        

        public async Task<ResultOf<Book>> UpdateBook(Guid id, string title, string description, int totalCopies, decimal reservationPricePerDay)
        {
            Book? book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return Error.NotFound(code: "BookService.UpdateBook.NotFound", message: $"Book with id {id} not found.");
            }

            book.Title = title;
            book.Description = description;
            book.TotalCopies = totalCopies;
            book.ReservationPricePerDay = reservationPricePerDay;

            await _dbContext.SaveChangesAsync();
            return book;
        }
    }
}
