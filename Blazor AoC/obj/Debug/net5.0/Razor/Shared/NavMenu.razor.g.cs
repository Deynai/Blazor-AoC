#pragma checksum "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\Shared\NavMenu.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2d76fb1a75b0786d6040405146671ba85b5ddbb8"
// <auto-generated/>
#pragma warning disable 1591
namespace Blazor_AoC.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\_Imports.razor"
using Blazor_AoC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\_Imports.razor"
using Blazor_AoC.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\_Imports.razor"
using Blazor_AoC.Controls;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\_Imports.razor"
using Blazor_AoC.Pages;

#line default
#line hidden
#nullable disable
    public partial class NavMenu : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "top-row pl-4 navbar navbar-dark");
            __builder.AddAttribute(2, "b-icx9apfy2w");
            __builder.AddMarkupContent(3, "<a class=\"navbar-brand\" href b-icx9apfy2w>Blazor AoC</a>\r\n    ");
            __builder.OpenElement(4, "button");
            __builder.AddAttribute(5, "class", "navbar-toggler");
            __builder.AddAttribute(6, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 3 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\Shared\NavMenu.razor"
                                             ToggleNavMenu

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(7, "b-icx9apfy2w");
            __builder.AddMarkupContent(8, "<span class=\"navbar-toggler-icon\" b-icx9apfy2w></span>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(9, "\r\n\r\n");
            __builder.OpenElement(10, "div");
            __builder.AddAttribute(11, "class", 
#nullable restore
#line 8 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\Shared\NavMenu.razor"
             NavMenuCssClass

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(12, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 8 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\Shared\NavMenu.razor"
                                        ToggleNavMenu

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(13, "b-icx9apfy2w");
            __builder.OpenElement(14, "ul");
            __builder.AddAttribute(15, "class", "nav flex-column");
            __builder.AddAttribute(16, "b-icx9apfy2w");
            __builder.OpenElement(17, "li");
            __builder.AddAttribute(18, "class", "nav-item px-3");
            __builder.AddAttribute(19, "b-icx9apfy2w");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Routing.NavLink>(20);
            __builder.AddAttribute(21, "class", "nav-link");
            __builder.AddAttribute(22, "href", "");
            __builder.AddAttribute(23, "Match", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.Routing.NavLinkMatch>(
#nullable restore
#line 11 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\Shared\NavMenu.razor"
                                                     NavLinkMatch.All

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(24, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddMarkupContent(25, "<span class=\"oi oi-home\" aria-hidden=\"true\" b-icx9apfy2w></span> Home\r\n            ");
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.AddMarkupContent(26, "\r\n        ");
            __builder.OpenElement(27, "li");
            __builder.AddAttribute(28, "class", "nav-item px-3");
            __builder.AddAttribute(29, "b-icx9apfy2w");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Routing.NavLink>(30);
            __builder.AddAttribute(31, "class", "nav-link");
            __builder.AddAttribute(32, "href", "2020");
            __builder.AddAttribute(33, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.AddMarkupContent(34, "<span class=\"oi oi-list\" aria-hidden=\"true\" b-icx9apfy2w></span> 2020\r\n            ");
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 44 "C:\Users\rowan\Source\Repos\Blazor AoC\Blazor-AoC\Blazor AoC\Shared\NavMenu.razor"
       
    private bool collapseNavMenu = true;

    private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
