using System.ComponentModel.DataAnnotations;

namespace PosterCMS.Models;

public class PosterModel
{
    public int ID { get; set;}

    [MaxLength(20)]
    public string? Title { get; set; }
    
    public string? Author { get; set; }
    
    public DateTime? Date { get; set; }
    
    public string? Sub1 { get; set; }
    
    public string? Text1 { get; set; } // Max 870
    
    public string? Sub2 { get; set; }
    
    public string? Text2 { get; set; } // Max 560
    
    public string? Sub3 { get; set; }
    
    public string? Text3 { get; set;} // Max 560
}
