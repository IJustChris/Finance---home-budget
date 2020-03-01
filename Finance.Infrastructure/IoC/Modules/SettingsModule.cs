using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Finance.Infrastructure.Extensions;
using Finance.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace Finance.Infrastructure.IoC.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<GeneralSettings>()).SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<JwtSettings>()).SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<SqlDatabaseSettings>()).SingleInstance();

        }
    }
}
