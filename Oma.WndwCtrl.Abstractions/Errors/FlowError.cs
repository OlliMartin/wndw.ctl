using LanguageExt;
using LanguageExt.Common;

namespace Oma.WndwCtrl.Abstractions.Errors;

public record FlowError : Error
{
    protected FlowError(Error other) : this(other.Message, other.IsExceptional, other.IsExpected)
    {
        Code = other.Code;
        Inner = other;
    }

    protected FlowError(TechnicalError technicalError) : this((Error)technicalError)
    {
    }
    
    protected FlowError(string message, bool isExceptional, bool isExpected)
    {
        Message = message;
        IsExceptional = isExceptional;
        IsExpected = isExpected;
    }
    
    public override int Code { get; }
    public override string Message { get; }
    public override bool IsExceptional { get; }
    public override bool IsExpected { get; }
    
    public override Option<Error> Inner { get; } = Option<Error>.None;
    
    public override bool Is<E>()
    {
        // TODO: Wrong implementation
        if(this is E) return true;
        return false;
    }

    public override ErrorException ToErrorException()
    {
        throw new NotImplementedException();
    }
    
    public static implicit operator FlowError(TechnicalError error)
        => new(error);
}