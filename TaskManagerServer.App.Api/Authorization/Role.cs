namespace TaskManagerServer.App.Api.Authorization;

public static class Role
{
    public static class TaskManagerServerEntity
    {
        public const string Viewer = "TaskManagerServerEntityViewer";
        public const string Editor = "TaskManagerServerEntityEditor";
        public const string Admin = "TaskManagerServerEntityAdmin";
    }
}