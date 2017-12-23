using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Moq;
using AdmissionCommittee.Domain.Abstract;
using AdmissionCommittee.Domain.Concrete;
using AdmissionCommittee.Domain.Entities;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IEnrolleeRepository>().To<EFEnrolleeRepository>();
        }
    }
}