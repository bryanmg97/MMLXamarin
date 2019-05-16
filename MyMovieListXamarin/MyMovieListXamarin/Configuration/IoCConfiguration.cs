using Autofac;
using MyMovieListXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMovieListXamarin.Configuration
{
    public class IoCConfiguration
    {
        private IContainer container;
        public IoCConfiguration()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<SessionService>().SingleInstance();
            this.container = builder.Build();
        }
        public SessionService SessionService
        {
            get { return this.container.Resolve<SessionService>(); }
        }
    }
}
