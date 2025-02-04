using System.Diagnostics.CodeAnalysis;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Oma.WndwCtrl.Abstractions.Errors;
using Oma.WndwCtrl.Abstractions.Messaging.Model.ComponentExecution;
using Oma.WndwCtrl.Abstractions.Model;
using Oma.WndwCtrl.Api.Attributes;
using Oma.WndwCtrl.Core.Model;

namespace Oma.WndwCtrl.Api.Controllers.Components;

[SuppressMessage(
  "ReSharper",
  "RouteTemplates.MethodMissingRouteParameters",
  Justification = "Won't fix: Controller template; route parameters resolved through convention."
)]
public class ButtonController : ComponentControllerBase<Button>
{
  [HttpPost("trigger")]
  [EndpointSummary("Trigger")]
  [Produces<ComponentCommandOutcomeEvent>]
  [Actionable]
  public async Task<Either<FlowError, FlowOutcome>> TriggerAsync() =>
    await ExecuteCommandAsync(Component.Command);
}