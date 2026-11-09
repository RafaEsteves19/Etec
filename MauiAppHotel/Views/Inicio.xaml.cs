
namespace MauiAppHotel.Views;

public partial class Inicio : ContentPage
{
    public Inicio()
    {
        InitializeComponent();
    }

    private async void OnIniciarClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ContratacaoHospedagem());
    }
}
