using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Bratu_Angelina_Lab2.Models;

namespace Bratu_Angelina_Lab2.Pages.BorrowingPages;

public class CreateModel : PageModel
{
    private readonly Bratu_Angelina_Lab2Context _context;

    public CreateModel(Bratu_Angelina_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        var bookList = _context.Book
            .Include(b => b.Author)
            .Select(x => new
            {
                x.ID,
                BookFullName = x.Title + " - " + x.Author.LastName + " " + x.Author.FirstName
            });

        ViewData["BookID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
            bookList, "ID", "BookFullName");

        ViewData["MemberID"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
            _context.Member, "ID", "FullName");

        return Page();
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Borrowing.Add(Borrowing);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
