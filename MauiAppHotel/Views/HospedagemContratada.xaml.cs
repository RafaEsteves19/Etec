
using MauiAppHotel.Models;

namespace MauiAppHotel.Views;

public partial class HospedagemContratada : ContentPage
{
    public HospedagemContratada(
        Quarto quarto,
        int adultos,
        int criancas,
        DateTime checkin,
        DateTime checkout,
        int diarias,
        double valorTotal)
    {
        InitializeComponent();

        lbl_quarto.Text = quarto.Descricao;
        lbl_adultos.Text = $"Adultos: {adultos}";
        lbl_criancas.Text = $"Crianças: {criancas}";
        lbl_checkin.Text = $"Check-in: {checkin:dd/MM/yyyy}";
        lbl_checkout.Text = $"Check-out: {checkout:dd/MM/yyyy}";
        lbl_diarias.Text = $"Diárias: {diarias}";
        lbl_total.Text = valorTotal.ToString("C2");
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
