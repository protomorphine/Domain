namespace Domain.Core.Entities;

/// <summary>
/// Base entity interface
/// </summary>
/// <typeparam name="TId">type of entity id</typeparam>
public interface IEntity<TId>
{
    /// <summary>
    /// Entity id
    /// </summary>
    public TId Id { get; set; }
}