using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projet.net.Models;

namespace projet.net.Data
{
    public class projetnetContext : DbContext
    {
        public projetnetContext (DbContextOptions<projetnetContext> options)
            : base(options)
        {
        }

        public DbSet<Etudiant> Etudiant { get; set; } = default!;
    }
}
