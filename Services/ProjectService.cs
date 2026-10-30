using Microsoft.EntityFrameworkCore;
using MywebAPI.Data;
using MywebAPI.Data.Entities;
using MywebAPI.Dtos;

namespace MywebAPI.Services;

public class ProjectService : IProjectService
{
    private readonly PortfolioDbContext _db;

    // Constructorinjectie: ASP.NET Core geeft de DbContext mee
    public ProjectService(PortfolioDbContext db)
    {
        _db = db;
    }

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
        var project = new Project
        {
            Titel = dto.Titel,
            Beschrijving = dto.Beschrijving,
            Categorie = dto.Categorie,
            GitHubUrl = dto.GitHubUrl,
            Datum = dto.Datum
        };

        _db.Projects.Add(project);
        await _db.SaveChangesAsync();   // INSERT in SQL Server, Id wordt ingevuld
        return ToDto(project);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProjectDto dto)
    {
        var project = await _db.Projects.FindAsync(id);
        if (project == null) return false;

        project.Titel = dto.Titel;
        project.Beschrijving = dto.Beschrijving;
        project.Categorie = dto.Categorie;
        project.GitHubUrl = dto.GitHubUrl;
        project.Datum = dto.Datum;

        await _db.SaveChangesAsync();   // UPDATE
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await _db.Projects.FindAsync(id);
        if (project == null) return false;

        _db.Projects.Remove(project);
        await _db.SaveChangesAsync();   // DELETE
        return true;
    }

    // ===== DTO-MAPPING: entiteit naar DTO, op één vaste plek =====
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