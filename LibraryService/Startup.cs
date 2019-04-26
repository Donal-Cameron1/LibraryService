using Microsoft.Owin;
using Owin;
using Hangfire;
using LibraryService.DAL;
using System.Net.Mail;
using System.Net;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(LibraryService.Startup))]
namespace LibraryService
{
    public partial class Startup
    {


        public void Configuration(IAppBuilder app)
        {

            ConfigureAuth(app);

            GlobalConfiguration.Configuration.UseSqlServerStorage(new LibraryContext().Database.Connection.ConnectionString);  // Copy connection string
            //GlobalConfiguration.Configuration.UseSqlServerStorage("LibraryContext");
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate(() => new Services.Service.LibraryItemService().UpdateStatus(), Cron.Daily);

            RecurringJob.AddOrUpdate(() => new Services.Service.LibraryItemService().SendOverdueMail(), Cron.Daily);
           
        }
    }

    /*public class HangfireContext : DbContext
    {
        public HangfireContext() : base("HangfireContext")  // Remove "name="
        {
            Database.SetInitializer<HangfireContext>(null);
            Database.CreateIfNotExists();
        }
    }*/
}
