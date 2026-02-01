using System.ComponentModel.DataAnnotations;

namespace Phonebook.Api.Entities;

public class Person
{
    public long Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Surname { get; set; } = null!;

    [Range(0, 150)]
    public int Age { get; set; }

    [EmailAddress, MaxLength(200)]
    public string? Email { get; set; }

    [Required, MaxLength(30)]
    public string PhoneNumber { get; set; } = null!;

    // soft delete
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }
}
