using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tickets.Website.Views.Shared
{
    public class ManageTicketsPages
    {
        public static string CurrentTickets => "CurrentTickets";

        public static string TicketsArchive => "TicketsArchive";

        public static string CurrentTicketsNavClass(ViewContext viewContext) => PageNavClass(viewContext, CurrentTickets);

        public static string TicketsArchiveNavClass(ViewContext viewContext) => PageNavClass(viewContext, TicketsArchive);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
