namespace Shelves.Domain;

public class Reservation : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Guid BookId { get; set; }
    public Book Book { get; set; } = null!;
    public Guid ClientId { get; set; }
    public Client Client { get; set; } = null!;



    public Reservation(
        Guid bookId,
        Guid clientId,
        DateTime endDate)
        : base()
    {
        BookId = bookId;
        ClientId = clientId;
        StartDate = DateTime.UtcNow;
        EndDate = endDate;
    }
}
