using System.ComponentModel.DataAnnotations;

namespace PosterCMS.Models;

public class PosterModel
{
    public int ID { get; set; }

    [MaxLength(20)]
    public string? Title { get; set; }

    [MaxLength(30)]
    public string? Author { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }

    [Required]
    public DateTime EditDate { get; set; }

    [MaxLength(30)]
    public string? Sub1 { get; set; }

    [MaxLength(870)]
    public string? Text1 { get; set; }

    [MaxLength(20)]
    public string? Sub2 { get; set; }

    [MaxLength(560)]
    public string? Text2 { get; set; }

    [MaxLength(20)]
    public string? Sub3 { get; set; }

    [MaxLength(560)]
    public string? Text3 { get; set; }

    [MaxLength(200)]
    public string? ImageUrl1 { get; set; }

    [MaxLength(200)]
    public string? ImageUrl2 { get; set; }

    [MaxLength(200)]
    public string? ImageUrl3 { get; set; }
}
