using Newtonsoft.Json;
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
    }
}
