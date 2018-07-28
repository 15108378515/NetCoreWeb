using Microsoft.EntityFrameworkCore;
using Model;
using System;

namespace DAL
{
    public class DbBase: DbContext
    {

        public DbBase(DbContextOptions<DbBase> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<ImageModel> Images { get; set; }

    }
}
