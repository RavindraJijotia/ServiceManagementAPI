using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM.Persistence
{
    public class SMDbContextFactory : DesignTimeDbContextFactoryBase<SMDbContext>
    {
        protected override SMDbContext CreateNewInstance(DbContextOptions<SMDbContext> options)
        {
            return new SMDbContext(options);
        }
    }
}
