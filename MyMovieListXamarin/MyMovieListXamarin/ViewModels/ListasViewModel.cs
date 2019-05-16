using MyMovieListXamarin.Base;
using MyMovieListXamarin.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MyMovieListXamarin.ViewModels
{
    public class ListasViewModel: ViewModelBase
    {
        RepositoryMyMovieList repo;
        public ListasViewModel()
        {
            this.repo = new RepositoryMyMovieList();
            Task.Run(async () =>
            {
                await this.CargarApuestas();
            });
        }
        public async Task CargarApuestas()
        {
            SearchContainer<SearchMovie> lista = await this.repo.PeliculasPopulares();
            this.Apuestas = new ObservableCollection<SearchMovie>(lista.Results);
        }
        private ObservableCollection<SearchMovie> _Apuestas;
        public ObservableCollection<SearchMovie> Apuestas
        {
            get { return this._Apuestas; }
            set
            {
                this._Apuestas = value;
                OnPropertyChanged("Apuestas");
            }
        }
    }
}
