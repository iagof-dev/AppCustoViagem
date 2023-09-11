using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCustoViagem
{
    public partial class MainPage : ContentPage
    {
        private App PropriedadesApp;
        public MainPage()
        {
            InitializeComponent();

            PropriedadesApp = (App)Application.Current;

        }

        private async void btnPedagio_Clicked(object sender, EventArgs e)
        {
            try
            {
                int qnt_pedagios = PropriedadesApp.ListaPedagios.Count;
                PropriedadesApp.ListaPedagios.Add(new Model.Pedagio
                {
                    NroPedagio = qnt_pedagios + 1,
                    Valor = (int)Convert.ToDecimal(etyValorP.Text)
                });
                await DisplayAlert("Adicionado!", "Veja na lista de pedagios", "Ok");

                etyValorP.Text = string.Empty;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", "ocorreu um erro: " + ex.Message, "Ok");
            }
        }

        private async void btnListaPedagio_Clicked(object sender, EventArgs e)
        {

            try
            {
                await Navigation.PushAsync(new ListaPedagios());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro:" + ex.Message, "Ok");

            }
        }

        private async void btnCalcular_Clicked(object sender, EventArgs e)
        {
            try
            {
                decimal valor_total_pedagios = PropriedadesApp.ListaPedagios.Sum(item => item.Valor);

                if (string.IsNullOrEmpty(etyDistancia.Text))
                    throw new Exception("Por favor, preencha a distância.");
                if (string.IsNullOrEmpty(etyConsumo.Text))
                    throw new Exception("Por favor, preencha o consumo do veiculo.");
                if (string.IsNullOrEmpty(etyValorC.Text))
                    throw new Exception("Por favor, preencha o valor do combustivel.");

                decimal consumo = Convert.ToDecimal(etyConsumo.Text);
                decimal preco_combustivel = Convert.ToDecimal(etyValorC.Text);
                decimal distancia = Convert.ToDecimal(etyDistancia.Text);

                decimal consumo_carro = (distancia/consumo) * preco_combustivel;

                decimal custo_total = consumo_carro + valor_total_pedagios;

                string origem = etyOrigem.Text;
                string destino = etyDestino.Text;

                string mensagem = String.Format("Viagem de {0} a {1} custará {2}", origem, destino, custo_total.ToString("C"));

                await DisplayAlert("Resultado Final", mensagem, "Ok");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro:" + ex.Message, "Ok");

            }
        }

        private async void btnLimpar_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool confirmar = await DisplayAlert("Tem certeza?", "Limpar todos os dados?", "Sim", "Não");
                if(confirmar)
                {
                    etyConsumo.Text = string.Empty;
                    etyDestino.Text = string.Empty;
                    etyDistancia.Text = string.Empty;
                    etyOrigem.Text = string.Empty;
                    etyValorC.Text = string.Empty;
                    etyValorP.Text = string.Empty;

                    PropriedadesApp.ListaPedagios.Clear();
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Ops", "Ocorreu um erro:" + ex.Message, "Ok");
            }
        }
    }
}
