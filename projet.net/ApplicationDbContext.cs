using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using projet.net.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace projet.net.Models
{
    [Authorize]
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Etudiant> Etudiants { get; set; }
        public virtual DbSet<Notes> Notes { get; set; }
        public virtual DbSet<Module> Modules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Notes>()
                .HasOne(n => n.Etudiant)
                .WithMany(e => e.Notes)
                .HasForeignKey(n => n.EtudiantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notes>()
                .HasOne(n => n.Module)
                .WithMany() 
                .HasForeignKey(n => n.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}