namespace Domain.Core.Entities;

/// <summary>
/// Base entity class
/// </summary>
/// <typeparam name="TId">type of entity id</typeparam>
public class Entity<TId> : IEntity<TId>
{
    /// <summary>
    /// Entity id
    /// </summary>
    public TId Id { get; set; } = default!;
}