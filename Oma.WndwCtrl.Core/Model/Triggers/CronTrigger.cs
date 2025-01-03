using System.Text.Json.Serialization;
using Oma.WndwCtrl.Abstractions;

namespace Oma.WndwCtrl.Core.Model.Triggers;

public record CronTrigger : BaseTrigger, ISchedulableTrigger
{
  [JsonRequired]
  public string Expression { get; init; } = string.Empty;
}