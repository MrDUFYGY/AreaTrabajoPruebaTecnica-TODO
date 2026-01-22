using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors; 

namespace SL_API_TODO
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API

            // ---------------------------------------------------------
            // 1. HABILITAR CORS GLOBALMENTE
            // ---------------------------------------------------------
            // Esto le dice al servidor: "Acepta peticiones de quien sea (*), 
            // con cualquier header (*) y cualquier método (GET/POST/PUT/DELETE)"
            var cors = new EnableCorsAttribute(origins: "*", headers: "*", methods: "*");
            config.EnableCors(cors);
            // ---------------------------------------------------------

            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}