# FMArslan.Web
Simple Multi Language Web Framework on .Net Framework 4.7.1

You can easly make a website for you with razor. 

## Steps
  * You should declare your navigation in App_Data folder.
  * You should add your resource files(css, js, img, font etc) move to Content folder.
  * You can develop your pages in language folders where in Content
####  <span style="color:red">Important</span>
 * <span style="color:red">You must save solution file to parent folder</span>

<a href="https://raw.githubusercontent.com/fmarslan/FMArslan.Web/master/App_Data/navigation.xml" target="_blank">Navigation XML Sample</a>

### Example
  
  ``` RAZOR
@model FMArslan.Web.Model.PageConfig
<div id="header">
    <div>
        <a href="index.html" class="logo"><img src="/Content/images/logo.png" alt=""></a>
        <ul id="navigation">
            @foreach (var page in Model.Navigation.Pages.Where(x=>x.Key.Any(y=>y.Language.Equals(Model.Language))))
                {
                    <li class='menu @(page.getKey(Model.Language) == Model.Page.getKey(Model.Language) ? "selected":"" )'>
                        <a href="/en/@page.getKey(Model.Language)">@(String.IsNullOrEmpty(page["enTitle"])==false? page["enTitle"] : (String.IsNullOrEmpty(page.Title) == false ? page.Title : page.getKey(Model.Language)))</a>
                    </li>
            }
        </ul>
    </div>
</div>
  ```
### Config

``` XML
  <appSettings>
    <add key="webpages:Version" value="3.0.0.0" />
    <add key="defaultLanguage" value="en" />
    <add key="languages" value="en,tr" />
    <add key="errorPage" value="/shared/error.cshtml" /> <!-- in language folder -->
    <add key="webpages:Enabled" value="false" />
    <add key="ClientValidationEnabled" value="true" />
    <add key="UnobtrusiveJavaScriptEnabled" value="true" />
    <add key="contentFolder" value="~/Content" />
  </appSettings>
```

### Path
``` PATH
 Content
 |__Pages
    |__en
    |  |__ ... {your pages} ...
    |__tr
       |__ ... {your pages} ...
 ```

### References

* <a href="https://freewebsitetemplates.com/preview/frozenyogurtshop/index.html" target="_blank">Template 1(freewebsitetemplates/frozenyogurtshop)</a>
* <a href="https://startbootstrap.com/themes/creative/" target="_blank">Template 2(startbootstrap)</a>
