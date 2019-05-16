using MyMovieListXamarin.Base;
using MyMovieListXamarin.Models;
using MyMovieListXamarin.Repositories;
using MyMovieListXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyMovieListXamarin.ViewModels
{
    public class UsuarioViewModel: ViewModelBase
    {
        RepositoryMyMovieList repo;
        public UsuarioViewModel()
        {
            this.repo = new RepositoryMyMovieList();
            if(this.Usuario == null)
            {
                this.Usuario = new Usuario();
            }
        }
        private Usuario _Usuario;
        public Usuario Usuario
        {
            get { return this._Usuario; }
            set
            {
                this._Usuario = value;
                OnPropertyChanged("Usuario");
            }
        }
        public Command IniciarSesion
        {
            get
            {
                return new Command(async () =>
                {
                    String token = await this.repo.GetToken(this.Usuario.NombreUsuario, this.Usuario.Password);
                    if (token == null)
                    {
                        this.Error = "Usuario/Password incorrectos";
                    }
                    else
                    {
                        SessionService session = App.Locator.SessionService;
                        session.Token = token;
                        session.Usuario = Usuario;
                        this.Error = "Usuario correcto";
                    }
                });
            }
        }
        private String _Error;
        public String Error
        {
            get { return this._Error; }
            set
            {
                this._Error = value;
                OnPropertyChanged("Error");
            }
        }
    }
}
