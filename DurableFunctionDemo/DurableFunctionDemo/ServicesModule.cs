using Autofac;
using DurableFunctionDemo.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DurableFunctionDemo
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GitHubApiServiceAutofac>().As<IGitHubApiService>();
        }
    }
}
