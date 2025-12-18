namespace TaskManagerServer.Lib.Core.Interfaces;

public interface IModelState
{
    void AddModelError(string key, string errorMessage);
    bool IsValid { get; }
}