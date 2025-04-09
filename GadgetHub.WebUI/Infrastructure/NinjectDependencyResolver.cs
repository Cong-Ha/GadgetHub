using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using Moq;
using GadgetHub.Domain.DataBaseAccess.Repository;
using static GadgetHub.Domain.Email.EmailOrderProcessor;

namespace GadgetHub.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBinding();
        }

        public object GetService(Type serviceType)
        {
            return kernel.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        /// <summary>
        /// Add Repository bindings here
        /// </summary>
        private void AddBinding()
        {
            kernel.Bind<IGadgetCatalogRepository>().To<GadgetCatalogRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Email.WriteAsFile"]),
            };
            kernel.Bind<IOrderProcessor>()
                .To<EmailOrderProcess>()
                .WithConstructorArgument("settings", emailSettings);
        }
    }
}