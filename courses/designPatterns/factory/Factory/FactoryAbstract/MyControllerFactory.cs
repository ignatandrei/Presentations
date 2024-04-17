using System.Web.Mvc;
using System.Web.Routing;

namespace FactoryAbstract
{
    class MyControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            //Maybe construct a new controller
            if (controllerName == "andrei")
                return null;
            return base.CreateController(requestContext, controllerName);
        }
    }
}