using System.ComponentModel.DataAnnotations;

namespace PosterCMS.Models;

public class UserModel
{
    [Key]
    [EmailAddress]
    [Required]
    public required String Email { get; set; }

    [Required]
    public required byte[] PasswordHash { get; set; }

    [Required]
    public required byte[] PasswordSalt { get; set; }

    [MaxLength(30)]
    public string? FirstName { get; set; }

    [MaxLength(30)]
    public string? LastName { get; set; }

    [MaxLength(50)]
    public string? Socials { get; set; }
}
