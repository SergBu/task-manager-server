namespace TaskManagerServer.Lib.Domain.Exceptions;

public class AccessDeniedException(string message) : Exception(message)
{
    public AccessDeniedException():this("Доступ запрещен")
    {
    }
}