namespace Api.Utilities.Exceptions;

public abstract class NotFoundException(string message) : Exception(message);