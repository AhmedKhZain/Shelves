namespace Shelves.Domain;

public class Author : BaseEntity
{
    public ICollection<Book> Books { get; set; } = new List<Book>();
    public string Name { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string? Bio { get; set; } = string.Empty;



    public Author(
        string name,
        DateTime? birthDate,
        string? bio)
        : base()
    {
        Name = name;
        BirthDate = birthDate;
        Bio = bio;
    }



}
