#pragma checksum "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\AnalyzeTrack.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "757827f9f7b42d7b9063e46ada31cfbaae9dee64"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_AnalyzeTrack), @"mvc.1.0.view", @"/Views/Home/AnalyzeTrack.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"757827f9f7b42d7b9063e46ada31cfbaae9dee64", @"/Views/Home/AnalyzeTrack.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7a6a66ef04fe5145422089ef0a3362b0210e5e12", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_AnalyzeTrack : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CodeLouSpotify.Models.AudioAnalysis>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\AnalyzeTrack.cshtml"
  
    ViewData["Title"] = "AnalyzeTrack";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>AnalyzeTrack</h1>\r\n\r\n<div>\r\n    <h4>AudioAnalysis</h4>\r\n    <h3>Duration of song</h3>\r\n    <p class=\"text-black-50\">");
#nullable restore
#line 12 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\AnalyzeTrack.cshtml"
                        Write(Model.Track.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    <hr />\r\n    <h1>Bar Lengths</h1>\r\n    <dl class=\"list-group\">\r\n");
#nullable restore
#line 16 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\AnalyzeTrack.cshtml"
          
            for (int i = 0; i < Model.bars.Length; i++)
            {
                //TODO: Improve UI
                var count = i +1;

#line default
#line hidden
#nullable disable
            WriteLiteral("                <dt class=\"list-group-item\">Bar ");
#nullable restore
#line 21 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\AnalyzeTrack.cshtml"
                                           Write(count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dt>\r\n                <dd class=\"list-group-item\">");
#nullable restore
#line 22 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\AnalyzeTrack.cshtml"
                                       Write(Model.bars[i].Duration);

#line default
#line hidden
#nullable disable
            WriteLiteral(" seconds</dd>\r\n");
#nullable restore
#line 23 "C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\Views\Home\AnalyzeTrack.cshtml"
            }
        

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dl>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CodeLouSpotify.Models.AudioAnalysis> Html { get; private set; }
    }
}
#pragma warning restore 1591
