namespace Application.UnitTest.Common;
public class CommandTestBase : IDisposable
{
    protected readonly ApplicationDbContext _context;

    public CommandTestBase()
    {
        _context = ApplicationDbContextFactory.Create();
    }

    public void Dispose()
    {
        ApplicationDbContextFactory.Destroy(_context);
    }
}