using System.ComponentModel.DataAnnotations;

namespace MywebAPI.Dtos;

public class ProjectDto
{
    public int Id { get; set; }
    public string Titel { get; set; }
    public string Beschrijving { get; set; }
    public string Categorie { get; set; }
    public string GitHubUrl { get; set; }
    public DateTime Datum { get; set; }
}

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