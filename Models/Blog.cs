using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Models;

public class Blog {
    public int Id {get; set;}

    [Required]
    [StringLength(100)]
    public string Title {get; set;} = string.Empty;

    [Required]
    [StringLength(5000)]
    public string Content {get; set;} = string.Empty;

    [Required]
    public string Author {get; set;} = string.Empty;

    [DataType(DataType.Date)]
    public DateTime PublishedDate {get; set;}

    [DataType(DataType.Date)]
    public DateTime LastUpdatedDate {get; set;}
}