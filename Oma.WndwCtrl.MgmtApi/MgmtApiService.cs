using Oma.WndwCtrl.Api.Extensions;
using Oma.WndwCtrl.CoreAsp;
using Oma.WndwCtrl.MgmtApi.Model;
using Oma.WndwCtrl.MgmtApi.Workers;

namespace Oma.WndwCtrl.MgmtApi;

public class MgmtApiService : WebApplicationWrapper<MgmtApiService>
{
  protected override IServiceCollection ConfigureServices(IServiceCollection services)
  {
    return base.ConfigureServices(services)
      .AddSingleton<ServiceState>()
      .AddComponentApi()
      .AddHostedService<ServiceWorker>();
  }
}