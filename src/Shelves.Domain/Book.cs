namespace Shelves.Domain;

public class Book : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int CurrentAvailableCopies { get; set; } = 0;

    public int TotalCopies { get; set; } = 0;

    public decimal ReservationPricePerDay { get; set; } = 0;

    public Guid? AuthrId { get; set; }
    public Author? Authr { get; set; } = null;

    public Book(
        string title,
        string description,
        int totalCopies,
        decimal reservationPricePerDay,
        Guid? authrId = null
        )
        : base()
    {
        AuthrId = authrId;
        Title = title;
        Description = description;
        CurrentAvailableCopies = totalCopies;
        TotalCopies = totalCopies;
        ReservationPricePerDay = reservationPricePerDay;
    }
}
