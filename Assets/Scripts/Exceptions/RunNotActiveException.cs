using System;

public class RunNotActiveException : Exception
{
    public RunNotActiveException() { }

    public RunNotActiveException(string message) : base(message)
    {

    }

    public RunNotActiveException(string message, Exception inner) : base(message, inner)
    {

    }
}
