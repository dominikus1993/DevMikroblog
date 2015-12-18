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
            _kernel = AddBindings();

        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (IController)(controllerType == null ? null : _kernel.Get(controllerType));
        }

        private IKernel AddBindings()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IDbContext>().To<ApplicationDbContext>();
            kernel.Bind<IPostRepository>().To<PostRepository>();
            kernel.Bind<IPostService>().To<PostService>();
            kernel.Bind<ITagRepository>().To<TagRepository>();
            kernel.Bind<ITagService>().To<TagService>();
            return kernel;
        }
    }
}