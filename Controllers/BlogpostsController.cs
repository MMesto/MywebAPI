using Microsoft.AspNetCore.Mvc;
using MywebAPI.Dtos;
using MywebAPI.Services;

namespace MywebAPI.Controllers;

[Route("api/blogposts")]
[ApiController]
public class BlogpostsController : ControllerBase
{
    private readonly IBlogpostService _service;

    // Constructorinjectie: ASP.NET Core geeft de service mee
    public BlogpostsController(IBlogpostService service)
    {
        _service = service;
    }

    // GET: api/blogposts
    [HttpGet]
    public async Task<ActionResult<List<BlogpostDto>>> GetAll()
    {
        var blogposts = await _service.GetAllAsync();
        return Ok(blogposts);
    }

    // GET: api/blogposts/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BlogpostDto>> GetById(int id)
    {
        var blogpost = await _service.GetByIdAsync(id);
        if (blogpost == null)
        {
            return NotFound();
        }
        return Ok(blogpost);
    }

    // POST: api/blogposts
    [HttpPost]
    public async Task<ActionResult<BlogpostDto>> Create(CreateBlogpostDto dto)
    {
        var created = await _service.CreateAsync(dto);
        // 201 Created + Location-header naar GET api/blogposts/{id}
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/blogposts/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateBlogpostDto dto)
    {
        var gelukt = await _service.UpdateAsync(id, dto);
        if (!gelukt)
        {
            return NotFound();
        }
        return NoContent();
    }

    // DELETE: api/blogposts/5
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
