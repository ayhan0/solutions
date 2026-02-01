using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phonebook.Api.Entities;
using Phonebook.Api.Infrastructure;
using Phonebook.Api.Models;

namespace Phonebook.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EntityController : ControllerBase
{
    private readonly AppDbContext _db;

    public EntityController(AppDbContext db)
    {
        _db = db;
    }

    // POST /api/entity/add
    [HttpPost("add")]
    public async Task<ActionResult<PersonResponse>> Add([FromBody] PersonCreateRequest req, CancellationToken ct)
    {

        if (req == null) return BadRequest(new { message = "Body is required." });

        if (string.IsNullOrWhiteSpace(req.Name))
            return BadRequest(new { message = "Name is required." });

        if (string.IsNullOrWhiteSpace(req.Surname))
            return BadRequest(new { message = "Surname is required." });

        if (req.Age <= 0)
            return BadRequest(new { message = "Age must be greater than 0." });
        // persons controller ile aynı validation mantığı
        var phone = req.PhoneNumber?.Trim();
        if (string.IsNullOrWhiteSpace(phone))
            return BadRequest(new { message = "PhoneNumber is required." });

        var exists = await _db.Persons.AnyAsync(x => x.PhoneNumber == phone, ct);
        if (exists) return Conflict(new { message = "PhoneNumber already exists." });

        var entity = new Person
        {
            Name = req.Name.Trim(),
            Surname = req.Surname.Trim(),
            Age = req.Age,
            Email = string.IsNullOrWhiteSpace(req.Email) ? null : req.Email.Trim(),
            PhoneNumber = phone,
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow,
            UpdatedAtUtc = null
        };

        _db.Persons.Add(entity);
        await _db.SaveChangesAsync(ct);

        var res = new PersonResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Surname = entity.Surname,
            Age = entity.Age,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            CreatedAtUtc = entity.CreatedAtUtc,
            UpdatedAtUtc = entity.UpdatedAtUtc
        };

        return StatusCode(StatusCodes.Status201Created, res);
    }

    // DELETE /api/entity/delete/{name}
    // Not: name benzersiz değil. Burada "name" ile eşleşen TÜM aktif kayıtları soft-delete yapıyorum.
    [HttpDelete("delete/{name}")]
    public async Task<IActionResult> DeleteByName([FromRoute] string name, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest(new { message = "Name is required." });

        var term = name.Trim();

        var matches = await _db.Persons
            .Where(x => x.IsActive && EF.Functions.ILike(x.Name, term))
            .ToListAsync(ct);

        if (matches.Count == 0) return NotFound();

        var now = DateTime.UtcNow;
        foreach (var p in matches)
        {
            p.IsActive = false;
            p.UpdatedAtUtc = now;
        }

        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    // GET /api/entity/list
    [HttpGet("list")]
    public async Task<ActionResult<List<PersonResponse>>> List(CancellationToken ct)
    {
        var list = await _db.Persons.AsNoTracking()
            .OrderBy(x => x.Name).ThenBy(x => x.Surname)
            .Select(x => new PersonResponse
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Age = x.Age,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                CreatedAtUtc = x.CreatedAtUtc,
                UpdatedAtUtc = x.UpdatedAtUtc
            })
            .ToListAsync(ct);

        return Ok(list);
    }

    // GET /api/entity/search/{name}
    // Not: requirement "by name" diyor; kullanıcı deneyimi için contains (case-insensitive) yaptım.
    [HttpGet("search/{name}")]
    public async Task<ActionResult<List<PersonResponse>>> SearchByName([FromRoute] string name, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest(new { message = "Name is required." });


        var term = name.Trim();

        var list = await _db.Persons.AsNoTracking()
            .Where(x => EF.Functions.ILike(x.Name, $"%{term}%"))
            .OrderBy(x => x.Name).ThenBy(x => x.Surname)
            .Select(x => new PersonResponse
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Age = x.Age,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                CreatedAtUtc = x.CreatedAtUtc,
                UpdatedAtUtc = x.UpdatedAtUtc
            })
            .ToListAsync(ct);

        return Ok(list);
    }
}
