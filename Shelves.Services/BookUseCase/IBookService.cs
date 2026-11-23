using Shelves.Domain;
using Shelves.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shelves.Services.BookUseCase
{
    public interface IBookService
    {
        Task<ResultOf<Book>> AddBook(string title, string description, int totalCopies, decimal reservationPricePerDay, Guid? authrId);
        Task<ResultOf<List<Book>>> GetAll(string? title, string? description, int pageNum = 0, int pageSize = 12);
        Task<ResultOf<Book>> GetBookById(Guid id);
        Task<ResultOf<Book>> DeleteBook(Guid id);
        Task<ResultOf<Book>> UpdateBook(Guid id, string title, string description, int totalCopies, decimal reservationPricePerDay);
        Task<ResultOf<Book>> ReservingBook(Guid bookId);
        Task<ResultOf<Book>> ReturningBook(Guid bookId);
    }
    
}
