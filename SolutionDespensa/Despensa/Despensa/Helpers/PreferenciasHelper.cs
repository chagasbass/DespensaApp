using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Despensa.Helpers
{
    namespace Despensa.Helpers
    {
        public static class PreferenciasHelper
        {
            private static ISettings AppSettings
            {
                get
                {
                    return CrossSettings.Current;
                }
            }

            #region Setting Constants

            private const string DadosDeLogin = "Login";
            private const string DadosDeSenha = "Senha";
            private const string NomeUsuario = "NomeUsuario";
            private static readonly string ValorPadrao = string.Empty;

            #endregion

            public static string GravarLogin
            {
                get
                {
                    return AppSettings.GetValueOrDefault(DadosDeLogin, ValorPadrao);
                }
                set
                {
                    AppSettings.AddOrUpdateValue(DadosDeLogin, value);
                }
            }

            public static string GravarSenha
            {
                get
                {
                    return AppSettings.GetValueOrDefault(DadosDeSenha, ValorPadrao);
                }
                set
                {
                    AppSettings.AddOrUpdateValue(DadosDeSenha, value);
                }
            }

            public static string GravarNomeUsuario
            {
                get
                {
                    return AppSettings.GetValueOrDefault(ValorPadrao, ValorPadrao);
                }
                set
                {
                    AppSettings.AddOrUpdateValue(ValorPadrao, value);
                }
            }
        }
    }
}