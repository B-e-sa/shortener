namespace Shortener.Domain.Common.Exceptions.Base;

public abstract class BadRequestException(string message)
    : Exception(
        string.IsNullOrEmpty(message)
        ? "The request could not be understood or was missing required parameters."
        : message);