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
        // d1: nieuwe blogpost maken van de dto
        var blogpost = new Blogpost
        {
            Titel = dto.Titel,
            Inhoud = dto.Inhoud,
            Publicatiedatum = dto.Publicatiedatum
        };

        // d2: opslaan, daarna is het Id ingevuld
        _db.Blogposts.Add(blogpost);
        await _db.SaveChangesAsync();

        // d3: teruggeven met het nieuwe Id erin
        return ToDto(blogpost);
    }

    public async Task<bool> UpdateAsync(int id, UpdateBlogpostDto dto)
    {
        // e1: ophalen met tracking aan, anders slaat SaveChanges niks op
        var blogpost = await _db.Blogposts.FindAsync(id);
        if (blogpost == null) return false;

        // e2: nieuwe waarden zetten
        blogpost.Titel = dto.Titel;
        blogpost.Inhoud = dto.Inhoud;
        blogpost.Publicatiedatum = dto.Publicatiedatum;

        // e3: wijzigingen wegschrijven
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        // f1: bestaat de blogpost wel?
        var blogpost = await _db.Blogposts.FindAsync(id);
        if (blogpost == null) return false;

        // f2: weghalen en opslaan
        _db.Blogposts.Remove(blogpost);
        await _db.SaveChangesAsync();
        return true;
    }

    private static BlogpostDto ToDto(Blogpost b) => new BlogpostDto
    {
        Id = b.Id,
        Titel = b.Titel,
        Inhoud = b.Inhoud,
        Publicatiedatum = b.Publicatiedatum
    };
}