using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MyPlayMarket.Infrastructure.Entities;
using System;
using Azure;

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
        public PageViewModel PageModel { get; set; }
        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");
            tag.AddCssClass("pagination-lg");
            tag.AddCssClass("justify-content-center");


            TagBuilder previous = CreateTag(PageModel.CurrentPage - 1, urlHelper, ["Previous", "«"]);
            TagBuilder next = CreateTag(PageModel.CurrentPage + 1, urlHelper, ["Next", "»"]);
            tag.InnerHtml.AppendHtml(previous);
            TagBuilder first = CreateTag(1, urlHelper, null);
            tag.InnerHtml.AppendHtml(first);
            TagBuilder last = CreateTag(PageModel.TotalPages, urlHelper, null);



            for (int i = PageModel.CurrentPage - 3; i <= PageModel.CurrentPage + 3; i++)
            {
                if (i > 0 && i < PageModel.TotalPages && i != 1 && i != PageModel.TotalPages)
                {
                    TagBuilder temp = CreateTag(i, urlHelper, null);
                    tag.InnerHtml.AppendHtml(temp);
                }
            }

            tag.InnerHtml.AppendHtml(last);
            tag.InnerHtml.AppendHtml(next);
            output.Content.AppendHtml(tag);
        }

        TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper, string[] paramButton)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");
            if (pageNumber == this.PageModel.CurrentPage)
            {
                item.AddCssClass("active");
            }

            if (PageModel.CurrentPage > 4 && PageModel.CurrentPage - 3 == pageNumber || PageModel.CurrentPage + 3 == pageNumber)
            {

                item.AddCssClass("page-item");
                link.AddCssClass("page-link");
                link.InnerHtml.Append("...");
                item.InnerHtml.AppendHtml(link);
            }
            else
            {

                link.Attributes["href"] = urlHelper.Action(PageAction, new { currentpage = pageNumber });
                item.AddCssClass("page-item");
                link.AddCssClass("page-link");
                if (pageNumber <= 0 || pageNumber > PageModel.TotalPages)
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
