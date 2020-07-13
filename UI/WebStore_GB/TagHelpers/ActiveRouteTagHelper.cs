using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebStore_GB.TagHelpers
{
    [HtmlTargetElement(Attributes = ATTRIBUTE_NAME)]
    public class ActiveRouteTagHelper : TagHelper
    {
        private const string ATTRIBUTE_NAME = "is-active-route";
        private const string IGNORE_ACTION = "ignore-action";

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public IDictionary<string, string> RouteValues { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var ignoreAction = output.Attributes.ContainsName(IGNORE_ACTION);

            if (IsActive(ignoreAction))
            {
                MakeActive(output);
            }

            output.Attributes.RemoveAll(IGNORE_ACTION);
            output.Attributes.RemoveAll(ATTRIBUTE_NAME);
        }

        private bool IsActive(bool ignoreAction)
        {
            var routeValues = ViewContext.RouteData.Values;

            var currentController = routeValues["controller"].ToString();
            var currentAction = routeValues["action"].ToString();

            const StringComparison IGNORE_CASE = StringComparison.OrdinalIgnoreCase;
            if (!string.IsNullOrEmpty(Controller) && !string.Equals(currentController, Controller, IGNORE_CASE))
            {
                return false;
            }

            if (!ignoreAction && !string.IsNullOrEmpty(Action) && !string.Equals(currentAction, Action, IGNORE_CASE))
            {
                return false;
            }

            foreach (var (key, value) in RouteValues)
            {
                if (!routeValues.ContainsKey(key) || routeValues[key].ToString() != value)
                {
                    return false;
                }
            }

            return true;
        }

        private static void MakeActive(TagHelperOutput output)
        {
            var classAttribute = output.Attributes.FirstOrDefault(attr => attr.Name == "class");

            if (classAttribute is null)
            {
                output.Attributes.Add("class", "active");
            }
            else
            {
                if (classAttribute.Value.ToString()?.Contains("active") ?? false)
                {
                    return;
                }
                output.Attributes.SetAttribute("class", classAttribute.Value + " active");
            }
        }
    }
}
