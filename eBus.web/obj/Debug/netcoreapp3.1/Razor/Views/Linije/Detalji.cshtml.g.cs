#pragma checksum "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\Linije\Detalji.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "35387c36db6482451d07fdf638f534873617eccf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Linije_Detalji), @"mvc.1.0.view", @"/Views/Linije/Detalji.cshtml")]
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
#line 1 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\_ViewImports.cshtml"
using eBus.web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\_ViewImports.cshtml"
using eBus.web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"35387c36db6482451d07fdf638f534873617eccf", @"/Views/Linije/Detalji.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8a1364278d3fe4350f2f572d2cd60ec483415a25", @"/Views/_ViewImports.cshtml")]
    public class Views_Linije_Detalji : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<eBus.web.ViewModels.LinijeDetaljiVM>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\Linije\Detalji.cshtml"
  
   
    Layout = "_PutnikLayout";
    ViewData["Title"] = "Detalji";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<!-- ======= Services Section ======= -->

<div id=""services"" class=""services-area area-padding"">
    <div class=""container"" style=""margin-top:10px;"">
        <div class=""row"">
            <div class=""col-md-12 col-sm-12 col-xs-12"">
                <div class=""section-headline services-head text-center"">
                    <h2>Detalji</h2>
                </div>
            </div>
        </div>
        <div class=""row text-center"">
        

      
            <div class=""col-md-12 razmakRedova"">
                <div class=""card bg-secondary text-white"">
                    <div class=""card-body"">
                        <div class=""float-left"" style=""padding-left:150px;"">
                            <a class=""services-icon"" data-toggle=""tooltip"" title=""Polazište!"" href=""#"">
                                <i class=""fa fa-check""></i>
                            </a>
                            <h3 style=""color:white"">");
#nullable restore
#line 30 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\Linije\Detalji.cshtml"
                                               Write(Model.PolazisteNaziv);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>
                        </div>
                        <div class=""float-right"" style=""padding-right:150px;"">
                            <a class=""services-icon"" data-toggle=""tooltip"" title=""Odredište!"" href=""#"">
                                <i class=""fa fa-bullseye""></i>
                            </a>
                            <h3 style=""color:white"">");
#nullable restore
#line 36 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\Linije\Detalji.cshtml"
                                               Write(Model.OdredisteNaziv);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>

                        </div>
                    </div>
                </div>

            </div>

            <div class=""col-md-12 razmakRedova"">
                <div class=""card bg-secondary text-white"">
                    <div class=""card-body"">
                        <div class=""float-left"" style=""padding-left:150px; "">
                            <a class=""services-icon"" data-toggle=""tooltip"" title=""Vrijeme polaska!"" href=""#"">
                                <i class=""fa fa-clock-o""></i>
                            </a>
                            <h3 style=""color:white"">");
#nullable restore
#line 51 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\Linije\Detalji.cshtml"
                                               Write(Model.VrijemePolaska);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>
                        </div>
                        <div class=""float-right"" style=""padding-right:150px;"">
                            <a class=""services-icon"" data-toggle=""tooltip"" title=""Vrijeme dolaska!"" href=""#"">
                                <i class=""fa fa-clock-o""></i>
                            </a>
                            <h3 style=""color:white"">");
#nullable restore
#line 57 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\Linije\Detalji.cshtml"
                                               Write(Model.VrijemeDolaska);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>

                        </div>
                    </div>
                </div>

            </div>

            <div class=""col-md-12 razmakRedova"">
                <div class=""card bg-secondary text-white"">
                    <div class=""card-body"">
                        <div class=""float-left"" style=""padding-left:150px;"">
                            <a class=""services-icon"" data-toggle=""tooltip"" title=""Datum!"" href=""#"">
                                <i class=""fa fa-calendar""></i>
                            </a>
                            <h3 style=""color:white"">");
#nullable restore
#line 72 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\Linije\Detalji.cshtml"
                                               Write(Model.DatumPretrage.ToString("dd-MM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>
                        </div>
                        <div class=""float-right"" style=""padding-right:150px;"">
                            <a class=""services-icon"" data-toggle=""tooltip"" title=""Cijena karte!"" href=""#"">
                                <i class=""fa fa-money""></i>
                            </a>
                            <h3 style=""color:white"">");
#nullable restore
#line 78 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\Linije\Detalji.cshtml"
                                               Write(Model.Cijena);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" KM</h3>

                        </div>
                    </div>
                </div>

            </div>

            <div class=""col-md-12 razmakRedova"">
                <div class=""card bg-secondary text-white"">
                    <div class=""card-body"">
                        <div class=""float-left"" style=""padding-left:150px;"">
                            <a class=""services-icon"" data-toggle=""tooltip"" title=""Kompanija!"" href=""#"">
                                <i class=""fa fa-building-o""></i>
                            </a>
                            <div>
                                <h3 style=""color:white"">");
#nullable restore
#line 94 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\Linije\Detalji.cshtml"
                                                   Write(Model.Kompanija);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>
                            </div>
                        </div>
                        <div class=""float-right"" style=""padding-right:150px;"">
                            <a class=""services-icon"" data-toggle=""tooltip"" title=""Vozilo kompanije!"" href=""#"">
                                <i class=""fa fa-bus""></i>
                            </a>
                            <h3 style=""color:white"">");
#nullable restore
#line 101 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\Linije\Detalji.cshtml"
                                               Write(Model.Vozilo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n\r\n            </div>\r\n            <div class=\"col-md-12 razmakRedova\">\r\n                ");
#nullable restore
#line 109 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\Linije\Detalji.cshtml"
           Write(Html.ActionLink("Sjedišta", "PrikaziSjedista", new { id = Model.voziloId, datum = Model.DatumPretrage, vrijeme = Model.VrijemePolaska },  new { @class = "btn btn-success izmD" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

            </div>
        </div>
    </div>
</div><!-- End Services Section -->

<style>
    .izmD{
        width:100%;
    }

    .razmakRedova{
        margin-top:10px;
        margin-bottom:10px;
    
    }

    .col-md-12{
      height:100px;
    }
   
</style>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<eBus.web.ViewModels.LinijeDetaljiVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
