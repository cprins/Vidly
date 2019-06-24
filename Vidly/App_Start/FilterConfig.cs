using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // CPRINS Para que solicite la conexion de un usuario en cualquier parte de la aplicacion
            filters.Add(new AuthorizeAttribute());
            // CPRINS Para que el ingreso de la aplicacion sea solo mediante HTTPS (Conexion segura)
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
