using PK.DataAccess;
using PK.Services;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace WebApplication2
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<IMovesRepository, MovesRepository>();
            container.RegisterType<IServicesMoves, ServicesMoves>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}