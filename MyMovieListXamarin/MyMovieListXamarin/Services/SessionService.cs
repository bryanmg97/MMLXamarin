using MyMovieListXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMovieListXamarin.Services
{
    public class SessionService
    {
        public String Token { get; set; }
        public Usuario Usuario { get; set; }
        public SessionService()
        {
            this.Usuario = new Usuario();
        }
    }
}

