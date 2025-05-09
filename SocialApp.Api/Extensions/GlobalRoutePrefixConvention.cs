namespace SocialApp.Api.Extensions;

using Microsoft.AspNetCore.Mvc.ApplicationModels;

public class GlobalRoutePrefixConvention : IApplicationModelConvention
{
    private readonly AttributeRouteModel _routePrefix;

    public GlobalRoutePrefixConvention(string prefix)
    {
        _routePrefix = new AttributeRouteModel(new Microsoft.AspNetCore.Mvc.RouteAttribute(prefix));
    }

    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            var matchedSelectors = controller.Selectors
                .Where(x => x.AttributeRouteModel != null)
                .ToList();

            foreach (var selectorModel in matchedSelectors)
            {
                selectorModel.AttributeRouteModel = 
                    AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selectorModel.AttributeRouteModel);
            }

            var unmatchedSelectors = controller.Selectors
                .Where(x => x.AttributeRouteModel == null)
                .ToList();

            foreach (var selectorModel in unmatchedSelectors)
            {
                selectorModel.AttributeRouteModel = _routePrefix;
            }
        }
    }
}
