using MauiAppHotel.Models;

namespace MauiAppHotel.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    private readonly App PropriedadesApp;

    private int adultos = 2;
    private int criancas = 0;

    public ContratacaoHospedagem()
    {
        InitializeComponent();

        PropriedadesApp = (App)Application.Current!;

        pck_quarto.ItemsSource = PropriedadesApp.lista_quartos;

        AtualizarHospedes();

        DateTime hoje = DateTime.Today;

        dtpck_checkin.MinimumDate = hoje;
        dtpck_checkin.MaximumDate = hoje.AddMonths(1);
        dtpck_checkin.Date = hoje;

        dtpck_checkout.MinimumDate = hoje.AddDays(1);
        dtpck_checkout.MaximumDate = hoje.AddMonths(6);
        dtpck_checkout.Date = hoje.AddDays(1);
    }

    // Atualiza os números exibidos na tela.
    private void AtualizarHospedes()
    {
        lbl_adultos.Text = adultos.ToString();
        lbl_criancas.Text = criancas.ToString();
    }

    // Controles de adultos.
    private void OnAumentarAdultos(object sender, EventArgs e)
    {
        if (adultos < 10)
            adultos++;

        AtualizarHospedes();
    }

    private void OnDiminuirAdultos(object sender, EventArgs e)
    {
        if (adultos > 0)
            adultos--;

        AtualizarHospedes();
    }

    // Controles de crianças.
    private void OnAumentarCriancas(object sender, EventArgs e)
    {
        if (criancas < 10)
            criancas++;

        AtualizarHospedes();
    }

    private void OnDiminuirCriancas(object sender, EventArgs e)
    {
        if (criancas > 0)
            criancas--;

        AtualizarHospedes();
    }

    // Atualização das datas.
    private void dtpck_checkin_DateSelected(
        object sender,
        DateChangedEventArgs e)
    {
        DateTime dataCheckin = e.NewDate ?? DateTime.Today;

        dtpck_checkout.MaximumDate = dataCheckin.AddMonths(6);
        dtpck_checkout.MinimumDate = dataCheckin.AddDays(1);

        if ((dtpck_checkout.Date ?? dataCheckin.AddDays(1)) <= dataCheckin)
        {
            dtpck_checkout.Date = dataCheckin.AddDays(1);
        }
    }

    // Navegação para a tela Sobre.
    private async void OnSobreClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Sobre());
    }

    // Cálculo e confirmação da hospedagem.
    private async void OnAvancarClicked(object sender, EventArgs e)
    {
        if (pck_quarto.SelectedItem is not Quarto quarto)
        {
            await DisplayAlertAsync(
                "Atenção",
                "Selecione uma suíte.",
                "OK"
            );
            return;
        }

        if (adultos + criancas == 0)
        {
            await DisplayAlertAsync(
                "Atenção",
                "Informe pelo menos um hóspede.",
                "OK"
            );
            return;
        }

        DateTime checkin = dtpck_checkin.Date ?? DateTime.Today;
        DateTime checkout = dtpck_checkout.Date ?? checkin.AddDays(1);

        int diarias = (checkout - checkin).Days;

        if (diarias <= 0)
        {
            await DisplayAlertAsync(
                "Atenção",
                "Selecione datas válidas.",
                "OK"
            );
            return;
        }

        double valorAdultos = adultos * quarto.ValorDiariaAdulto;
        double valorCriancas = criancas * quarto.ValorDiariaCrianca;

        double valorTotal = (valorAdultos + valorCriancas) * diarias;

        await Navigation.PushAsync(
            new HospedagemContratada(
                quarto,
                adultos,
                criancas,
                checkin,
                checkout,
                diarias,
                valorTotal
            )
        );
    }
}
