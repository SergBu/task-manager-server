namespace TaskManagerServer.Lib.Domain.Exceptions;

public class NotFoundEntityException(string message) : Exception(message);