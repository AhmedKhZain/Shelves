namespace Shelves.Domain;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    public BaseEntity()
    {
        Id = Guid.CreateVersion7();
    }

}
