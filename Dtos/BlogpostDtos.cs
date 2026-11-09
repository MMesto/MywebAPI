using System.ComponentModel.DataAnnotations;

namespace MywebAPI.Dtos;

// wat de client terugkrijgt
public class BlogpostDto
{
    public int Id { get; set; }
    public string Titel { get; set; }
    public string Inhoud { get; set; }
    public DateTime Publicatiedatum { get; set; }
}

// POST: geen Id, die maakt de database
public class CreateBlogpostDto
{
    [Required, StringLength(150)]
    public string Titel { get; set; }

    [Required]
    public string Inhoud { get; set; }

    public DateTime Publicatiedatum { get; set; }
}

// PUT: Id staat in de url
public class UpdateBlogpostDto
{
    [Required, StringLength(150)]
    public string Titel { get; set; }

    [Required]
    public string Inhoud { get; set; }

    public DateTime Publicatiedatum { get; set; }
}