using DevFramework.core3.CrossCuttingConcerns.Security.Web;
using DevFramework.core3.Utilities.Mvc.Infrastructure;
using DevFramework.Northwind.Business.DependencyResolvers.Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace DevFramework.Northwind.MVC_WEBUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new BussinesModule(), new AutoMapperModule()));
        }
        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;


            base.Init();
        }

        private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie == null)
                {
                    return;
                }

                var encTicket = authCookie.Value;

                if (string.IsNullOrEmpty(encTicket))
                {
                    return;
                }

                var ticket = FormsAuthentication.Decrypt(encTicket);

                var securityUtlities = new SecurityUtilities();

                var indentity = securityUtlities.FormsAuthTicketToIdentity(ticket);

                var principal = new GenericPrincipal(indentity, indentity.Roles);

                HttpContext.Current.User = principal;

                Thread.CurrentPrincipal = principal;

            }
            catch (Exception)
            {


            }
        }
    }
}
