using JetBrains.Annotations;
using LanguageExt;
using LanguageExt.Common;

namespace Oma.WndwCtrl.Abstractions.Errors;

public record TechnicalError : Exceptional
{
  private const int ErrorCodeOffset = 500_000;

  [PublicAPI]
  public TechnicalError(string Message, int Code) : this(
    Message,
    ErrorCodeOffset + Code,
    new InvalidOperationException(Message)
  )
  {
  }

  [PublicAPI]
  public TechnicalError(string Message, int Code, Exception ex) : base(Message, ErrorCodeOffset + Code)
  {
    Inner = New(ex);
  }

  public override Option<Error> Inner { get; } = Option<Error>.None;
}