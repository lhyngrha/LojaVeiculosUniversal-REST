using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string ip = "http://10.21.0.137/";

        List<Models.Fabricante> fabricantes;
        List<Models.Veiculo> veiculos;
        public MainPage()
        {
            fabricantes = new List<Models.Fabricante>();
            veiculos = new List<Models.Veiculo>();
            this.InitializeComponent();
            getFabricantes();
            getVeiculos();
        }

        private async void getFabricantes()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110029/api/fabricante");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Fabricante> obj = JsonConvert.DeserializeObject<List<Models.Fabricante>>(str);
            lstFabricantes.ItemsSource = obj;
        }

        private async void getVeiculos()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
  
            var response = await httpClient.GetAsync("/20131011110029/api/veiculo");
            
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Veiculo> obj2 = JsonConvert.DeserializeObject<List<Models.Veiculo>>(str);

            cmbFabricantes.ItemsSource = obj2;
            cmbFabricantes.DisplayMemberPath = "Descricao";
            cmbFabricantes.SelectedValuePath = "Id";
            veiculos = obj2;
            lstVeic.ItemsSource = obj2.ToString();

            foreach (Models.Veiculo v in obj2)
            {
                if (v.SituacaoVenda)
                    lstVeicDisp.Items.Add(v);
                else
                    lstveicVendidos.Items.Add(v);
            }
        }

        private async void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            Models.Fabricante f = new Models.Fabricante
            {
                Descricao = txtDesc.Text
            };

            string s = JsonConvert.SerializeObject(f);
            var content = new StringContent(s, Encoding.UTF8, "application/json");
            await httpClient.PostAsync("/20131011110029/api/fabricante", content);
        }

        private async void btnInserirVeic_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            bool k;
            if ((bool)checkBox.IsChecked)
                k = true;
            else
                k = false;

            Models.Veiculo v = new Models.Veiculo
            {
                Modelo = txtModelo.Text,
                Ano = txtAno.Text,
                Preco = double.Parse(txtPreco.Text),
                IdFabricante = int.Parse(cmbFabricantes.SelectedValuePath),
                SituacaoVenda = k
            };

            string s = JsonConvert.SerializeObject(v);
            var content = new StringContent(s, Encoding.UTF8, "application/json");
            await httpClient.PostAsync("/20131011110029/api/veiculo", content);
        }

        private void btnListFab_Click(object sender, RoutedEventArgs e)
        {
            getFabricantes();
        }

        private async void btnVender_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            Models.Veiculo x = (from y in veiculos where y.Id == int.Parse(txtIdVeic.Text) select y).Single();
            x.SituacaoVenda = false;

            var content = new StringContent(JsonConvert.SerializeObject(x), Encoding.UTF8, "application/json");
            await httpClient.PutAsync("/20131011110029/api/veiculo/" + x.Id, content);
            getVeiculos();

        }
    }
}
