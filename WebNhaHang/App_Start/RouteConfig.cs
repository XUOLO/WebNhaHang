using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebNhaHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Contact",
               url: "lien-he",
               defaults: new { controller = "Contact", action = "Index", alias = UrlParameter.Optional },
               namespaces: new[] { "WebNhaHang.Controllers" }
           );

            routes.MapRoute(
               name: "About us",
               url: "About-us",
               defaults: new { controller = "Home", action = "About", alias = UrlParameter.Optional },
               namespaces: new[] { "WebNhaHang.Controllers" }
           );
            routes.MapRoute(
              name: "Menu",
              url: "Thuc-don",
              defaults: new { controller = "Product", action = "Index", alias = UrlParameter.Optional },
              namespaces: new[] { "WebNhaHang.Controllers" }
          );
            routes.MapRoute(
             name: "Recruitment",
             url: "Tuyen-dung",
             defaults: new { controller = "Recruitment", action = "Index", alias = UrlParameter.Optional },
             namespaces: new[] { "WebNhaHang.Controllers" }
         );
            routes.MapRoute(
              name: "CheckOut",
              url: "thanh-toan",
              defaults: new { controller = "ShoppingCart", action = "CheckOut", alias = UrlParameter.Optional },
              namespaces: new[] { "WebNhaHang.Controllers" }
          );
            routes.MapRoute(
               name: "Reservation",
               url: "Dat-ban",
               defaults: new { controller = "Reservation", action = "Create", alias = UrlParameter.Optional },
               namespaces: new[] { "WebNhaHang.Controllers" }
           );
            routes.MapRoute(
              name: "CategoryProduct",
              url: "danh-muc-san-pham/{alias}-{id}",
              defaults: new { controller = "Product", action = "ProductCategory", id = UrlParameter.Optional },
              namespaces: new[] { "WebNhaHang.Controllers" }
          );
            routes.MapRoute(
               name: "Product",
               url: "san-pham",
               defaults: new { controller = "Product", action = "Index", alias = UrlParameter.Optional },
               namespaces: new[] { "WebNhaHang.Controllers" }
           );
            routes.MapRoute(
               name: "detailProduct",
               url: "chi-tiet/{alias}-p{id}",
               defaults: new { controller = "Product", action = "Detail", alias = UrlParameter.Optional },
               namespaces: new[] { "WebNhaHang.Controllers" }
           );
            routes.MapRoute(
              name: "ShoppingCart",
              url: "Gio-hang",
              defaults: new { controller = "ShoppingCart", action = "Index", alias = UrlParameter.Optional },
              namespaces: new[] { "WebNhaHang.Controllers" }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebNhaHang.Controllers" }
            );
        }
    }
}
