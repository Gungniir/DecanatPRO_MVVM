using System.Data.Entity;
using ModelLayer.Models;

namespace ModelLayer.Repositories.Entity
{
    public class EntityContext : DbContext
    {
        public EntityContext() : base("name=EntityContextDatabase") { }
        public DbSet<Student> Students { get; set; }
    }
}