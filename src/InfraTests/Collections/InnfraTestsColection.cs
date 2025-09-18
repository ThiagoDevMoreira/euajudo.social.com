using InfraTests.DB;

namespace InfraTests.Collections
{
    [CollectionDefinition("InfraTests")]
    public class InfraTestsCollection : ICollectionFixture<PostgresTestDatabaseFixture> {}
}
