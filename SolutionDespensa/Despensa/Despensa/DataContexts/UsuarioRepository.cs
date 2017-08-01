using Despensa.Models;
using SQLite;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Despensa.DataContexts
{
    public class UsuarioRepository
    {
        SQLiteAsyncConnection _Connection;

        public UsuarioRepository()
        {
            _Connection = DependencyService.Get<IDataContext>().GetConnection();
        }

        public async void CriarTabelas()
        {
            await _Connection.CreateTableAsync<Usuario>();
        }

        public async void CadastrarUsuarioAsync(Usuario usuario)
        {
            await _Connection.InsertAsync(usuario);
        }

        public async Task<Usuario> RecuperarUsuarioPorEmailAsync(string email)
        {
            var usuarios = await _Connection.Table<Usuario>().ToListAsync();

            var usuario = usuarios.Find(x => x.Email == email);

            return usuario;
        }

        public async Task<Usuario> ValidarLoginAsync(string email, string senha)
        {
            var usuarios = await _Connection.Table<Usuario>().ToListAsync();

            var usuario = usuarios.Find(x => x.Email == email && x.Senha == senha);

            return usuario;
        }

        public async Task<Usuario> AtualizarUsuarioAsync(Usuario usuario)
        {
            int retorno = await _Connection.UpdateAsync(usuario);

            if (retorno > 0)
                return usuario;

            return null;
        }
    }
}