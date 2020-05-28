using Autofac;
using AutofacOnFunctions.Services.Ioc;

namespace DurableFunctionDemo
{
    public class Bootstrapper : IBootstrapper
    {
        public Module[] CreateModules()
        {
            return new Module[]
            {
                new ServicesModule()
            };
        }
    }
}
