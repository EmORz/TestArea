﻿using System;
using System.Linq;
using System.Reflection;
using SIS.HTTP.Enums;
using SIS.HTTP.Responses;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Action;
<<<<<<< HEAD
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Routing;
using SIS.WebServer;
using SIS.WebServer.Result;
=======
using SIS.MvcFramework.Routing;
using SIS.WebServer;
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37
using SIS.WebServer.Routing;

namespace SIS.MvcFramework
{
    public static class WebHost
    {
        public static void Start(IMvcApplication application)
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();
            AutoRegisterRoutes(application, serverRoutingTable);
            application.ConfigureServices();
            application.Configure(serverRoutingTable);
            var server = new Server(8000, serverRoutingTable);
            server.Run();
        }

        private static void AutoRegisterRoutes(
            IMvcApplication application, IServerRoutingTable serverRoutingTable)
        {
            var controllers = application.GetType().Assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract
                    && typeof(Controller).IsAssignableFrom(type));
            // TODO: RemoveToString from InfoController
            foreach (var controller in controllers)
            {
                var actions = controller
                    .GetMethods(BindingFlags.DeclaredOnly
                    | BindingFlags.Public
                    | BindingFlags.Instance)
                    .Where(x => !x.IsSpecialName && x.DeclaringType == controller)
                    .Where(x => x.GetCustomAttributes().All(a => a.GetType() != typeof(NonActionAttribute)));
                foreach (var action in actions)
                {
                    var path = $"/{controller.Name.Replace("Controller", string.Empty)}/{action.Name}";
                    var attribute = action.GetCustomAttributes().Where(
                        x => x.GetType().IsSubclassOf(typeof(BaseHttpAttribute))).LastOrDefault() as BaseHttpAttribute;
                    var httpMethod = HttpRequestMethod.Get;
                    if (attribute != null)
                    {
                        httpMethod = attribute.Method;
                    }

                    if (attribute?.Url != null)
                    {
                        path = attribute.Url;
                    }

                    if (attribute?.ActionName != null)
                    {
                        path = $"/{controller.Name.Replace("Controller", string.Empty)}/{attribute.ActionName}";
                    }

                    serverRoutingTable.Add(httpMethod, path, request =>
                    {
                        // request => new UsersController().Login(request)
                        var controllerInstance = Activator.CreateInstance(controller);
<<<<<<< HEAD
                        ((Controller)controllerInstance).Request = request;
                        var controllerPrincipal = ((Controller)controllerInstance).User;
                        //
                        var authorizeAttribute = action.GetCustomAttributes()
                            .LastOrDefault(x => x.GetType() == typeof(AuthorizeAttribute)) as AuthorizeAttribute;
                        if (authorizeAttribute != null && !authorizeAttribute.IsInAuthority(controllerPrincipal))
                        {
                            return new HttpResponse(HttpResponseStatusCode.Forbidden);
                        }
                        var response = action.Invoke(controllerInstance, new object[0]) as ActionResult;
=======
                        var response = action.Invoke(controllerInstance, new[] { request }) as IHttpResponse;
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37
                        return response;
                    });

                    Console.WriteLine(httpMethod + " " + path);
                }
            }
            // Reflection
            // Assembly
            // typeof(Server).GetMethods()
            // sb.GetType().GetMethods();
            // Activator.CreateInstance(typeof(Server))
            var sb = DateTime.UtcNow;

        }
    }
}
