namespace Shortener.Domain.Common.Exceptions.Base;

public abstract class ConflictException(string message)
    : Exception(
        string.IsNullOrEmpty(message)
        ? "The request could not be completed due to a conflict with the current state of the resource"
        : message);