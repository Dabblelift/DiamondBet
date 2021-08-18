using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(DiamondBet.Web.Areas.Identity.IdentityHostingStartup))]

namespace DiamondBet.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
