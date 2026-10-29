using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Bratu_Angelina_Lab2.Models;

namespace Bratu_Angelina_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Bratu_Angelina_Lab2Context _context;

    public IndexModel(Bratu_Angelina_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;
    public IList<Book>? Books { get; set; }
    public int CategoryID { get; set; }

    public async Task OnGetAsync(int? id)
    {
        Category = await _context.Category.ToListAsync();
        if (id != null)
        {
            CategoryID = id.Value;

            Books = await _context.Book
                .Include(b => b.Author)
                .Where(b => b.BookCategories.Any(
                    bc => bc.CategoryID == id.Value))
                .OrderBy(b => b.Title)
                .ToListAsync();
        }
    }
}
