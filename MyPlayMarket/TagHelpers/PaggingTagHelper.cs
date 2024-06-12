using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using Azure;
using MyPlayMarket.Infrastructure.Entities.DTO;

namespace MyPlayMarket.Web.TagHelpers
{
    public class PaggingTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PaggingTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public IndexPaggingDTO indexPagging { get; set; }
        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");
            tag.AddCssClass("pagination-lg");
            tag.AddCssClass("justify-content-center");


            TagBuilder previous = CreateTag(indexPagging.pageViewDTO.CurrentPage - 1, urlHelper, ["Previous", "«"]);
            TagBuilder next = CreateTag(indexPagging.pageViewDTO.CurrentPage + 1, urlHelper, ["Next", "»"]);
            tag.InnerHtml.AppendHtml(previous);
            TagBuilder first = CreateTag(1, urlHelper, null);
            tag.InnerHtml.AppendHtml(first);
            TagBuilder last = CreateTag(indexPagging.pageViewDTO.TotalPages, urlHelper, null);



            for (int i = indexPagging.pageViewDTO.CurrentPage - 3; i <= indexPagging.pageViewDTO.CurrentPage + 3; i++)
            {
                if (i > 0 && i < indexPagging.pageViewDTO.TotalPages && i != 1 && i != indexPagging.pageViewDTO.TotalPages)
                {
                    TagBuilder temp = CreateTag(i, urlHelper, null);
                    tag.InnerHtml.AppendHtml(temp);
                }
            }
            if (indexPagging.pageViewDTO.PageItems < indexPagging.pageViewDTO.TotalItems)
            {
                tag.InnerHtml.AppendHtml(last);
            }
            tag.InnerHtml.AppendHtml(next);
            output.Content.AppendHtml(tag);
        }

        TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper, string[] paramButton)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");
            if (pageNumber == this.indexPagging.pageViewDTO.CurrentPage)
            {
                item.AddCssClass("active");
            }

            if (indexPagging.pageViewDTO.CurrentPage > 4 && indexPagging.pageViewDTO.CurrentPage - 3 == pageNumber || indexPagging.pageViewDTO.CurrentPage + 3 == pageNumber)
            {

                item.AddCssClass("page-item");
                link.AddCssClass("page-link");
                link.InnerHtml.Append("...");
                item.InnerHtml.AppendHtml(link);
            }
            else
            {
                item.AddCssClass("page-item");
                link.AddCssClass("page-link");
                var routeValues = new RouteValueDictionary {
                    { "pageViewDTO.CurrentPage" , pageNumber },                   
                    { "sortDTO.SortBy", indexPagging.sortDTO.SortBy },
                    { "filterDTO.Name", indexPagging.filterDTO.Name },
                    { "filterDTO.Company", indexPagging.filterDTO.Company },
                    { "filterDTO.Release", indexPagging.filterDTO.Release }
                };
                link.Attributes["href"] = urlHelper.Action(PageAction, routeValues);
                if (pageNumber <= 0 || pageNumber > indexPagging.pageViewDTO.TotalPages)
                {
                    item.AddCssClass("disabled");
                }
                if (paramButton != null)
                {
                    link.Attributes["aria-label"] = urlHelper.Action(PageAction, paramButton[0]);
                    item.InnerHtml.AppendHtml(link);
                    TagBuilder firstSpan = new TagBuilder("span");
                    firstSpan.Attributes["aria-hidden"] = "true";
                    firstSpan.InnerHtml.Append(paramButton[1]);
                    link.InnerHtml.AppendHtml(firstSpan);

                    TagBuilder secondSpan = new TagBuilder("span");
                    secondSpan.AddCssClass("sr-only");

                    secondSpan.InnerHtml.Append(paramButton[0]);

                    link.InnerHtml.AppendHtml(secondSpan);

                }
                else
                {
                    link.InnerHtml.Append(pageNumber.ToString());
                    item.InnerHtml.AppendHtml(link);
                }
            }

            return item;
        }
    }
}
