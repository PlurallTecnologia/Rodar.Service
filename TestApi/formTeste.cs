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
using System.IO;

namespace TestApi
{
    public partial class formTeste : Form
    {
        public Token Authentication;

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
                Usuario usuario = new Usuario { Nome = "Jonatan", Sobrenome = "Costa", Email = "testeusuario@gmail.com", Senha = "abc" };
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
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Authentication.access_token);

                Usuario usuario = new Usuario { Nome = "Nome", Sobrenome = "Sobrenome", Email = "1@1.com.br", Senha = "123" };

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
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Authentication.access_token);

                var response = client.GetAsync("api/Usuario/Buscar").Result;

                if (response.IsSuccessStatusCode)
                {
                    var retorno = response.Content.ReadAsStringAsync().Result;

                    var usuarioEncontrado = JsonConvert.DeserializeObject<Usuario>(retorno, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    MessageBox.Show(retorno);
                }
            }
        }

        private void btnAutenticar_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");

                List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
                
                pairs.Add(new KeyValuePair<string, string>("username", "1@1.com.br"));
                //pairs.Add(new KeyValuePair<string, string>("username", "123456789"));
                pairs.Add(new KeyValuePair<string, string>("password", "1"));
                pairs.Add(new KeyValuePair<string, string>("grant_type", "password"));

                FormUrlEncodedContent content = new FormUrlEncodedContent(pairs);

                //var response = client.PostAsync("api/login?facebookLogin", content).Result;
                var response = client.PostAsync("api/login", content).Result;
                //var response = client.PostAsync("api/ExternalLogin", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var retorno = response.Content.ReadAsStringAsync().Result;

                    Authentication = JsonConvert.DeserializeObject<Token>(retorno, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    if (!string.IsNullOrWhiteSpace(Authentication.access_token))
                    {
                        MessageBox.Show("Acesso permitido");
                    }
                    else
                    {
                        MessageBox.Show("Acesso negado");
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao requisitar acesso");
                }
            }

        }

        private void btnCadastrarEvento_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Authentication.access_token);

                Evento evento = new Evento
                {
                    descricaoEvento = "Evento Teste 1",
                    enderecoBairro = "Bairro 1",
                    enderecoCEP = "95041-020",
                    enderecoCidade = "Caxias do Sul",
                    enderecoComplemento = "Complemento 1",
                    enderecoNumero = 123,
                    enderecoRua = "Rua Teste",
                    enderecoUF = "RS",
                    urlImagem1 = "/imagem1.jpg",
                    urlImagem2 = "/imagem2.jpg",
                    urlImagem3 = "/imagem3.jpg",
                    urlImagem4 = "/imagem4.jpg",
                    urlImagem5 = "/imagem5.jpg",
                    urlImagemCapa = "/imagemcapa.jpg"
                };

                var response = client.PostAsJsonAsync("api/Evento/Cadastrar", evento).Result;

                if (response.IsSuccessStatusCode)
                {
                    var a = response.Content.ReadAsStringAsync();

                    if (a.Result.ToString().Trim() == "0")
                    {
                        MessageBox.Show("Erro. Evento Não Cadastrado");
                    }
                    else
                    {
                        MessageBox.Show("Evento cadastrado com sucesso");
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar");
                }
            }
        }

        private void btnBuscarEvento_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Authentication.access_token);

                var response = client.GetAsync("api/Evento/Buscar/1").Result;

                if (response.IsSuccessStatusCode)
                {
                    var retorno = response.Content.ReadAsStringAsync().Result;

                    var eventoRetorno = JsonConvert.DeserializeObject<Evento>(retorno, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    MessageBox.Show(retorno);
                }
            }
        }

        private void btnBuscarTodosEventos_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Authentication.access_token);

                var response = client.GetAsync("api/Evento/BuscarTodos").Result;

                if (response.IsSuccessStatusCode)
                {
                    var retorno = response.Content.ReadAsStringAsync().Result;

                    var eventosRetorno = JsonConvert.DeserializeObject<List<Evento>>(retorno, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    MessageBox.Show(retorno);
                }
            }
        }

        private void btnExcluirEvento_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Authentication.access_token);

                var response = client.DeleteAsync("api/Evento/Excluir/1").Result;

                if (response.IsSuccessStatusCode)
                {
                    var a = response.Content.ReadAsStringAsync();

                    if (a.Result.ToString().Trim() == "0")
                    {
                        MessageBox.Show("Erro. Evento Não Excluído");
                    }
                    else
                    {
                        MessageBox.Show("Evento excluido com sucesso");
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao excluir");
                }
            }
        }

        private void btnCadastrarCarona_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Authentication.access_token);

                EventoCarona eventoCarona = new EventoCarona
                {
                    enderecoPartidaBairro = "Partida Bairro 1",
                    enderecoPartidaCEP = "99999-000",
                    enderecoPartidaCidade = "Partida Cidade 1",
                    enderecoPartidaComplemento = "Partida Complemento 1",
                    enderecoPartidaNumero = 12345,
                    enderecoPartidaRua = "Partida Rua 1",
                    enderecoPartidaUF = "sc",
                    valorParticipacao = 100.55m,
                    Mensagem = "Mensagem teste 1",
                    quantidadeVagas = 10,
                    idEvento = 1,
                    idUsuarioMotorista = 1
                };

                var response = client.PostAsJsonAsync("api/EventoCarona/Cadastrar", eventoCarona).Result;

                if (response.IsSuccessStatusCode)
                {
                    var a = response.Content.ReadAsStringAsync();

                    if (a.Result.ToString().Trim() == "0")
                    {
                        MessageBox.Show("Erro. Evento Não Cadastrado");
                    }
                    else
                    {
                        MessageBox.Show("Evento cadastrado com sucesso");
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar");
                }
            }
        }

        private void btnBuscarCarona_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Authentication.access_token);

                var response = client.GetAsync("api/EventoCarona/Buscar/1").Result;

                if (response.IsSuccessStatusCode)
                {
                    var retorno = response.Content.ReadAsStringAsync().Result;

                    var eventoCaronaRetorno = JsonConvert.DeserializeObject<EventoCarona>(retorno, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    MessageBox.Show(retorno);
                }
            }
        }

        private void btnBuscarTodasCaronas_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Authentication.access_token);

                var response = client.GetAsync("api/EventoCarona/BuscarTodos").Result;

                if (response.IsSuccessStatusCode)
                {
                    var retorno = response.Content.ReadAsStringAsync().Result;

                    var eventosCaronaRetorno = JsonConvert.DeserializeObject<List<EventoCarona>>(retorno, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    MessageBox.Show(retorno);
                }
            }
        }

        private void btnExcluirCarona_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Authentication.access_token);

                var response = client.DeleteAsync("api/EventoCarona/Excluir/1").Result;

                if (response.IsSuccessStatusCode)
                {
                    var a = response.Content.ReadAsStringAsync();

                    if (a.Result.ToString().Trim() == "0")
                    {
                        MessageBox.Show("Erro. Carona Não Excluído");
                    }
                    else
                    {
                        MessageBox.Show("Carona excluido com sucesso");
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao excluir");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Authentication.access_token);

                var response = client.DeleteAsync("api/EventoTransporte/Excluir/1").Result;

                if (response.IsSuccessStatusCode)
                {
                    var a = response.Content.ReadAsStringAsync();

                    if (a.Result.ToString().Trim() == "0")
                    {
                        MessageBox.Show("Erro. Transporte Não Excluído");
                    }
                    else
                    {
                        MessageBox.Show("Transporte excluido com sucesso");
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao excluir");
                }
            }
        }

        private void btnCadastrarTransporte_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Authentication.access_token);

                EventoTransporte eventoTransporte = new EventoTransporte
                {
                    enderecoPartidaBairro = "Partida Bairro 1",
                    enderecoPartidaCEP = "99999-000",
                    enderecoPartidaCidade = "Partida Cidade 1",
                    enderecoPartidaComplemento = "Partida Complemento 1",
                    enderecoPartidaNumero = 12345,
                    enderecoPartidaRua = "Partida Rua 1",
                    enderecoPartidaUF = "sc",
                    valorParticipacao = 100.55m,
                    Mensagem = "Mensagem teste 1",
                    quantidadeVagas = 10,
                    idEvento = 1,
                    idUsuarioTransportador = 1
                };

                var response = client.PostAsJsonAsync("api/EventoTransporte/Cadastrar", eventoTransporte).Result;

                if (response.IsSuccessStatusCode)
                {
                    var a = response.Content.ReadAsStringAsync();

                    if (a.Result.ToString().Trim() == "0")
                    {
                        MessageBox.Show("Erro. Transporte Não Cadastrado");
                    }
                    else
                    {
                        MessageBox.Show("Transporte cadastrado com sucesso");
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar");
                }
            }
        }

        private void btnBuscarTransporte_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Authentication.access_token);

                var response = client.GetAsync("api/EventoTransporte/Buscar/1").Result;

                if (response.IsSuccessStatusCode)
                {
                    var retorno = response.Content.ReadAsStringAsync().Result;

                    var eventoCaronaRetorno = JsonConvert.DeserializeObject<EventoTransporte>(retorno, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    MessageBox.Show(retorno);
                }
            }
        }

        private void btnBuscarTodosTransportes_Click(object sender, EventArgs e)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Authentication.access_token);

                var response = client.GetAsync("api/EventoTransporte/BuscarTodos").Result;

                if (response.IsSuccessStatusCode)
                {
                    var retorno = response.Content.ReadAsStringAsync().Result;

                    var eventosCaronaRetorno = JsonConvert.DeserializeObject<List<EventoTransporte>>(retorno, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    MessageBox.Show(retorno);
                }
            }
        }

        private void btnEnviarFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                UploadToRestService(openFileDialog.FileName, openFileDialog.SafeFileName);
            }
        }

        private void UploadToRestService(string FileName, string safeFilename)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50081");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Authentication.access_token);

                MultipartFormDataContent form = new MultipartFormDataContent();
                HttpContent content = new StringContent("fileToUpload");

                //form.Add(content, "fileToUpload");

                var stream = new FileStream(FileName, FileMode.Open);
                content = new StreamContent(stream);

                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "fileToUpload",
                    FileName = safeFilename
                };
                form.Add(content);

                HttpResponseMessage response = null;

                try
                {
                    response = (client.PostAsync("api/Usuario/EnviarSelfie", form)).Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                var k = response.Content.ReadAsStringAsync().Result;
            }
        }

        //public static async Task<string> Upload(byte[] image)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        using (var content =
        //            new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
        //        {
        //            content.Add(new StreamContent(new MemoryStream(image)), "bilddatei", "upload.jpg");

        //            using (
        //               var message =
        //                   await client.PostAsync("http://www.directupload.net/index.php?mode=upload", content))
        //            {
        //                var input = await message.Content.ReadAsStringAsync();

        //                return !string.IsNullOrWhiteSpace(input) ? Regex.Match(input, @"http://\w*\.directupload\.net/images/\d*/\w*\.[a-z]{3}").Value : null;
        //            }
        //        }
        //    }
        //}
    }
}
