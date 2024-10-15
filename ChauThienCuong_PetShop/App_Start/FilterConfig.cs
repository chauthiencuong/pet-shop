using System.Web;
using System.Web.Mvc;

namespace ChauThienCuong_PetShop
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
