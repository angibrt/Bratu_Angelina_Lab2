using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Bratu_Angelina_Lab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bratu_Angelina_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Bratu_Angelina_Lab2Context _context;

    public IndexModel(Bratu_Angelina_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;
    public BookData BookD { get; set; }
    public int BookID { get; set; }
    public int CategoryID { get; set; }
    [BindProperty(SupportsGet = true)]
    public int? AuthorID { get; set; }

    public SelectList AuthorList { get; set; } = default!;

    public async Task OnGetAsync(int? id, int? categoryID)
    {
        BookD = new BookData();
        AuthorList = new SelectList(await _context.Authors
            .Select(a => new
            {
                a.ID,
                FullName = a.FirstName + " " + a.LastName
            })
            .ToListAsync(), "ID", "FullName", AuthorID);

        var books = _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
            .AsNoTracking()
            .AsQueryable();
        if (AuthorID.HasValue)
        {
            books = books.Where(b => b.AuthorID == AuthorID.Value);
        }
        BookD.Books = await books
    .OrderBy(b => b.Title)
    .ToListAsync();

        Book = BookD.Books.ToList();

        if (id != null)
        {
            BookID = id.Value;

            Book book = BookD.Books
                .Where(i => i.ID == id.Value).Single();

            BookD.Categories = book.BookCategories.Select(s => s.Category);
        }
    }
}
