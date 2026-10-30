using Microsoft.EntityFrameworkCore;
using MywebAPI.Data;
using MywebAPI.Data.Entities;
using MywebAPI.Dtos;

namespace MywebAPI.Services;

public class BlogpostService : IBlogpostService
{
    private readonly PortfolioDbContext _db;

    public BlogpostService(PortfolioDbContext db)
    {
        _db = db;
    }

    public async Task<List<BlogpostDto>> GetAllAsync()
    {
        var blogposts = await _db.Blogposts.AsNoTracking().ToListAsync();
        return blogposts.Select(ToDto).ToList();
    }

    public async Task<BlogpostDto?> GetByIdAsync(int id)
    {
        var blogpost = await _db.Blogposts.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        return blogpost == null ? null : ToDto(blogpost);
    }

    public async Task<BlogpostDto> CreateAsync(CreateBlogpostDto dto)
    {
        var blogpost = new Blogpost
        {
            Titel = dto.Titel,
            Inhoud = dto.Inhoud,
            Publicatiedatum = dto.Publicatiedatum
        };

        _db.Blogposts.Add(blogpost);
        await _db.SaveChangesAsync();
        return ToDto(blogpost);
    }

    public async Task<bool> UpdateAsync(int id, UpdateBlogpostDto dto)
    {
        var blogpost = await _db.Blogposts.FindAsync(id);
        if (blogpost == null) return false;

        blogpost.Titel = dto.Titel;
        blogpost.Inhoud = dto.Inhoud;
        blogpost.Publicatiedatum = dto.Publicatiedatum;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var blogpost = await _db.Blogposts.FindAsync(id);
        if (blogpost == null) return false;

        _db.Blogposts.Remove(blogpost);
        await _db.SaveChangesAsync();
        return true;
    }

    // ===== DTO-MAPPING =====
    private static BlogpostDto ToDto(Blogpost b) => new BlogpostDto
    {
        Id = b.Id,
        Titel = b.Titel,
        Inhoud = b.Inhoud,
        Publicatiedatum = b.Publicatiedatum
    };
}