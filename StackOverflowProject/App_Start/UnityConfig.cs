using System.Web.Http;
using Unity;
using Unity.WebApi;
using Unity.Mvc5;
using StackOverflowProject.ServiceLayer;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace StackOverflowProject
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IQuestionsService, QuestionsService>();
            container.RegisterType<IUsersService, UsersService>();
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container)); //enable injection for mvc5
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container); //enable injection for web api
            
        
        }
    }
}