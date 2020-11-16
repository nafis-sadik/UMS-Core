using Services.Abstraction;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Application
{
    public class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() => {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer Container => container.Value;

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ILogInService, LogInService>();
        }
    }
}
