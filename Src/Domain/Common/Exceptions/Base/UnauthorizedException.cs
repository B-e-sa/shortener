using System;

namespace Shortener.Domain.Common.Exceptions.Base
{
    public abstract class UnauthorizedException(string? message) : Exception(message)
    {
    }
}
