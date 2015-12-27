using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using DevMikroblog.Domain.DatabaseContext.Implementation;
using DevMikroblog.Domain.DatabaseContext.Interface;
using DevMikroblog.Domain.Repositories.Implementation;
using DevMikroblog.Domain.Repositories.Interface;
using DevMikroblog.Domain.Services.Implementation;
using DevMikroblog.Domain.Services.Interface;
using Ninject;

namespace DevMikroblog.WebApp.Infrastructure
{
    public class ApplicationDependencyResolver: IDependencyResolver
    {
        private readonly IKernel _kernel;

        public ApplicationDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            _kernel.Bind<IPostTagService>().To<PostTagService>();
            _kernel.Bind<IPostService>().To<PostService>();
            _kernel.Bind<ITagService>().To<TagService>();
            _kernel.Bind<ITagRepository>().To<TagRepository>();
            _kernel.Bind<IPostRepository>().To<PostRepository>();
            _kernel.Bind<IDbContext>().To<ApplicationDbContext>();
        }

        public void Dispose()
        {
        }

        public object GetService(Type serviceType) => _kernel.TryGet(serviceType);

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _kernel.GetAll(serviceType);
            }
            catch (Exception)
            {
                return Enumerable.Empty<object>();
            }
        }

        public IDependencyScope BeginScope() => this;

        public static ApplicationDependencyResolver Get() => new ApplicationDependencyResolver();
    }
}