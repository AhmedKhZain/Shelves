using Shelves.Domain;
using Shelves.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shelves.Services.ReservationUseCase;

public interface IReservationService
{
    Task <ResultOf<Reservation>> AddReservation(Guid clientId, Guid bookId, DateTime endDate);
    Task<ResultOf<List<Reservation>>> GetAllReservations(int pageNum = 0, int pageSize = 12);
    Task<ResultOf<List<Reservation>>> GetReservationsByClientId(Guid clientId, int pageNum = 0, int pageSize = 12);
    Task<ResultOf<List<Reservation>>> GetAllReservationsByBookId(Guid bookId, int pageNum = 0, int pageSize = 12);
    Task<ResultOf<Reservation>> GetReservationById(Guid id);
    Task<ResultOf<Reservation>> ReturnReservation(Guid id);




}
