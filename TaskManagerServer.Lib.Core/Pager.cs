namespace TaskManagerServer.Lib.Core;

public class Pager
{
    public const int PerPageDefault = 20;

    public const int PageNumberDefault = 1;

    public int Number { get; set; } = PageNumberDefault;

    public int PerPage { get; set; } = PerPageDefault;

    public int Skip => (Number - 1) * PerPage;

    public int Take => PerPage;
}