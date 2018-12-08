using Microsoft.EntityFrameworkCore;
using System;
using AspCoreMVC.Models;

namespace AspCoreMVC.DesignTimeDbContextFactory
{
    public class DbContextDesignTimeDbContextFactory :
        DesignTimeDbContextFactoryBase<MovieContext>
    {
        protected override MovieContext CreateNewInstance(
            DbContextOptions<MovieContext> options)
        {
            return new MovieContext(options);
        }
    }
}
