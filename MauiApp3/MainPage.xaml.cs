using Microsoft.Win32;
using MySql.Data.MySqlClient;
namespace MauiApp3
{
    public partial class MainPage : ContentPage
    {
        private string connectionString = "Server=localhost;Database=ced;User ID=User_System;Password=1223";
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username=usernameEntry.Text;
            string password=passwordEntry.Text;


            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Por favor ingrese un usuario y una contraseña", "OK");
                return;
            }

            bool loginExitoso = await ValidarUsuarioAsync(username, password);

            if (loginExitoso)
            {
                await DisplayAlert("Éxito", "Login exitoso", "OK");
                // Navegar a otra página, por ejemplo:
                await Navigation.PushAsync(new Inicio());
            }
            else
            {
                await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registro());
        }



        private async Task<bool> ValidarUsuarioAsync(string usuario, string contrasena)
        {
            try
            {
                using (var conexion = new MySqlConnection(connectionString))
                {
                    await conexion.OpenAsync();

                    string query = "SELECT COUNT(*) FROM usuario WHERE nombre_usuario = @Usuario AND Contraseña = @Contrasena";
                    using (var cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                        var resultado = await cmd.ExecuteScalarAsync();
                        return Convert.ToInt32(resultado) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Error al conectar con la base de datos: " + ex.Message, "OK");
                return false;
            }
        }

    }

}
