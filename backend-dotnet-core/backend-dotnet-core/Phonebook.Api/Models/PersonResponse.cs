namespace Phonebook.Api.Models;

public class PersonResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public int Age { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
}
