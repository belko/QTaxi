using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QTaxi.Startup))]
namespace QTaxi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
