using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Finance.Infrastructure.Mappers;
using Microsoft.Extensions.Configuration;

namespace Finance.Infrastructure.IoC.Modules
{
    public class ContainerModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.Initialize())
                .SingleInstance();

            builder.RegisterModule<CommandModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();

            builder.RegisterModule(new SettingsModule(_configuration));

        }
    }
}
