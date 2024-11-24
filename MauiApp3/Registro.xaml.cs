using MySql.Data.MySqlClient;

namespace MauiApp3;

public partial class Registro : ContentPage
{
    private string connectionString = "Server=localhost;Database=ced;User ID=User_System;Password=1223";
    public Registro()
    {
        InitializeComponent();
    }
    private async void OnRegistrarClicked(object sender, EventArgs e)
    {
        string username = usernameEntry.Text;
        string password = passwordEntry.Text;
        string email = emailEntry.Text;
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
        {
            await DisplayAlert("Error", "Debe llenar todos los espacios", "Ok");
            return;
        }
        bool registroExitoso = await ValidarRegistroAsync(username, password, email);
        if (registroExitoso)
        {
            username = string.Empty;
            password = string.Empty;
            email = string.Empty;
            await DisplayAlert("Exito", "Registro exitoso", "Aceptar");
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contraseña ya esta en uso", "Ok");
        }
    }
    private async Task<bool> ValidarRegistroAsync(string usuario, string contraseña, string correo)
    {
        try
        {
            using (var conexion = new MySqlConnection(connectionString))
            {
                await conexion.OpenAsync();
                string query = "INSERT INTO usuario(nombre_usuario,correo,contraseña) VALUES(@usuario,@correo,@contraseña)";
                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@contraseña", contraseña);

                    var resultado = await cmd.ExecuteNonQueryAsync();
                    return resultado > 0;
                }
            }
        }
        catch (System.Exception ex)
        {
            await DisplayAlert("Error", "Error al intentar conectarse a la base de datos."+ ex.Message, "OK");
            return false;
        }
    }
}