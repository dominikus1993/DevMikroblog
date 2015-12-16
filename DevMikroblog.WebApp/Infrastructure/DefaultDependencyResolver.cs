using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DevMikroblog.Domain.DatabaseContext.Implementation;
using DevMikroblog.Domain.DatabaseContext.Interface;
using DevMikroblog.Domain.Repositories.Implementation;
using DevMikroblog.Domain.Repositories.Interface;
using DevMikroblog.Domain.Services.Implementation;
using DevMikroblog.Domain.Services.Interface;
using Ninject;

namespace DevMikroblog.WebApp.Infrastructure
{
    public class DefaultDependencyResolver:DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public DefaultDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (IController)(controllerType == null ? null : _kernel.Get(controllerType));
        }

        private void AddBindings()
        {
            _kernel.Bind<IDbContext>().To<ApplicationDbContext>();
            _kernel.Bind<IPostRepository>().To<PostRepository>();
            _kernel.Bind<IPostService>().To<PostService>();
        }
    }
}