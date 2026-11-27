using Shelves.Domain;
using Shelves.Domain.Persistence;
using Shelves.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Shelves.Services.ReservationUseCase
{
    public class ReservationService : IReservationService
    {
        private readonly ShelvesDbContext _context;
        public ReservationService(ShelvesDbContext context)
        {
            _context = context;
        }

        public async Task<ResultOf<Reservation>> AddReservation(Guid clientId, Guid bookId, DateTime endDate)
        {
            Reservation reservation = new Reservation(bookId, clientId, endDate);
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            if (book.CurrentAvailableCopies <= 0)
            {
                return Error.Failure("Book is not available for reservation.");
            }

            if (reservation.StartDate >= reservation.EndDate)
            {
                return Error.Failure("End date must be in the future.");
            }

            if (book == null || !await _context.Clients.AnyAsync(c => c.Id == clientId))
            {
                return Error.Failure("Book or Client is not available.");
            }
            book.CurrentAvailableCopies--;
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<ResultOf<List<Reservation>>> GetAllReservations(int pageNum = 0, int pageSize = 12)
        {
            var reservations = await _context.Reservations.OrderBy(r => r.StartDate).Skip(pageNum * pageSize).Take(pageSize).ToListAsync();
            return reservations;
        }

        public async Task<ResultOf<List<Reservation>>> GetAllReservationsByBookId(Guid bookId, int pageNum = 0, int pageSize = 12)
        {
            var reservations = await _context.Reservations.Where(r => r.BookId == bookId).OrderBy(r => r.StartDate).Skip(pageNum * pageSize).Take(pageSize).ToListAsync();
           return reservations;
        }

        public async Task<ResultOf<Reservation>> GetReservationById(Guid id)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);
            if (reservation == null)
            {
                return Error.NotFound("Reservation not found");
            }
            return reservation;
        }

        public async Task<ResultOf<List<Reservation>>> GetReservationsByClientId(Guid clientId, int pageNum = 0, int pageSize = 12)
        {
            var reservations = await _context.Reservations.Where(r => r.ClientId == clientId).OrderBy(r => r.StartDate).Skip(pageNum * pageSize).Take(pageSize).ToListAsync();
            return reservations;
        }

        public async Task<ResultOf<Reservation>> ReturnReservation(Guid id)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                return Error.NotFound("Reservation not found");
            }

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == reservation.BookId);

            _context.Reservations.Remove(reservation);
            book.CurrentAvailableCopies++;
            await _context.SaveChangesAsync();
            return reservation;
        }
    }
}
