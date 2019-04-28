using Microsoft.EntityFrameworkCore;

namespace FireProtectionV1.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<FireProtectionV1DbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for FireProtectionV1DbContext */
            dbContextOptions.UseMySql(connectionString);
        }
    }
}
