namespace Domain.Core.Entities;

public class ExampleEntity : Entity<long>
{
    public string Name { get; set; }
    public string Content { get; set; }
}