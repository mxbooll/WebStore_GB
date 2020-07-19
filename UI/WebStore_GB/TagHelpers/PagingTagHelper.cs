﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore_GB.Domain.ViewModels;

namespace WebStore_GB.TagHelpers
{
    public class PagingTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _UrlHelperFactory;

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PageViewModel PageModel { get; set; }

        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } =
            new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        public PagingTagHelper(IUrlHelperFactory UrlHelperFactory) => _UrlHelperFactory = UrlHelperFactory;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");

            var urlHelper = _UrlHelperFactory.GetUrlHelper(ViewContext);
            for (int i = 1, total_pages = PageModel.TotalPages; i < total_pages; i++)
                ul.InnerHtml.AppendHtml(CreateItem(i, urlHelper));

            output.Content.AppendHtml(ul);
        }

        private TagBuilder CreateItem(int PageNumber, IUrlHelper Url)
        {
            var li = new TagBuilder("li");
            var a = new TagBuilder("a");

            if (PageNumber == PageModel.PageNumber)
            {
                li.AddCssClass("active");
            }
            else
            {
                PageUrlValues["page"] = PageNumber;
                a.Attributes["href"] = Url.Action(PageAction, PageUrlValues);                
            }

            a.InnerHtml.AppendHtml(PageNumber.ToString());
            li.InnerHtml.AppendHtml(a);
            return li;
        }
    }
}
