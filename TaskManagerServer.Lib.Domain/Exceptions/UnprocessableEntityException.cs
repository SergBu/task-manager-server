namespace TaskManagerServer.Lib.Domain.Exceptions
{
    public class UnprocessableEntityException(string message) : Exception(message);
}