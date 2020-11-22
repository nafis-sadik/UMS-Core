using Application;
using System.Web.Http;
using Unity.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ProviderConfig), nameof(ProviderConfig.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(ProviderConfig), nameof(ProviderConfig.Shutdown))]

namespace Application
{
    public static class ProviderConfig
    {
        public static void Start()
        {
            UnityDependencyResolver resolver = new UnityDependencyResolver(UnityConfig.Container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        public static void Shutdown()
        {
            UnityConfig.Container.Dispose();
        }
    }
}
