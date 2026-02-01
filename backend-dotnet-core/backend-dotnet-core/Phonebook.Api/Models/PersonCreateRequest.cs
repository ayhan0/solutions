using System.ComponentModel.DataAnnotations;

namespace Phonebook.Api.Models;

public class PersonCreateRequest
{
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
}
