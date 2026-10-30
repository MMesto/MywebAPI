using MywebAPI.Dtos;

namespace MywebAPI.Services;

public interface IProjectService
{
    Task<List<ProjectDto>> GetAllAsync();
    Task<ProjectDto?> GetByIdAsync(int id);                 // null = niet gevonden
    Task<ProjectDto> CreateAsync(CreateProjectDto dto);
    Task<bool> UpdateAsync(int id, UpdateProjectDto dto);   // false = niet gevonden
    Task<bool> DeleteAsync(int id);   // false = niet gevonden
}