using Microsoft.EntityFrameworkCore;
using Phonebook.Api.Entities;
using System;

namespace Phonebook.Api.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Person> Persons => Set<Person>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(b =>
        {
            b.ToTable("persons");
            b.HasKey(x => x.Id);

           
            b.HasIndex(x => x.PhoneNumber).IsUnique();

          
            b.HasQueryFilter(x => x.IsActive);

            b.Property(x => x.Name).IsRequired().HasMaxLength(100);
            b.Property(x => x.Surname).IsRequired().HasMaxLength(100);
            b.Property(x => x.Email).HasMaxLength(200);
            b.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(30);
        });
    }
}
