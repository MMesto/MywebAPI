using MywebAPI.Data;
using MywebAPI.Data.Entities;
using MywebAPI.Dtos;

namespace MywebAPI.Services;

public class ProjectService
{
    private readonly PortfolioDbContext _db;

    public ProjectService(PortfolioDbContext db)
    {
        _db = db;
    }

    public List<ProjectDto> GetAll()
    {
        return _db.Projects.ToList().Select(ToDto).ToList();
    }

    public ProjectDto? GetById(int id)
    {
        var project = _db.Projects.FirstOrDefault(p => p.Id == id);
        return project == null ? null : ToDto(project);
    }

    public ProjectDto Create(CreateProjectDto dto)
    {
        var project = new Project
        {
            Titel = dto.Titel,
            Beschrijving = dto.Beschrijving,
            Categorie = dto.Categorie,
            GitHubUrl = dto.GitHubUrl,
            Datum = dto.Datum
        };

        _db.Projects.Add(project);
        _db.SaveChanges();

        // Id is nu ingevuld door de database
        return ToDto(project);
    }

    public bool Update(int id, UpdateProjectDto dto)
    {
        var project = _db.Projects.Find(id);
        if (project == null) return false;

        project.Titel = dto.Titel;
        project.Beschrijving = dto.Beschrijving;
        project.Categorie = dto.Categorie;
        project.GitHubUrl = dto.GitHubUrl;
        project.Datum = dto.Datum;

        // EF ziet zelf welke velden veranderd zijn
        _db.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var project = _db.Projects.Find(id);
        if (project == null) return false;

        _db.Projects.Remove(project);
        _db.SaveChanges();
        return true;
    }

    // entity -> dto, op één plek zodat een nieuw veld maar hier bij hoeft
    private static ProjectDto ToDto(Project p) => new ProjectDto
    {
        Id = p.Id,
        Titel = p.Titel,
        Beschrijving = p.Beschrijving,
        Categorie = p.Categorie,
        GitHubUrl = p.GitHubUrl,
        Datum = p.Datum
    };
}
