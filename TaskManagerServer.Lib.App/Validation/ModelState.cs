using Microsoft.AspNetCore.Mvc.ModelBinding;
using TaskManagerServer.Lib.Core.Interfaces;

namespace TaskManagerServer.Lib.App.Validation;

public class ModelState(ModelStateDictionary modelStateDictionary) : IModelState
{
    public void AddModelError(string key, string errorMessage)
    {
        modelStateDictionary.AddModelError(key, errorMessage);
    }

    public bool IsValid => modelStateDictionary.IsValid;
}