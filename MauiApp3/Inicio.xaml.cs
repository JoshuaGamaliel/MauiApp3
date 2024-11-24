namespace MauiApp3;

public partial class Inicio : ContentPage
{
	public Inicio()
	{
		InitializeComponent();
	}
	private async void OnPropClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Propuesta());
    }
	private async void OnEduMexClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Educacion_Mexico());
	}
}