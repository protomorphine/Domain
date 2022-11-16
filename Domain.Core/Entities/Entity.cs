namespace Domain.Core.Entities;

public class Entity<TId> : IEntity<TId>
{
    public TId Id { get; set; }
}