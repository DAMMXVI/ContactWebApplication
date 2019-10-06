using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContactWebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)//Serverdan her istek yapıldığında çalış.
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];    //cookideki dil bilgisini al.
            if (cookie != null && cookie.Value != null) //dil değeri boş değilse o dile göre tarih saat ve arayüzü ayarla.
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);
            }
            else //dil değeri boş ise türkçeye ayarla.
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("tr");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("tr");
            }
        }
    }
}
