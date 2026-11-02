using Microsoft.AspNetCore.Mvc;
using MywebAPI.Dtos;
using MywebAPI.Services;

namespace MywebAPI.Controllers;

[Route("api/projects")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _service;

    public ProjectsController(IProjectService service)
    {
        _service = service;
    }

    // GET: api/projects
    [HttpGet]
    public async Task<ActionResult<List<ProjectDto>>> GetAll()
    {
        var projects = await _service.GetAllAsync();
        return Ok(projects);
    }

    // GET: api/projects/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectDto>> GetById(int id)
    {
        var project = await _service.GetByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }

    // POST: api/projects
    [HttpPost]
    public async Task<ActionResult<ProjectDto>> Create(CreateProjectDto dto)
    {
        var created = await _service.CreateAsync(dto);
        // 201 + Location-header, dan weet de client waar het nieuwe project te vinden is
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/projects/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProjectDto dto)
    {
        var gelukt = await _service.UpdateAsync(id, dto);
        if (!gelukt)
        {
            return NotFound();
        }
        return NoContent();
    }

    // DELETE: api/projects/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var gelukt = await _service.DeleteAsync(id);
        if (!gelukt)
        {
            return NotFound();
        }
        return NoContent();
    }
}
