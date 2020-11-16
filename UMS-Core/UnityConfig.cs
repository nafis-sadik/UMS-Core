using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;
namespace UMS_Core
{
    public class UnityConfig
    {
        private static Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() => {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static void RegisterTypes(IUnityContainer container)
        {
            //container.RegisterType<ILogInService, LogInService>();
        }
    }
}
