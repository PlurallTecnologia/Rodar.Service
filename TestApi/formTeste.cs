using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using TestApi.Models;

namespace TestApi
{
    public partial class formTeste : Form
    {
        public formTeste()
        {
            InitializeComponent();
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");
                Login lgn = new Login { Email = "teste@teste.com.br", Senha = "12345" };
                var response = client.PostAsJsonAsync("api/Login/DoLogin", lgn).Result;

                if (response.IsSuccessStatusCode)
                {
                    var a = response.Content.ReadAsStringAsync();

                    if (a.Result.ToString().Trim() == "0")
                    {
                        MessageBox.Show("Usuário ou Senha incorretas");
                    }
                    else
                    {
                        MessageBox.Show("Login realizado com sucesso");
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao logar");
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");
                Usuario usuario = new Usuario { nomeCompleto = "Teste Usuario", Email = "testeusuario@gmail.com", Senha = "abc" };
                var response = client.PostAsJsonAsync("api/Usuario/Cadastrar", usuario).Result;

                if (response.IsSuccessStatusCode)
                {
                    var a = response.Content.ReadAsStringAsync();

                    if (a.Result.ToString().Trim() == "0")
                    {
                        MessageBox.Show("Erro. Usuário Não Cadastrado");
                    }
                    else
                    {
                        MessageBox.Show("Usuário cadastrado com sucesso");
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar");
                }
            }

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");
                Usuario usuario = new Usuario { nomeCompleto = "Ana Paula", Email = "anapaulaludke@gmail.com", Senha = "123" };

                var response = client.PostAsJsonAsync("api/Usuario/Atualizar", usuario).Result;

                if (response.IsSuccessStatusCode)
                    MessageBox.Show("Atualizado");
                else
                    MessageBox.Show("Erro ao atualizar");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");

                var response = client.GetAsync("api/Usuario/Buscar/1").Result;

                if (response.IsSuccessStatusCode)
                {
                    var retorno = response.Content.ReadAsStringAsync().Result;//.TrimStart('\"').TrimEnd('\"').Replace("\\", "");

                    var usuarioEncontrado = JsonConvert.DeserializeObject<Usuario>(retorno, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    //List<AccountInfo> lc = JsonConvert.DeserializeObject<List<AccountInfo>>(json, 
                }

            }
        }
    }
}
