#pragma checksum "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\SearchResult.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "901d7b32e56c4326a715cac8c4c10c7e2a026097"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_SearchResult), @"mvc.1.0.view", @"/Views/Home/SearchResult.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\_ViewImports.cshtml"
using CodeLouSpotify;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\_ViewImports.cshtml"
using CodeLouSpotify.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"901d7b32e56c4326a715cac8c4c10c7e2a026097", @"/Views/Home/SearchResult.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7a6a66ef04fe5145422089ef0a3362b0210e5e12", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_SearchResult : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CodeLouSpotify.Models.Tracks>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\SearchResult.cshtml"
  
    ViewData["Title"] = "SearchResult";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>SearchResult</h1>\r\n<p>Total found: ");
#nullable restore
#line 7 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\SearchResult.cshtml"
           Write(Model.total);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n<div>\r\n    <dl>\r\n");
#nullable restore
#line 10 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\SearchResult.cshtml"
          
            for (var i = 0; i < Model.items.Length; i++)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <dt> ");
#nullable restore
#line 13 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\SearchResult.cshtml"
                Write(Model.items[i].name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dt>\r\n");
#nullable restore
#line 14 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\SearchResult.cshtml"

                for (var j = 0; j < Model.items[i].album.images.Length; j++)
                {


#line default
#line hidden
#nullable disable
            WriteLiteral("                    <dd>\r\n                        <img");
            BeginWriteAttribute("src", " src=\"", 449, "\"", 490, 1);
#nullable restore
#line 19 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\SearchResult.cshtml"
WriteAttributeValue("", 455, Model.items[i].album.images[j].url, 455, 35, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("height", " height=\"", 491, "\"", 538, 1);
#nullable restore
#line 19 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\SearchResult.cshtml"
WriteAttributeValue("", 500, Model.items[i].album.images[j].height, 500, 38, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("width", " width=\"", 539, "\"", 584, 1);
#nullable restore
#line 19 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\SearchResult.cshtml"
WriteAttributeValue("", 547, Model.items[i].album.images[j].width, 547, 37, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                        <a");
            BeginWriteAttribute("href", " href=\"", 616, "\"", 650, 1);
#nullable restore
#line 20 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\SearchResult.cshtml"
WriteAttributeValue("", 623, Model.items[i].preview_url, 623, 27, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Preview</a>\r\n                        ");
#nullable restore
#line 21 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\SearchResult.cshtml"
                   Write(Html.ActionLink("Analyize", "AnalyzeTrack",new Item2 { id = Model.items[i].id  }));

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n\r\n\r\n                    </dd>\r\n");
#nullable restore
#line 25 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\SearchResult.cshtml"


                }




            }
        

#line default
#line hidden
#nullable disable
            WriteLiteral("    </dl>\r\n\r\n</div>\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CodeLouSpotify.Models.Tracks> Html { get; private set; }
    }
}
#pragma warning restore 1591
