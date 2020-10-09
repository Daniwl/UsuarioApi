using Microsoft.EntityFrameworkCore;

namespace ApiUsuarios.Models
{
    public class UsuarioDBContext : DbContext
    {
        public UsuarioDBContext(DbContextOptions<UsuarioDBContext> options) : base(options)
        { 

        }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}