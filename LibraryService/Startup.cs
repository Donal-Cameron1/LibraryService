using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibraryService.Startup))]
namespace LibraryService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
