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
    public class ApplicationControllerFactory:DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public ApplicationControllerFactory()
        {
            _kernel = new StandardKernel();
            AddBindings();

        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
             => (IController)(controllerType == null ? null : _kernel.Get(controllerType));

        private void AddBindings()
        {
            _kernel.Bind<IPostService>().To<PostService>();
            _kernel.Bind<ITagService>().To<TagService>();
            _kernel.Bind<ITagRepository>().To<TagRepository>();
            _kernel.Bind<IPostRepository>().To<PostRepository>();
            _kernel.Bind<IDbContext>().To<ApplicationDbContext>();
        }
    }
}