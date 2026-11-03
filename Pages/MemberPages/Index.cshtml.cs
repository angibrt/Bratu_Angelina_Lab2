using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Bratu_Angelina_Lab2.Models;

namespace Bratu_Angelina_Lab2.Pages.MemberPages;

public class IndexModel : PageModel
{
    private readonly Bratu_Angelina_Lab2Context _context;

    public IndexModel(Bratu_Angelina_Lab2Context context)
    {
        _context = context;
    }

    public IList<Member> Member { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Member = await _context.Member.ToListAsync();
    }
}
