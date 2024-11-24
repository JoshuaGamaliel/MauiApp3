using MauiApp3;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
namespace MauiApp3;
public partial class Propuesta : ContentPage
{
    private string connectionString = "Server=localhost;Database=ced;User ID=User_System;Password=1223";
    private ObservableCollection <RPropuesta> rPropuestas = new ObservableCollection <RPropuesta>();
    private RPropuesta selectpropuesta;
    public Propuesta()
    {
        InitializeComponent();
        ListProp.ItemSelected += onPropuestaSelected;
    }
    private async void OnAddClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(nameEntry.Text) || string.IsNullOrEmpty(detailEntry.Text))
        {
            await DisplayAlert("Error", "LLene todos los espacios", "OK");
            return;
        }
        bool Rprop = await ValidarRegistroAsync(nameEntry.Text, detailEntry.Text);

        if (Rprop)
        {
            RPropuesta nuevapropuesta = new RPropuesta(nameEntry.Text,detailEntry.Text,stateEntry.Text);
            rPropuestas.Add(nuevapropuesta);

            nameEntry.Text = string.Empty;
            detailEntry.Text = string.Empty;
            stateEntry.Text = string.Empty;

            ListProp.ItemsSource = null;
            ListProp.ItemsSource = rPropuestas;
        }
    }
    private void onPropuestaSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectpropuesta = (RPropuesta)e.SelectedItem;
        if (selectpropuesta != null) 
        {
            nameEntry.Text = string.Empty;
            detailEntry.Text= string.Empty;
            stateEntry.Text= string.Empty;
        }
    }
    private async Task<bool> ValidarRegistroAsync(string usuario, string correo)
    {
        try
        {
            using (var conexion = new MySqlConnection(connectionString))
            {
                await conexion.OpenAsync();
                string query = "INSERT INTO propuestas(nombre_propuesta,detalles_propuesta) VALUES(@usuario,@correo)";
                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@correo", correo);

                    var resultado = await cmd.ExecuteNonQueryAsync();
                    return resultado > 0;
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