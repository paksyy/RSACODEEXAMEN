using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProyectoLogin.Models;
using ProyectoLogin.Recursos;
using ProyectoLogin.Servicios.Contrato;

namespace ProyectoLogin.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DbpruebaContext _dbContext;
        public UsuarioService(DbpruebaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> GetUsuario(string correo, string clave)
        {
            //string claveDescifradaDB = Utilidades.DescifrarRSA(clave);

            List<Usuario> usuarios = await _dbContext.Usuarios
                .Where(u => u.Correo == correo)
                .ToListAsync();

            Usuario usuario_encontrado = usuarios.FirstOrDefault(u => Utilidades.DescifrarRSA(u.Clave) == clave);

            return usuario_encontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _dbContext.Usuarios.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }
    }
}