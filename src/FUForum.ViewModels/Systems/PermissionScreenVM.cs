namespace FUForum.ViewModels.Systems;

public class PermissionScreenVM
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string ParentId { get; set; }

    public string HasCreate { get; set; }
    public string HasUpdate { get; set; }
    public string HasDelete { get; set; }
    public string HasView { get; set; }
    public string HasApprove { get; set; }
}