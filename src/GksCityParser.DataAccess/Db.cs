using GksCityParser.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GksCityParser.DataAccess
{
    public class Db : DbContext  
    {
        public Db() : base("Db")
        {
        }

        public DbSet<City> Cities { get; set; }
    }
}
