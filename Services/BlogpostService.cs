using MywebAPI.Data;
using MywebAPI.Data.Entities;
using MywebAPI.Dtos;

namespace MywebAPI.Services;

public class BlogpostService
{
    private readonly PortfolioDbContext _db;

    public BlogpostService(PortfolioDbContext db)
    {
        _db = db;
    }

    public List<BlogpostDto> GetAll()
    {
        return _db.Blogposts.ToList().Select(ToDto).ToList();
    }

    public BlogpostDto? GetById(int id)
    {
        var blogpost = _db.Blogposts.FirstOrDefault(b => b.Id == id);
        return blogpost == null ? null : ToDto(blogpost);
    }

    public BlogpostDto Create(CreateBlogpostDto dto)
    {
        var blogpost = new Blogpost
        {
            Titel = dto.Titel,
            Inhoud = dto.Inhoud,
            Publicatiedatum = dto.Publicatiedatum
        };

        _db.Blogposts.Add(blogpost);
        _db.SaveChanges();

        // Id is nu ingevuld door de database
        return ToDto(blogpost);
    }

    public bool Update(int id, UpdateBlogpostDto dto)
    {
        var blogpost = _db.Blogposts.Find(id);
        if (blogpost == null) return false;

        blogpost.Titel = dto.Titel;
        blogpost.Inhoud = dto.Inhoud;
        blogpost.Publicatiedatum = dto.Publicatiedatum;

        // EF ziet zelf welke velden veranderd zijn
        _db.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var blogpost = _db.Blogposts.Find(id);
        if (blogpost == null) return false;

        _db.Blogposts.Remove(blogpost);
        _db.SaveChanges();
        return true;
    }

    // entity -> dto, op één plek zodat een nieuw veld maar hier bij hoeft
    private static BlogpostDto ToDto(Blogpost b) => new BlogpostDto
    {
        Id = b.Id,
        Titel = b.Titel,
        Inhoud = b.Inhoud,
        Publicatiedatum = b.Publicatiedatum
    };
}
