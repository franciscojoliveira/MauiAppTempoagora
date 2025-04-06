using MauiAppTempoagora.Models;
using MauiAppTempoagora.Services;

namespace MauiAppTempoagora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
       
        public MainPage()
        {
            InitializeComponent();
        }

       

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {

                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat}\n" +
                                         $"Longitude: {t.lon}\n" +
                                         $"Nascer do Sol: {t.sunrise}\n" +
                                         $"Pôr do Sol: {t.sunset}\n" +
                                         $"Temperatura Máxima: {t.temp_max}°C\n" +
                                         $"Temperatura Mínima: {t.temp_min}°C\n" +
                                         $"Velocidade do Vento: {t.speed} m/s\n" +
                                         $"Visibilidade: {t.visibility} metros\n" +
                                         $"Descrição: {t.description}\n";

                        lbl_res.Text = dados_previsao;


                    } else
                    {
                        lbl_res.Text = "Sem dados de Previsão";
                    }
                }
                else
                {
                    lbl_res.Text = "Prencha a cidade.";
                    
                }

            } catch(Exception ex) 
            {
                await DisplayAlert("Ops", ex.Message, "Ok");
            }
        }
    }

}
