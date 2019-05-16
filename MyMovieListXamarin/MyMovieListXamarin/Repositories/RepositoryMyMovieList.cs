using MyMovieListXamarin.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MyMovieListXamarin.Repositories
{
    public class RepositoryMyMovieList
    {
        private String uriapi;
        public RepositoryMyMovieList()
        {
            this.uriapi = "https://mymovielistapi.azurewebsites.net/";
        }
        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
        public async Task<String> GetToken(String usuario
            , String password)
        {
            using (HttpClient client = this.GetHttpClient())
            {
                client.BaseAddress = new Uri(this.uriapi);
                LoginModel login = new LoginModel();
                login.NombreUsuario = usuario;
                login.Password = password;
                String json = JsonConvert.SerializeObject(login);
                StringContent content =
                    new StringContent(json
                    , System.Text.Encoding.UTF8, "application/json");
                String peticion = "Auth/Login";
                HttpResponseMessage response =
                    await client.PostAsync(peticion, content);
                if (response.IsSuccessStatusCode)
                {
                    String contenido =
                        await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(contenido);
                    return jObject.GetValue("response").ToString();
                }
                else
                {
                    return null;
                }
            }
        }
        private async Task<T> CallApi<T>(String peticion, String token)
        {

            using (HttpClient client = this.GetHttpClient())
            {

                client.BaseAddress = new Uri(this.uriapi);
                if (token != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                }
                HttpResponseMessage response = await client.GetAsync(peticion);
                if (response.IsSuccessStatusCode)
                {
                    String content = await response.Content.ReadAsStringAsync();
                    T datos = JsonConvert.DeserializeObject<T>(content);
                    return (T)Convert.ChangeType(datos, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
        }
        public async Task<SearchContainer<SearchMovie>> PeliculasPopulares()
        {
            String peticion = "api/Peliculas/PeliculasPopulares";
            Uri uri = new Uri(this.uriapi + peticion);
            HttpClient client = this.GetHttpClient();
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                String json = await response.Content.ReadAsStringAsync();
                SearchContainer<SearchMovie> resultados = JsonConvert.DeserializeObject<SearchContainer<SearchMovie>>(json);
                return resultados;
            }
            else
            {
                return null;
            }
        }
        public async Task RegistroUsuario(Usuario user)
        {
            String jsonusuario = JsonConvert.SerializeObject(user, Formatting.Indented);
            byte[] bufferusuario = Encoding.UTF8.GetBytes(jsonusuario);
            ByteArrayContent content = new ByteArrayContent(bufferusuario);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            String peticion = "api/Usuarios/CrearUsuario";
            Uri uri = new Uri(this.uriapi + peticion);
            HttpClient client = this.GetHttpClient();
            HttpResponseMessage response = await client.PostAsync(uri, content);
        }
    }
}
