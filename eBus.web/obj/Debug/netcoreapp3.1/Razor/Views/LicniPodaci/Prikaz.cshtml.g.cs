#pragma checksum "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\LicniPodaci\Prikaz.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ba6791629a031620734d9258feb5ea75b4eb0c08"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_LicniPodaci_Prikaz), @"mvc.1.0.view", @"/Views/LicniPodaci/Prikaz.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ba6791629a031620734d9258feb5ea75b4eb0c08", @"/Views/LicniPodaci/Prikaz.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8a1364278d3fe4350f2f572d2cd60ec483415a25", @"/Views/_ViewImports.cshtml")]
    public class Views_LicniPodaci_Prikaz : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<eBus.Model.Putnik>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\LicniPodaci\Prikaz.cshtml"
  
    Layout = "_PutnikLayout";
    ViewData["Title"] = "Prikaz";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""suscribe-area kreirajRazmak"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-lg-12 col-md-12 col-sm-12 col-xs=12"">
                <div class=""suscribe-text text-center"">
                    <h3>Lični podaci</h3>

                </div>
            </div>
        </div>
    </div>
</div><!-- End Suscribe Section -->

<section class=""services"" style=""padding-top: 20px;"">
    <div class=""container"">

        <div class=""row"">

            <div class=""col-md-12"">
                <div class=""col-md-12"">
                    <div class=""icon-box icon-box-pink"" style=""padding:10px; margin: 0 0 20px 0;"">
                        <img");
            BeginWriteAttribute("src", " src=", 801, "", 888, 1);
#nullable restore
#line 28 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\LicniPodaci\Prikaz.cshtml"
WriteAttributeValue("", 806, string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(Model.Slika)), 806, 82, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""img-responsive mx-auto d-block"" style=""width:150px"" alt=""..."">
                    </div>
                </div>
            </div>



            <div class=""col-md-12"" data-aos=""fade-up"">
                <div class=""col-md-12"">
                    <div class=""icon-box icon-box-pink"" style=""padding:10px; margin: 0 0 20px 0; text-align:center; background-color:lightblue"">
                        <p class=""description"" style=""margin:5px;"">Ime i prezime:</p>
                        <h4 class=""title"" style=""margin:5px; color:darkblue;"">");
#nullable restore
#line 39 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\LicniPodaci\Prikaz.cshtml"
                                                                         Write(Model.Ime);

#line default
#line hidden
#nullable disable
            WriteLiteral("   ");
#nullable restore
#line 39 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\LicniPodaci\Prikaz.cshtml"
                                                                                      Write(Model.Prezime);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h4>
                    </div>
                </div>
            </div>


            <div class=""col-md-6"" data-aos=""fade-up"">
                <div class=""col-md-12"">
                    <div class=""icon-box icon-box-pink"" style=""padding:10px; margin: 0 0 20px 0;text-align:center;background-color:lightblue"">
                        <p class=""description"" style=""margin:5px;"">Email:</p>
                        <h4 class=""title"" style=""margin:5px;color:darkblue;"">");
#nullable restore
#line 49 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\LicniPodaci\Prikaz.cshtml"
                                                                        Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h4>
                    </div>
                </div>
            </div>
            <div class=""col-md-6"" data-aos=""fade-up"">
                <div class=""col-md-12"">
                    <div class=""icon-box icon-box-pink"" style=""padding:10px; margin: 0 0 20px 0;text-align:center;background-color:lightblue"">
                        <p class=""description"" style=""margin:5px;"">Datum rođenja:</p>
                        <h4 class=""title"" style=""margin:5px;color:darkblue;"">");
#nullable restore
#line 57 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\LicniPodaci\Prikaz.cshtml"
                                                                        Write(Model.DatumRodjenja.Value.ToString("dd-MM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h4>
                    </div>
                </div>
            </div>

            <div class=""col-md-6"">
                <div class=""col-md-12"">
                    <div class=""icon-box icon-box-pink"" style=""padding:10px; margin: 0 0 20px 0;text-align:center;background-color:lightblue"">
                        <p class=""description"" style=""margin:5px;"">Korisničko ime:</p>
                        <h4 class=""title"" style=""margin:5px;color:darkblue;"">");
#nullable restore
#line 66 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\LicniPodaci\Prikaz.cshtml"
                                                                        Write(Model.KorisnickoIme);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h4>
                    </div>
                </div>
            </div>

            <div class=""col-md-6"">
                <div class=""col-md-12"">
                    <div class=""icon-box icon-box-pink"" style=""padding:10px; margin: 0 0 20px 0;text-align:center;background-color:lightblue"">
                        <p class=""description"" style=""margin:5px;"">Datum registracije:</p>
                        <h4 class=""title"" style=""margin:5px;color:darkblue;"">");
#nullable restore
#line 75 "C:\Users\Tarik\Desktop\Zavrsni rad\eBus\eBus.web\Views\LicniPodaci\Prikaz.cshtml"
                                                                        Write(Model.DatumRegistracije);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h4>
                    </div>
                </div>
            </div>

            <div class=""col-md-12"" style=""text-align:center; padding:15px;"">
                <a href=""/LicniPodaci/Uredi"" class=""btn btn-light btn-lg active col-md-5"" style=""padding:10px;"" role=""button"" aria-pressed=""true"">Uredi</a>
            </div>
        </div>
    </div>
</section><!-- End Services Section -->
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<eBus.Model.Putnik> Html { get; private set; }
    }
}
#pragma warning restore 1591
