using SliceCloud.Repository.ViewModels;

namespace SliceCloud.Web.Helpers;

public static class SidebarMenuProvider
{
    #region GetMenu

    public static List<SidebarMenuItemViewModel> GetMenu()
    {
        return
        [
            new SidebarMenuItemViewModel
            {
                Text = "Dashboard",
                Controller = "Dashboard",
                Action = "Dashboard",
                Icon = "dashboard_default.svg",
                ActiveIcon = "dashboard_active.svg",
                Roles = ["Admin",]
            },
            new SidebarMenuItemViewModel
            {
                Text = "Users",
                Controller = "Users",
                Action = "Users",
                Icon = "user_default.svg",
                ActiveIcon = "user_active.svg",
                Roles = ["Admin"]
            },
            new SidebarMenuItemViewModel
            {
                Text = "Role And Permission",
                Controller = "RoleAndPermission",
                Action = "RoleAndPermission",
                Icon = "roles_default.svg",
                ActiveIcon = "roles_active.svg",
                Roles = ["Admin",]
            },
            new SidebarMenuItemViewModel
            {
                Text = "Menu",
                Controller = "Menu",
                Action = "Menu",
                Icon = "menu_default.svg",
                ActiveIcon = "menu_active.svg",
                Roles = ["Admin",]
            },
            new SidebarMenuItemViewModel
            {
                Text = "TableAndSection",
                Controller = "TableAndSection",
                Action = "TableAndSection",
                Icon = "table_default.svg",
                ActiveIcon = "table_active.svg",
                Roles = ["Admin",]
            },
            new SidebarMenuItemViewModel
            {
                Text = "TaxesAndFees",
                Controller = "TaxesAndFees",
                Action = "TaxesAndFees",
                Icon = "tax_default.svg",
                ActiveIcon = "tax_active.svg",
                Roles = ["Admin",]
            },
            new SidebarMenuItemViewModel
            {
                Text = "Orders",
                Controller = "Orders",
                Action = "Orders",
                Icon = "orders_default.svg",
                ActiveIcon = "orders_active.svg",
                Roles = ["Admin",]
            },
            new SidebarMenuItemViewModel
            {
                Text = "Customers",
                Controller = "Customers",
                Action = "Customers",
                Icon = "customer_default.svg",
                ActiveIcon = "customer_active.svg",
                Roles = ["Admin",]
            },
        ];
    }

    #endregion
}
