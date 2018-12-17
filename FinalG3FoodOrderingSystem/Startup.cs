using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalG3FoodOrderingSystem.Startup))]
namespace FinalG3FoodOrderingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
