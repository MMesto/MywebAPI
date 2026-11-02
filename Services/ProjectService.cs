using Microsoft.EntityFrameworkCore;
using MywebAPI.Data;
using MywebAPI.Data.Entities;
using MywebAPI.Dtos;

namespace MywebAPI.Services;

public class ProjectService : IProjectService
{
    private readonly PortfolioDbContext _db;

    public ProjectService(PortfolioDbContext db)
    {
        _db = db;
    }

    // AsNoTracking omdat we hier alleen lezen, scheelt EF wat werk
    public async Task<List<ProjectDto>> GetAllAsync()
    {
        var projects = await _db.Projects.AsNoTracking().ToListAsync();
        return projects.Select(ToDto).ToList();
    }

    public async Task<ProjectDto?> GetByIdAsync(int id)
    {
        var project = await _db.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        return project == null ? null : ToDto(project);
    }

    public async Task<ProjectDto> CreateAsync(CreateProjectDto dto)
    {
        // a1: dto omzetten naar een entity (Id laat de database zelf maken)
        var project = new Project
        {
            Titel = dto.Titel,
            Beschrijving = dto.Beschrijving,
            Categorie = dto.Categorie,
            GitHubUrl = dto.GitHubUrl,
            Datum = dto.Datum
        };

        // a2: opslaan, na SaveChanges heeft project.Id een waarde
        _db.Projects.Add(project);
        await _db.SaveChangesAsync();

        // a3: terugsturen als dto, inclusief het nieuwe Id
        return ToDto(project);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProjectDto dto)
    {
        // b1: bestaand project ophalen, zonder AsNoTracking want EF moet de wijzigingen zien
        var project = await _db.Projects.FindAsync(id);
        if (project == null) return false;

        // b2: velden overschrijven
        project.Titel = dto.Titel;
        project.Beschrijving = dto.Beschrijving;
        project.Categorie = dto.Categorie;
        project.GitHubUrl = dto.GitHubUrl;
        project.Datum = dto.Datum;

        // b3: EF ziet zelf welke velden veranderd zijn en maakt de UPDATE
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        // c1: eerst kijken of het project bestaat
        var project = await _db.Projects.FindAsync(id);
        if (project == null) return false;

        // c2: verwijderen en opslaan
        _db.Projects.Remove(project);
        await _db.SaveChangesAsync();
        return true;
    }

    // mapping op één plek, zodat een nieuw veld maar hier toegevoegd hoeft te worden
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