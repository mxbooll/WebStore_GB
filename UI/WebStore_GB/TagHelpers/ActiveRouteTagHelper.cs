using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Diagnostics;

namespace WebStore_GB.TagHelpers
{
    [HtmlTargetElement(Attributes = ATTRIBUTE_NAME)]
    public class ActiveRouteTagHelper : TagHelper
    {
        private const string ATTRIBUTE_NAME = "is-active-route";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll(ATTRIBUTE_NAME);
        }
    }
}
