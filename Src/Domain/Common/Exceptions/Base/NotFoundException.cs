using System;

namespace Shortener.Domain.Common.Exceptions.Base;

public abstract class NotFoundException(string message)
    : Exception(
        string.IsNullOrEmpty(message)
        ? "Searched resource was not found."
        : message)
{
}