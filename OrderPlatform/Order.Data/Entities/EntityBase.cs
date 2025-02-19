namespace Order.Data.Entities;

public class EntityBase
{
    public Guid Id { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}