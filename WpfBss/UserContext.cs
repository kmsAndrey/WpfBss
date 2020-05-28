using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBss.Model;

namespace WpfBss
{
    class UserContext : DbContext
    {
        public UserContext()
            : base("DbConnection")
        { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
