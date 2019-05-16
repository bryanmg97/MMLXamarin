using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMovieListXamarin.Models
{
    public class Lista
    {
        [JsonProperty("IdLista")]
        public int IdLista { get; set; }
        [JsonProperty("Peliculas")]
        public String Peliculas { get; set; }
        [JsonProperty("Usuario")]
        public String Usuario { get; set; }
    }
}
