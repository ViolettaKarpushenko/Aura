using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;

namespace Aura.Web
{
    public class AuraControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public AuraControllerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return _kernel.Get(controllerType) as IController;
        }
    }
}