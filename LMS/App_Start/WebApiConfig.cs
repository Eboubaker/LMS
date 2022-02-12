using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Http;
using LMS.Models;
using System.Globalization;
using Microsoft.Ajax.Utilities;
using System.Text.RegularExpressions;
using LMS.App_Start;
using System.Net.Http.Headers;
using LMS.Controllers;
using System.Web.Mvc;

namespace LMS
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            var formattingSettings = config.Formatters.JsonFormatter.SerializerSettings;
            formattingSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            formattingSettings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Development.DoWork();
        }
    }
}
