using System.Collections.ObjectModel;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
namespace MauiApp3;

public partial class Educacion_Mexico : ContentPage
{
    private string connectionString = "Server=localhost;Database=ced;User ID=User_System;Password=1223";
    private ObservableCollection<Estados> estados { get; set; } = new ObservableCollection<Estados>();
    public Educacion_Mexico()
    {
        InitializeComponent();
        BindingContext= this;
    }
    private async void OnAddClicked(object sender, EventArgs e)
    {
        string nomEstado=estadoEntry.Text;
        string nivAcademico=estadoEntry.Text;
        if(string.IsNullOrEmpty(nomEstado)||string.IsNullOrEmpty(nivAcademico))
        {
            await DisplayAlert("Error", "Llene todos los espacios", "ok");
            return;
        }
        bool Rprop = await ValidarRegistroAsync(nomEstado,nivAcademico);
        if (Rprop) 
        {
            Estados nuevar = new Estados(nomEstado,nivAcademico);
        }

    }
    private async Task<bool> ValidarRegistroAsync(string state, string academy)
    {
        try
        {
            using (var conexion = new MySqlConnection(connectionString))
            {
                await conexion.OpenAsync();
                string query = "SELECT COUNT(*) FROM poblacion_aprobada WHERE id_estado = @Usuario AND id_nivel_academico = @Contrasena";
                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@usuario", state);
                    cmd.Parameters.AddWithValue("@correo", academy);

                    using( var resultado = await cmd.ExecuteReaderAsync())
                        if ( await resultado.ReadAsync())
                        {
                            int count = resultado.GetInt32(0);
                            return count > 0;
                        }
                    return false;
                }
            }
        }
        catch (System.Exception ex)
        {
            await DisplayAlert("Error", "Error al intentar conectarse a la base de datos." + ex.Message, "OK");
            return false;
        }
    }
}