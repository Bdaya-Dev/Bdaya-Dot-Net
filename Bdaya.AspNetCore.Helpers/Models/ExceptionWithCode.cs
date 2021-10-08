using System.Runtime.Serialization;

namespace System;

public class ExceptionWithCode : Exception
{
    public ExceptionWithCode()
    {
    }

    public ExceptionWithCode(string message) : base(message)
    {
    }

    public ExceptionWithCode(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ExceptionWithCode(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public int ReturnCode { get; set; } = 400;    
}
