using Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Infrastructure.Data.Configuration;

/// <summary>
/// Configure database table for <see cref="ExampleEntity"/>
/// </summary>
public class ExampleEntityConfig : IEntityTypeConfiguration<ExampleEntity>
{
    public void Configure(EntityTypeBuilder<ExampleEntity> builder)
    {
        builder.ToTable("example_entities");
        builder.HasKey(it => it.Id);
    }
}