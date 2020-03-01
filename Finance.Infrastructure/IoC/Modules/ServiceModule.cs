using Autofac;
using Finance.Core.Repositories;
using Finance.Infrastructure.Services;
using Finance.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Finance.Infrastructure.IoC.Modules
{
    public class ServiceModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServiceModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<Encrypter>()
                .As<IEncrypter>()
                .SingleInstance();

            builder.RegisterType<JwtHandler>()
               .As<IJwtHandler>()
               .SingleInstance();

            builder.RegisterType<HandlerTask>()
                .As<IHandlerTask>();

            builder.RegisterType<Handler>()
                .As<IHandler>();

            builder.RegisterType<HandlerTaskRunner>()
                .As<IHandlerTaskRunner>();
        }
    }
}
