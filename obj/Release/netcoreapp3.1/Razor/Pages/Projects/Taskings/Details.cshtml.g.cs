#pragma checksum "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "43cfa9a891e3a9b7b4b11cff01191292b429fd39"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(TaskManager.Pages.Projects.Taskings.Pages_Projects_Taskings_Details), @"mvc.1.0.razor-page", @"/Pages/Projects/Taskings/Details.cshtml")]
namespace TaskManager.Pages.Projects.Taskings
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
#line 1 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\_ViewImports.cshtml"
using TaskManager;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\_ViewImports.cshtml"
using TaskManager.Data;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"43cfa9a891e3a9b7b4b11cff01191292b429fd39", @"/Pages/Projects/Taskings/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"06a6f9b14f31b2174dff73f44d4c6e1f1bc915af", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Projects_Taskings_Details : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "./Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("my-btn3-dlt sizing "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "./Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("my-btn3-back"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/Projects/TaskManagement", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2 class=\"center\">Task Information</h2>\r\n<div>\r\n    <br /><br />\r\n    <div class=\"subcol narrow\">\r\n        <div class=\"align-right\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43cfa9a891e3a9b7b4b11cff01191292b429fd395770", async() => {
                WriteLiteral("<i class=\"fas fa-edit my-btn4 sizing widen-mobile\"></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 13 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
                                           WriteLiteral(Model.Tasking.TaskingID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n        <div class=\"subform-ylw widen-mobile\">\r\n\r\n            <h4 class=\"center4\">");
#nullable restore
#line 17 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
                           Write(Html.DisplayFor(model => model.Tasking.TaskTitle));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n            <p class=\"center4 grey small\">Created By ");
#nullable restore
#line 18 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
                                                Write(Html.DisplayFor(modelitem => Model.Tasking.TaskOwnerName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            <hr />\r\n            <div class=\"center3 large\">\r\n");
#nullable restore
#line 21 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
                 if (Model.Tasking.Description != null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <span>\"</span>");
#nullable restore
#line 23 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
                             Write(Html.Raw(Model.Tasking.Description.Replace("\r\n", " < br /> ")));

#line default
#line hidden
#nullable disable
            WriteLiteral("<span>\"</span>\r\n");
#nullable restore
#line 24 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
                } else  {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <span>\"\"</span>\r\n");
#nullable restore
#line 26 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n            <br />\r\n");
#nullable restore
#line 29 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
             if (Model.Tasking.Severity == 0)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div><strong>Severity:</strong> Low <i class=\"fas fa-battery-empty grey-icon\"></i></div>\r\n");
#nullable restore
#line 32 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 33 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
             if (Model.Tasking.Severity == 1)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div><strong>Severity:</strong> Medium <i class=\"fas fa-battery-half grey-icon\"></i></div>\r\n");
#nullable restore
#line 36 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 37 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
             if (Model.Tasking.Severity == 2)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div><strong>Severity:</strong> High <i class=\"fas fa-battery-full grey-icon\"></i></div>\r\n");
#nullable restore
#line 40 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 42 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
             if (Model.Tasking.Progression == 0)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div><strong>Progression:</strong> Created</div>\r\n");
#nullable restore
#line 45 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 46 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
             if (Model.Tasking.Progression == 1)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div><strong>Progression:</strong> In Progress</div>\r\n");
#nullable restore
#line 49 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 50 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
             if (Model.Tasking.Progression == 2)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div><strong>Progression:</strong> Completed</div>\r\n");
#nullable restore
#line 53 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <br />\r\n            <div class=\"grey center4 small\">Assigned To ");
#nullable restore
#line 55 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
                                                   Write(Html.DisplayFor(model => model.Tasking.Assignment));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        </div>\r\n        <div class=\"sub-mit widen-mobile\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43cfa9a891e3a9b7b4b11cff01191292b429fd3914003", async() => {
                WriteLiteral("Delete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 58 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
                                                                WriteLiteral(Model.Tasking.TaskingID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n        <div class=\"sizing sub-back widen-mobile\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43cfa9a891e3a9b7b4b11cff01191292b429fd3916376", async() => {
                WriteLiteral("Back to Project Page");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 61 "C:\Users\Liam York\source\repos\TaskManager\TaskManager\Pages\Projects\Taskings\Details.cshtml"
                                                                         WriteLiteral(Model.Tasking.ProjectID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <br /> <br /> <br />\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TaskManager.Pages.Projects.Taskings.DetailsModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<TaskManager.Pages.Projects.Taskings.DetailsModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<TaskManager.Pages.Projects.Taskings.DetailsModel>)PageContext?.ViewData;
        public TaskManager.Pages.Projects.Taskings.DetailsModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
