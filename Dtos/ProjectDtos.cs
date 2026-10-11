using System.ComponentModel.DataAnnotations;

namespace MywebAPI.Dtos;

// UITVOER: wat de client terugkrijgt bij GET
public class ProjectDto
{
    public int Id { get; set; }
    public string Titel { get; set; } = string.Empty;
    public string Beschrijving { get; set; } = string.Empty;
    public string Categorie { get; set; } = string.Empty;
    public string GitHubUrl { get; set; } = string.Empty;
    public DateTime Datum { get; set; }
}

// INVOER bij POST: geen Id, want dat kiest de database
public class CreateProjectDto
{
    [Required, StringLength(100)]
    public string Titel { get; set; } = string.Empty;

    [Required]
    public string Beschrijving { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Categorie { get; set; } = string.Empty;

    [Url]
    public string GitHubUrl { get; set; } = string.Empty;

    public DateTime Datum { get; set; }
}

// INVOER bij PUT: geen Id, want dat staat in de URL
public class UpdateProjectDto
{
    [Required, StringLength(100)]
    public string Titel { get; set; } = string.Empty;

    [Required]
    public string Beschrijving { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Categorie { get; set; } = string.Empty;

    [Url]
    public string GitHubUrl { get; set; } = string.Empty;

    public DateTime Datum { get; set; }
}