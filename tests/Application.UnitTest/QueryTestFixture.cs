namespace Application.UnitTest;

public class QueryTestFixture : IDisposable
{
    public ApplicationDbContext Context { get; private set; }
    public IMapper Mapper { get; private set; }

    public QueryTestFixture()
    {
        Context = ApplicationDbContextFactory.Create();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        Mapper = configurationProvider.CreateMapper();
    }

    public void Dispose()
    {
        ApplicationDbContextFactory.Destroy(Context);
    }
}

[CollectionDefinition("QueryCollection")]
public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
