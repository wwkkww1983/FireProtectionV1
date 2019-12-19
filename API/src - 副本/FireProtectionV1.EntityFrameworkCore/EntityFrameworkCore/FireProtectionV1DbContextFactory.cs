using FireProtectionV1.Configuration;
using FireProtectionV1.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FireProtectionV1.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class FireProtectionV1DbContextFactory : IDesignTimeDbContextFactory<FireProtectionV1DbContext>
    {
        public FireProtectionV1DbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FireProtectionV1DbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(FireProtectionV1Consts.ConnectionStringName)
            );

            return new FireProtectionV1DbContext(builder.Options);
        }
    }
}