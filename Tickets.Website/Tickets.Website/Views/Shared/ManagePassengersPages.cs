using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tickets.Website.Views.Shared
{
    public class ManagePassengersPages
    {
        public static string Passengers => "Passengers";

        public static string AddPassenger => "AddPassenger";

        public static string PassengersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Passengers);

        public static string AddPassengerNavClass(ViewContext viewContext) => PageNavClass(viewContext, AddPassenger);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
