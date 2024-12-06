using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Db: DbContext
    {
        public DbSet<User>Users { get; set; }
        public DbSet<Role>Roles { get; set; }
        public DbSet<Category>Categorys { get; set; }
        public DbSet<Borrow>Borrows { get; set; }
        public DbSet<Book> Books { get; set; }

        public Db(DbContextOptions options) : base(options) { }

        public static implicit operator DBNull(Db v)
        {
            throw new NotImplementedException();
        }
    }
}
