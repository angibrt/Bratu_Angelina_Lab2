using Microsoft.EntityFrameworkCore;

public class Bratu_Angelina_Lab2Context(DbContextOptions<Bratu_Angelina_Lab2Context> options) : DbContext(options)
{
    public DbSet<Bratu_Angelina_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Bratu_Angelina_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Bratu_Angelina_Lab2.Models.Authors> Authors { get; set; } = default!;
}
