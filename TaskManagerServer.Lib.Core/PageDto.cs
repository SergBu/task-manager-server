namespace TaskManagerServer.Lib.Core;

public class PageDto<TEntities>
{
    public ICollection<TEntities> Entities { get; set; } = [];

    public int Total { get; set; }
}