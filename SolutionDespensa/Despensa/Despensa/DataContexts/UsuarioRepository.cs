using Android.Runtime;
using Despensa.Models;
using SQLite.Net;
using Xamarin.Forms;

namespace Despensa.DataContexts
{
    [Preserve(AllMembers = true)]
    public class UsuarioRepository
    {
        SQLiteConnection _Connection;

        public UsuarioRepository()
        {
            _Connection = DependencyService.Get<IDataContext>().GetConnection();
        }

        public  void CadastrarUsuario(Usuario usuario)
        {
             _Connection.Insert(usuario);
        }

        public Usuario RecuperarUsuarioPorEmail(string email)
        {
            var usuario = _Connection.Table<Usuario>().Where(x => x.Email == email).FirstOrDefault();

            return usuario;
        }

        public Usuario ValidarLogin(string email, string senha)
        {
            var usuario = _Connection.Table<Usuario>().Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();

            return usuario;
        }

        public Usuario AtualizarUsuario(Usuario usuario)
        {
            int retorno =  _Connection.Update(usuario);

            if (retorno > 0)
                return usuario;

            return null;
        }
    }
}