using Microsoft.EntityFrameworkCore;


namespace TicTacDough.Server {
    public class Startup {

        // members
        public IConfiguration Configuration { get; }

        // constructor
        public Startup(IConfiguration configuration) {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) {

            // register DbCOntext as a service in the app
            services.AddDbContext<Data.ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }
    }
}