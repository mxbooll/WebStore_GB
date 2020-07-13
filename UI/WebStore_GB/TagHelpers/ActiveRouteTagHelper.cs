using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace WebStore_GB.TagHelpers
{
    [HtmlTargetElement(Attributes = ATTRIBUTE_NAME)]
    public class ActiveRouteTagHelper : TagHelper
    {
        private const string ATTRIBUTE_NAME = "is-active-route";

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp all route data", DictionaryAttributePrefix = "asp route ")]
        public IDictionary<string, string> RouteValues { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll(ATTRIBUTE_NAME);
        }
    }
}
