using System.ComponentModel.DataAnnotations;

namespace MywebAPI.Dtos;

// wat de client terugkrijgt
public class ProjectDto
{
    public int Id { get; set; }
    public string Titel { get; set; }
    public string Beschrijving { get; set; }
    public string Categorie { get; set; }
    public string GitHubUrl { get; set; }
    public DateTime Datum { get; set; }
}

// POST: geen Id, die maakt de database
public class CreateProjectDto
{
    [Required, StringLength(100)]
    public string Titel { get; set; }

    [Required]
    public string Beschrijving { get; set; }

    [Required, StringLength(50)]
    public string Categorie { get; set; }

    [Url]
    public string GitHubUrl { get; set; }

    public DateTime Datum { get; set; }
}

// PUT: Id staat in de url
public class UpdateProjectDto
{
    [Required, StringLength(100)]
    public string Titel { get; set; }

    [Required]
    public string Beschrijving { get; set; }

    [Required, StringLength(50)]
    public string Categorie { get; set; }

    [Url]
    public string GitHubUrl { get; set; }

    public DateTime Datum { get; set; }
}