namespace SliceCloud.Repository.ViewModels;

public class SidebarMenuItemViewModel
{
    public string Text { get; set; } = string.Empty;
    public string Controller { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string ActiveIcon { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = [];
}
