using Microsoft.EntityFrameworkCore;
using PhoneBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Repository
{
    public class PhoneBookDbContext : DbContext
    {
        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
        {

        }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<PhoneBook.Core.Models.PhoneBook> PhoneBooks { get; set; }
    }
}
