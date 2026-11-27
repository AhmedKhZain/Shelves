using System.ComponentModel.DataAnnotations;

namespace Shelves.API.Requests;

public record ClientAddRequest([MaxLength(200)] string Name, [MaxLength(14)] string NationalID, [MaxLength(260)] string? Email, [MaxLength(20)] string? PhoneNumber);
