using Microsoft.Owin;
using Owin;
using Hangfire;
using LibraryService.DAL;

[assembly: OwinStartupAttribute(typeof(LibraryService.Startup))]
namespace LibraryService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            GlobalConfiguration.Configuration.UseSqlServerStorage("Data Source = (LocalDb)\\MSSQLLocalDB; Initial Catalog = LibraryService1; Integrated Security = SSPI; Integrated Security = True; Initial Catalog = aspnetdb");
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate(() => new Services.Service.LibraryItemService().UpdateStatus(), Cron.Daily);
        }
    }
}
