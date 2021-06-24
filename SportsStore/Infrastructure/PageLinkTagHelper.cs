using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;


namespace SportsStore.Infrastructure
{
    [HtmlTargetElement("div", Attributes ="page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContex { get; set; }
        public PagingInfo PageModel { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
        public int PageScope { get; set; } = 3;
        public int FirstPageOnPagination { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (PageModel.CurrentPage + PageScope > PageModel.TotalPages) FirstPageOnPagination = PageModel.TotalPages - PageScope;
            else FirstPageOnPagination = PageModel.CurrentPage;
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContex);
            TagBuilder result = new TagBuilder("div");
            result.InnerHtml.AppendHtml(CreatePageTag(PageModel.CurrentPage - 1, urlHelper, "«"));
            for (int i = FirstPageOnPagination; i <= FirstPageOnPagination + PageScope; i++)
            {
                result.InnerHtml.AppendHtml(CreatePageTag(i, urlHelper));
            }
            result.InnerHtml.AppendHtml(CreatePageTag(PageModel.CurrentPage + 1, urlHelper, "»"));

            output.Content.AppendHtml(result.InnerHtml);
            
        }
        public TagBuilder CreatePageTag(int i, IUrlHelper urlHelper, string value = "")
        {
            TagBuilder tag = new TagBuilder("a");
            tag.Attributes["href"] = urlHelper.Action(PageAction, new { productPage = i });
            if (PageClassesEnabled )
            {
                if (i == 0 || i == PageModel.TotalPages + 1) tag.AddCssClass("disabled"); 
                tag.AddCssClass(PageClass);
                tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
            }
            if (string.IsNullOrEmpty(value)) tag.InnerHtml.AppendHtml(i.ToString());
            else tag.InnerHtml.AppendHtml(value);
            return tag;
        }

        
    }
}
