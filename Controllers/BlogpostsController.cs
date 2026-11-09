using Microsoft.AspNetCore.Mvc;
using MywebAPI.Dtos;
using MywebAPI.Services;

namespace MywebAPI.Controllers;

[Route("api/blogposts")]
[ApiController]
public class BlogpostsController : ControllerBase
{
    private readonly BlogpostService _service;

    public BlogpostsController(BlogpostService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<BlogpostDto>> GetAll()
    {
        return _service.GetAll();
    }

    [HttpGet("{id:int}")]
    public ActionResult<BlogpostDto> GetById(int id)
    {
        var blogpost = _service.GetById(id);
        if (blogpost == null)
        {
            return NotFound();
        }
        return blogpost;
    }

    [HttpPost]
    public ActionResult<BlogpostDto> Create(CreateBlogpostDto dto)
    {
        var created = _service.Create(dto);
        // 201 + Location-header naar het nieuwe item
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, UpdateBlogpostDto dto)
    {
        var gelukt = _service.Update(id, dto);
        if (!gelukt)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var gelukt = _service.Delete(id);
        if (!gelukt)
        {
            return NotFound();
        }
        return NoContent();
    }
}
