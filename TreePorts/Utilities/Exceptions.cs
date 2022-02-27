namespace TreePorts.Utilities;


[Serializable]
public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message) { }

    public NotFoundException(string message, Exception inner)
        : base(message, inner) { }
}



[Serializable]
public class NoContentException : Exception
{
    public NoContentException(string message)
        : base(message) { }

    public NoContentException(string message, Exception inner)
        : base(message, inner) { }
}


[Serializable]
public class ServiceUnavailableException : Exception
{


    public ServiceUnavailableException(string message)
        : base(message) { }

    public ServiceUnavailableException(string message, Exception inner)
        : base(message, inner) { }
}



[Serializable]
public class InvalidException : Exception
{


    public InvalidException(string message)
        : base(message) { }

    public InvalidException(string message, Exception inner)
        : base(message, inner) { }
}

[Serializable]
public class UnauthorizedException : Exception
{


    public UnauthorizedException(string message)
        : base(message) { }

    public UnauthorizedException(string message, Exception inner)
        : base(message, inner) { }
}