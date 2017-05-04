using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab9_10.Models;


namespace Lab9_10.Models
{
    public class HumanContext : DbContext
    {
        public DbSet<Human> Humans { get; set; }
        public HumanContext(DbContextOptions<HumanContext> options)
            : base(options)
        {
        }
    }
}


