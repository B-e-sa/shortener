using System;

namespace Shortener.Domain.Common.Exceptions.Base;

public abstract class NotFoundException(string message)
    : Exception(
        string.IsNullOrEmpty(message)
        ? "One or more validation failures have occurred."
        : message)
{
}