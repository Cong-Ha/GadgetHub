using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using Moq;

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

        private void AddBinding()
        {
            Mock<IGadgetCatalogRepository> mock = new Mock<IGadgetCatalogRepository>();
            mock.Setup(m => m.GadgetCatalogs).Returns(new List<GadgetCatalog>
            {
                new GadgetCatalog { Name = "Iphone", Brand = "Apple", Category = "Phone", Description = "Latest Apple flagship smartphone", Price = 1000.99m },
                new GadgetCatalog { Name = "Erickson", Brand = "Sony", Category = "Phone", Description = "A classic Sony smartphone", Price = 900.99m },
                new GadgetCatalog { Name = "Key", Brand = "BlackBerry", Category = "Phone", Description = "BlackBerry phone with a physical keyboard", Price = 600.99m },
                new GadgetCatalog { Name = "Galaxy S23", Brand = "Samsung", Category = "Phone", Description = "High-end Samsung Android smartphone", Price = 850.50m },
                new GadgetCatalog { Name = "Pixel 7", Brand = "Google", Category = "Phone", Description = "Google's latest AI-powered smartphone", Price = 700.75m },
                new GadgetCatalog { Name = "Xperia 1", Brand = "Sony", Category = "Phone", Description = "Sony’s flagship smartphone with a great camera", Price = 950.30m },
                new GadgetCatalog { Name = "OnePlus 11", Brand = "OnePlus", Category = "Phone", Description = "High-performance smartphone from OnePlus", Price = 780.25m },
                new GadgetCatalog { Name = "Razor Phone", Brand = "Razer", Category = "Phone", Description = "Gaming-focused smartphone with high refresh rate", Price = 670.89m },
                new GadgetCatalog { Name = "Redmi Note 12", Brand = "Xiaomi", Category = "Phone", Description = "Affordable smartphone with good specs", Price = 500.99m },
                new GadgetCatalog { Name = "Asus ROG Phone", Brand = "Asus", Category = "Phone", Description = "Ultimate gaming smartphone with RGB lighting", Price = 1100.49m }
            });



            kernel.Bind<IGadgetCatalogRepository>().ToConstant(mock.Object);
        }
    }
}